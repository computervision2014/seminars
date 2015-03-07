using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace HoughTransformLine
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



            ImageProcessing process = new ImageProcessing();
            byte[] processedImageBytes;
            byte[] grayscale = process.setGrayscale(originalImageBytes);

            ArrayData inputData = new ArrayData(grayscale, originalImage.PixelWidth, originalImage.PixelHeight);

            timer.Start();
            var outputData = process.houghTransform(inputData, 360, 360, 20);
            processedImageBytes = outputData.dataArray;
            timer.Stop();
            timeElapsed1.Content = "Time: " + timer.ElapsedMilliseconds + " ms";
            blurImage1.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, outputData.width, outputData.height, 1);



        }

    }
}
