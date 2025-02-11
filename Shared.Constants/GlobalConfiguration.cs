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
        /// Az algoritmus teszteket tároló fájl elérési útvonalát tartalmazó beállítás kulcsa.
        /// </summary>
        public const string AlgorithmTestsFilePath = "AlgorithmTestsFilePath";

        /// <summary>
        /// A teszteset generált bemenetének maximális méretét bájtban tartalmazó beállítás kulcsa.
        /// </summary>
        public const string ChunkSizeInBytes = "ChunkSizeInBytes";

        /// <summary>
        /// Az alapértelmezett kapcsolódási karakterlánc kulcsa.
        /// </summary>
        public const string DefaultConnection = "DefaultConnection";

        /// <summary>
        /// Az alkalmazás beállításainak fájlneve.
        /// </summary>
        public const string Json = "appsettings.json";

        /// <summary>
        /// A teszteset bemenetének maximális méretét bájtban tartalmazó beállítás kulcsa.
        /// </summary>
        public const string MaxSizeInBytes = "MaxSizeInBytes";

        /// <summary>
        /// A teszteset bemenetének beállításainak kulcsa.
        /// </summary>
        public const string TestCaseInput = "TestCaseInput";
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
            ?? throw new DirectoryNotFoundException(ErrorMessages.NotFound.SolutionPath);

        _configurationRoot = new ConfigurationBuilder()
            .SetBasePath(solutionPath)
            .AddJsonFile(AppSettings.Json, optional: false, reloadOnChange: true)
            .Build();
    }

    /// <summary>
    /// Az algoritmus teszteket tároló fájl elérési útvonala.
    /// </summary>
    public static string AlgorithmTestsFilePath => _configurationRoot[AppSettings.AlgorithmTestsFilePath]
        ?? throw new KeyNotFoundException(ErrorMessages.NotFound.AlgorithmTestsFilePath);

    /// <summary>
    /// Az alkalmazás globális konfigurációja.
    /// </summary>
    public static IConfiguration Configuration => _configurationRoot;

    /// <summary>
    /// Az alapértelmezett kapcsolódási karakterlánc.
    /// </summary>
    public static string DefaultConnection => _configurationRoot.GetConnectionString(AppSettings.DefaultConnection)
        ?? throw new KeyNotFoundException(ErrorMessages.NotFound.DefaultConnection);

    /// <summary>
    /// A teszteset generált bemenetének maximális mérete bájtban.
    /// </summary>
    public static int TestCaseInputChunkSize => int.Parse(TestCaseInputSection[AppSettings.ChunkSizeInBytes]
        ?? throw new KeyNotFoundException(ErrorMessages.NotFound.TestCaseInputChunkSizeInBytes));

    /// <summary>
    /// A teszteset bemenetének maximális mérete bájtban.
    /// </summary>
    public static int TestCaseInputMaxSize => int.Parse(TestCaseInputSection[AppSettings.MaxSizeInBytes]
        ?? throw new KeyNotFoundException(ErrorMessages.NotFound.TestCaseInputMaxSizeInBytes));

    /// <summary>
    /// A teszteset bemenetének beállításait tartalmazó szekció.
    /// </summary>
    private static IConfigurationSection TestCaseInputSection => _configurationRoot.GetSection(AppSettings.TestCaseInput);
}
