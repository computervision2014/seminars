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

namespace SegmentationImage
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
                binaryButton.IsEnabled = true;
                erosionButton.IsEnabled = true;
                dilatationButton.IsEnabled = true;
                crossButton.IsEnabled = true;
            }
        }

        // show image on the window
        private void showImage(string filename)
        {
            originalImage = ImageConvertor.FilenameToImage(filename);
            originalImageBytes = ImageConvertor.ImageToByteArray(filename);
            originalPanel.Source = originalImage;
        }

        // click on processing buttons
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string buttonName = (sender as Button).Name.ToString();
            ImageProcessing process = new ImageProcessing();
            byte[] processedImageBytes;
            switch (buttonName[0])
            {
                // if the button is "Binary"
                case 'b':
                    processedImageBytes = process.setBinary(originalImageBytes);
                    processedPanel.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, originalImage.PixelWidth, originalImage.PixelHeight, 1);
                    break;

                // if the button is "Erosion"
                case 'e':
                    processedImageBytes = process.setErosion(originalImageBytes);
                    processedPanel.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, originalImage.PixelWidth, originalImage.PixelHeight, 1);
                    break;

                // if the button is "Dilatation"
                case 'd':
                    processedImageBytes = process.setDilatation(originalImageBytes);
                    processedPanel.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, originalImage.PixelWidth, originalImage.PixelHeight, 1);
                    break;

                // if the button is "Edge"
                case 'c':
                    processedImageBytes = process.setEdges(originalImageBytes);
                    processedPanel.Source = ImageConvertor.ByteArrayToImage(processedImageBytes, originalImage.PixelWidth, originalImage.PixelHeight, 1);
                    break;

                // if smth stupid happend
                default:
                    break;
            }

        }
    }
}
