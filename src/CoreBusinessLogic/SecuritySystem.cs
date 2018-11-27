using CoreBusiness.Data;
using CoreBusiness.Models;
using CoreDataAccess;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace CoreBusinessLogic
{
    public class SecuritySystem
    {
        static int numberOfRows = Convert.ToInt32(ConfigurationManager.AppSettings["GridRows"]);
        static int numberOfColumns = Convert.ToInt32(ConfigurationManager.AppSettings["GridColumns"]);
        static int challengeSize = Convert.ToInt32(ConfigurationManager.AppSettings["ChallengeSize"]);
        static string FilePath = ConfigurationManager.AppSettings["FilePath"];
        static int[] charInt = new int[91];
        static char[] alphabetArray = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        static Random random = null;
        public static string[,] GenerateGridByUserName(string username)
        {
            random = new Random(username.GetHashCode());
            string[,] result = new string[numberOfRows + 1, numberOfColumns + 1];
            for (int side = 0; side < (numberOfRows + 1); side++)
            {
                if (side == 0)
                {
                    for (int head = 0; head < (numberOfColumns + 1); head++)
                    {
                        if (head == 0)
                        {
                            result[side, head] = "*";
                        }
                        else
                        {
                            result[side, head] = alphabetArray[head - 1].ToString();
                        }
                    }
                }
                else
                {
                    for (int head = 0; head < (numberOfColumns + 1); head++)
                    {
                        if (head == 0)
                        {
                            result[side, head] = (side).ToString();
                        }
                        else
                        {
                            result[side, head] = alphabetArray[random.Next(0, 26)].ToString();
                            result[side, head] += random.Next(0, 9).ToString();
                        }
                    }
                }
            }
            return result;
        }

        public static string[,] GenerateGridByUserName(string username, string cipherNotation)
        {
            random = new Random((username + cipherNotation).GetHashCode());
            string[,] result = new string[numberOfRows + 1, numberOfColumns + 1];
            for (int side = 0; side < (numberOfRows + 1); side++)
            {
                if (side == 0)
                {
                    for (int head = 0; head < (numberOfColumns + 1); head++)
                    {
                        if (head == 0)
                        {
                            result[side, head] = "*";
                        }
                        else
                        {
                            result[side, head] = alphabetArray[head - 1].ToString();
                        }
                    }
                }
                else
                {
                    for (int head = 0; head < (numberOfColumns + 1); head++)
                    {
                        if (head == 0)
                        {
                            result[side, head] = (side).ToString();
                        }
                        else
                        {
                            result[side, head] = alphabetArray[random.Next(0, 26)].ToString();
                            result[side, head] += random.Next(0, 9).ToString();
                        }
                    }
                }
            }
            return result;
        }

        public static bool AuthenticateGridChallengeUsingCipherNotation(User usr, string cipherNotation, string[] challenge, string[] response)
        {
            bool result = false;
            try
            {
                string[,] array = GenerateGridByUserName(usr.Username, cipherNotation);
                string[] challengeAnswer = new string[challenge.Length];
                for (int i = 0; i < challenge.Length; i++)
                {
                    var firstChar = challenge[i].Substring(0, 1);
                    var secondChar = challenge[i].Substring(1, 1);
                    var columnPosition = alphabetArray.ToList().IndexOf(firstChar.ToCharArray()[0]);
                    var rowPosition = int.Parse(secondChar);
                    challengeAnswer[i] = array[rowPosition + 1, columnPosition + 1];
                }
                result = challengeAnswer == challenge ? true : false;
            }
            catch (Exception)
            {
            }
            return result;
        }
        public static bool AuthenticateGridChallengeUsingCipherNotation(User usr, Challenge challenge)
        {
            bool result = false;
            try
            {
                string[,] array = GenerateGridByUserName(usr.Username, usr.Pin);
                string[] challengeAnswer = new string[challenge.Response.Length];
                for (int i = 0; i < challenge.Request.Length; i++)
                {
                    var firstChar = challenge.Request[i].Substring(0, 1);
                    var secondChar = challenge.Request[i].Substring(1, 1);
                    var columnPosition = alphabetArray.ToList().IndexOf(firstChar.ToCharArray()[0]);
                    var rowPosition = int.Parse(secondChar);
                    challengeAnswer[i] = array[rowPosition + 1, columnPosition + 1];
                }
                result = challengeAnswer == challenge.Request ? true : false;
            }
            catch (Exception)
            {
            }
            return result;
        }
        public static string[] GenerateGridChallenge(bool withArray)
        {
            string[] result = new string[challengeSize];
            random = new Random(DateTime.Now.ToString("ddMMyyyyhhmmssss").GetHashCode());
            for (int i = 0; i < challengeSize; i++)
            {
                string challengeCell = string.Empty;
                char alphabetColumn = alphabetArray[random.Next(0, numberOfColumns - 1)];
                string numberRow = random.Next(1, numberOfRows).ToString();
                result[i] = $"{alphabetColumn}{numberRow}";
            }
            return result;
        }
        public static Challenge GenerateGridChallenge()
        {
            Challenge result = new Challenge(challengeSize);
            random = new Random(DateTime.Now.ToString("ddMMyyyyhhmmssss").GetHashCode());
            for (int i = 0; i < challengeSize; i++)
            {
                string challengeCell = string.Empty;
                char alphabetColumn = alphabetArray[random.Next(0, numberOfColumns - 1)];
                string numberRow = random.Next(1, numberOfRows).ToString();
                result.Request[i] = $"{alphabetColumn}{numberRow}";
            }
            return result;
        }
        public static bool AuthenticateGridChallenge(User usr, string[] challenge, string[] response)
        {
            bool result = false;
            try
            {
                string[,] array = usr.SecurityGrid;//GenerateGridByUserName(usr.Username);
                string[] challengeAnswer = new string[challenge.Length];
                for (int i = 0; i < challenge.Length; i++)
                {
                    var firstChar = challenge[i].Substring(0, 1);
                    var secondChar = challenge[i].Substring(1, 1);
                    var columnPosition = alphabetArray.ToList().IndexOf(firstChar.ToCharArray()[0]);
                    var rowPosition = int.Parse(secondChar);
                    challengeAnswer[i] = array[rowPosition + 1, columnPosition + 1];
                }
                result = challengeAnswer == challenge ? true : false;
            }
            catch (Exception)
            {
            }
            return result;
        }
        public static bool AuthenticateGridChallenge(User usr, Challenge challenge)
        {
            bool result = false;
            try
            {
                string[,] array = usr.SecurityGrid;//GenerateGridByUserName(usr.Username);
                string[] challengeAnswer = new string[challenge.Response.Length];
                for (int i = 0; i < challenge.Request.Length; i++)
                {
                    var firstChar = challenge.Request[i].Substring(0, 1);
                    var secondChar = challenge.Request[i].Substring(1, 1);
                    var columnPosition = alphabetArray.ToList().IndexOf(firstChar.ToCharArray()[0]);
                    var rowPosition = int.Parse(secondChar);
                    challengeAnswer[i] = array[rowPosition + 1, columnPosition + 1];
                }
                result = challengeAnswer == challenge.Request ? true : false;
            }
            catch (Exception)
            {
            }
            return result;
        }
        public static bool AuthenticateGridChallenge(string[,] array, string[] challenge, string[] response)
        {
            bool result = false;
            try
            {
                //string[,] array = GenerateGridByUserName(usr);
                #region Debug
                PrintGrid(array);
                #endregion
                string[] challengeAnswer = new string[challenge.Length];
                for (int i = 0; i < challenge.Length; i++)
                {
                    var firstChar = challenge[i].Substring(0, 1);
                    var secondChar = challenge[i].Substring(1, 1);
                    var columnPosition = alphabetArray.ToList().IndexOf(firstChar.ToCharArray()[0]);
                    var rowPosition = int.Parse(secondChar);
                    challengeAnswer[i] = array[rowPosition, columnPosition + 1];
                }
                result = ArrayEquals(challengeAnswer, response);
            }
            catch (Exception)
            {
            }
            return result;
        }

        public static bool AuthenticateGridChallenge(string[,] array,Challenge challenge)
        {
            bool result = false;
            try
            {
                //string[,] array = GenerateGridByUserName(usr);
                #region Debug
                PrintGrid(array);
                #endregion
                string[] challengeAnswer = new string[challenge.Response.Length];
                for (int i = 0; i < challenge.Request.Length; i++)
                {
                    var firstChar = challenge.Request[i].Substring(0, 1);
                    var secondChar = challenge.Request[i].Substring(1, 1);
                    var columnPosition = alphabetArray.ToList().IndexOf(firstChar.ToCharArray()[0]);
                    var rowPosition = int.Parse(secondChar);
                    challengeAnswer[i] = array[rowPosition, columnPosition + 1];
                }
                result = ArrayEquals(challengeAnswer, challenge.Response);
            }
            catch (Exception)
            {
            }
            return result;
        }

        public static bool ArrayEquals(string[] a, string[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (!a[i].Equals(b[i]))
                    return false;
            }
            return true;
        }
        public static void PrintGrid(string[,] arr)
        {
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", arr[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
        }


        private const int BufferSize = 0x10000; // should always be power of 2
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public static string DecryptInstitutionDataFile(User institution, string cipher, out string errMsg)
        {
            string result = string.Empty;
            errMsg = string.Empty;
            try
            {
                string fileTimeStamp = DateTime.Now.ToString("yyyyMMddhhmmssss");
                string dataFile = $@"{FilePath}{institution.Username}_{fileTimeStamp}.txt";
                string cipherFile = $@"{FilePath}{institution.Username}_{fileTimeStamp}.cph";
                //Write data to file 
                File.WriteAllText(cipherFile, cipher, Encoding.UTF8);

                PGPSnippet.PGPDecryption.PGPDecrypt.Decrypt(cipher, institution.DecryptionKey, institution.PassPhrase, dataFile, true);
                result = File.ReadAllText(dataFile);
                File.Delete(dataFile);
                File.Delete(cipherFile);
            }
            catch (PgpException pE)
            {
                errMsg = pE.Message;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }

        public static string EncryptInstitutionDataFile(User institution, string data, out string errMsg)
        {
            string cipher = string.Empty;
            errMsg = string.Empty;
            try
            {
                string fileTimeStamp = DateTime.Now.ToString("yyyyMMddhhmmssss");
                string dataFile = $@"{FilePath}{institution.Username}_{fileTimeStamp}.txt";
                string cipherFile = $@"{FilePath}{institution.Username}_{fileTimeStamp}.cph";
                string privateKey = $@"{FilePath}PrivateKey_{fileTimeStamp}.asc";
                string publicKey = $@"{FilePath}PublicKey_{fileTimeStamp}.asc";
                string partnerKey = $@"{FilePath}PartnerKey_{fileTimeStamp}.asc";
                //Write data to file 
                File.WriteAllText(dataFile, data, Encoding.UTF8);
                File.WriteAllText(privateKey, institution.DecryptionKey, Encoding.UTF8);
                File.WriteAllText(publicKey, institution.EncryptionKey, Encoding.UTF8);
                File.WriteAllText(partnerKey, institution.EncryptionKey, Encoding.UTF8);
                PGPSnippet.Keys.PgpEncryptionKeys encryptionKeys = new PGPSnippet.Keys.PgpEncryptionKeys(institution.EncryptionKey, institution.EncryptionKey, institution.PassPhrase, true);
                PGPSnippet.PGPEncryption.PgpEncrypt encrypter = new PGPSnippet.PGPEncryption.PgpEncrypt(encryptionKeys);
                using (Stream outputStream = File.Create(cipherFile))
                {
                    encrypter.EncryptAndSign(outputStream, new FileInfo(dataFile));
                }
                byte[] hardCipher = File.ReadAllBytes(cipherFile);
                cipher = Convert.ToBase64String(hardCipher);
                File.Delete(dataFile);
                //File.Delete(cipherFile);
                File.Delete(privateKey);
                File.Delete(publicKey);
                File.Delete(partnerKey);
            }
            catch (PgpException e)
            {
                errMsg = e.Message;
            }
            return cipher;
        }


        public static string EncryptInstitutionData(User institution, string data, out string errMsg)
        {
            string cipher = string.Empty;
            errMsg = string.Empty;
            try
            {
                string fileTimeStamp = DateTime.Now.ToString("yyyyMMddhhmmssss");
                string dataFile = $@"{FilePath}\{institution.Username}_{fileTimeStamp}.txt";
                string cipherFile = $@"{FilePath}\{institution.Username}_{fileTimeStamp}.cph";
                //Write data to file 
                File.WriteAllText(dataFile, data, Encoding.UTF8);
                using (Stream publicKeyStream = GenerateStreamFromString(institution.EncryptionKey))
                {
                    PgpPublicKey encKey = ReadPublicKey(publicKeyStream);

                    using (MemoryStream bOut = new MemoryStream())
                    {
                        PgpCompressedDataGenerator comData = new PgpCompressedDataGenerator(CompressionAlgorithmTag.Zip);
                        PgpUtilities.WriteFileToLiteralData(comData.Open(bOut), PgpLiteralData.Binary, new FileInfo(dataFile));

                        comData.Close();
                        PgpEncryptedDataGenerator cPk = new PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag.Cast5, true, new SecureRandom());

                        cPk.AddMethod(encKey);
                        byte[] bytes = bOut.ToArray();

                        using (Stream outputStream = File.Create(cipherFile))
                        {
                            if (false)
                            {
                                using (ArmoredOutputStream armoredStream = new ArmoredOutputStream(outputStream))
                                {
                                    using (Stream cOut = cPk.Open(armoredStream, bytes.Length))
                                    {
                                        cOut.Write(bytes, 0, bytes.Length);
                                    }
                                }
                            }
                            else
                            {
                                using (Stream cOut = cPk.Open(outputStream, bytes.Length))
                                {
                                    cOut.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                    }
                }
                cipher = File.ReadAllText(cipherFile);
                File.Delete(dataFile);
                File.Delete(cipherFile);

            }
            catch (PgpException e)
            {
                errMsg = e.Message;
            }
            return cipher;
        }

        public static string DecryptInstitutionData(User institution, string cipher, out string errMsg)
        {
            string result = string.Empty;
            errMsg = string.Empty;
            try
            {
                string fileTimeStamp = DateTime.Now.ToString("yyyyMMddhhmmssss");
                string dataFile = $@"{FilePath}\{institution.Username}_{fileTimeStamp}.txt";
                string cipherFile = $@"{FilePath}\{institution.Username}_{fileTimeStamp}.cph";
                //Write data to file 
                File.WriteAllText(cipherFile, cipher, Encoding.UTF8);

                using (Stream inputStream = File.OpenRead(cipherFile))
                {
                    using (Stream keyIn = GenerateStreamFromString(institution.EncryptionKey))
                    {
                        Decrypt(inputStream, keyIn, institution.PassPhrase, dataFile);
                    }
                }
                cipher = File.ReadAllText(dataFile);
                File.Delete(dataFile);
                File.Delete(cipherFile);
            }
            catch (PgpException pE)
            {
                errMsg = pE.Message;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }

        public static void EncryptFile(string inputFile, string outputFile, string publicKeyFile, bool armor, bool withIntegrityCheck)
        {
            try
            {
                using (Stream publicKeyStream = File.OpenRead(publicKeyFile))
                {
                    PgpPublicKey encKey = ReadPublicKey(publicKeyStream);

                    using (MemoryStream bOut = new MemoryStream())
                    {
                        PgpCompressedDataGenerator comData = new PgpCompressedDataGenerator(CompressionAlgorithmTag.Zip);
                        PgpUtilities.WriteFileToLiteralData(comData.Open(bOut), PgpLiteralData.Binary, new FileInfo(inputFile));

                        comData.Close();
                        PgpEncryptedDataGenerator cPk = new PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag.Cast5, withIntegrityCheck, new SecureRandom());

                        cPk.AddMethod(encKey);
                        byte[] bytes = bOut.ToArray();

                        using (Stream outputStream = File.Create(outputFile))
                        {
                            if (armor)
                            {
                                using (ArmoredOutputStream armoredStream = new ArmoredOutputStream(outputStream))
                                {
                                    using (Stream cOut = cPk.Open(armoredStream, bytes.Length))
                                    {
                                        cOut.Write(bytes, 0, bytes.Length);
                                    }
                                }
                            }
                            else
                            {
                                using (Stream cOut = cPk.Open(outputStream, bytes.Length))
                                {
                                    cOut.Write(bytes, 0, bytes.Length);
                                }
                            }
                        }
                    }
                }
            }
            catch (PgpException e)
            {
                throw;
            }
        }
        public static void EncryptAndSign(string inputFile, string outputFile, string publicKeyFile, string privateKeyFile, string passPhrase, bool armor)
        {
            PgpEncryptionKeys encryptionKeys = new PgpEncryptionKeys(publicKeyFile, privateKeyFile, passPhrase);

            if (!File.Exists(inputFile))
                throw new FileNotFoundException(String.Format("Input file [{0}] does not exist.", inputFile));

            if (!File.Exists(publicKeyFile))
                throw new FileNotFoundException(String.Format("Public Key file [{0}] does not exist.", publicKeyFile));

            if (!File.Exists(privateKeyFile))
                throw new FileNotFoundException(String.Format("Private Key file [{0}] does not exist.", privateKeyFile));

            if (String.IsNullOrEmpty(passPhrase))
                throw new ArgumentNullException("Invalid Pass Phrase.");

            if (encryptionKeys == null)
                throw new ArgumentNullException("Encryption Key not found.");

            using (Stream outputStream = File.Create(outputFile))
            {
                if (armor)
                    using (ArmoredOutputStream armoredOutputStream = new ArmoredOutputStream(outputStream))
                    {
                        OutputEncrypted(inputFile, armoredOutputStream, encryptionKeys);
                    }
                else
                    OutputEncrypted(inputFile, outputStream, encryptionKeys);
            }
        }

        private static void OutputEncrypted(string inputFile, Stream outputStream, PgpEncryptionKeys encryptionKeys)
        {
            using (Stream encryptedOut = ChainEncryptedOut(outputStream, encryptionKeys))
            {
                FileInfo unencryptedFileInfo = new FileInfo(inputFile);
                using (Stream compressedOut = ChainCompressedOut(encryptedOut))
                {
                    PgpSignatureGenerator signatureGenerator = InitSignatureGenerator(compressedOut, encryptionKeys);
                    using (Stream literalOut = ChainLiteralOut(compressedOut, unencryptedFileInfo))
                    {
                        using (FileStream inputFileStream = unencryptedFileInfo.OpenRead())
                        {
                            WriteOutputAndSign(compressedOut, literalOut, inputFileStream, signatureGenerator);
                            inputFileStream.Close();
                        }
                    }
                }
            }
        }
        private static void WriteOutputAndSign(Stream compressedOut, Stream literalOut, FileStream inputFile, PgpSignatureGenerator signatureGenerator)
        {
            int length = 0;
            byte[] buf = new byte[BufferSize];
            while ((length = inputFile.Read(buf, 0, buf.Length)) > 0)
            {
                literalOut.Write(buf, 0, length);
                signatureGenerator.Update(buf, 0, length);
            }
            signatureGenerator.Generate().Encode(compressedOut);
        }

        private static Stream ChainEncryptedOut(Stream outputStream, PgpEncryptionKeys m_encryptionKeys)
        {
            PgpEncryptedDataGenerator encryptedDataGenerator;
            encryptedDataGenerator = new PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag.TripleDes, new SecureRandom());
            encryptedDataGenerator.AddMethod(m_encryptionKeys.PublicKey);
            return encryptedDataGenerator.Open(outputStream, new byte[BufferSize]);
        }

        private static Stream ChainCompressedOut(Stream encryptedOut)
        {
            PgpCompressedDataGenerator compressedDataGenerator = new PgpCompressedDataGenerator(CompressionAlgorithmTag.Zip);
            return compressedDataGenerator.Open(encryptedOut);
        }

        private static Stream ChainLiteralOut(Stream compressedOut, FileInfo file)
        {
            PgpLiteralDataGenerator pgpLiteralDataGenerator = new PgpLiteralDataGenerator();
            return pgpLiteralDataGenerator.Open(compressedOut, PgpLiteralData.Binary, file);
        }

        private static PgpSignatureGenerator InitSignatureGenerator(Stream compressedOut, PgpEncryptionKeys m_encryptionKeys)
        {
            const bool IsCritical = false;
            const bool IsNested = false;
            PublicKeyAlgorithmTag tag = m_encryptionKeys.SecretKey.PublicKey.Algorithm;
            PgpSignatureGenerator pgpSignatureGenerator = new PgpSignatureGenerator(tag, HashAlgorithmTag.Sha1);
            pgpSignatureGenerator.InitSign(PgpSignature.BinaryDocument, m_encryptionKeys.PrivateKey);
            foreach (string userId in m_encryptionKeys.SecretKey.PublicKey.GetUserIds())
            {
                PgpSignatureSubpacketGenerator subPacketGenerator = new PgpSignatureSubpacketGenerator();
                subPacketGenerator.SetSignerUserId(IsCritical, userId);
                pgpSignatureGenerator.SetHashedSubpackets(subPacketGenerator.Generate());
                // Just the first one!
                break;
            }
            pgpSignatureGenerator.GenerateOnePassVersion(IsNested).Encode(compressedOut);
            return pgpSignatureGenerator;
        }
        public static void Decrypt(string inputfile, string privateKeyFile, string passPhrase, string outputFile)
        {
            if (!File.Exists(inputfile))
                throw new FileNotFoundException(String.Format("Encrypted File [{0}] not found.", inputfile));

            if (!File.Exists(privateKeyFile))
                throw new FileNotFoundException(String.Format("Private Key File [{0}] not found.", privateKeyFile));

            if (String.IsNullOrEmpty(outputFile))
                throw new ArgumentNullException("Invalid Output file path.");

            using (Stream inputStream = File.OpenRead(inputfile))
            {
                using (Stream keyIn = File.OpenRead(privateKeyFile))
                {
                    Decrypt(inputStream, keyIn, passPhrase, outputFile);
                }
            }
        }
        public static void Decrypt(Stream inputStream, Stream privateKeyStream, string passPhrase, string outputFile)
        {
            try
            {
                PgpObjectFactory pgpF = null;
                PgpEncryptedDataList enc = null;
                PgpObject o = null;
                PgpPrivateKey sKey = null;
                PgpPublicKeyEncryptedData pbe = null;
                PgpSecretKeyRingBundle pgpSec = null;

                pgpF = new PgpObjectFactory(PgpUtilities.GetDecoderStream(inputStream));
                // find secret key
                pgpSec = new PgpSecretKeyRingBundle(PgpUtilities.GetDecoderStream(privateKeyStream));

                if (pgpF != null)
                    o = pgpF.NextPgpObject();

                // the first object might be a PGP marker packet.
                if (o is PgpEncryptedDataList)
                    enc = (PgpEncryptedDataList)o;
                else
                    enc = (PgpEncryptedDataList)pgpF.NextPgpObject();

                // decrypt
                foreach (PgpPublicKeyEncryptedData pked in enc.GetEncryptedDataObjects())
                {
                    sKey = FindSecretKey(pgpSec, pked.KeyId, passPhrase.ToCharArray());

                    if (sKey != null)
                    {
                        pbe = pked;
                        break;
                    }
                }

                if (sKey == null)
                    throw new ArgumentException("Secret key for message not found.");

                PgpObjectFactory plainFact = null;

                using (Stream clear = pbe.GetDataStream(sKey))
                {
                    plainFact = new PgpObjectFactory(clear);
                }

                PgpObject message = plainFact.NextPgpObject();

                if (message is PgpCompressedData)
                {
                    PgpCompressedData cData = (PgpCompressedData)message;
                    PgpObjectFactory of = null;

                    using (Stream compDataIn = cData.GetDataStream())
                    {
                        of = new PgpObjectFactory(compDataIn);
                    }

                    message = of.NextPgpObject();
                    if (message is PgpOnePassSignatureList)
                    {
                        message = of.NextPgpObject();
                        PgpLiteralData Ld = null;
                        Ld = (PgpLiteralData)message;
                        using (Stream output = File.Create(outputFile))
                        {
                            Stream unc = Ld.GetInputStream();
                            Streams.PipeAll(unc, output);
                        }
                    }
                    else
                    {
                        PgpLiteralData Ld = null;
                        Ld = (PgpLiteralData)message;
                        using (Stream output = File.Create(outputFile))
                        {
                            Stream unc = Ld.GetInputStream();
                            Streams.PipeAll(unc, output);
                        }
                    }
                }
                else if (message is PgpLiteralData)
                {
                    PgpLiteralData ld = (PgpLiteralData)message;
                    string outFileName = ld.FileName;

                    using (Stream fOut = File.Create(outputFile))
                    {
                        Stream unc = ld.GetInputStream();
                        Streams.PipeAll(unc, fOut);
                    }
                }
                else if (message is PgpOnePassSignatureList)
                    throw new PgpException("Encrypted message contains a signed message - not literal data.");
                else
                    throw new PgpException("Message is not a simple encrypted file - type unknown.");

                #region commented code

                //if (pbe.IsIntegrityProtected())
                //{
                //    if (!pbe.Verify())
                //        msg = "message failed integrity check.";
                //    //Console.Error.WriteLine("message failed integrity check");
                //    else
                //        msg = "message integrity check passed.";
                //    //Console.Error.WriteLine("message integrity check passed");
                //}
                //else
                //{
                //    msg = "no message integrity check.";
                //    //Console.Error.WriteLine("no message integrity check");
                //}

                #endregion commented code
            }
            catch (PgpException ex)
            {
                throw;
            }
        }
        private static PgpPublicKey ReadPublicKey(Stream inputStream)
        {
            inputStream = PgpUtilities.GetDecoderStream(inputStream);

            PgpPublicKeyRingBundle pgpPub = new PgpPublicKeyRingBundle(inputStream);

            // we just loop through the collection till we find a key suitable for encryption, in the real
            // world you would probably want to be a bit smarter about this.
            // iterate through the key rings.
            foreach (PgpPublicKeyRing kRing in pgpPub.GetKeyRings())
            {
                foreach (PgpPublicKey k in kRing.GetPublicKeys())
                {
                    if (k.IsEncryptionKey)
                        return k;
                }
            }

            throw new ArgumentException("Can't find encryption key in key ring.");
        }
        private static PgpPrivateKey FindSecretKey(PgpSecretKeyRingBundle pgpSec, long keyId, char[] pass)
        {
            PgpSecretKey pgpSecKey = pgpSec.GetSecretKey(keyId);

            if (pgpSecKey == null)
                return null;

            return pgpSecKey.ExtractPrivateKey(pass);
        }

    }

    public class PgpEncryptionKeys
    {
        public PgpPublicKey PublicKey { get; private set; }

        public PgpPrivateKey PrivateKey { get; private set; }

        public PgpSecretKey SecretKey { get; private set; }

        /// <summary>
        /// Initializes a new instance of the EncryptionKeys class.
        /// Two keys are required to encrypt and sign data. Your private key and the recipients public key.
        /// The data is encrypted with the recipients public key and signed with your private key.
        /// </summary>
        /// <param name="publicKeyPath">The key used to encrypt the data</param>
        /// <param name="privateKeyPath">The key used to sign the data.</param>
        /// <param name="passPhrase">The (your) password required to access the private key</param>
        /// <exception cref="ArgumentException">Public key not found. Private key not found. Missing password</exception>
        public PgpEncryptionKeys(string publicKeyPath, string privateKeyPath, string passPhrase)
        {
            if (!File.Exists(publicKeyPath))
                throw new ArgumentException("Public key file not found", "publicKeyPath");
            if (!File.Exists(privateKeyPath))
                throw new ArgumentException("Private key file not found", "privateKeyPath");
            if (String.IsNullOrEmpty(passPhrase))
                throw new ArgumentException("passPhrase is null or empty.", "passPhrase");
            PublicKey = ReadPublicKey(publicKeyPath);
            SecretKey = ReadSecretKey(privateKeyPath);
            PrivateKey = ReadPrivateKey(passPhrase);
        }

        #region Secret Key

        private PgpSecretKey ReadSecretKey(string privateKeyPath)
        {
            using (Stream keyIn = File.OpenRead(privateKeyPath))
            {
                using (Stream inputStream = PgpUtilities.GetDecoderStream(keyIn))
                {
                    PgpSecretKeyRingBundle secretKeyRingBundle = new PgpSecretKeyRingBundle(inputStream);
                    PgpSecretKey foundKey = GetFirstSecretKey(secretKeyRingBundle);
                    if (foundKey != null)
                        return foundKey;
                }
            }
            throw new ArgumentException("Can't find signing key in key ring.");
        }

        /// <summary>
        /// Return the first key we can use to encrypt.
        /// Note: A file can contain multiple keys (stored in "key rings")
        /// </summary>
        private PgpSecretKey GetFirstSecretKey(PgpSecretKeyRingBundle secretKeyRingBundle)
        {
            foreach (PgpSecretKeyRing kRing in secretKeyRingBundle.GetKeyRings())
            {
                PgpSecretKey key = kRing.GetSecretKeys()
                    .Cast<PgpSecretKey>()
                    .Where(k => k.IsSigningKey)
                    .FirstOrDefault();
                if (key != null)
                    return key;
            }
            return null;
        }

        #endregion Secret Key

        #region Public Key

        private PgpPublicKey ReadPublicKey(string publicKeyPath)
        {
            using (Stream keyIn = File.OpenRead(publicKeyPath))
            {
                using (Stream inputStream = PgpUtilities.GetDecoderStream(keyIn))
                {
                    PgpPublicKeyRingBundle publicKeyRingBundle = new PgpPublicKeyRingBundle(inputStream);
                    PgpPublicKey foundKey = GetFirstPublicKey(publicKeyRingBundle);
                    if (foundKey != null)
                        return foundKey;
                }
            }
            throw new ArgumentException("No encryption key found in public key ring.");
        }

        private PgpPublicKey GetFirstPublicKey(PgpPublicKeyRingBundle publicKeyRingBundle)
        {
            foreach (PgpPublicKeyRing kRing in publicKeyRingBundle.GetKeyRings())
            {
                PgpPublicKey key = kRing.GetPublicKeys()
                    .Cast<PgpPublicKey>()
                    .Where(k => k.IsEncryptionKey)
                    .FirstOrDefault();
                if (key != null)
                    return key;
            }
            return null;
        }

        #endregion Public Key

        #region Private Key

        private PgpPrivateKey ReadPrivateKey(string passPhrase)
        {
            PgpPrivateKey privateKey = SecretKey.ExtractPrivateKey(passPhrase.ToCharArray());
            if (privateKey != null)
                return privateKey;
            throw new ArgumentException("No private key found in secret key.");
        }

        #endregion Private Key
    }

    public class SecurityWrapper
    {
        NHBusinessObjectDAO<User> BusinessObjectDAO = new NHBusinessObjectDAO<User>();
        static string FilePath = ConfigurationManager.AppSettings["FilePath"];
        public User UpdateInstitutionSecurityDetails(string username, out string errMsg)
        {
            User result = new User();
            errMsg = string.Empty;
            try
            {
                result = BusinessObjectDAO.RetrieveDataObjectByParameter(new KeyValuePair<string, object>("Username", username));//ClientDAO.RetrieveClientByInstitutionCode(institutionCode);
                if (result != null)
                {
                    string dateSalt = DateTime.Now.ToString("yyyyMMddhhmmssss");
                    result.DateLastModified = DateTime.Now;
                    result.PassPhrase = $"{result.Name}/{result.Username}/{dateSalt}";
                    PGPSnippet.KeyGeneration.KeysForPGPEncryptionDecryption.GenerateKey(result.Username, result.PassPhrase, FilePath);
                    result.EncryptionKey = File.ReadAllText($@"{FilePath}\PGPPublicKey.asc");
                    result.DecryptionKey = File.ReadAllText($@"{FilePath}\PGPPrivateKey.asc");
                    File.Delete($@"{FilePath}\PGPPublicKey.asc");
                    File.Delete($@"{FilePath}\PGPPrivateKey.asc");
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return result;
        }
    }
}
