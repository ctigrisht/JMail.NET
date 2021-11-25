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

        public static JMailLetterReceiptResult Receive(JMailLetterEncrypted letter, string ip)
        {
            try
            {
                DnsQuery.VerifyMessageSender(letter.Origin, ip);

                //decrypt letter
                JMailLetter decrypted = new JMailLetter();

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
            JMailLetterEncrypted encrypted = new JMailLetterEncrypted();
            if (!RelayData.Domains.Contains(letter.Origin)) return new JMailLetterSendResult
            {
                Message = $"This relay does not support the domain '{letter.Origin}'",
                Valid = false
            };;

            //get the PRIMARY IP for TXT record of the domain
            var address = DnsQuery.GetPrimaryAddressFromDomain(letter.Target);
            
            //get public key of relay to encrypt
            var publicKey = await _getServerPublicKey(address);

            //encrypt the letter
            encrypted.Sender = Encryption.Encrypt(letter.Sender, publicKey);
            encrypted.Recipient = Encryption.Encrypt(letter.Recipient, publicKey);
            encrypted.EncryptedContent = Encryption.Encrypt(letter.Message, publicKey);
            
            encrypted.Origin = letter.Origin;
            encrypted.DateSent = DateTime.UtcNow;



            return default;
        }

        private static async Task<JMailLetterSendResult> _postLetter(JMailLetterEncrypted letter)
        {

        }

        private static async Task<string> _getServerPublicKey(RelayAddress address)
        {
            using var response = await _httpClient.PostAsync($"http://{address.Address}:{address.Port}/jmailctl/relay/getkey", null);
            if (!response.IsSuccessStatusCode) return null;
            
            return await response.Content.ReadAsStringAsync();
        }

        private static async Task<string[]> _getServerAuthroizedDomains(RelayAddress address)
        {
            using var response = await _httpClient.PostAsync($"http://{address.Address}:{address.Port}/jmailctl/relay/info", null);
            if (!response.IsSuccessStatusCode) throw new InvalidJMailRelayResponseException("The relay did not respond correctly");
            var data = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            if (!data.RootElement.TryGetProperty("domains", out var domainsElement)) throw new InvalidJMailRelayResponseException();
            var domains = domainsElement.Deserialize<string[]>();
            return domains;
        }
    }
}
