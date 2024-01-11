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

namespace WICInvestigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri("yr-webp-lossy.webp", UriKind.RelativeOrAbsolute);
            bmp.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            bmp.EndInit();

            webpi.Source = bmp;

            //var decoder = BitmapDecoder.Create(new Uri(@"yr-png.png", UriKind.RelativeOrAbsolute), BitmapCreateOptions.DelayCreation, BitmapCacheOption.OnDemand);
            //ImageSource igsrc = new BitmapImage(new Uri("yr-png.png", UriKind.RelativeOrAbsolute));
            //LoadImages();
        }

        //private void LoadImages()
        //{
        //    LoadPngImage();
        //    LoadWebpLosslessImage();
        //    LoadWebpLossyImage();
        //}

        //private void LoadWebpLossyImage()
        //{
        //    this.webplImage.Source = (ImageSource)converter.ConvertFromString("yr-webp-lossy.webp");
        //}

        //private void LoadWebpLosslessImage()
        //{
        //    this.webpllImage.Source = (ImageSource)converter.ConvertFromString("yr-webp-lossless.webp");
        //}

        //private void LoadPngImage()
        //{
        //    BitmapImage bitmap = new BitmapImage();
        //    bitmap.BeginInit();
        //    bitmap.UriSource = new Uri("yr-png.png", UriKind.Relative);
        //    bitmap.EndInit();

        //    this.pngImage.Source = bitmap;
        //}

        //ImageSourceConverter converter = new ImageSourceConverter();
    }
}