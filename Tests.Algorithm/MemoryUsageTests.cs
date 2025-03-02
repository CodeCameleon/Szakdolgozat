using Microsoft.Extensions.DependencyInjection;
using Shared.Algorithms.Interfaces;
using Shared.Constants;
using TestResults.Dtos;
using TestResults.Services.Interfaces;

namespace Tests.Algorithm;

/// <summary>
/// A titkosító algoritmusok memóriahasználatát vizsgáló tesztesetek.
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
    /// Az aktuális teszt futtatási környezetéhez tartozó szolgáltatás példányokat tároló adattag.
    /// </summary>
    private IServiceScope _serviceScope;

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _serviceScope = DatabaseSetUp.ServiceProvider.CreateScope();
        _memoryUsageResultService = _serviceScope.ServiceProvider.GetRequiredService<IMemoryUsageResultService>();
    }

    /// <summary>
    /// A teszteket lezáró függvény.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _serviceScope.Dispose();
    }

    /// <summary>
    /// Az összes teszteset memóriahasználatát vizsgáló teszt.
    /// </summary>
    /// <param name="algorithm">A tesztelendő algoritmus típusa.</param>
    /// <param name="testCase">A teszteset.</param>
    [Test]
    public async Task All([ValueSource(typeof(DatabaseSetUp), nameof(DatabaseSetUp.GetTestAlgorithms))] Type algorithm,
        [ValueSource(typeof(DatabaseSetUp), nameof(DatabaseSetUp.GetTestCases))] TestCaseDto testCase)
    {
        using ICryptographicAlgorithm algorithmInstance = Activator.CreateInstance(algorithm) as ICryptographicAlgorithm
            ?? throw new InvalidOperationException(ErrorMessages.Undefined.AlgorithmImplementation);

        string input = DatabaseSetUp.CreateInput(testCase.Input, testCase.Size);

        if (algorithmInstance is IEncryptionAlgorithm encryption)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long memoryBefore = GC.GetTotalMemory(true);

            string cipherText = encryption.Encrypt(input);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long memoryBetween = GC.GetTotalMemory(true);

            string plainText = encryption.Decrypt(cipherText);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long memoryAfter = GC.GetTotalMemory(true);

            long encryptionMemoryUsage = Math.Max(0, memoryBetween - memoryBefore);
            long decryptionMemoryUsage = Math.Max(0, memoryAfter - memoryBetween);

            await _memoryUsageResultService.CreateAsync(new MemoryUsageResultDto
            {
                AlgorithmName = algorithmInstance.AlgorithmName,
                AlgorithmType = algorithmInstance.AlgorithmType,
                TestCase = testCase,
                IsSuccessful = plainText.Equals(input),
                EncryptionMemoryUsage = encryptionMemoryUsage,
                DecryptionMemoryUsage = decryptionMemoryUsage
            });

            Assert.That(plainText, Is.EqualTo(input));
        }
        else if (algorithmInstance is IHashingAlgorithm hashing)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long memoryBefore = GC.GetTotalMemory(true);

            string hashedText = hashing.Hash(input);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long memoryAfter = GC.GetTotalMemory(true);

            long hashingMemoryUsage = Math.Max(0, memoryAfter - memoryBefore);

            await _memoryUsageResultService.CreateAsync(new MemoryUsageResultDto
            {
                AlgorithmName = algorithmInstance.AlgorithmName,
                AlgorithmType = algorithmInstance.AlgorithmType,
                TestCase = testCase,
                IsSuccessful = !hashedText.Equals(input),
                EncryptionMemoryUsage = hashingMemoryUsage,
                DecryptionMemoryUsage = 0
            });

            Assert.That(hashedText, Is.Not.EqualTo(input));
        }
    }
}
