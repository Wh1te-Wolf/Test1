namespace Test1.Reporters.Interfaces;

public interface IReportBuilder
{
    Task<byte[]> BuildAsync(int reportId);
    void Stop(int reportId);
}
