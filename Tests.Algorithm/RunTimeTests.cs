﻿using Microsoft.Extensions.DependencyInjection;
using Shared.Algorithms.Interfaces;
using Shared.Constants;
using System.Diagnostics;
using TestResults.Dtos;
using TestResults.Services.Interfaces;

namespace Tests.Algorithm;

/// <summary>
/// A titkosító algoritmusok futási idejét vizsgáló tesztesetek.
/// </summary>
[TestFixture]
[NonParallelizable]
internal class RunTimeTests
{
    /// <summary>
    /// A futási idő eredményeket kezelő szolgáltatást tároló adattag.
    /// </summary>
    private IRunTimeResultService _runTimeResultService;

    /// <summary>
    /// Az aktuális teszt futtatási környezetéhez tartozó szolgáltatás példányokat tároló adattag.
    /// </summary>
    private IServiceScope _serviceScope;

    /// <summary>
    /// A futási idő mérésére szolgáló osztályt tároló adattag.
    /// </summary>
    private Stopwatch _stopwatch;

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _serviceScope = DatabaseSetUp.ServiceProvider.CreateScope();
        _runTimeResultService = _serviceScope.ServiceProvider.GetRequiredService<IRunTimeResultService>();
        _stopwatch = new();
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
    /// Az összes teszteset futási idejét vizsgáló teszt.
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
            _stopwatch.Restart();
            string cipherText = encryption.Encrypt(input);
            _stopwatch.Stop();

            TimeSpan timeToEncrypt = _stopwatch.Elapsed;

            _stopwatch.Restart();
            string plainText = encryption.Decrypt(cipherText);
            _stopwatch.Stop();

            TimeSpan timeToDecrypt = _stopwatch.Elapsed;

            await _runTimeResultService.CreateAsync(new RunTimeResultDto
            {
                AlgorithmName = algorithmInstance.AlgorithmName,
                AlgorithmType = algorithmInstance.AlgorithmType,
                TestCase = testCase,
                IsSuccessful = plainText.Equals(input),
                TimeToEncrypt = timeToEncrypt.TotalMilliseconds,
                TimeToDecrypt = timeToDecrypt.TotalMilliseconds
            });

            Assert.That(plainText, Is.EqualTo(input));
        }
        else if (algorithmInstance is IHashingAlgorithm hashing)
        {
            _stopwatch.Restart();
            string hashedText = hashing.Hash(input);
            _stopwatch.Stop();

            TimeSpan timeToHash = _stopwatch.Elapsed;

            await _runTimeResultService.CreateAsync(new RunTimeResultDto
            {
                AlgorithmName = algorithmInstance.AlgorithmName,
                AlgorithmType = algorithmInstance.AlgorithmType,
                TestCase = testCase,
                IsSuccessful = !hashedText.Equals(input),
                TimeToEncrypt = timeToHash.TotalMilliseconds,
                TimeToDecrypt = 0
            });

            Assert.That(hashedText, Is.Not.EqualTo(input));
        }
    }
}
