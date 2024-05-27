using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESGCsvUploader
{
    public class CustomerDetails
    {
        private const int CustomerRefCol = 0;
        private const int CustomerNameCol = 1;
        private const int AddressLine1Col = 2;
        private const int AddressLine2Col = 3;
        private const int TownCol = 4;
        private const int CountyCol = 5;
        private const int CountryCol = 6;
        private const int PostCodeCol = 7;

        private readonly string[] parts;

        public CustomerDetails(string line)
        {
            parts = line.Split(',');
        }
        //public string User => parts[UserCol];

        //public string Vehicle => parts[VehicleCol];

        public int CustomerRef
        {
            get
            {
                int.TryParse(parts[CustomerRefCol], out var result);
                return result;
            }
        }
        public string CustomerName
        {
            get
            {
                return parts[CustomerNameCol];
            }
        }
        public string AddressLine1
        {
            get
            {
                return parts[AddressLine1Col];
            }
        }
        public string AddressLine2
        {
            get
            {
                return parts[AddressLine2Col];
            }
        }
        public string Town
        {
            get
            {
                return parts[TownCol];
            }
        }
        public string County
        {
            get
            {
                return parts[CountyCol];
            }
        }
        public string Country
        {
            get
            {
                return parts[CountryCol];
            }
        }
        public string PostCode
        {
            get
            {
                return parts[PostCodeCol];
            }
        }
    }
}
