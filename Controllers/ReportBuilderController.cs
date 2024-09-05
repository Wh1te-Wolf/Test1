using Microsoft.AspNetCore.Mvc;
using Test1.DTO;
using Test1.Reporters.Interfaces;
using Test1.Services.Interfaces;

namespace Test1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportBuilderController : ControllerBase
{
    private readonly IReportBuilder _reportBuilder;
    private readonly IReportManager _reportManager;
    private readonly IReportIdGenerator _reportIdGenerator;

    public ReportBuilderController(IReportBuilder reportBuilder, IReportManager reportManager, IReportIdGenerator reportIdGenerator)
    {
        _reportBuilder = reportBuilder;
        _reportManager = reportManager;
        _reportIdGenerator = reportIdGenerator;
    }

    [HttpGet(nameof(Build))]
    public IActionResult Build()
    { 
        int reportId = _reportIdGenerator.GenerateReportId();
        Task.Run(() => _reportManager.BuildReportAsync(reportId));
        return Ok(reportId);
    }

    [HttpPost(nameof(Stop))]
    public IActionResult Stop(StopBuildRequest stopBuildRequest) 
    {
        _reportBuilder.Stop(stopBuildRequest.ReportId);
        return Ok();
    }
}
