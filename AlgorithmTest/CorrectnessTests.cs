using AlgorithmTest.Helpers;
using AlgorithmTest.Models;
using MathCrypt.Enums;
using MathCrypt.Services;
using System.Diagnostics;

namespace AlgorithmTest;

/// <summary>
/// A titkosító algoritmusok pontosságát vizsgáló tesztesetek.
/// </summary>
[TestFixture]
internal class CorrectnessTests
{
    /// <summary>
    /// Az értékek, amikel az osztály a teszteket végzni.
    /// </summary>
    private static readonly object[] TestCases =
    [
        new object[] { "Alma" },
        new object[] { "Bannán" },
        new object[] { "Narancs" },
        new object[] { "Ez már kicsit nehezebb!" },
        new object[] { "4-5-1 és még 4 talán 2 is?" }
    ];

    /// <summary>
    /// A MathCrypt titkosító algoritmus tároló adattag.
    /// </summary>
    private MathCryptAlgorithm _mathCrpyt;

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [OneTimeSetUp]
    public void SetUp()
    {
        _mathCrpyt = new(KeyGenService.Instance
            .GenerateKey(
                strength: 2,
                ECharset.Space,
                ECharset.Numbers,
                ECharset.MathSymbols,
                ECharset.Punctuations,
                ECharset.EN,
                ECharset.HU
            )
        );
    }

    /// <summary>
    /// A MathCrypt titkosító algoritmus pontosságát vizsgáló teszt.
    /// </summary>
    /// <param name="input">A szöveg, amivel a teszt dolgozik.</param>
    [Test, TestCaseSource(nameof(TestCases))]
    public void MathCryptCorrectness(string input)
    {
        (string cipherText, Stopwatch encryptTime) = _mathCrpyt.Encrypt(input);
        TestContext.WriteLine(StringHelper.TimeToEncrypt(encryptTime));
        
        (string plainText, Stopwatch decryptTime) = _mathCrpyt.Decrypt(cipherText);
        TestContext.WriteLine(StringHelper.TimeToDecrypt(decryptTime));

        Assert.That(plainText, Is.EqualTo(input), StringHelper.Error.Correctness(_mathCrpyt));
    }
}
