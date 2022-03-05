using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Retry;
using PollyClientASP.Policies;
using System;
using System.Net;
using System.Net.Http;

namespace PollyClientASP.Polices
{
    public class Politics : IPolicy
    {
        public AsyncRetryPolicy InternalServerErrorPolicy { get; set; }
        public Politics()
        {
            InternalServerErrorPolicy = Policy
                 .Handle<Exception>()
                 .WaitAndRetryAsync(10, retryAttempt => {
                     var timeToWait = TimeSpan.FromSeconds(1);
                     Console.WriteLine($"Waiting {timeToWait.TotalSeconds} seconds");
                     return timeToWait;
                 }
                 );
        }
    }
}
