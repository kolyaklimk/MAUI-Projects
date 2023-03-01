using NbrbAPI.Models;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace MyApplication.Pages.CurrencyConverter.Services
{
    internal class RateService : IRateService
    {
        private readonly List<int> listOfId = new() { 426, 456, 451, 431, 462, 429 };
        private readonly HttpClient _httpClient;
        public RateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Rate>> GetRates(DateTime date)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var message = await _httpClient.GetAsync(
                   "https://www.nbrb.by/api/exrates/rates?ondate=" +
                    $"{date:yyyy-M-d}&periodicity=0");
                if (message.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<List<Rate>>(message.Content.ReadAsStream()).
                        Where(objId => listOfId.Contains(objId.Cur_ID)).ToList();
                }
            }
            return null;
        }
    }
}