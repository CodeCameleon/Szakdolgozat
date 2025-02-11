using NUnit.Engine;
using Shared.Constants;
using System.Xml;
using Thesis.WebApp.Constants;
using Thesis.WebApp.Services.Interfaces;
using Thesis.WebApp.ViewModels;

namespace Thesis.WebApp.Services.Implementations;

/// <summary>
/// Az NUnit teszteket futtató szolgáltatást megvalósító osztály.
/// </summary>
public class TestRunnerService
    : ITestRunnerService
{
    /// <summary>
    /// Az algoritmusok NUnit tesztfuttatóját tároló adattag.
    /// </summary>
    private readonly ITestRunner _algorithmRunner;

    /// <summary>
    /// Az NUnit tesztmotort tároló adattag.
    /// </summary>
    private readonly ITestEngine _engine;
    
    /// <summary>
    /// A szolgáltatás alapértelmezett konstruktora.
    /// </summary>
    public TestRunnerService()
    {
        _engine = TestEngineActivator.CreateInstance();
        TestPackage algorithmPackage = new(GlobalConfiguration.AlgorithmTestsFilePath);
        _algorithmRunner = _engine.GetRunner(algorithmPackage);
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        _algorithmRunner.Dispose();
        _engine.Dispose();

        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    public TestSummaryViewModel RunAlgorithmMemoryUsageTests()
    {
        TestFilter filter = new(XmlBuilder.Filter(XmlBuilder.Class(TestNames.AlgorithmMemoryUsage)));
        XmlNode result = _algorithmRunner.Run(null, filter);
        return AsViewModel(result);
    }

    /// <inheritdoc />
    public TestSummaryViewModel RunAlgorithmRunTimeTests()
    {
        TestFilter filter = new(XmlBuilder.Filter(XmlBuilder.Class(TestNames.AlgorithmRunTime)));
        XmlNode result = _algorithmRunner.Run(null, filter);
        return AsViewModel(result);
    }

    /// <inheritdoc />
    public TestSummaryViewModel RunAlgorithmTests()
    {
        XmlNode result = _algorithmRunner.Run(null, TestFilter.Empty);
        return AsViewModel(result);
    }

    /// <summary>
    /// Átalakítja a tesztek eredményét nézetmodell formátumba.
    /// </summary>
    /// <param name="xmlNode">A tesztek eredménye XML formátumban.</param>
    /// <returns>A tesztek eredménye nézetmodell formátumban.</returns>
    private static TestSummaryViewModel AsViewModel(XmlNode xmlNode)
    {
        ArgumentNullException.ThrowIfNull(xmlNode.Attributes, nameof(xmlNode.Attributes));

        return new TestSummaryViewModel()
        {
            TotalTests = int.Parse(xmlNode.Attributes[XmlBuilder.Attributes.Total]?.Value
                ?? throw new NullReferenceException(ErrorMessages.XmlAttributeNotFound)
            ),
            PassedTests = int.Parse(xmlNode.Attributes[XmlBuilder.Attributes.Passed]?.Value
                ?? throw new NullReferenceException(ErrorMessages.XmlAttributeNotFound)
            ),
            FailedTests = int.Parse(xmlNode.Attributes[XmlBuilder.Attributes.Failed]?.Value
                ?? throw new NullReferenceException(ErrorMessages.XmlAttributeNotFound)
            )
        };
    }
}
