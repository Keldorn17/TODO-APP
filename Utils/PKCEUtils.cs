using System.Security.Cryptography;
using System.Text;
using TODO.Domain;

namespace TODO.Utils;

public static class PKCEUtils
{

    private static readonly RandomNumberGenerator SecureRandom = RandomNumberGenerator.Create();
    private static readonly SHA256 Sha256 = SHA256.Create();

    public static PKCEPair GeneratePKCEPair()
    {
        string codeVerifier = GenerateCodeVerifier();
        return new PKCEPair(codeVerifier, GenerateCodeChallenge(codeVerifier));
    }

    private static string GenerateCodeVerifier()
    {
        byte[] codeVerifier = new byte[64];
        SecureRandom.GetBytes(codeVerifier);
        return Base64UrlEncode(codeVerifier);
    }

    private static string GenerateCodeChallenge(string codeVerifier)
    {
        byte[] codeChallenge = Sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
        return Base64UrlEncode(codeChallenge);
    }

    private static string Base64UrlEncode(byte[] bytes)
    {
        return Convert.ToBase64String(bytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

}