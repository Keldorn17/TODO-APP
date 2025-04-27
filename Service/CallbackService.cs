using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace TODO.Service;

public class CallbackService : ICallbackService
{
    public Task<string> ListenForCallback(string callbackUrl)
    {
        var tcs = new TaskCompletionSource<string>();
        var listener = new HttpListener();
        string suffixedCallbackUrl = callbackUrl.EndsWith('/') ? callbackUrl : callbackUrl + '/';
        try
        {
            listener.Prefixes.Add(suffixedCallbackUrl);
            listener.Start();

            // Asynchronously wait for one incoming request without blocking the caller
            // Run the handling logic in a separate thread pool thread
            Task.Run(async () =>
            {
                try
                {
                    // Wait for a request
                    HttpListenerContext context = await listener.GetContextAsync();
                    // Process the request
                    await HandleCallback(context, tcs);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
                finally
                {
                    // Stop the listener after handling the single request or encountering an error
                    if (listener.IsListening)
                    {
                        listener.Stop();
                    }

                    listener.Close();
                }
            });
        }
        catch (Exception ex)
        {
            // If startup fails, complete the task exceptionally
            tcs.TrySetException(new InvalidOperationException($"Failed to start listener on {suffixedCallbackUrl}", ex));
            listener.Close();
        }

        // Return the Task that will be completed by HandleCallback or the catch blocks
        return tcs.Task;
    }

    private async Task HandleCallback(HttpListenerContext context, TaskCompletionSource<string> tcs)
    {
        string? authCode;
        try
        {
            var request = context.Request;
            Uri? requestUri = request.Url;
            string query = requestUri?.Query ?? string.Empty;

            if (!string.IsNullOrEmpty(query))
            {
                // Parse the query string parameters
                var queryParams = HttpUtility.ParseQueryString(query);
                authCode = queryParams["code"];

                if (!string.IsNullOrEmpty(authCode))
                {
                    // Complete the task successfully
                    tcs.TrySetResult(authCode);
                }
                else
                {
                    tcs.TrySetException(
                        new ArgumentException("Callback received but 'code' parameter was missing or empty."));
                }
            }
            else
            {
                tcs.TrySetException(new ArgumentException("Callback received without query parameters."));
            }

            // Send a response back to the browser regardless of success/failure
            var page = await File.ReadAllTextAsync("redirect.html");
            byte[] buffer = Encoding.UTF8.GetBytes(page);
            context.Response.ContentLength64 = buffer.Length;
            Stream st = context.Response.OutputStream;
            await st.WriteAsync(buffer);

            context.Response.Close();
        }
        catch (Exception ex)
        {
            tcs.TrySetException(ex);
        }
    }
}