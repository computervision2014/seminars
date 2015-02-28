using Microsoft.Win32;
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
using System.Diagnostics;

namespace FastGaussianBlur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private BitmapImage originalImage;
        private byte[] originalImageBytes;
        private Stopwatch timer = new Stopwatch();
        // open file from the finder
        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg)|*.jpg; *.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                showImage(openFileDialog.FileName);
                proccessButton.IsEnabled = true;

            }
        }
        // show image on the window
        private void showImage(string filename)
        {
            originalImage = ImageConvertor.FilenameToImage(filename);
            originalImageBytes = ImageConvertor.ImageToByteArray(filename);
            originalPanel.Source = originalImage;
        }

        private void proccessButton_Click(object sender, RoutedEventArgs e)
        {
            resultsGrid.Visibility = System.Windows.Visibility.Visible;


            // fill all 4 images with source just for fun
            string buttonName = (sender as Button).Content.ToString();
            ImageProcessing process = new ImageProcessing();
            byte[] processedImageBytes;       
            byte[] grayscale = process.setGrayscale(originalImageBytes);

            timer.Start();
            processedImageBytes = process.blurGauss_1(grayscale, originalImage.PixelWidth, originalImage.PixelHeight, 5);
            timer.Stop();
            timeElapsed1.Content = "Time: " + timer.ElapsedMilliseconds + " ms";
            blurImage1.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, originalImage.PixelWidth, originalImage.PixelHeight, 1);

            timer.Restart();
            processedImageBytes = process.blurGauss_2(grayscale, originalImage.PixelWidth, originalImage.PixelHeight, 5);
            timer.Stop();
            timeElapsed2.Content = "Time: " + timer.ElapsedMilliseconds + " ms";
            blurImage2.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, originalImage.PixelWidth, originalImage.PixelHeight, 1);

            timer.Restart();
            processedImageBytes = process.blurGauss_3(grayscale, originalImage.PixelWidth, originalImage.PixelHeight, 5);
            timer.Stop();
            timeElapsed3.Content = "Time: " + timer.ElapsedMilliseconds + " ms";
            blurImage3.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, originalImage.PixelWidth, originalImage.PixelHeight, 1);

            timer.Restart();
            processedImageBytes = process.blurGauss_4(grayscale, originalImage.PixelWidth, originalImage.PixelHeight, 5);
            timer.Stop();
            timeElapsed4.Content = "Time: " + timer.ElapsedMilliseconds + " ms";
            blurImage4.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, originalImage.PixelWidth, originalImage.PixelHeight, 1);

        }

        

    }
}
