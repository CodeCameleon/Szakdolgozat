using AlgorithmTest.Models;
using System.Diagnostics;

namespace AlgorithmTest.Helpers;

/// <summary>
/// A szövegműveleteket segítő statikus osztály.
/// </summary>
internal static class StringHelper
{
    /// <summary>
    /// A hibaüzeneteket tartalmazó belső osztály.
    /// </summary>
    public static class Error
    {
        /// <summary>
        /// A pontossági teszt sikertelensége esetén dobandó hibaüzenet.
        /// </summary>
        /// <param name="algorithm">Az algoritmus, amely megbukott a teszten.</param>
        /// <returns>A hibaüzenet.</returns>
        public static string Correctness(IAlgorithm algorithm)
        {
            return $"A {nameof(algorithm)} megbukott a pontosségi teszten.";
        }
    }

    /// <summary>
    /// A titkosításhoz szükséges időt kiíró üzenet.
    /// </summary>
    /// <param name="stopwatch">Az időt tartalmazó objektum.</param>
    /// <returns>Az üzenet.</returns>
    public static string TimeToEncrypt(Stopwatch stopwatch)
    {
        return $"A titkosítás lefutott {stopwatch.Elapsed} ms alatt.";
    }

    /// <summary>
    /// A visszafejtéshez szükséges időt kiíró üzenet.
    /// </summary>
    /// <param name="stopwatch">Az időt tartalmazó objektum.</param>
    /// <returns>Az üzenet.</returns>
    public static string TimeToDecrypt(Stopwatch stopwatch)
    {
        return $"A visszafejtés lefutott {stopwatch.Elapsed} ms alatt.";
    }
}
