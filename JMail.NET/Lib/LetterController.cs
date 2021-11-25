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

        

        private static async Task<string> _getServerPublicKey(string ip, int port)
        {
            using var response = await _httpClient.PostAsync($"http://{ip}:{port}/api/jmail/info/getkey", null);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
