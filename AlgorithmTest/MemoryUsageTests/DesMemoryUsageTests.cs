using AlgorithmTest.Models;

namespace AlgorithmTest.MemoryUsageTests;

/// <summary>
/// A DES titkosító algoritmus memória használatát vizsgáló tesztesetek.
/// </summary>
[TestFixture]
[NonParallelizable]
internal class DesMemoryUsageTests
    : BaseMemoryUsage
{
    /// <summary>
    /// A DES titkosító algoritmust tároló adattag.
    /// </summary>
    private DESAlgorithm _des;

    /// <inheritdoc />
    [SetUp]
    public override void SetUp()
    {
        _des = new();

        base.SetUp();
    }

    /// <inheritdoc />
    [TearDown]
    public override void TearDown()
    {
        _des.Dispose();

        base.TearDown();
    }

    /// <summary>
    /// Az egyszerű tesztesetek memória használatát vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.SimpleTestCases))]
    public void SimpleMemoryUsage(string input)
    {
        MemoryUsage(_des, input);
    }
}
