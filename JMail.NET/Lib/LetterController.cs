using JMail.NET.Datastore;
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

        public static JMailLetterReceiptResult Receive(this JMailLetterEncrypted letter, string ip)
        {
            try
            {
                DnsQuery.VerifyMessageSender(letter.Origin, ip);

                //decrypt letter
                JMailLetter decrypted = new JMailLetter();

                decrypted.Date = letter.DateSent;
                decrypted.Recipient = Encryption.Decrypt(letter.Recipient);
                decrypted.Sender = Encryption.Decrypt(letter.Sender);
                decrypted.Message = JsonSerializer.Deserialize<JMailMessage>(Encryption.Decrypt(letter.EncryptedContent));

                return new JMailLetterReceiptResult
                {
                    Letter = decrypted,
                    Valid = true
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

        public static async Task<JMailLetterSendResult> SendMail(JMailLetter letter, short priority)
        {
            if (!RelayData.Domains.Contains(letter.Origin)) return new JMailLetterSendResult
            {
                Message = $"This relay does not support the domain '{letter.Origin}'",
                Valid = false
            };

            //get the PRIMARY IP for TXT record of the domain
            var address = DnsQuery.GetPrimaryAddressFromDomain(letter.Target);
            
            //get public key of relay to encrypt
            var publicKey = await _getServerPublicKey(address);
            if (publicKey is null) return new JMailLetterSendResult
            {
                Message = "The target does not have a valid public key",
                Valid = false
            };

            //encrypt the letter
            var encrypted = new JMailLetterEncrypted
            {
                Origin = letter.Origin,

                Priority = letter.Priority,
                DateSent = DateTime.UtcNow,

                Sender = Encryption.Encrypt(letter.Sender, publicKey),
                Recipient = Encryption.Encrypt(letter.Recipient, publicKey),
                EncryptedContent = Encryption.Encrypt(letter.Message, publicKey),
            };

            return await _postLetter(encrypted, address);
        }

        private static async Task<JMailLetterSendResult> _postLetter(JMailLetterEncrypted letter, RelayAddress address)
        {
            try
            {
                using var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"letter", JsonSerializer.Serialize(letter)}
                });
                using var response = await _httpClient.PostAsync($"http://{address.IP}:{address.Port}/jmailctl/relay/mail", content);

                if (!response.IsSuccessStatusCode) return new JMailLetterSendResult
                {
                    Message = $"The server did not accept the mail, error: {await response.Content.ReadAsStringAsync()}",
                    Valid = false
                };

                return new JMailLetterSendResult
                {
                    Message = String.Empty,
                    Valid = true
                };
            }
            catch (Exception e)
            {
                return new JMailLetterSendResult
                {
                    Message = "An error has occured",
                    Valid = false
                };
            }
        }

        private static async Task<string> _getServerPublicKey(RelayAddress address)
        {
            using var response = await _httpClient.PostAsync($"http://{address.IP}:{address.Port}/jmailctl/relay/getkey", null);
            if (!response.IsSuccessStatusCode) return null;
            
            return await response.Content.ReadAsStringAsync();
        }

        private static async Task<string[]> _getServerAuthroizedDomains(RelayAddress address)
        {
            using var response = await _httpClient.PostAsync($"http://{address.IP}:{address.Port}/jmailctl/relay/info", null);
            if (!response.IsSuccessStatusCode) throw new InvalidJMailRelayResponseException("The relay did not respond correctly");
            var data = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            if (!data.RootElement.TryGetProperty("Domains", out var domainsElement)) throw new InvalidJMailRelayResponseException();
            var domains = domainsElement.Deserialize<string[]>();
            return domains;
        }

        public static bool ValidateAddress(string address)
        {
            try
            {
                var count = address.Count(x => x is '#');
                if (count is 0 || count > 1) return false;

                var split = address.Split('#');

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
