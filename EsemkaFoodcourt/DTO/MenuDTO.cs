using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsemkaFoodcourt.DTO
{
    internal class MenuDTO
    {
        public string Menu { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Subtotal { get; set; }
    }
}
