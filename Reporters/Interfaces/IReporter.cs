namespace Test1.Reporters.Interfaces
{
    public interface IReporter
    {
        Task ReportSuccessAsync(byte[] data, int id);

        Task ReportErrorAsync(int id);

        Task ReportTimeoutAsync(int id);
    }
}
