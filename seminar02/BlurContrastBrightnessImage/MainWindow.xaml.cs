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

namespace BlurContrastBrightnessImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapImage originalImage;
        private byte[] originalImageBytes;

        public MainWindow()
        {
            InitializeComponent();
        }

        // open file from the finder
        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg)|*.jpg; *.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                showImage(openFileDialog.FileName);
                slidersGrid.IsEnabled = true;

            }
        }
        // show image on the window
        private void showImage(string filename)
        {
            originalImage = ImageConvertor.FilenameToImage(filename);
            originalImageBytes = ImageConvertor.ImageToByteArray(filename);
            originalPanel.Source = originalImage;
        }

        // event on slider changed value
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
	        int value = (int)slider.Value;
            string sliderName = slider.Name;
            ImageProcessing process = new ImageProcessing();
            byte[] processedImageBytes;
            switch (sliderName[0])
            {
                // if the slider is "Gaussian BLur"
                case 'g':
                    processedImageBytes = process.setGaussianBlur(originalImageBytes, value);
                    grayscalePanel.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, originalImage.PixelWidth, originalImage.PixelHeight, 4);
                    break;

                // if the sliber is "Brightness"
                case 'b':
                    processedImageBytes = process.setBrightness(originalImageBytes, value-50);
                    grayscalePanel.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, originalImage.PixelWidth, originalImage.PixelHeight, 4);
                    break;

                // if the slider is "Contrast"
                case 'c':
                    processedImageBytes = process.setContrast(originalImageBytes, value-100);
                    grayscalePanel.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, originalImage.PixelWidth, originalImage.PixelHeight, 4);
                    break;

                // if smth stupid happend
                default:
                    break;
            }
        }


    }
}
