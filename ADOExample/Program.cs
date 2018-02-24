using ADOExample.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
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
            var firstletter = Console.ReadLine();

            var invoiceQuery = new InvoiceQuery();
            var invoices = invoiceQuery.GetInvoiceByTrackFirstLetter(firstletter);

            foreach (var invoice in invoices)
            {
                Console.WriteLine($"Invoice ID {invoice.InvoiceId} was shipped to {invoice.BillingAddress}.");
            }

            var invoiceModifier = new InvoiceModifier();
            invoiceModifier.DeleteInvoice(1);

            Console.ReadLine();

        }
    }
}
