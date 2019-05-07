using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MouseIt
{
    public static class Api
    {

        static HttpClient client = new HttpClient();

        public static async Task Send(string jsonData)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://kloubma16.sps-prosek.cz/Is%20that%20all/RestApi/api.php");

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("key", jsonData));
            // Přidání dat do dotazu
            request.Content = new FormUrlEncodedContent(keyValues);
            // Zaslání POST dotazu
            var response = await client.SendAsync(request);
            // Získání odpovědi
            string responseContent = await response.Content.ReadAsStringAsync();


        }


        public static async Task<List<Profile>> Ask()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://kloubma16.sps-prosek.cz/Is%20that%20all/RestApi/api.php");

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("ask", "ask"));
     
            request.Content = new FormUrlEncodedContent(keyValues);
           
            var response = await client.SendAsync(request);
         
            string responseContent = await response.Content.ReadAsStringAsync();


            return JsonConvert.DeserializeObject<List<Profile>>(responseContent);


        }


    }
}