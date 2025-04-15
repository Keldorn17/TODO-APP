using System.IO;
using System.Security.Cryptography;
using System.Text;
using TODO.DTO;
using TODO.Exceptions;

namespace TODO.Provider;

public class RefreshTokenProvider
{
    private const string FileName = "refreshToken.dat";

    private string _refreshToken = string.Empty;

    public bool IsRefreshTokenAvailable()
    {
        return File.Exists(FileName);
    }

    public string GetRefreshToken()
    {
        CheckRefreshTokenFile();
        LoadRefreshToken();
        return _refreshToken;
    }

    public void SaveRefreshToken(string refreshToken)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(refreshToken);
        byte[] encryptedData = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
        File.WriteAllBytes(FileName, encryptedData);
        _refreshToken = refreshToken;
    }

    public void ClearRefreshToken()
    {
        _refreshToken = string.Empty;
        if (File.Exists(FileName))
        {
            File.Delete(FileName);
        }
    }

    private void CheckRefreshTokenFile()
    {
        if (!File.Exists(FileName))
        {
            throw new InvalidRefreshTokenException(new ErrorResponse
            {
                Type = "Client Error",
                Title = "Refresh token does not exist",
                Details = "Refresh token file does not exists"
            });
        }
    }

    private void LoadRefreshToken()
    {
        if (!string.IsNullOrEmpty(_refreshToken)) return;
        byte[] encryptedData = File.ReadAllBytes(FileName);
        byte[] decryptedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
        _refreshToken = Encoding.UTF8.GetString(decryptedData);
    }
}