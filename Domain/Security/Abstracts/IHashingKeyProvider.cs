namespace Domain.Security
{
    public interface IEncryptionKeyProvider 
    {
        string PublicKey { get; }

        string PrivateKey { get; }

    }
}
