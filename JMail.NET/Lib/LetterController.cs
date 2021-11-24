using JMail.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JMail.NET.Lib
{
    public static partial class LetterController
    {
        private static HttpClient _httpClient = new HttpClient();

        public static JMailLetterReceiptResult Receive(JMailLetter letter, string ip)
        {
            try
            {
                DnsQuery.VerifyMessageSender(letter.Origin, ip);

                //decrypt letter
                JMailLetterDecrypted decrypted = new JMailLetterDecrypted();

                decrypted.DateReceived = letter.DateSent;
                decrypted.Recipient = Encryption.Decrypt(letter.Recipient);
                decrypted.Sender = Encryption.Decrypt(letter.Sender);
                decrypted.Message = JsonSerializer.Deserialize<JMailMessage>(Encryption.Decrypt(letter.EncryptedContent));

                return new JMailLetterReceiptResult
                {
                    Letter = decrypted,
                    Valid = true
                };
            }
            catch (UnauthorizedJMailSenderException e)
            {
                return new JMailLetterReceiptResult
                {
                    Letter = null,
                    Valid = false
                };
            }
            catch (InvalidJMailAddressException e)
            {
                return new JMailLetterReceiptResult
                {
                    Letter = null,
                    Valid = false
                };
            }
            catch (JMailEncryptionException e)
            {
                return new JMailLetterReceiptResult
                {
                    Letter = null,
                    Valid = false
                };
            }
            catch (Exception e)
            {
                return new JMailLetterReceiptResult
                {
                    Letter = null,
                    Valid = false
                };
            }
        }

        public static async Task<JMailLetterReceiptResult> SendMail(JMailLetter letter)
        {
            return default;
        }

        private static string _getServerPublicKey(string ip)
        {



            return default(string);
        }
    }
}
