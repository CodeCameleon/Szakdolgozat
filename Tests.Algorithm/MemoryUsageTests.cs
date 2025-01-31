using Microsoft.Extensions.DependencyInjection;
using Shared.Algorithms.Interfaces;
using TestResults.Dtos;
using TestResults.Services.Interfaces;

namespace Tests.Algorithm;

/// <summary>
/// A titkosító algoritmusok memória használatát vizsgáló tesztesetek.
/// </summary>
[TestFixture]
[NonParallelizable]
internal class MemoryUsageTests
{
    /// <summary>
    /// A memóriahasználat eredményeket kezelő szolgáltatást tároló adattag.
    /// </summary>
    private IMemoryUsageResultService _memoryUsageResultService;

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _memoryUsageResultService = DatabaseSetUp.ServiceProvider.GetRequiredService<IMemoryUsageResultService>();
    }

    /// <summary>
    /// A teszteket lezáró függvény.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _memoryUsageResultService.Dispose();
    }

    /// <summary>
    /// Az összes teszteset memória használatát vizsgáló teszt.
    /// </summary>
    /// <param name="algorithm">A tesztelendő algoritmus.</param>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test]
    public void All([ValueSource(typeof(DatabaseSetUp), nameof(DatabaseSetUp.TestAlgorithms))] IEncryptionAlgorithm algorithm,
        [ValueSource(typeof(DatabaseSetUp), nameof(DatabaseSetUp.TestInputs))] string input)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long memoryBefore = GC.GetTotalMemory(true);

        string cipherText = algorithm.Encrypt(input);

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long memoryBetween = GC.GetTotalMemory(true);

        string plainText = algorithm.Decrypt(cipherText);

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long memoryAfter = GC.GetTotalMemory(true);

        long encryptionMemoryUsage = Math.Max(0, memoryBetween - memoryBefore);
        long decryptionMemoryUsage = Math.Max(0, memoryAfter - memoryBetween);

        _memoryUsageResultService.CreateAsync(new MemoryUsageResultDto
        {
            AlgorithmName = algorithm.AlgorithmName,
            AlgorithmType = algorithm.AlgorithmType,
            Input = input,
            IsSuccessful = plainText.Equals(input),
            EncryptionMemoryUsage = encryptionMemoryUsage,
            DecryptionMemoryUsage = decryptionMemoryUsage
        });

        Assert.That(plainText, Is.EqualTo(input));
    }
}
