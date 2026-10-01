using System.Security.Cryptography;
using TalepYonetimi.Application.Interfaces;
using TalepYonetimi.Application.Security;

namespace TalepYonetimi.Application.Services;

public sealed class Pbkdf2SifreHasher : ISifreHasher
{
    private const int IterationCount = 210_000;
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const string AlgorithmName = "PBKDF2-SHA256";

    public string Hash(string sifre)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sifre);

        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            sifre,
            salt,
            IterationCount,
            HashAlgorithmName.SHA256,
            HashSize);

        return string.Join(
            '$',
            AlgorithmName,
            IterationCount,
            Convert.ToBase64String(salt),
            Convert.ToBase64String(hash));
    }

    public bool Verify(string sifre, string sifreHash)
    {
        if (string.IsNullOrEmpty(sifre) || string.IsNullOrEmpty(sifreHash))
        {
            return false;
        }

        var parts = sifreHash.Split('$');
        if (parts.Length != 4 ||
            parts[0] != AlgorithmName ||
            !int.TryParse(parts[1], out var iterations))
        {
            return false;
        }

        try
        {
            var salt = Convert.FromBase64String(parts[2]);
            var expectedHash = Convert.FromBase64String(parts[3]);
            var actualHash = Rfc2898DeriveBytes.Pbkdf2(
                sifre,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}