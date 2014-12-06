using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlurContrastBrightnessImage
{
    class ImageProcessing
    {
        /* blurs image with Gaussian kernel
         this kernel looks like: 
         | 1/16f, 1/8f, 1/16f, |
         | 1/8f, 1/4f, 1/8f,   |
         | 1/16f, 1/8f, 1/16f, |
        */

        public byte[] setGaussianBlur(byte[] originalImage, int coef)
        {

            // TODO: create kernel and new image
            // TODO: convolve it with Gaussian kernel

            return null;
        }

        // changes birghness of an image
        public byte[] setBrightness(byte[] originalImage, int coef)
        {
            // TODO: set brightness to the image
            // it looks like: newPixel = oldPixel + coef
            // don't forget about pixel [0, 255]
            // TODO: check if the value is less than 0 -> make pixel = 0
            // TODO: check if the value is more than 255 -> make pixel = 255
            // else everything is fine

            return null;
        }

        // changes contrast of an image
        public byte[] setContrast(byte[] originalImage, int coef)
        {
            // TODO: create contrastLevel = ((100.0 + coef)/100.0)^2
            // TODO: all pixels should have adjusted contrast, according to the formula
            // newPixel = (((oldPixel/255.0 - 0.5) * contastLevel) + 0.5) * 255.0
            // don't forget to check all the values!

            return null;
        }

        // checks if the value is in the correct interval [0, 255] 
        private byte checkPixelValue(int pixel)
        {
            // TODO: check pixel value and return 0, 255 or pixel itself 
            return (byte)pixel;
        }
    }
}
