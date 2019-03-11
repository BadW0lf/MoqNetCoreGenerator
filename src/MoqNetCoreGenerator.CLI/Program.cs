using System;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace MoqNetCoreGenerator.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "Mock";

            app.HelpOption("-?|-h|--help");

            app.Command("mock", command => {
                command.Description = "Mock specified method";
                command.HelpOption("-?|-h|--help");
 
                var classOption = app.Option("-c|--class <value>",
                    "Class Name",
                    CommandOptionType.SingleValue);
            
                var methodOption = app.Option("-m|--method <value>",
                    "Method Name",
                    CommandOptionType.SingleValue);
                
                var outputOption = app.Option("-o|--output <value>",
                    "Output File path",
                    CommandOptionType.SingleValue);

                command.OnExecute(() => {
                    if (!classOption.HasValue() || !methodOption.HasValue() || !outputOption.HasValue())
                    {
                        app.ShowHelp();
                        return -1;
                    }

                    var serviceCollection = new ServiceCollection();
                    var serviceProvider = serviceCollection.BuildServiceProvider();
                    var processor = serviceProvider.GetRequiredService<SyntaxProcessor>();
                    
                    processor.Process();
                    
                    return 0;
                });
            });

            try
            {
                app.Execute(args);
            }
            catch (CommandParsingException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to execute application: {0}", e.Message);
            }
        }
        
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<SyntaxProcessor>();
        }
    }
}