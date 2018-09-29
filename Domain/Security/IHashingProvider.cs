namespace Domain.Security
{
    public interface IHashProvider
    {
        byte[] GenerateSalt();

        byte[] GenerateHash(string text, byte[] salt);
    }
}
