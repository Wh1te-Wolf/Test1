using System.Text;
using Test1.Exceptions;
using Test1.Reporters.Interfaces;

namespace Test1.Reporters;

public class ReportBuilder : IReportBuilder
{
    private const int _timeout = 30; // в секундах
    private readonly Dictionary<int, CancellationTokenSource> _cancellationTokenSources = new ();

    public async Task<byte[]> BuildAsync(int reportId)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        _cancellationTokenSources.Add(reportId, cancellationTokenSource);

        CancellationToken cancellationToken = cancellationTokenSource.Token;

        Random rnd = new Random();
        int buildTime = rnd.Next(5, 46);

        try
        {
            for (int i = 0; i < buildTime; i++)
            {
                if (i == 3 && rnd.NextDouble() < 0.2)
                {
                    throw new BuildReportException("Report failed");
                }

                if (i > _timeout)
                {
                    throw new TimeoutException("Build cancelled or timed out.");
                }

                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(1000, cancellationToken);
            }

            string message = $"Report built in {buildTime} s.";
            return Encoding.UTF8.GetBytes(message);
        }
        catch (OperationCanceledException)
        {
            _cancellationTokenSources.Remove(reportId);
            throw new BuildReportException("Report failed");
        }
        catch
        {
            _cancellationTokenSources.Remove(reportId);
            throw;
        }
    }

    public void Stop(int reportId)
    {
        if (_cancellationTokenSources.TryGetValue(reportId, out CancellationTokenSource cts))
        { 
            cts.Cancel();
            _cancellationTokenSources.Remove(reportId);
        }
    }
}
