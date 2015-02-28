using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastGaussianBlur
{
    class ImageProcessing
    {
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

        public byte[] blurGauss_1(byte[] originalImage, int w, int h, double r)
        {
            byte[] blurImage = new byte[originalImage.Length];
            int rs = (int)Math.Ceiling(r * 2.57);
            for (int i = 0; i < h; i++) {
                for (int j = 0; j < w; j++) {
                    double val = 0, wsum = 0;
                    for (int iy = i - rs; iy < i + rs + 1; iy++ )
                    {
                        for (int ix = j - rs; ix < j + rs + 1; ix++)
                        {
                            int x = Math.Min(w-1, Math.Max(0, ix));
                            int y = Math.Min(h-1, Math.Max(0, iy));
                            int dsq = (ix - j) * (ix - j) + (iy - i) * (iy - i);
                            double wght = Math.Exp( -dsq / (2*r*r) ) / (Math.Sqrt(Math.PI*2)*r);
                            val += originalImage[y * w + x] * wght; wsum += wght;
                        }
                    }
                    blurImage[i * w + j] = (byte)Math.Round(val/wsum);
                }
            } 
            return blurImage;
        }


        public byte[] blurGauss_2(byte[] originalImage, int w, int h, double r)
        {
            byte[] blurImage = new byte[originalImage.Length];
            var bxs = boxesForGauss(r, 3);
            boxBlur_2(originalImage, blurImage, w, h, (bxs[0] - 1) / 2);
            boxBlur_2(blurImage, originalImage, w, h, (bxs[1] - 1) / 2);
            boxBlur_2(originalImage, blurImage, w, h, (bxs[2] - 1) / 2);


            return blurImage;
        }

        // standard deviation, number of boxes
        private int[] boxesForGauss(double sigma, int n){

            double wIdeal = Math.Sqrt((12 * sigma * sigma / n) + 1);// Ideal averaging filter width 
            int wl = (int)Math.Floor(wIdeal); if (wl % 2 == 0) wl--;
            int wu = wl+2;
            double mIdeal = (12 * sigma * sigma - n * wl * wl - 4 * n * wl - 3 * n) / (-4 * wl - 4);
            int m = (int)Math.Round(mIdeal);
            int[] sizes = new int[n];
            for (int i = 0; i < n; i++ )
            {
                sizes[i] = (i< m ? wl : wu);
            }
            return sizes;
        }

        private void boxBlur_2(byte[] originalImage, byte[] blurImage, int w, int h, int r){
            for (int i = 0; i < h; i++ )
            {
                for (int j = 0; j < w; j++) {
                    double val = 0;
                    for (int iy = i - r; iy < i + r + 1; iy++ )
                    {
                        for (int ix = j - r; ix < j + r + 1; ix++ )
                        {
                            int x = Math.Min(w-1, Math.Max(0,ix));
                            int y = Math.Min(h - 1, Math.Max(0, iy));
                            val += originalImage[y * w + x];
                        }
                    }
                    blurImage[i * w + j] = (byte)(val / ((r + r + 1) * (r + r + 1)));
                }
            }
           
        }

        public byte[] blurGauss_3(byte[] originalImage, int w, int h, double r)
        {
            byte[] blurImage = new byte[originalImage.Length];
            var bxs = boxesForGauss(r, 3);
            boxBlur_3(originalImage, blurImage, w, h, (bxs[0] - 1) / 2);
            boxBlur_3(blurImage, originalImage, w, h, (bxs[1] - 1) / 2);
            boxBlur_3(originalImage, blurImage, w, h, (bxs[2] - 1) / 2);

            return blurImage;
        }

        private void boxBlur_3(byte[] originalImage, byte[] blurImage, int w, int h, int r)
        {
            for (int i = 0; i < originalImage.Length; i++ )
            {
                blurImage[i] = originalImage[i];
            }
            boxBlurH_3(blurImage, originalImage, w, h, r);
            boxBlurT_3(originalImage, blurImage, w, h, r);
        }

        private void boxBlurH_3(byte[] originalImage, byte[] blurImage, int w, int h, int r)
        {
            for (int i = 0; i < h; i++) {
                for (int j = 0; j < w; j++) {
                    double val = 0;

                    for (int ix = j - r; ix < j + r + 1; ix++ )
                    {
                        int x = Math.Min(w - 1, Math.Max(0, ix));
                        val+= originalImage[i*w+x];
                    }
                    blurImage[i * w + j] = (byte)(val / (r + r + 1));
                }
            }
        }

        private void boxBlurT_3(byte[] originalImage, byte[] blurImage, int w, int h, int r)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    double val = 0;
                    for (int iy = i - r; iy < i + r + 1; iy++)
                    {
                        int y = Math.Min(h - 1, Math.Max(0, iy));
                        val += originalImage[y * w + j];
                    }
                    blurImage[i * w + j] = (byte)(val / (r + r + 1));
                }
            }
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

                for(int j = 0; j<r;j++) val+=originalImage[ti+j];
                for (int j = 0; j <= r; j++) 
                {
                    val += originalImage[ri++] - fv;
                    blurImage[ti++] = (byte)Math.Round(val * iarr);
                }

                for (int j = r+1; j < w - r; j++)
                {
                    val += originalImage[ri++] - originalImage[li++];
                    blurImage[ti++] = (byte)Math.Round(val * iarr);
                }

                for (int j = w-r; j < w; j++)
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
                int ti = i, li = ti, ri = ti + r*w;
                byte fv = originalImage[ti], lv = originalImage[ti + w * (h - 1)];
                double val = (r + 1) * fv;

                for (int j = 0; j < r; j++) val += originalImage[ti + j*w];
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
        
    }
}
