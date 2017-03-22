using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Takhfif.Controllers
{
    public class Thumbnail
    {
        static public Image ResizeImage(Image ImageToResize, Size ResizedImageSize)
        {
            return (Image)(new Bitmap(ImageToResize, ResizedImageSize));
        }

        static public byte[] ConvertImageToByte(System.Drawing.Image ImageToConvert)
        {
            MemoryStream ms = new MemoryStream();
            ImageToConvert.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}