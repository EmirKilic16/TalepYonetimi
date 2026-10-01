namespace TalepYonetimi.Application.Security;

public interface ISifreHasher
{
    string Hash(string sifre);
    bool Verify(string sifre, string sifreHash);
}