using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Drawing.Imaging;

namespace CoreBusiness.Models
{
    public enum ImageType
    {
        jpeg = 0,
        png = 1,
        gif = 2,
        tiff = 3
    }
    [Serializable]
    public class ImageModel : IExternalObjectModel
    {
        string filePath = ConfigurationManager.AppSettings["FilePath"];
        public string fileName { get; set; }
        public string QualifiedFileName()
        {
            return filePath + fileName + Extension.ToString();
        }
        public Bitmap Image { get; set; }
        public ImageType Extension { get; set; }
        public string Base64Image
        {
            get
            {
                string result = string.Empty;
                if(Image == null)
                {
                    error = "Image is null";
                    return result;
                }
                try
                {
                    result = ToBase64();
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
                return result;
            }
        }
        public string ToBase64()
        {
            string imageString = string.Empty;
            MemoryStream ms = new MemoryStream();
            try
            {
                Image.Save(ms, ImageFormat.Jpeg);
                imageString = Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            finally
            {
                if (ms != null) ms.Dispose();
            }
            return imageString;
        }
        public string error { get; set; }
    }
}
