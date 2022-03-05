using Polly.Retry;
using System.Net.Http;

namespace PollyClientASP.Policies
{
    public interface IPolicy
    {
        public AsyncRetryPolicy InternalServerErrorPolicy { get; set; }
    }
}
