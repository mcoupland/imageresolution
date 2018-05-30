using System.Windows.Media.Imaging;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Controls;
using System.Windows.Media;

namespace AtAGlanceGenerator
{
    public static class ChartImaging
    {
        public static void AddPic(iTextSharp.text.Document document, string imagefile, int chartid, Canvas canvas)
        {
            ImageFromPieControl(imagefile, chartid, canvas);
            iTextSharp.text.Image pdfimage = iTextSharp.text.Image.GetInstance(imagefile);
            pdfimage.SetDpi(300, 300);
            float left = 425;
            if (chartid == 2)
            {
                pdfimage.ScaleAbsolute(120, 120 * (550f / 350f));
                left = 440;
            }
            else
            {
                pdfimage.ScaleToFit(140, 140);
            }
            float y = 0;
            switch (chartid)
            {
                case 0:
                    y = 600;
                    break;
                case 1:
                    y = 365;
                    break;
                case 2:
                    y = 45;
                    break;
            }
            pdfimage.SetAbsolutePosition(left, y);
            document.Add(pdfimage);
        }

        private static void ImageFromPieControl(string imagefile, int chartid, Canvas canvas)
        {
            Control chart = canvas.Children[chartid] as Control;
            float dpiscale = 300f / 96f;
            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)((chart.ActualWidth + chart.Margin.Left + chart.Margin.Right) * dpiscale),
                (int)((chart.ActualHeight + chart.Margin.Top + chart.Margin.Bottom) * dpiscale),
                300,
                300,
                PixelFormats.Pbgra32);
            rtb.Render(chart);

            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            using (var fileStream = new System.IO.FileStream(imagefile, System.IO.FileMode.Create))
            {
                png.Save(fileStream);
            }
        }        
    }
}
