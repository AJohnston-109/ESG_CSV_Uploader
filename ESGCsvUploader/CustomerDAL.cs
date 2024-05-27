using ESGCsvUploader.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace ESGCsvUploader
{
    public class CustomerDAL
    {
        public string connectionString;
        //public EmployeeDAL()
        //{
        //    connectionString = AppConfig.GetConnectionString();
        //}
        //
        //public List<Customer> InsertCustomer()
        //{
        //    List<Customer> customerList = new List<Customer>();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        using (SqlCommand cmd = new SqlCommand("usp_ConsoleCsvUpload", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //
        //            cmd.Parameters.Add("@CustomerRef", SqlDbType.VarChar).Value = txtFirstName.Text;
        //            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;
        //            //@CustomerRef
        //            //			, @CustomerName
        //            //			, @AddressLine1
        //            //			, @AddressLine2
        //            //			, @Town
        //            //			, @County
        //            //			, @Country
        //            //			, @Postcode
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //
        //    return employeesList;
        //}
    }
}
