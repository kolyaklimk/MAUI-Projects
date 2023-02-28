using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NbrbAPI.Models;

namespace MyApplication.Pages.CurrencyConverter.Services
{
    internal interface IRateService
    {
        IEnumerable<Rate> GetRates(DateTime date);
    }
}
