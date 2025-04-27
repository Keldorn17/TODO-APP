using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using TODO.Domain;

namespace TODO.Service;

public class ProfileService : IProfileService
{
    private const string FileName = "profile.dat";
    
    private readonly ProfileConfig _config;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly ILogger<ProfileService> _log;

    private Profile? _profile;
    
    public ProfileService(ProfileConfig config, JsonSerializerOptions serializerOptions, IMessenger messenger, ILogger<ProfileService> log)
    {
        _config = config;
        _serializerOptions = serializerOptions;
        _log = log;
        messenger.Register<LogoutMessage>(this, HandleLogoutMessage);
    }
    
    public void OpenProfilePage()
    {
        _log.LogDebug("Opening profile page: {url}", _config.ProfileEndpoint);
        Process.Start(new ProcessStartInfo(_config.ProfileEndpoint) { UseShellExecute = true });
    }

    public void SaveProfile(string idToken)
    {
        var parts = idToken.Split('.');
        if (parts.Length != 3)
        {
            _log.LogError("Invalid ID token: {idToken}", idToken);
            throw new Exception("Invalid id token format");
        }
        _log.LogDebug("Saving profile");
        var jsonBytes = ParseBase64WithoutPadding(parts[1]);
        var json = Encoding.UTF8.GetString(jsonBytes);
        var bytes = Encoding.UTF8.GetBytes(json);
        var encryptedData = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
        File.WriteAllBytes(FileName, encryptedData);
        _log.LogDebug("Profile saved");
    }

    public Profile GetProfile()
    {
        if (_profile != null)
        {
            _log.LogDebug("Returning cached profile");
            return _profile;
        }
        _log.LogDebug("Profile is not cached, reading profile");
        var encryptedData = File.ReadAllBytes(FileName);
        var decryptedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
        var profileJson = Encoding.UTF8.GetString(decryptedData);
        _profile = JsonSerializer.Deserialize<Profile>(profileJson, _serializerOptions)!;
        _log.LogDebug("Profile read successfully");
        return _profile;
    }

    private void HandleLogoutMessage(object sender, LogoutMessage logoutMessage)
    {
        _log.LogDebug("Deleting profile");
        _profile = null;
        File.Delete(FileName);
    }
    
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }

        base64 = base64.Replace('-', '+').Replace('_', '/');
        return Convert.FromBase64String(base64);
    }
}