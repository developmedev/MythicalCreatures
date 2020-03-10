using MythicalCreatures.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MythicalCreatures.Services
{
    public class RestService
    {
        readonly HttpClient httpclient;
        public RestService()
        {
            httpclient = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };

        }

        public List<Item> GetVsAsync()
        {
            var items = new List<Item>();
            var weburl = "https://api.github.com/gists/ca33ec757e761860db24cfc34891e915";
            var response = httpclient.GetAsync(weburl).GetAwaiter().GetResult();
            string jsonString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            JObject jObject = JObject.Parse(jsonString);
            var text1 = (string)jObject["files"]["MythicalCreatures.json"]["content"];
            JObject jObject1 = JObject.Parse(text1);
            var text = (JArray)jObject1["files"];
            for (int i = 0; i < text.Count; i++)
            {
                JObject aItem = (JObject)text[i];
                var item = new Item { Id = Guid.NewGuid().ToString(), Name = aItem["Name"].ToString(), Description = aItem["Description"].ToString(), Image = aItem["Image"].ToString(), Location= aItem["Location"].ToString(), Origin= aItem["Origin"].ToString() };
                items.Add(item);
            }
            return items;
        }
        //public void GetTestAsync()
        //{
        //    var items = new List<Item>();
        //    var weburl = "https://www.google.com";
        //    var response = httpclient.GetAsync(weburl).GetAwaiter().GetResult();
        //    string jsonString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //}
    }

}
