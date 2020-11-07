using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Cw1
{
    class Program
    {
        public static async Task Main(string[] args)
        {

            if (args.Length == 0 || args == null)
            {
                throw new ArgumentNullException("The url parameter was not passed");
            }
            bool result = Uri.TryCreate(args[0], UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
           
            if (!result)
            {
                throw new ArgumentException("url is not correct");
            }

            using HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uriResult);


            string content = "";
            if (response.StatusCode == HttpStatusCode.OK)
            {
                content = await response.Content.ReadAsStringAsync();

            }
            else
            {
                Console.WriteLine("Bład w czasie pobierania strony");
            }

            var regex = new Regex("[a-z0-9]+@[a-z.]+");
            MatchCollection matches = regex.Matches(content);
            if (matches.Count == 0)
            {
                Console.WriteLine("Nie znaleziono adresów email");
            }
            else
            {
                HashSet<string> uniqueEmail = new HashSet<string>();
                foreach (Match m in matches)
                {
                    uniqueEmail.Add(m.Value);
                }
                uniqueEmail.ToList().ForEach(Console.WriteLine);
            }

        }

    }
}

