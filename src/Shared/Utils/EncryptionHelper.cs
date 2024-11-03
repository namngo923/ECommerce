using System.Security.Cryptography;
using System.Text;

namespace SPSVN.Shared.Utils;

public static class EncryptionHelper
{
    public const string LicenseSP365 = "SpSRc365b#p@$cVietNam";

    private const string EncryptionKey = "SpSRcb#p@$cVietNam";
    [Obsolete("Obsolete")]
    public static string Encrypt(string clearText)
    {
        var clearBytes = Encoding.Unicode.GetBytes(clearText);
        using var encryptor = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(EncryptionKey, [0x86, 0x68, 0x89, 0x6e, 0x98, 0x4d, 0x86, 0x68, 0x96, 0x89, 0x86, 0x68, 0x86]);
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        encryptor.Mode = CipherMode.CBC;

        encryptor.Padding = PaddingMode.PKCS7;

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cs.Write(clearBytes, 0, clearBytes.Length);
            cs.Close();
        }
        clearText = Convert.ToBase64String(ms.ToArray());

        return clearText;
    }

    [Obsolete("Obsolete")]
    public static string Decrypt(string cipherText)
    {
        cipherText = cipherText.Replace(" ", "+");
        var cipherBytes = Convert.FromBase64String(cipherText);
        using var encryptor = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(EncryptionKey, [0x86, 0x68, 0x89, 0x6e, 0x98, 0x4d, 0x86, 0x68, 0x96, 0x89, 0x86, 0x68, 0x86]);
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cs.Write(cipherBytes, 0, cipherBytes.Length);
            cs.Close();
        }
        cipherText = Encoding.Unicode.GetString(ms.ToArray());

        return cipherText;
    }

    [Obsolete("Obsolete")]
    public static string Decrypt(string cipherText, string key)
    {

        cipherText = cipherText.Replace(" ", "+");
        var cipherBytes = Convert.FromBase64String(cipherText);
        using var encryptor = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(key, [0x86, 0x68, 0x89, 0x6e, 0x98, 0x4d, 0x86, 0x68, 0x96, 0x89, 0x86, 0x68, 0x86]);
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cs.Write(cipherBytes, 0, cipherBytes.Length);
            cs.Close();
        }
        cipherText = Encoding.Unicode.GetString(ms.ToArray());

        return cipherText;
    }

    [Obsolete("Obsolete")]
    public static string Encrypt(string clearText, string key)
    {
        var clearBytes = Encoding.Unicode.GetBytes(clearText);
        using var encryptor = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(key, [0x86, 0x68, 0x89, 0x6e, 0x98, 0x4d, 0x86, 0x68, 0x96, 0x89, 0x86, 0x68, 0x86]);
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        encryptor.Mode = CipherMode.CBC;
        encryptor.Padding = PaddingMode.PKCS7;

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cs.Write(clearBytes, 0, clearBytes.Length);
            cs.Close();
        }
        clearText = Convert.ToBase64String(ms.ToArray());

        return clearText;
    }
}