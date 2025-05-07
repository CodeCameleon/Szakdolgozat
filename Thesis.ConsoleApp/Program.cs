using Thesis.MathCrypt.Implementations;

MathCryptKeyGenerator keyGenerator = new();
MathCrypt mathCrypt = new(keyGenerator.GenerateKey(strength: 2));

Console.WriteLine(mathCrypt.ToString());
