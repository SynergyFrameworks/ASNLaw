using System;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Parse
{
  public class Cryptographer
  {
    private static string _key;

    public static string Key
    {
      set => Cryptographer._key = value;
    }

    /// <summary>Encrypt the given string using the default key.</summary>
    /// <param name="strToEncrypt">The string to be encrypted.</param>
    /// <returns>The encrypted string.</returns>
    public static string Encrypt(string strToEncrypt)
    {
      string str;
      try
      {
        str = Cryptographer.Encrypt(strToEncrypt, Cryptographer._key);
      }
      catch (Exception ex)
      {
        str = "Wrong Input. " + ex.Message;
      }
      return str;
    }

    /// <summary>Decrypt the given string using the default key.</summary>
    /// <param name="strEncrypted">The string to be decrypted.</param>
    /// <returns>The decrypted string.</returns>
    public static string Decrypt(string strEncrypted)
    {
      string str;
      try
      {
        str = Cryptographer.Decrypt(strEncrypted, Cryptographer._key);
      }
      catch (Exception ex)
      {
        str = "Wrong Input. " + ex.Message;
      }
      return str;
    }

    /// <summary>Encrypt the given string using the specified key.</summary>
    /// <param name="strToEncrypt">The string to be encrypted.</param>
    /// <param name="strKey">The encryption key.</param>
    /// <returns>The encrypted string.</returns>
    public static string Encrypt(string strToEncrypt, string strKey)
    {
      string str;
      try
      {
         using (TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider())
         using (MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider())
         {
            byte[] hash = hasher.ComputeHash( Encoding.ASCII.GetBytes( strKey ) );
            cryptoServiceProvider.Key = hash;
            cryptoServiceProvider.Mode = CipherMode.ECB;
            byte[] bytes = Encoding.ASCII.GetBytes( strToEncrypt );
            str = Convert.ToBase64String( cryptoServiceProvider.CreateEncryptor().TransformFinalBlock( bytes, 0, bytes.Length ) );
         }
      }
      catch (Exception ex)
      {
        str = "Wrong Input. " + ex.Message;
      }
      return str;
    }

    /// <summary>Decrypt the given string using the specified key.</summary>
    /// <param name="strEncrypted">The string to be decrypted.</param>
    /// <param name="strKey">The decryption key.</param>
    /// <returns>The decrypted string.</returns>
    public static string Decrypt(string strEncrypted, string strKey)
    {
      string str;
      try
      {
         using(TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider())
         using(MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider())
         {
           byte[] hash = hasher.ComputeHash(Encoding.ASCII.GetBytes(strKey));
           cryptoServiceProvider.Key = hash;
           cryptoServiceProvider.Mode = CipherMode.ECB;
           byte[] inputBuffer = Convert.FromBase64String(strEncrypted);
           str = Encoding.ASCII.GetString(cryptoServiceProvider.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
			}
      }
      catch (Exception ex)
      {
        str = "Wrong Input. " + ex.Message;
      }
      return str;
    }
  }
}
