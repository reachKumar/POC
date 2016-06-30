using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using POManagementASPNet.CurrencyConverter;

using System.Net;
using System.Text.RegularExpressions;
using System.Web.Script.Services;

namespace POManagementASPNet
{
    public class Common
    {
        [WebMethod]
        public static string GetCurrencyUSDollarValue(string strFromCurrency)
        {
            string strReturnValue = string.Empty;
            try
            {
                Currency fromCurrency = new Currency();
                foreach (Currency currency in Enum.GetValues(typeof(Currency)))
                {
                    if (currency.ToString() == strFromCurrency)
                    {
                        fromCurrency = currency;
                    }
                }
                CurrencyConvertorSoapClient soapClient = new CurrencyConvertorSoapClient();
                double rate = soapClient.ConversionRate(fromCurrency, Currency.USD);
                strReturnValue = rate.ToString();
            }
            catch { }
            return strReturnValue;
        }
    }

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        [WebMethod]
        public decimal ConvertYHOO(decimal amount, string fromCurrency, string toCurrency)
        {
            WebClient web = new WebClient();
            string url = string.Format("http://finance.yahoo.com/d/quotes.csv?e=.csv&f=sl1d1t1&s={0}{1}=X", fromCurrency.ToUpper(), toCurrency.ToUpper());
            string response = web.DownloadString(url);
            string[] values = Regex.Split(response, ",");
            decimal rate = System.Convert.ToDecimal(values[1]);
            //return rate * amount;
            return rate;
        }
    }

}