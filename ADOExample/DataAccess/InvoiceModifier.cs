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
    class InvoiceModifier
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;

        public bool DeleteInvoice(int invoiceId)
        {
            if(DeleteLineItems(invoiceId))
            {
                return false;
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"delete 
                                    from Invoice
                                    where InvoiceId = @InvoiceId";

                connection.Open();
                var invoiceIdParam = new SqlParameter("@InvoiceId", SqlDbType.Int);
                invoiceIdParam.Value = invoiceId;
                cmd.Parameters.Add(invoiceIdParam);

                var result = cmd.ExecuteNonQuery();

                return result == 1;
            }
        }

        bool DeleteLineItems(int invoiceId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"delete 
                                    from InvoiceLine
                                    where InvoiceId = @InvoiceId";

                connection.Open();
                var invoiceIdParam = new SqlParameter("@InvoiceId", SqlDbType.Int);
                invoiceIdParam.Value = invoiceId;
                cmd.Parameters.Add(invoiceIdParam);

                var result = cmd.ExecuteNonQuery();

                return result >= 1;
            }
        }
    }
}
