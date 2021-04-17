using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarketapp
{
    public class reportsDataModel
    {
        public int invoice_id;
        public int total_price;
        public DateTime datetime;
        public String added_by_username;
        public int discount;
        public int cash_in;
        public int cash_out;

        public void set(int invoice_id,int total_price,DateTime datetime,string added_by_username,int discount,int cash_in,int cash_out) {
            this.invoice_id = invoice_id;
            this.total_price = total_price;
            this.datetime = datetime;
            this.added_by_username = added_by_username;
            this.discount = discount;
            this.cash_in = cash_in;
            this.cash_out = cash_out;
        }

        public reportsDataModel get() {
            return this;
        }
      
    }

}
