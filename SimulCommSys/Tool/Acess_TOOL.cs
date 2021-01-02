using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulCommSys.Tool
{
   public class Acess_TOOL
    {
        private void ACCESS_QUERY()
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\GGS\\Documents\\Database2.mdb;Persist Security Info=True;Jet OLEDB:Database Password='119120';"); //Jet OLEDB:Database Password=
            OleDbCommand cmd = conn.CreateCommand();

            cmd.CommandText = "select * from Model_name";
            conn.Open();
            OleDbDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            if (dr.HasRows)
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    dt.Columns.Add(dr.GetName(i));
                }
                dt.Rows.Clear();
            }
            while (dr.Read())
            {
                DataRow row = dt.NewRow();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    row[i] = dr[i];
                }
                dt.Rows.Add(row);
            }
            cmd.Dispose();
            conn.Close();
           // DG.ItemsSource = dt.DefaultView;
        }
    }
}
