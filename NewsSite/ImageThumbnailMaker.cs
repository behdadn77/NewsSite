using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace newsSite
{
    public static class ImageThumbnailMaker
    {

        public static byte[] CreateThumbNail(Image image,int width=1280, int height = 600)
        {
            Bitmap bitmap = new Bitmap(image,width,height);
            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Jpeg);
            //return Image.FromStream(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
