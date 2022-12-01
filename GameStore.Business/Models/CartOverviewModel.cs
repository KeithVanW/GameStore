using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Business.Models
{
    public class CartOverviewModel
    {
        public string UserId { get; set; }
        public IEnumerable<GameModel> Games { get; set; }
        public double TotalPrice { get; set; }
    }
}
