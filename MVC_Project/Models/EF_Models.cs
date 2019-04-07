using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
        public class Company
        {
            [Key]
            public string Symbol { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
            public bool IsEnabled { get; set; }
            public string Type { get; set; }
            public string IexId { get; set; }
        }
        public class Divident
        {
            [Key]
            public DateTime Exdate { get; set; }
            public DateTime Payment_date { get; set; }
            public DateTime Record_date { get; set; }
            //public DateTime declaredDate { get; set; }
            public float? Amount { get; set; }
            public string type { get; set; }
            public string qualified { get; set; }
    }

    public class Price
    {
        [Key]
        public double Amount { get; set; }
        public string Symbol { get; set; }
    }


    public class Largest_Trade
    {
        [Key]
        public double price { get; set; }
        public double size { get; set; }
        public DateTime time { get; set; }
        public DateTime timelabel { get; set; }
        public string venue { get; set; }
        public string name { get; set; }
    }

    public class Splits
    {
        [Key]
       
        public DateTime exDate { get; set; }
        public DateTime declaredDate { get; set; }
        public DateTime recordDate { get; set; }
        public DateTime paymentDate { get; set; }
        public double ratio { get; set; }
        public double toFactor { get; set; }
        public double forFactor { get; set; }
    }

}
