using System.IO.Abstractions;

namespace EncDecryptTool;

public interface IEncDecryptorWrapper
{
    byte[] DecryptFile(FileInfoBase fileLocation, string key);
    bool IsEncrypted(byte[] file);
}