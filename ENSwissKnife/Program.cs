using Jargoon.Data.IHO;
using Jargoon.Data.S57;

// read the decrypted ENC
//var ENCFileAsString = File.ReadAllText("Decrypted_GB40623A.000");

// decoder takes a filestream as input
var ENCFileStream = File.OpenRead("Decrypted_GB40623A.000");
//var ENCFileStream = File.OpenRead("GBL0623A.000");    // are we sure this is a S57 ENC? 
ENCFileStream.Seek(0, SeekOrigin.Begin);

var s57Decoder = new S57Decoder();
s57Decoder.Decode(ENCFileStream);

// DEBUG here - to inspect the decoded object

var s57ENCDataSet = s57Decoder.S57DataSet;
// can 

var s57DataSetGeneralInformationRecord = s57ENCDataSet.DataSetGeneralInformationRecord;

// TODO - explore how to list data
IHOENCDataModel iHOENCDataModel = new IHOENCDataModel();
iHOENCDataModel.CreateModel(s57ENCDataSet);


Console.ReadLine();
