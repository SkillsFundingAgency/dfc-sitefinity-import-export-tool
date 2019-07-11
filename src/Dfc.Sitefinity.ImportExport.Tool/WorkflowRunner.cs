using System;
using CommandLine;
using Dfc.Sitefinity.ImportExport.Tool.Models;
using Dfc.Sitefinity.ImportExport.Tool.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dfc.Sitefinity.ImportExport.Tool
{
    public class WorkflowRunner
    {

        [Verb("run-workflow", HelpText = "Pushes the content to site finity.")]
        public class Options
        {
            [Option('f', "workflowFile", Required = true,
                HelpText = "The file containing the sitefinity workflow to be executed.")]
            public string WorkflowFile { get; set; }

        }


        public static SuccessFailCode Execute(IServiceProvider services, Options opts)
        {
            var logger = services.GetService<ILogger<WorkflowRunner>>();

            try
            {
                var configuration = services.GetService<IConfiguration>();
                configuration.GetSection("AppSettings").Bind(opts);

                var siteFinityService = services.GetService<ISiteFinityHttpService>();

                SitefinityActionExecutor.RunWorkflowFromFile(siteFinityService, logger, opts.WorkflowFile).GetAwaiter()
                    .GetResult();

                return SuccessFailCode.Succeed;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured loading cms content");
                return SuccessFailCode.Fail;
            }
        }
    }
}