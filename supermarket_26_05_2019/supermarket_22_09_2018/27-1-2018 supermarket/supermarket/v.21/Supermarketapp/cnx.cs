using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Supermarketapp
{
    class cnx
    {

        public MySqlConnection conx() 
        {
        String cs2 = "server=127.0.0.1;uid=root;" + "pwd='';database=supermarket;";
        MySqlConnection conn2 = null;
        conn2 = new MySqlConnection(cs2);
        return conn2;
        }
    }
}
