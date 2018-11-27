using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace CoreBusiness.Processors
{
    public class BusinessObjectTransformer<T> where T : class
    {
        public static bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }
        public static bool IsXml(string input)
        {
            input = input.Trim();
            return input.StartsWith("<") && input.EndsWith(">");
        }
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string SerializeType(T obj, bool isJSON, out string errMsg, bool noNS = false)
        {
            string result = string.Empty;
            errMsg = string.Empty;
            try
            {
                if (isJSON)
                {
                    result = JsonConvert.SerializeObject(obj);
                }
                else
                {
                    var serializer = new XmlSerializer(typeof(T));
                    if (noNS == false)
                    {
                        using (var stringwriter = new System.IO.StringWriter())
                        {
                            serializer.Serialize(stringwriter, obj);
                            result = stringwriter.ToString();
                        }
                    }
                    else
                    {
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add("", "");
                        using (StringWriter writer = new Utf8StringWriter())
                        {
                            serializer.Serialize(writer, obj, ns);
                            result = writer.ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return result;
        }


        public static T DeserializeType(string xmlString, Type t, bool isJSON, out string errMsg)
        {
            T result = default(T);
            errMsg = string.Empty;
            try
            {
                if (isJSON)
                {
                    result = JsonConvert.DeserializeObject<T>(xmlString) as T;
                }
                else
                {
                    var stringReader = new System.IO.StringReader(xmlString);
                    var serializer = new XmlSerializer(t);
                    result = serializer.Deserialize(stringReader) as T;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return result;
        }

        public static object DeserializeTypeAsObject(string xmlString, Type t, bool isJSON, out string errMsg)
        {
            object result = default(object);
            errMsg = string.Empty;
            try
            {
                if (isJSON)
                {
                    result = JsonConvert.DeserializeObject(xmlString, t) as object;
                }
                else
                {
                    var stringReader = new System.IO.StringReader(xmlString);
                    var serializer = new XmlSerializer(t);
                    result = serializer.Deserialize(stringReader) as object;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return result;
        }


        /* This function is used to read decriptions from Enums */
        public static string GetDescription<T>(T enumMember) where T : struct, IConvertible
        {
            //If this is not an enum, we dont want it
            if (!typeof(T).IsEnum) return null;

            var fi = typeof(T).GetField(enumMember.ToString());
            var attributes = (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : enumMember.ToString();
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static List<KeyValuePair<string, object>> GetKeyValuePairFromObject<T>(T keyValueObject)
        {
            List<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            foreach (var property in keyValueObject.GetType().GetProperties())
            {
                result.Add(new KeyValuePair<string, object>(property.Name, property.GetValue(keyValueObject, null)));
            }
            return result;
        }

    }

    public class ObjectTransformer<T> where T : class
    {
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string SerializeType(T obj, bool isJSON, out string errMsg, bool noNS = false)
        {
            string result = string.Empty;
            errMsg = string.Empty;
            try
            {
                if (isJSON)
                {
                    result = JsonConvert.SerializeObject(obj);
                }
                else
                {
                    var serializer = new XmlSerializer(typeof(T));
                    if (noNS == false)
                    {
                        using (var stringwriter = new System.IO.StringWriter())
                        {

                            serializer.Serialize(stringwriter, obj);
                            result = stringwriter.ToString();
                        }
                    }
                    else
                    {
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add("", "");
                        using (StringWriter writer = new Utf8StringWriter())
                        {
                            serializer.Serialize(writer, obj, ns);
                            result = writer.ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return result;
        }



        public static object DeserializeType(string xmlString, Type t, bool isJSON, out string errMsg)
        {
            object result = new object();
            errMsg = string.Empty;
            try
            {
                if (isJSON) { result = JsonConvert.DeserializeObject<T>(xmlString); }
                else
                {
                    var stringReader = new System.IO.StringReader(xmlString);
                    var serializer = new XmlSerializer(t);
                    result = serializer.Deserialize(stringReader);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            return result;
        }


        /* This function is used to read decriptions from Enums */
        public static string GetDescription<T>(T enumMember) where T : struct, IConvertible
        {
            //If this is not an enum, we dont want it
            if (!typeof(T).IsEnum) return null;
            var fi = typeof(T).GetField(enumMember.ToString());
            var attributes = (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : enumMember.ToString();
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

    }

    public static class Crypto
    {
        private static string StrToEncrpt;

        private static string StrEncrypted;

        private static string StrToDecrypt;

        private static string StrDecrypted;

        private static char charhold;

        private static string[] delim;
        public static int charToAscii(char ch)
        {
            return (int)ch;
        }
        static Crypto()
        {
            RuntimeHelpers.RunClassConstructor(typeof(Crypto).TypeHandle);
            try
            {
                int num = 10;
                if (num >= 0)
                {
                    string[] expr_1F = new string[num];
                    expr_1F[0] = "H";
                    expr_1F[1] = "L";
                    expr_1F[2] = "O";
                    expr_1F[3] = "G";
                    expr_1F[4] = "I";
                    expr_1F[5] = "M";
                    expr_1F[6] = "P";
                    expr_1F[7] = "S";
                    expr_1F[8] = "N";
                    expr_1F[9] = "W";
                    delim = expr_1F;
                    return;
                }
                throw new ArrayTypeMismatchException();
            }
            catch
            { }
            object arg_7A_0;
            //Throwable throwable = Throwable.<exceptFilter>(arg_7A_0);
            //endfilter((throwable == null) ? 0 : 1);
        }
        private static string ReverseStr(string Str)
        {
            int num = Str.Length;
            string text = string.Empty;
            for (int i = num - 1; i >= 0; i += -1)
            {
                text = new StringBuilder().Append(text).Append(Str[i]).ToString();
            }
            return text;
        }
        public static string Encrypt(this string StrToencode)
        {
            int num = -1;
            StrEncrypted = string.Empty;
            int num2 = StrToencode.Length;
            //delim = new string[StrToencode.Length];
            int num3 = delim.Length - 1;
            for (int i = 0; i < num2; i++)
            {
                char ch = (StrToencode[i]);
                int strToHex = charToAscii(ch);
                if (num >= num3)
                {
                    num = 0;
                }
                num++;
                if (num < 0 || num > 9)
                {
                    num = 0;
                }
                string text = delim[num];
                string text2 = ToHex(strToHex);
                StrEncrypted = new StringBuilder().Append(StrEncrypted).Append(text2).ToString();
                StrEncrypted = new StringBuilder().Append(StrEncrypted).Append(text).ToString();
            }
            return AESEncrypt(ReverseStr(StrEncrypted));
        }
        public static string ToHex(int StrToHex)
        {
            string text = string.Empty;
            int num = StrToHex;
            int num2;
            do
            {
                int apval;
                if (num >= 16)
                {
                    apval = int.Parse(((double)num - Math.Floor((double)(num / 16)) * 16.0).ToString());
                    num2 = int.Parse(Math.Ceiling((double)(num / 16)).ToString());
                    num = num2;
                }
                else
                {
                    num2 = 0;
                    apval = num;
                }
                text = AppendHex(text, apval);
            }
            while (num2 != 0);
            return ReverseStr(text);
        }

        public static string ToHex(long StrToHex)
        {
            string text = string.Empty;
            long num = StrToHex;
            long num2;
            do
            {
                long apval;
                if (num >= 16)
                {
                    apval = long.Parse(((double)num - Math.Floor((double)(num / 16)) * 16.0).ToString());
                    num2 = long.Parse(Math.Ceiling((double)(num / 16)).ToString());
                    num = num2;
                }
                else
                {
                    num2 = 0;
                    apval = num;
                }
                text = AppendHex(text, apval);
            }
            while (num2 != 0);
            return ReverseStr(text);
        }
        private static string AppendHex(string Hex1, int Apval)
        {
            string text = string.Empty;
            text = new StringBuilder().Append(text).Append(Hex1).ToString();
            if (Apval >= 0 && Apval < 10)
            {
                text = new StringBuilder().Append(text).Append(Apval).ToString();
            }
            else if (Apval == 10)
            {
                text = new StringBuilder().Append(text).Append("A").ToString();
            }
            else if (Apval == 11)
            {
                text = new StringBuilder().Append(text).Append("B").ToString();
            }
            else if (Apval == 12)
            {
                text = new StringBuilder().Append(text).Append("C").ToString();
            }
            else if (Apval == 13)
            {
                text = new StringBuilder().Append(text).Append("D").ToString();
            }
            else if (Apval == 14)
            {
                text = new StringBuilder().Append(text).Append("E").ToString();
            }
            else if (Apval == 15)
            {
                text = new StringBuilder().Append(text).Append("F").ToString();
            }
            return text;
        }
        private static string AppendHex(string Hex1, long Apval)
        {
            string text = string.Empty;
            text = new StringBuilder().Append(text).Append(Hex1).ToString();
            if (Apval >= 0 && Apval < 10)
            {
                text = new StringBuilder().Append(text).Append(Apval).ToString();
            }
            else if (Apval == 10)
            {
                text = new StringBuilder().Append(text).Append("A").ToString();
            }
            else if (Apval == 11)
            {
                text = new StringBuilder().Append(text).Append("B").ToString();
            }
            else if (Apval == 12)
            {
                text = new StringBuilder().Append(text).Append("C").ToString();
            }
            else if (Apval == 13)
            {
                text = new StringBuilder().Append(text).Append("D").ToString();
            }
            else if (Apval == 14)
            {
                text = new StringBuilder().Append(text).Append("E").ToString();
            }
            else if (Apval == 15)
            {
                text = new StringBuilder().Append(text).Append("F").ToString();
            }
            return text;
        }
        public static string Decrypt(this string StrTodecode2)
        {
            string StrTodecode = AESDecrypt(StrTodecode2);
            StrDecrypted = string.Empty;
            int num = StrTodecode.Length;
            int num2 = delim.Length;
            StrTodecode = ReverseStr(StrTodecode);
            string text = string.Empty;
            for (int i = 0; i < num; i++)
            {
                string text2 = string.Empty;
                bool flag = false;
                char character = StrTodecode[i];
                text2 = character.ToString();
                for (int j = 0; j < num2; j++)
                {
                    if (string.CompareOrdinal(text2, delim[j]) == 0)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    text = new StringBuilder().Append(text).Append(character.ToString()).ToString();
                }
                else
                {
                    char c = (char)ToDec(text);
                    StrDecrypted = new StringBuilder().Append(StrDecrypted).Append(c).ToString();
                    text = string.Empty;
                }
            }
            return (StrDecrypted);
        }
        private static int power(int value, int exp)
        {
            int num = 1;
            if (exp == 0)
            {
                num = 1;
            }
            if (exp >= 1)
            {
                num = value;
            }
            for (int i = 2; i <= exp; i++)
            {
                num *= value;
            }
            return num;
        }
        private static int ToDec(string Str)
        {
            int num = Str.Length - 1;
            int num2 = 0;
            for (int i = 0; i <= num; i++)
            {
                char c = Str[i];
                char character = c;
                int num3 = HexVal(character.ToString());
                int num4 = power(16, num - i);
                num2 += num3 * num4;
            }
            return num2;
        }
        private static int HexVal(string Hex1)
        {
            int integer = 0;
            int result = integer;
            char c = Hex1[0];
            int num = 10;
            if (num >= 0)
            {
                string[] expr_2E = new string[num];
                expr_2E[0] = "0";
                expr_2E[1] = "1";
                expr_2E[2] = "2";
                expr_2E[3] = "3";
                expr_2E[4] = "4";
                expr_2E[5] = "5";
                expr_2E[6] = "6";
                expr_2E[7] = "7";
                expr_2E[8] = "8";
                expr_2E[9] = "9";
                string[] array = expr_2E;
                bool flag = false;
                for (int i = 0; i < array.Length; i++)
                {
                    if (string.CompareOrdinal(Hex1, array[i]) == 0)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    result = int.Parse(Hex1);
                }
                else if (Hex1.Equals("A"))
                {
                    result = 10;
                }
                else if (Hex1.Equals("B"))
                {
                    result = 11;
                }
                else if (Hex1.Equals("C"))
                {
                    result = 12;
                }
                else if (Hex1.Equals("D"))
                {
                    result = 13;
                }
                else if (Hex1.Equals("E"))
                {
                    result = 14;
                }
                else if (Hex1.Equals("F"))
                {
                    result = 15;
                }
                return result;
            }
            throw new ArrayTypeMismatchException();
        }
        private static string ToHexString(string str)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.Unicode.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"
        }

        private static string FromHexString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.Unicode.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
        }
        public static string AESEncrypt(string word, string password)
        {
            byte[] baPwd = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Encoding.UTF8.GetBytes(word);

            if (true)
                baText = Compress(baText);

            byte[] baSalt = GetRandomBytes();
            byte[] baEncrypted = new byte[baSalt.Length + baText.Length];

            // Combine Salt + Text
            for (int i = 0; i < baSalt.Length; i++)
                baEncrypted[i] = baSalt[i];
            for (int i = 0; i < baText.Length; i++)
                baEncrypted[i + baSalt.Length] = baText[i];

            baEncrypted = AES_Encrypt(baEncrypted, baPwdHash);

            string result = ToHexString(Convert.ToBase64String(baEncrypted));
            return result;
        }
        private static string AESEncrypt(string word)
        {
            byte[] baPwd = Encoding.UTF8.GetBytes("");

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Encoding.UTF8.GetBytes(word);

            if (true)
                baText = Compress(baText);

            byte[] baSalt = GetRandomBytes();
            byte[] baEncrypted = new byte[baSalt.Length + baText.Length];

            // Combine Salt + Text
            for (int i = 0; i < baSalt.Length; i++)
                baEncrypted[i] = baSalt[i];
            for (int i = 0; i < baText.Length; i++)
                baEncrypted[i + baSalt.Length] = baText[i];

            baEncrypted = AES_Encrypt(baEncrypted, baPwdHash);

            string result = ToHexString(Convert.ToBase64String(baEncrypted));
            return result;
        }
        static string BytesToStringConverted(byte[] bytes)
        {
            string response = string.Empty;

            foreach (byte b in bytes)
                response += (Char)b;

            return response;
        }

        public static string AESDecrypt(string cipher)
        {
            byte[] baPwd = Encoding.UTF8.GetBytes("");

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Convert.FromBase64String(FromHexString(cipher));

            byte[] baDecrypted = AES_Decrypt(baText, baPwdHash);

            // Remove salt
            int saltLength = GetSaltLength();
            byte[] baResult = new byte[baDecrypted.Length - saltLength];
            for (int i = 0; i < baResult.Length; i++)
                baResult[i] = baDecrypted[i + saltLength];

            if (true)
                baResult = Decompress(baResult);

            string result = Encoding.UTF8.GetString(baResult);
            return result;
        }
        public static string AESDecrypt(string cipher, string password)
        {
            byte[] baPwd = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Convert.FromBase64String(FromHexString(cipher));

            byte[] baDecrypted = AES_Decrypt(baText, baPwdHash);

            // Remove salt
            int saltLength = GetSaltLength();
            byte[] baResult = new byte[baDecrypted.Length - saltLength];
            for (int i = 0; i < baResult.Length; i++)
                baResult[i] = baDecrypted[i + saltLength];

            if (true)
                baResult = Decompress(baResult);

            string result = Encoding.UTF8.GetString(baResult);
            return result;
        }
        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public static byte[] GetRandomBytes()
        {
            int saltLength = GetSaltLength();
            byte[] ba = new byte[saltLength];
            RNGCryptoServiceProvider.Create().GetBytes(ba);
            return ba;
        }
        public static int GetSaltLength()
        {
            string cipher = "";
            return !string.IsNullOrWhiteSpace(cipher) ? cipher.Length : 0;
        }
        public static byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionMode.Compress))
            {
                dstream.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }

        public static byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return output.ToArray();
        }
    }
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
