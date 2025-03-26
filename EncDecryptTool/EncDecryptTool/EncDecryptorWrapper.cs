using System.IO.Abstractions;
using UKHO.Torus.Crypto;

namespace EncDecryptTool;

public class EncDecryptorWrapper : IEncDecryptorWrapper
{
    private readonly IEncDecryptor _encDecryptor = new EncEncryptionManager();
    
    public byte[] DecryptFile(FileInfoBase fileLocation, string key) => _encDecryptor.DecryptFile(fileLocation, key);
    public bool IsEncrypted(byte[] file) => CryptoExtensions.BytesAreEncrypted(file);
}