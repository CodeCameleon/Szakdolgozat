namespace AlgorithmTest.Models;

/// <summary>
/// Az DES titkosító algoritmust megvalósító osztály.
/// </summary>
internal class DESAlgorithm
    : IAlgorithm
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private readonly DES _des;

    /// <summary>
    /// A futási idő mérésére szolgáló osztályt tároló adattag.
    /// </summary>
    private readonly Stopwatch _stopwatch;

    /// <summary>
    /// Az osztály konstruktora.
    /// </summary>
    public DESAlgorithm()
    {
        _des = DES.Create();
        _des.GenerateKey();
        _des.GenerateIV();
        _stopwatch = new Stopwatch();
    }

    /// <summary>
    /// Az osztály paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A kulcs, amelyet a titkosító algoritmus használ.</param>
    /// <param name="iv">Az inicializáló vektor.</param>
    public DESAlgorithm(byte[] key, byte[] iv)
    {
        _des = DES.Create();
        _des.Key = key;
        _des.IV = iv;
        _stopwatch = new Stopwatch();
    }

    /// <summary>
    /// Az osztály destruktora.
    /// </summary>
    ~DESAlgorithm()
    {
        _des.Dispose();
    }

    /// <summary>
    /// Titkosítja a megadott szöveget.
    /// </summary>
    /// <param name="plainText">A titkosítandó szöveg.</param>
    /// <returns>A titkosított szöveg és a futási idő.</returns>
    public (string CipherText, TimeSpan TimeToRun) Encrypt(string plainText)
    {
        _stopwatch.Restart();
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, _des.CreateEncryptor(), CryptoStreamMode.Write);
        using StreamWriter sw = new(cs);

        sw.Write(plainText);
        sw.Flush();
        cs.FlushFinalBlock();

        string cipherText = Convert.ToBase64String(ms.ToArray());
        _stopwatch.Stop();

        return (cipherText, _stopwatch.Elapsed);
    }

    /// <summary>
    /// Visszafejti a megadott szöveget.
    /// </summary>
    /// <param name="cipherText">A visszafejtendő szöveg.</param>
    /// <returns>A visszafejtett szöveg és a futási idő.</returns>
    public (string PlainText, TimeSpan TimeToRun) Decrypt(string cipherText)
    {
        _stopwatch.Restart();
        using MemoryStream ms = new(Convert.FromBase64String(cipherText));
        using CryptoStream cs = new(ms, _des.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader sr = new(cs);

        string plainText = sr.ReadToEnd();
        _stopwatch.Stop();

        return (plainText, _stopwatch.Elapsed);
    }
}
