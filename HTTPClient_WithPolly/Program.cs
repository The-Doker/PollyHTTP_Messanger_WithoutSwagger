using Polly;
using Polly.Retry;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace PollyWithHttpClientFactoryInAConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {

            AsyncRetryPolicy _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(10, retryAttempt => {
                    var timeToWait = TimeSpan.FromSeconds(1);
                    Console.WriteLine($"Waiting {timeToWait.TotalSeconds} seconds");
                    return timeToWait;
                }
                );

            var s = "1";
            while (s != "0")
            {
                Console.WriteLine("Press 0 to stop");
                s = Console.ReadLine();
                var url = "https://localhost:5003/client";
                try
                {
                    var request = WebRequest.Create(url);
                    request.Method = "GET";
                    using var webResponse = (HttpWebResponse)request.GetResponse();
                    using var webStream = webResponse.GetResponseStream();
                    using var reader = new StreamReader(webStream);
                    var data = reader.ReadToEnd();
                    Console.WriteLine("Server Response with " + data);

                }
                catch
                {
                    Console.WriteLine("Server Response with 500");
                }
            }
        }
    }
}
