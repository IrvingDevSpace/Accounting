using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Utility
{
    internal class ImageEncoder
    {
        public static void CompressionAndSave(Bitmap originalImage, String imgPath, long quality)
        {
            using (originalImage)
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID for the Quality parameter category.  
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

                // First compression with 50L quality  
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                myEncoderParameters.Param[0] = myEncoderParameter;
                originalImage.Save(imgPath, jpgEncoder, myEncoderParameters);
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static void ImageResizeAndSave(Bitmap originalImage, String imgPath)
        {
            int newWidth = 50;
            int newHeight = 50;
            using (originalImage)
            {
                using (Bitmap resizedImage = new Bitmap(newWidth, newHeight))
                {
                    using (Graphics g = Graphics.FromImage(resizedImage))
                    {
                        g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                    }
                    resizedImage.Save(imgPath);
                }
            }
        }
    }
}
