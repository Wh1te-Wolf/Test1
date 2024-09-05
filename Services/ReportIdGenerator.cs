using Test1.Services.Interfaces;

namespace Test1.Services
{
    public class ReportIdGenerator : IReportIdGenerator
    {
        private readonly List<int> _ids = new List<int>();

        public int GenerateReportId()
        {
            int id = 0;

            if (!_ids.Any())
                id = 1;

            else
                id = _ids.Max() + 1;

            _ids.Add(id);
            return id;
        }

        public void RemoveReportId(int reportId)
        {
            _ids.Remove(reportId);
        }
    }
}
