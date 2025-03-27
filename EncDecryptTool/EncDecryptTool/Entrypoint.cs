using Jargoon.Data.IHO;
using Jargoon.Data.S57;

namespace EncDecryptTool;

public class Entrypoint(IEncDecryptorWrapper encDecryptor) : IEntrypoint
{
    public Task RunAsync(string[] args)
    {
        var currentDirectory = "D:\\other\\Hackathon2025ENC\\EncDecryptTool\\resources";
        // var encFileLocation = new FileInfo(Path.Combine(currentDirectory, args[0]));
        // var encryptionKey = args[1];
        
        var encFileLocation = new FileInfo("D:\\other\\Hackathon2025ENC\\EncDecryptTool\\resources\\GB40623A.000");
        var encryptionKey = "79FBAEE9FA";
        
        var unencryptedEncFromFileLocation = encDecryptor.DecryptFile(encFileLocation, encryptionKey);

        if (!encDecryptor.IsEncrypted(unencryptedEncFromFileLocation))
            Console.WriteLine($"File {encFileLocation} has been decrypted");

        var decryptedFileLocation = new FileInfo(Path.Combine(currentDirectory, $"Decrypted_{encFileLocation.Name}"));
        File.WriteAllBytes(decryptedFileLocation.FullName, unencryptedEncFromFileLocation);

        //----------
        
        var stream = File.OpenRead(decryptedFileLocation.FullName);
        
        var decoder = new S57Decoder();
        
        stream.Seek(0, SeekOrigin.Begin);
        decoder.Decode(stream);

        var iho = new IHOENCDataModel();
        
        iho.CreateModel(decoder.S57DataSet);

        var features = iho.FeatureLookup
            .Select(feature => feature.Key)
            .ToList();
        
        //----------
        
        return Task.CompletedTask;
    }
}

public interface IEntrypoint
{
    Task RunAsync(string[] args);
}