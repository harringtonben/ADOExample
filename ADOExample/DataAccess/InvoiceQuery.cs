using ADOExample.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOExample.DataAccess
{
    class InvoiceQuery
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;
        public List<Invoice> GetInvoiceByTrackFirstLetter(string firstCharacter)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = $@"select  i.*
                                from invoice i
	                                join InvoiceLine x on x.InvoiceId = i.InvoiceId
                                where exists (select TrackId from Track where Name like @firstLetter + '%' and TrackId = x.TrackId)";

                var firstLetter = new SqlParameter("@firstLetter", SqlDbType.NVarChar);
                firstLetter.Value = firstCharacter;
                cmd.Parameters.Add(firstLetter);

                var reader = cmd.ExecuteReader();

                var invoices = new List<Invoice>();

                while (reader.Read())
                {
                    var invoice = new Invoice
                    {
                        InvoiceId = int.Parse(reader["InvoiceId"].ToString()),
                        CustomerId = int.Parse(reader["CustomerId"].ToString()),
                        InvoiceDate = DateTime.Parse(reader["InvoiceDate"].ToString()),
                        BillingAddress = reader["BillingAddress"].ToString(),
                        BillingCity = reader["BillingCity"].ToString(),
                        BillingState = reader["BillingState"].ToString(),
                        BillingCountry = reader["BillingCountry"].ToString(),
                        BillingPostalCode = reader["BillingPostalCode"].ToString(),
                        Total = double.Parse(reader["Total"].ToString())
                    };

                    invoices.Add(invoice);
                }

                return invoices;
            }
        }
    }

    
}
