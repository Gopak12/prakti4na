using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace prakti4na
{
    class db
    {
        public static void insertQuery(string s)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=praktBaza;Username=postgres;Password=pass");
            conn.Open();

            // Define a query
            NpgsqlCommand cmd = new NpgsqlCommand(s, conn);

            // Execute a query
            cmd.ExecuteNonQuery();

            // Close connection
            conn.Close();
        }

        public static int countQuery(string s)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=praktBaza;Username=postgres;Password=pass");
            conn.Open();

            // Define a query
            NpgsqlCommand cmd = new NpgsqlCommand(s, conn);

            // Execute a query
            var res = Int32.Parse(cmd.ExecuteScalar().ToString());

            // Close connection
            conn.Close();

            return res;
        }


        public static System.Data.DataTable selectQuery(string s)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Database=praktBaza;Username=postgres;Password=pass");
            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand(s, conn);
            try
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                conn.Close();
                return new DataTable();
            }
        }

    }
}
