using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using static Common.Glb;

namespace CleanApi;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();            
        ApiBaseAddress = ConfigurationManager.AppSettings["ApiBaseAddress"];

        Application.Run(new FrmCleanApi());
    }
}