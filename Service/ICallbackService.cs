namespace TODO.Service;

public interface ICallbackService
{

    Task<string> ListenForCallback(string callbackUrl);

}