using Polly;
using Polly.Retry;

namespace Client.Infrastructure;

public class ClientPolicy
{
    public AsyncRetryPolicy<HttpResponseMessage> ImmediateRetryPolicy { get; }
    public AsyncRetryPolicy<HttpResponseMessage> WaitRetryPolicy { get; }
    public AsyncRetryPolicy<HttpResponseMessage> WaitRetryLogPolicy { get; }


    public ClientPolicy()
    {
        ImmediateRetryPolicy = Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .RetryAsync(8);
        WaitRetryPolicy = Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .WaitAndRetryAsync(8, i => TimeSpan.FromSeconds(2));
        WaitRetryLogPolicy = Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .WaitAndRetryAsync(8, i => TimeSpan.FromSeconds(2 * i));
    }
}