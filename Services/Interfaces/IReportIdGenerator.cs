namespace Test1.Services.Interfaces
{
    public interface IReportIdGenerator
    {
        int GenerateReportId();

        void RemoveReportId(int reportId);
    }
}
