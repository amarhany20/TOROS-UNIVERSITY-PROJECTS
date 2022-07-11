using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace WebCrawler1
{
    public static class OtherMethods //Extentions too
    {
        public static string decodeUrl (this string strUrl)
        {
            return HtmlEntity.DeEntitize(strUrl);
        }
        public static string returnRootUrl(this string srUrl)
        {
            var uri = new Uri(srUrl);
            return uri.Host;
        }
        public static CrawledUrlsTable ConverToBaseMainUrlClass(this CrawledUrlsTable finalObject)
        {
            return JsonConvert.DeserializeObject<CrawledUrlsTable>(JsonConvert.SerializeObject(finalObject));
        }
        public static void CopyProperties(this object source, object destination)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");
            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();
            // Collect all the valid properties to map
            var results = from srcProp in typeSrc.GetProperties()
                          let targetProperty = typeDest.GetProperty(srcProp.Name)
                          where srcProp.CanRead
                          && targetProperty != null
                          && (targetProperty.GetSetMethod(true) != null && !targetProperty.GetSetMethod(true).IsPrivate)
                          && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
                          && targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)
                          select new { sourceProperty = srcProp, targetProperty = targetProperty };
            //map the properties
            foreach (var props in results)
            {
                props.targetProperty.SetValue(destination, props.sourceProperty.GetValue(source, null), null);
            }
        }

        public static void logMesssage(string srMsg)
        {
            lock (AllVariables._lock_swLogs)
            {
                AllVariables.swLog.WriteLine($"{DateTime.Now}\t\t{srMsg}");
            }
        }
    }
    public static class StringCompressor
    {
        public static string CompressString(this string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="compressedText">The compressed text.</param>
        /// <returns></returns>
        public static string DecompressString(this string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        public static double ToDouble(this object myObj)
        {
            double dblResult = 0;

            if (myObj == null)
            {
                return 0;
            }
            double.TryParse(myObj.ToString(), out dblResult);
            return dblResult;
        }
        public static class CompressionCalculator
        {
            public static double CompressionPercent(double str, double compstr)
            {

                double result;
                result = (compstr / str) * 100;
                return result;
            }
        }
    }
    public static class EncAndNorm
    {
        public static string ComputeHashExt(this string strUrl)
        {
            return ComputeSha256Hash(normalizingUrl(strUrl));
        }
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static string normalizingUrl( string str)
        {
            return str.ToLower(new System.Globalization.CultureInfo("en-US")).Trim();
        }
      

    }
   




}
