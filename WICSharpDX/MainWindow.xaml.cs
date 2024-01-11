using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WICSharpDX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.pngImage.Source = LoadImage("yr-png.png");
            this.webpllImage.Source = LoadImage("yr-webp-lossless.webp");
            this.webplImage.Source = LoadImage("yr-webp-lossy.webp");
        }

        private WriteableBitmap LoadImage(string imgPath)
        {
            WriteableBitmap bitmap;
            // Referencing SharpDX.Direct2D1 nuget package
            using (var factory = new SharpDX.WIC.ImagingFactory2())
            using (var decoder = new SharpDX.WIC.BitmapDecoder(factory, imgPath, SharpDX.WIC.DecodeOptions.CacheOnLoad))
            {
                Debug.Assert(decoder.FrameCount == 1);
                using (var frame = decoder.GetFrame(0))
                {
                    Debug.Assert(frame.PixelFormat == SharpDX.WIC.PixelFormat.Format32bppRGBA);
                    var size = frame.Size;
                    var stride = size.Width * 4;
                    var pixels = new byte[stride * size.Height];
                    frame.CopyPixels(pixels, stride);

                    for (int iy = 0; iy < size.Height; iy++)
                    {
                        for (int ix = 0; ix < size.Width; ix++)
                        {
                            // convert from RGBA to BGRA
                            Swap(ref pixels[iy * stride + ix * 4 + 0], ref pixels[iy * stride + ix * 4 + 2]);

                            // uncomment to reproduce the bug, WPF seems to ignore the alpha channel
                            // which you can simulate by setting alpha to 255 (opaque)
                            //pixels[iy * stride + ix * 4 + 3] = 255;
                        }
                    }

                    bitmap = new WriteableBitmap(size.Width, size.Height, 96, 96, PixelFormats.Bgra32, null);
                    bitmap.Lock();
                    try { bitmap.WritePixels(new Int32Rect(0, 0, size.Width, size.Height), pixels, stride, 0); }
                    finally { bitmap.Unlock(); }
                }
            }

            return bitmap;
        }

        private static void Swap(ref byte lhs, ref byte rhs)
        {
            var tmp = lhs;
            lhs = rhs;
            rhs = tmp;
        }
    }
}