namespace Shared.Constants;

/// <summary>
/// Az alkalmazás beállításainak kulcsait tartalmazó statikus osztály.
/// </summary>
public static class AppSettings
{
    /// <summary>
    /// Az alkalmazás beállításainak elérési útvonala.
    /// </summary>
    public static string BasePath => GetBasePath();

    /// <summary>
    /// Az alkalmazás fejlesztési beállításainak fájlneve.
    /// </summary>
    public static string DevelopmentJson => "appsettings.Development.json";

    /// <summary>
    /// Az alapértelmezett kapcsolódási karakterlánc kulcsa.
    /// </summary>
    public static string DefaultConnection => "DefaultConnection";

    /// <summary>
    /// Lekéri az alkalmazás beállításainak elérési útvonalát.
    /// </summary>
    /// <returns>Az alkalmazás beállításainak elérési útvonala.</returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    private static string GetBasePath()
    {
        string? currentDirectory = Directory.GetCurrentDirectory();

        string solutionPath = Directory.GetParent(currentDirectory)?.FullName
            ?? throw new DirectoryNotFoundException(ErrorMessages.SolutionPathNotFound);

        return Path.Combine(solutionPath, "Thesis.WebApp");
    }
}
