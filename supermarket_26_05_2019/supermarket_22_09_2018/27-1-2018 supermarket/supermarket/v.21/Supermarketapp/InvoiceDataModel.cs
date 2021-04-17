using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarketapp
{
    class InvoiceDataModel
    {

        public string item_name;
        public int real_time_price;
        public int quantity_purchased;
        public int discount;

        public InvoiceDataModel(string item_name, int real_time_price, int quantity_purchased, int discount)
        {
            this.item_name = item_name;
            this.real_time_price = real_time_price;
            this.quantity_purchased = quantity_purchased;
            this.discount = discount;
        }
      

    }
}
