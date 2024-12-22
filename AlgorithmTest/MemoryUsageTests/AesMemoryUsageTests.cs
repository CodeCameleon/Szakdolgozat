using AlgorithmTest.Models;

namespace AlgorithmTest.MemoryUsageTests;

/// <summary>
/// Az AES titkosító algoritmus memória használatát vizsgáló tesztesetek.
/// </summary>
[TestFixture]
[NonParallelizable]
internal class AesMemoryUsageTests
    : BaseMemoryUsage
{
    /// <summary>
    /// Az AES titkosító algoritmust tároló adattag.
    /// </summary>
    private AESAlgorithm _aes;

    /// <inheritdoc />
    [SetUp]
    public override void SetUp()
    {
        _aes = new();

        base.SetUp();
    }

    /// <inheritdoc />
    [TearDown]
    public override void TearDown()
    {
        _aes.Dispose();

        base.TearDown();
    }

    /// <summary>
    /// Az egyszerű tesztesetek memória használatát vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.SimpleTestCases))]
    public void SimpleMemoryUsage(string input)
    {
        MemoryUsage(_aes, input);
    }
}
