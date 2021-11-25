using JMail.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace JMail.NET.Lib
{
    public static class Encryption
    {
        static Encryption()
        {
            _rsaProvider = RSA.Create();
            _publicKey = _rsaProvider.ExportRSAPublicKey();
            _privateKey = _rsaProvider.ExportRSAPrivateKey(); 
            _publicKeyEncoded = Convert.ToBase64String(_publicKey);
            _privateKeyEncoded = Convert.ToBase64String(_privateKey);
        }

        private static RSA _rsaProvider = RSA.Create();
        private static byte[] _publicKey;
        private static byte[] _privateKey;

        private static string _publicKeyEncoded;
        private static string _privateKeyEncoded;

        public static string GetRsaPublicKey() => _publicKeyEncoded;

        public static string Decrypt(string data) => _decrypt(data);
        //public static JMailMessage Decrypt(string encryptedContent) => JsonSerializer.Deserialize<JMailMessage>(_decrypt(encryptedContent));
        //public static JMailMessage Decrypt(this JMailLetter letter) => JsonSerializer.Deserialize<JMailMessage>(letter.EncryptedContent);
        public static string Encrypt(JMailMessage message, string publicKey) => _encrypt(JsonSerializer.Serialize(message), publicKey);
        public static string Encrypt(string message, string publicKey) => _encrypt(message, publicKey);

        private static string _encrypt(string data, string publicKey)
        {
            try
            {
                var rsa = RSA.Create();
                rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out var count);
                var result = rsa.Encrypt(Encoding.Unicode.GetBytes(data), RSAEncryptionPadding.OaepSHA512);
                return Convert.ToBase64String(result);
            }
            catch
            {
                throw new JMailEncryptionException();
            }
        }

        private static string _decrypt(string data)
        {
            try
            {
                var result = _rsaProvider.Decrypt(Convert.FromBase64String(data), RSAEncryptionPadding.OaepSHA512);
                return Encoding.Unicode.GetString(result);
            }
            catch
            {
                throw new JMailEncryptionException();
            }
        }

    }

    [Serializable]
    public class JMailEncryptionException : Exception
    {
        public JMailEncryptionException() : base() { }
        public JMailEncryptionException(string message) : base(message) { }
        public JMailEncryptionException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected JMailEncryptionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
