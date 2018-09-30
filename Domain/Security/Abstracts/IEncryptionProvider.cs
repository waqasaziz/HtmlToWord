namespace Domain.Security
{
    public interface IEncryptionProvider
    {
        string Decrypt(string cipherText);

        string Encrypt(string text);
    }
}
