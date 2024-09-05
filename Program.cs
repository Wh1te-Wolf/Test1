
using Test1.Reporters;
using Test1.Reporters.Interfaces;
using Test1.Services;
using Test1.Services.Interfaces;

namespace Test1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IReportBuilder, ReportBuilder>();
            builder.Services.AddSingleton<IReporter, Reporter>();
            builder.Services.AddSingleton<IReportManager, ReportManager>();
            builder.Services.AddSingleton<IReportIdGenerator, ReportIdGenerator>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
