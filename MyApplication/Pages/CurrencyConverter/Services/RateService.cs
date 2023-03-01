using NbrbAPI.Models;
using System;
using System.Diagnostics;
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
            List<Rate> rates = new();
            var message = new HttpRequestMessage(HttpMethod.Get, 
                "https://www.nbrb.by/api/exrates/rates?ondate=" + 
                $"{date:yyyy-M-d}&periodicity=0");
            message.Headers.Add("Accept", "application/json");
            var response = await _httpClient.SendAsync(message);
            //var message = _httpClient.GetAsync(
            //   "https://www.nbrb.by/api/exrates/rates?ondate=" +
            //    $"{date:yyyy-M-d}&periodicity=0");
            if (response.IsSuccessStatusCode)
            {
                rates = JsonSerializer.Deserialize<List<Rate>>(response.Content.ReadAsStream());
            }
            return rates.Where(objId => listOfId.Contains(objId.Cur_ID)).ToList();
        }
    }
}