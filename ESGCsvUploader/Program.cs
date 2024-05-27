using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Reflection;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using ESGCsvUploader.Models;

namespace ESGCsvUploader
{
    class Program
    {
        //private static object ex;
        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            Environment.Exit(1);//after error - it exited here
        }
        static void Main(string[] args)
        {
            //var builder = new ConfigurationBuilder().AddJsonFile("config.json", false, false);
            //IConfiguration config = builder.Build();
            //var connectionString = config.GetSection("connectionString");
            //Console.WriteLine($"connectionString");
            //Directory.GetCurrentDirectory()
            //System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
            string ConnectionString = "Data Source=localhost;Initial Catalog=ESG_AJ;TrustServerCertificate=True;Integrated Security=True;";
            //SqlConnection myConn = new SqlConnection(ConnectionString);
            //myConn.Open();
            bool hasHeader = true;
            int rowCounter = 0;
            string ExeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string CsvDirectory = Path.Combine(ExeDirectory, @"CSV Files");
            
            //using HttpClient client = new();
            
                string folder = CsvDirectory;
                //@"C:\Temp\Excel to CSV\CSV for Upload";
                string filter = "*.csv";
                string[] files = Directory.GetFiles(folder, filter);
            
                foreach (string file in files)
                {            
                    using (StreamReader sr = new StreamReader(file))
                    {            
                        string row = string.Empty;            
                        while ((row = sr.ReadLine()) != null)
                        {
                            if (!hasHeader || rowCounter != 0)
                            {
                                List<Customer> customerList = new List<Customer>();
                                try
                                {
                                    var query = "usp_ConsoleCsvUpload";
                                    using (var connection = new SqlConnection(ConnectionString))//binding, endpoint))
                                    using (SqlCommand cmd = new SqlCommand(query, connection)) 
                                    {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    // parse the line to a customer structure
                                    CustomerDetails customer = new CustomerDetails(row);

                                    cmd.Parameters.Add("@CustomerRef", SqlDbType.VarChar).Value = customer.CustomerRef;
                                    cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = customer.CustomerName;
                                    cmd.Parameters.Add("@AddressLine1", SqlDbType.VarChar).Value = customer.AddressLine1;
                                    cmd.Parameters.Add("@AddressLine2", SqlDbType.VarChar).Value = customer.AddressLine2;
                                    cmd.Parameters.Add("@Town", SqlDbType.VarChar).Value = customer.Town;
                                    cmd.Parameters.Add("@County", SqlDbType.VarChar).Value = customer.County;
                                    cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = customer.Country;
                                    cmd.Parameters.Add("@PostCode", SqlDbType.VarChar).Value = customer.PostCode;

                                    connection.Open();
                                    SqlDataReader reader = cmd.ExecuteReader();
                                    
                                    Console.WriteLine($"{customer.CustomerRef} {customer.CustomerName} {customer.AddressLine1} {customer.AddressLine2} {customer.Town} {customer.County} {customer.Country}{customer.PostCode}");

                                    // log result
                                    //Console.WriteLine("Record: {0}, Result: {1}", rowCounter);
                                    //WriteLogFile.WriteLog("ConsoleLog", string.Format("{0} @ {1}", "Log is Created at", DateTime.Now));
                                    Console.WriteLine("Log is Written Successfully !!!");
                                    }
                                       
                                }
                                catch (IndexOutOfRangeException ix)
                                {
                                    Console.WriteLine(ix.InnerException);
                                    Console.WriteLine("Out of range ");
                                }
                                catch (InvalidDataException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
            
                            rowCounter++;
                        }
                    }
                }          
            
            Console.WriteLine("There were {0} lines.", rowCounter);
            Console.ReadLine();
        }
    }
}