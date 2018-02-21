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
            var connection = new SqlConnection("Server=(local);Database=Chinook;Trusted_Connection=True;");
            var cmd = connection.CreateCommand();

            cmd.CommandText = @"select count(*) as [Total Invoices], DATENAME(yyyy, InvoiceDate) as Year from Invoice
                                where InvoiceDate like '%2009%'
                                or InvoiceDate like '%2011%'
                                group by DATENAME(yyyy, InvoiceDate)";

            connection.Open();
        }
    }
}
