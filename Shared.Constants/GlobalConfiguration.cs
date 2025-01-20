using Microsoft.Extensions.Configuration;

namespace Shared.Constants;

/// <summary>
/// Az alkalmazás globális konfigurációját tartalmazó statikus osztály.
/// </summary>
public static class GlobalConfiguration
{
    /// <summary>
    /// Az alkalmazás beállításainak kulcsait tartalmazó statikus belső osztály.
    /// </summary>
    private static class AppSettings
    {
        /// <summary>
        /// Az alapértelmezett kapcsolódási karakterlánc kulcsa.
        /// </summary>
        public static string DefaultConnection => "DefaultConnection";

        /// <summary>
        /// Az alkalmazás beállításainak fájlneve.
        /// </summary>
        public static string Json => "appsettings.json";
    }

    /// <summary>
    /// Az alkalmazás globális konfigurációját tároló adattag.
    /// </summary>
    private static readonly IConfigurationRoot _configurationRoot;

    /// <summary>
    /// Az alkalmazás globális konfigurációjának statikus konstruktora.
    /// </summary>
    static GlobalConfiguration()
    {
        string? currentDirectory = Directory.GetCurrentDirectory();

        string solutionPath = Directory.GetParent(currentDirectory)?.FullName
            ?? throw new DirectoryNotFoundException(ErrorMessages.SolutionPathNotFound);

        _configurationRoot = new ConfigurationBuilder()
            .SetBasePath(solutionPath)
            .AddJsonFile(AppSettings.Json, optional: false, reloadOnChange: true)
            .Build();
    }

    /// <summary>
    /// Az alapértelmezett kapcsolódási karakterlánc.
    /// </summary>
    public static string DefaultConnection => _configurationRoot.GetConnectionString(AppSettings.DefaultConnection)
        ?? throw new KeyNotFoundException(ErrorMessages.DefaultConnectionNotFound);
}
