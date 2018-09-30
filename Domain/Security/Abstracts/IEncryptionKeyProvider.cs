namespace Domain.Security
{
    public interface IHashProvider
    {
        byte[] GenerateSalt();

        byte[] GenerateSHA256Hash(string text, byte[] salt);

        byte[] GeneratePBKDF2Hash(string password, byte[] salt);
    }
}
