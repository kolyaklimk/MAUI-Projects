using NbrbAPI.Models;
using Newtonsoft.Json;

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
        public IEnumerable<Rate> GetRates(DateTime date)
        {
            List<Rate> rates = new();
            string message = _httpClient.GetAsync(
                "https://www.nbrb.by/api/exrates/rates?ondate=" +
                $"{date:yyyy-M-d}&periodicity=0").ToString();
            if (message != null)
            {
                rates = JsonConvert.DeserializeObject<List<Rate>>(message);
            }
            return rates.Where(objId => listOfId.Contains(objId.Cur_ID)).ToList();
        }
    }
}