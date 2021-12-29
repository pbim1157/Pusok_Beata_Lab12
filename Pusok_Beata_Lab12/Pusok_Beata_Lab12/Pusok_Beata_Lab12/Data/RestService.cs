using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Pusok_Beata_Lab12.Models;
using System.Net.Http;

namespace Pusok_Beata_Lab12.Data
{
    public class RestService : IRestService
    {
        HttpClient client;
        //se va modifica ulterior cu ip-ul si portul corespuzator
        string RestUrl = "http://192.168.1.8/api/shoplists/{0}";
        public List<ShopList> Items { get; private set; }
        public RestService()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (MessageProcessingHandler, ClientCertificateOption, chain, errors) => { return true; };
            client = new HttpClient(httpClientHandler);
        }
        public async Task<List<ShopList>> RefreshDataAsync()
        {
            Items = new List<ShopList>();
            Uri uri = new Uri(string.Format(RestUrl, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<ShopList>>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return Items;
        }
        public async Task SaveShopListAsync(ShopList item, bool isNewItem = true)
        {
            Uri uri = new Uri(string.Format(RestUrl, string.Empty));
            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(@"\tTodoItem succesfully saved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"tERROR {0}", ex.Message);
            }
        }
        public async Task DeleteShopListAsync(int id)
        {
            Uri uri = new Uri(string.Format(RestUrl, id));
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(@"\TodoItem succesfully deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
