using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOExample
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var connection = new SqlConnection("Server=(local);Database=Chinook;Trusted_Connection=True;"))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select customerid, firstname, lastname, supportrepid from customer
                                where country = 'Brazil'";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var customerId = reader.GetInt32(0);
                    var firstname = reader.GetSqlString(1);
                    var lastname = reader.GetSqlString(2);
                    var supportRepId = reader.GetInt32(3);

                    Console.WriteLine($"{customerId} {firstname} {lastname} {supportRepId}");
                }
            }
               
            Console.ReadLine();
        }
    }
}
