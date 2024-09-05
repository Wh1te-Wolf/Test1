using Test1.Exceptions;
using Test1.Reporters.Interfaces;
using Test1.Services.Interfaces;

namespace Test1.Services
{
    public class ReportManager : IReportManager
    {
        private readonly IReportBuilder _reportBuilder;
        private readonly IReporter _reporter;

        public ReportManager(IReportBuilder reportBuilder, IReporter reporter)
        {
            _reportBuilder = reportBuilder;
            _reporter = reporter;
        }

        public async Task BuildReportAsync(int id)
        {
            try
            {
                byte[] buildReportTask = await _reportBuilder.BuildAsync(id);
                await _reporter.ReportSuccessAsync(buildReportTask, id);
            }
            catch (BuildReportException)
            {
                await _reporter.ReportErrorAsync(id);
            }
            catch(TimeoutException)
            {
                await _reporter.ReportTimeoutAsync(id);
            }
        }
    }
}
