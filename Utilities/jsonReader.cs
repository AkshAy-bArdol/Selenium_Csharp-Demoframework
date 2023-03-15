using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharpDemoFramework.Utilities
{
    public class JsonReader
    {
        public string extractData(String tokenName)
        {
           var myJasonString = File.ReadAllText("Utilities/testData.json");
            var jsonObject = JToken.Parse(myJasonString);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }

        public string[] extractDataArray(String tokenName)
        {
            var myJasonString = File.ReadAllText("Utilities/testData.json");
            var jsonObject = JToken.Parse(myJasonString);
            List <String> productsList = jsonObject.SelectTokens(tokenName).Values<string>().ToList();
           return productsList.ToArray();
        }
    }
}
