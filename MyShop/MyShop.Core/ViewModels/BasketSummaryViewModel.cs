using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class BasketSummaryViewModel
    {
        public int basketCount { get; set; }
        public decimal basketTotal { get; set; }

        public BasketSummaryViewModel()
        {
        }

        public BasketSummaryViewModel(int BasketCount, decimal BasketTotal)
        {
            basketCount = BasketCount;
            basketTotal = BasketTotal;
        }
    }
}
