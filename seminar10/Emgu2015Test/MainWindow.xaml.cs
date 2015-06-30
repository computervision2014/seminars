using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Windows.Threading;
using System.Windows.Interop;

namespace Emgu2015Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Capture capture;
        private CascadeClassifier haarCascade;
DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            capture = new Capture();
            haarCascade = new CascadeClassifier("../../haarcascade_frontalface_alt.xml");
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 33);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Image<Bgr, Byte> currentFrame = capture.QueryFrame();

            if (currentFrame != null)
            {
                Image<Gray, Byte> grayFrame = currentFrame.Convert<Gray, Byte>();

                var facesDetected = haarCascade.DetectMultiScale(
                    grayFrame,               //image
                    1.4,                //scaleFactor
                    2,                 //minNeighbors
                    new System.Drawing.Size(80, 80),   //minSize
                   new System.Drawing.Size(250, 250));        //maxSize
                
                //Draw detected faces
                foreach (System.Drawing.Rectangle face in facesDetected)
                    currentFrame.Draw(face, new Bgr(System.Drawing.Color.Red), 2);

                image.Source = convertoToImagesource(currentFrame);
            }
        }

        public static ImageSource convertoToImagesource(Image<Bgr, Byte> im)
        {
            var b = im.ToBitmap();
            var m = Imaging.CreateBitmapSourceFromHBitmap(b.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            return m;
        }
    }
}
