using System.Diagnostics;
using Polly;
using Polly.Retry;
using RestSharp;

public class Program
{
    
    private static RestClient restClient = new RestClient("http://cache-service/Cache");
    
    private static readonly AsyncRetryPolicy<int?> retryPolicy = Policy<int?>
        .Handle<Exception>()
        .RetryAsync(3, (exception, retryCount) =>
        {
            Console.WriteLine($"Retry {retryCount} due to {exception.Exception.Message}");
        });
    
    public static async Task Main()
    {

        var first = 1_000_000_000;
        var last = 1_000_000_020;

        var numberWithMostDivisors = first;
        var result = 0;
        
        var watch = Stopwatch.StartNew();
        for (var i = first; i <= last; i++)
        {
            var innerWatch = Stopwatch.StartNew();
            var divisorCounter = CountDivisors(i);//await GetDivisorCount(i);

            restClient.PostAsync(new RestRequest($"/cache?number={i}&divisorCounter={divisorCounter}"));
         
            
            innerWatch.Stop();
            Console.WriteLine("Counted " + divisorCounter + " divisors for " + i + " in " + innerWatch.ElapsedMilliseconds + "ms");

            if (divisorCounter > result)
            {
                numberWithMostDivisors = i;
                result = divisorCounter;
            }
        }
        watch.Stop();
        
        Console.WriteLine("The number with most divisors inside range is: " + numberWithMostDivisors + " with " + result + " divisors.");
        Console.WriteLine("Elapsed time: " + watch.ElapsedMilliseconds + "ms");
        Console.ReadLine();
    }
    
    // Thats wooorks
    private static int CountDivisors(long number)
    {
        var task = restClient.GetAsync<int>(new RestRequest("/cache?number=" + number));
        
        var divisorCounter = 0;
        {
            for (var divisor = 1; divisor <= number; divisor++)
            {
                if (task?.Status == TaskStatus.RanToCompletion)
                {
                    var cachedResult = task.Result;
                    if (cachedResult != 0)
                    {
                        return cachedResult;
                    }

                    task = null;
                }

                if (number % divisor == 0)
                {
                        divisorCounter++; 
                }
            }
            return divisorCounter;
            
        }
    } 
    
   
}