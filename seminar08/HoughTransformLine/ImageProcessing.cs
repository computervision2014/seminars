using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoughTransformLine
{
    class ImageProcessing
    {
        #region Grayscale
        // converts image to gray-scale
        public byte[] setGrayscale(byte[] originalImage)
        {
            byte[] grayscaleImage = new byte[originalImage.Length / 4];
            for (int i = 0; i < originalImage.Length; i += 4)
            {
                grayscaleImage[i / 4] = (byte)(0.114 * originalImage[i] + 0.587 * originalImage[i + 1] + 0.299 * originalImage[i + 2]);
            }
            return grayscaleImage;
        }
        #endregion

        #region GaussianBlur
        // standard deviation, number of boxes
        private int[] boxesForGauss(double sigma, int n)
        {

            double wIdeal = Math.Sqrt((12 * sigma * sigma / n) + 1);// Ideal averaging filter width 
            int wl = (int)Math.Floor(wIdeal); if (wl % 2 == 0) wl--;
            int wu = wl + 2;
            double mIdeal = (12 * sigma * sigma - n * wl * wl - 4 * n * wl - 3 * n) / (-4 * wl - 4);
            int m = (int)Math.Round(mIdeal);
            int[] sizes = new int[n];
            for (int i = 0; i < n; i++)
            {
                sizes[i] = (i < m ? wl : wu);
            }
            return sizes;
        }

        public byte[] blurGauss_4(byte[] originalImage, int w, int h, double r)
        {
            byte[] blurImage = new byte[originalImage.Length];
            var bxs = boxesForGauss(r, 3);
            boxBlur_4(originalImage, blurImage, w, h, (bxs[0] - 1) / 2);
            boxBlur_4(blurImage, originalImage, w, h, (bxs[1] - 1) / 2);
            boxBlur_4(originalImage, blurImage, w, h, (bxs[2] - 1) / 2);

            return blurImage;
        }

        private void boxBlur_4(byte[] originalImage, byte[] blurImage, int w, int h, int r)
        {
            for (int i = 0; i < originalImage.Length; i++)
            {
                blurImage[i] = originalImage[i];
            }
            boxBlurH_4(blurImage, originalImage, w, h, r);
            boxBlurT_4(originalImage, blurImage, w, h, r);
        }
        private void boxBlurH_4(byte[] originalImage, byte[] blurImage, int w, int h, int r)
        {
            double iarr = 1 / (r + r + 1);
            for (int i = 0; i < h; i++)
            {
                int ti = i * w, li = ti, ri = ti + r;
                byte fv = originalImage[ti], lv = originalImage[ti + w - 1];
                double val = (r + 1) * fv;

                for (int j = 0; j < r; j++) val += originalImage[ti + j];
                for (int j = 0; j <= r; j++)
                {
                    val += originalImage[ri++] - fv;
                    blurImage[ti++] = (byte)Math.Round(val * iarr);
                }

                for (int j = r + 1; j < w - r; j++)
                {
                    val += originalImage[ri++] - originalImage[li++];
                    blurImage[ti++] = (byte)Math.Round(val * iarr);
                }

                for (int j = w - r; j < w; j++)
                {
                    val += lv - originalImage[li++];
                    blurImage[ti++] = (byte)Math.Round(val * iarr);
                }
            }
        }

        private void boxBlurT_4(byte[] originalImage, byte[] blurImage, int w, int h, int r)
        {
            double iarr = 1 / (r + r + 1);
            for (int i = 0; i < w; i++)
            {
                int ti = i, li = ti, ri = ti + r * w;
                byte fv = originalImage[ti], lv = originalImage[ti + w * (h - 1)];
                double val = (r + 1) * fv;

                for (int j = 0; j < r; j++) val += originalImage[ti + j * w];
                for (int j = 0; j <= r; j++)
                {
                    val += originalImage[ri++] - fv;
                    blurImage[ti++] = (byte)Math.Round(val * iarr);
                }

                for (int j = r + 1; j < w - r; j++)
                {
                    val += originalImage[ri++] - originalImage[li++];
                    blurImage[ti++] = (byte)Math.Round(val * iarr);
                }

                for (int j = w - r; j < w; j++)
                {
                    val += lv - originalImage[li++];
                    blurImage[ti++] = (byte)Math.Round(val * iarr);
                }
            }
        }
        #endregion

        private double hypotenuse(double v1, double v2){
            return Math.Sqrt(v1 * v1 + v2 * v2);
        } 

        public ArrayData houghTransform(ArrayData originalImage, int thetaAxisSize, int rAxisSize, int minContrast)
        {
            int width = originalImage.width;
            int height = originalImage.height;            
            int maxRadius = (int)Math.Ceiling(hypotenuse(width, height));
            int halfRAxisSize = rAxisSize >> 1;
            var outputImage = new ArrayData(thetaAxisSize, rAxisSize);
            // x output ranges from 0 to pi
            // y output ranges from -maxRadius to maxRadius
            double[] sinTable = new double[thetaAxisSize];
            double[] cosTable = new double[thetaAxisSize];
            for (int theta = thetaAxisSize - 1; theta >= 0; theta--)
            {
              double thetaRadians = theta * Math.PI / thetaAxisSize;
              sinTable[theta] = Math.Sin(thetaRadians);
              cosTable[theta] = Math.Cos(thetaRadians);
            }
 
            for (int y = height - 1; y >= 0; y--)
            {
              for (int x = width - 1; x >= 0; x--)
              {
                  if (originalImage.contrast(x, y, minContrast))
                {
                  for (int theta = thetaAxisSize - 1; theta >= 0; theta--)
                  {
                    double r = cosTable[theta] * x + sinTable[theta] * y;
                    int rScaled = (int)Math.Round(r * halfRAxisSize / maxRadius) + halfRAxisSize;
                    outputImage.accumulate(theta, rScaled, 1);
                  }
                }
              }
            }
            return outputImage;
          }

    }
}
