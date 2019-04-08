using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.DataAccess;
using MVC_Project.Models;
using static MVC_Project.Models.Company;
using static MVC_Project.Models.Divident;
using Newtonsoft.Json;
namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        /*
            These lines are needed to use the Database context,
            define the connection to the API, and use the
            HttpClient to request data from the API
        */
        public ApplicationDbContext dbContext;
        //Base URL for the IEXTrading API. Method specific URLs are appended to this base URL.
        string BASE_URL = "https://api.iextrading.com/1.0/";
        HttpClient httpClient;

        /*
             These lines create a Constructor for the HomeController.
             Then, the Database context is defined in a variable.
             Then, an instance of the HttpClient is created.

        */
        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }

        /*
            Calls the IEX reference API to get the list of symbols.
            Returns a list of the companies whose information is available. 
        */

   
        //Companies
        public List<Company> GetSymbols()
        {
            string IEXTrading_API_PATH = BASE_URL + "ref-data/symbols";
            string companyList = "";
            List<Company> companies = null;

            // connect to the IEXTrading API and retrieve information
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                companyList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // now, parse the Json strings as C# objects
            if (!companyList.Equals(""))
            {
                // https://stackoverflow.com/a/46280739
                companies = JsonConvert.DeserializeObject<List<Company>>(companyList);
                companies = companies.GetRange(0, 50);
            }

            return companies;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        /*Added by Prachi
         * To display companies
         *
         */
        public IActionResult Companies()
        {
            //Set ViewBag variable first
            ViewBag.dbSuccessComp = 0;
            List<Company> companies = GetSymbols();

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["Companies"] = JsonConvert.SerializeObject(companies);

            return View(companies);
        }

        /*
            The Symbols action calls the GetSymbols method that returns a list of Companies.
            This list of Companies is passed to the Symbols View.
        */
        public IActionResult Symbols()
        {
            //Set ViewBag variable first
            ViewBag.dbSuccessComp = 0;
            List<Company> companies = GetSymbols();

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["Companies"] = JsonConvert.SerializeObject(companies);

            return View(companies);
        }



        //Dividents
        public List<Divident> GetDividents(string symbol)
        {
            
            string IEXTrading_API_PATH = BASE_URL + "/stock/" + symbol + "/dividends/5y";
            string dividentList = "";
            List<Divident> dividends = new List<Divident>();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            HttpResponseMessage respose = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            if (respose.IsSuccessStatusCode)
            {
                dividentList = respose.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            if (!dividentList.Equals(""))
            {
                dividends = JsonConvert.DeserializeObject<List<Divident>>(dividentList);
                dividends.GetRange(0, dividends.Count);
            }
            return dividends;
        }
        [Route("{id}")]
        public IActionResult Divident(string id)
        {
            String symbols = id;
            //Set ViewBag variable first
            ViewBag.dbSuccessComp = 0;
            List<Divident> divident = GetDividents(symbols);

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["divident"] = JsonConvert.SerializeObject(divident);
            //Console.WriteLine(divident);
            return View(divident);
        }







        //Price
        public double GetPrice(string symbol)
        {

            string IEXTrading_API_PATH = BASE_URL + "/stock/" + symbol + "/price";
            double price = 0;
           // List<Price> prices = new List<Price>();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            HttpResponseMessage respose = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            if (respose.IsSuccessStatusCode)
            {
                price = Convert.ToDouble(respose.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            
            return price;
        }

        
        public IActionResult Price(string id)
        {
            String symbols = id;
            //Set ViewBag variable first
            ViewBag.dbSuccessComp = 0;
            double Price = GetPrice(symbols);

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["Price"] = JsonConvert.SerializeObject(Price);
            ViewBag.Message = Price;
            //Console.WriteLine(divident);
            return View(Price);
        }








        //Largest Trades
        public List<Largest_Trade> GetLargestTrade(string symbol)
        {

            string IEXTrading_API_PATH = BASE_URL + "/stock/" + symbol + "/largest-trades";
            string largest_trade_List = "";
            List<Largest_Trade> largest_trades = new List<Largest_Trade>();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            HttpResponseMessage respose = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            if (respose.IsSuccessStatusCode)
            {
                largest_trade_List = respose.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            if (!largest_trade_List.Equals(""))
            {
                largest_trades = JsonConvert.DeserializeObject<List<Largest_Trade>>(largest_trade_List);
                largest_trades.GetRange(0, largest_trades.Count);
            }
            return largest_trades;
        }
        
        public IActionResult Largest_Trade(string id)
        {
            String symbols = id;
            //Set ViewBag variable first
            ViewBag.dbSuccessComp = 0;
            List<Largest_Trade> largest_trade = GetLargestTrade(symbols);

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["largest_trade"] = JsonConvert.SerializeObject(largest_trade);
            //Console.WriteLine(divident);
            return View(largest_trade);
        }




        //Splits
        public List<Splits> GetSplits(string symbol)
        {

            string IEXTrading_API_PATH = BASE_URL + "/stock/" + symbol + "/splits/5y";
            string split_List = "";
            List<Splits> split = new List<Splits>();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            HttpResponseMessage respose = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            if (respose.IsSuccessStatusCode)
            {
                split_List = respose.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            if (!split_List.Equals(""))
            {
                split = JsonConvert.DeserializeObject<List<Splits>>(split_List);
                split.GetRange(0, split.Count);
            }
            return split;
        }

        public IActionResult Split(string id)
        {
            String symbols = id;
            //Set ViewBag variable first
            ViewBag.dbSuccessComp = 0;
            List<Splits> split = GetSplits(symbols);

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["split"] = JsonConvert.SerializeObject(split);
            
            return View(split);
        }




        //Previous
        public Previous GetPrevious(string symbol)
        {

            string IEXTrading_API_PATH = BASE_URL + "/stock/" + symbol + "/previous";
            string previous_List = "";
            Previous previous = new Previous();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            HttpResponseMessage respose = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            if (respose.IsSuccessStatusCode)
            {
                previous_List = respose.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            if (!previous_List.Equals(""))
            {
                previous = JsonConvert.DeserializeObject<Previous>(previous_List);
                //previous.GetRange(0, previous.Count);
            }
            return previous;
        }

        public IActionResult Previous(string id)
        {
            String symbols = id;
            //Set ViewBag variable first
            ViewBag.dbSuccessComp = 0;
            Previous previous = GetPrevious(symbols);

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["previous"] = JsonConvert.SerializeObject(previous);

            return View(previous);
        }














        //Last Common Steps
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// Save the available symbols in the database
        /// </summary>
        /// <returns></returns>
        public IActionResult PopulateSymbols()
        {
           
            List<Company> companies = JsonConvert.DeserializeObject<List<Company>>(TempData["Companies"].ToString());

            foreach (Company company in companies)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                if (dbContext.Companies.Where(c => c.Symbol.Equals(company.Symbol)).Count() == 0)
                {
                    dbContext.Companies.Add(company);
                }
            }

            dbContext.SaveChanges();
            ViewBag.dbSuccessComp = 1;
            return View("Symbols", companies);
        }

        public IActionResult SaveDividend()
        {

            List<Divident> divident = JsonConvert.DeserializeObject<List<Divident>>(TempData["divident"].ToString());

            foreach (Divident div in divident)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                //if (dbContext.Divident.Where(c => c..Equals(div.Symbol)).Count() == 0)
                //{
                    dbContext.Divident.Add(div);
                //}
            }

            dbContext.SaveChanges();
            ViewBag.dbSuccessComp = 1;
            return View("Divident", divident);
        }


    }
}
