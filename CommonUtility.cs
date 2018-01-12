using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitys
{
    public static class CommonUtility
    {
        /// <summary>
        /// 把文字转换为Bitmap
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="rect">用于输出的矩形，文字在这个矩形内显示，为空时自动计算</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="backColor">背景颜色</param>
        /// <returns></returns>
        public static Bitmap TextToBitmap(string text)
        {
            var rect = new Rectangle(0, 0, 320, 20);
            Graphics g;
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);

            g = Graphics.FromImage(bmp);

            //使用ClearType字体功能
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(Brushes.White, rect);
            g.DrawString(text, new Font(FontFamily.GenericSerif, 10), Brushes.Black, rect, format);
            return bmp;
        }

        /// <summary>
        /// 合并保存图片
        /// </summary>
        /// <param name="imageList">需要拼接的图片</param>
        public static Image MergeImages(params Image[] imageList)
        {
            Int32 wide = 0;
            Int32 high = 0;

            foreach (var image in imageList)
            {
                high += image.Height;
                wide = image.Width > wide ? image.Width : wide;
            }

            Bitmap mybmp = new Bitmap(wide, high);
            Graphics gr = Graphics.FromImage(mybmp);
            var tempHeight = 0;
            foreach (var image in imageList)
            {
                gr.DrawImage(image, new Rectangle(0, tempHeight, image.Width, image.Height));
                tempHeight += image.Height;
            }

            gr.Dispose();

            return mybmp;
        }
    }
}
