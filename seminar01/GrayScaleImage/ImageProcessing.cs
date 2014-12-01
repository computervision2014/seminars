using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrayScaleImage
{
    class ImageProcessing
    {
        // converts image to gray-scale
        public byte[] setGrayscale(byte[] originalImage)
        {
            /* 
             * TODO: создать новый byte[], который будет создрежать градации серого
             * TODO: записать в него значения по формуле: x = 0.299R + 0.587G + 0.114B
             * TODO: вернуть это значение
             * note: в С# дефолтным сичтается формат Bgra
             */
            byte[] arr = new byte[originalImage.Length];
            byte[] res = new byte[originalImage.Length / 4];
            int j = 0;
            Array.Copy(originalImage, arr, originalImage.Length);
            for (int i = 0; i < arr.Length; i+=4)
            {
               arr[i] =(byte)(arr[i] * 0.114);
               arr[i + 1] = (byte)(arr[i + 1] * 0.587);
               arr[i + 2] = (byte)(arr[i + 2] * 0.299);
               res[j] = (byte)( arr[i] + arr[i + 1] + arr[i + 2]);
               j++;
            }
            return res;
        }

        // inverts image
        public byte[] setInvert(byte[] originalImage)
        {   
            /* 
             * TODO: создать новый byte[], который будет содержать инвертированное изображение, т.е. 255 минус текущее значение
             * TODO: вернуть это значение
             */
            byte[] arr = new byte[originalImage.Length];
            Array.Copy(originalImage, arr, originalImage.Length);
            for (int i = 0; i < arr.Length; i ++ )
            {
                arr[i] = (byte)(255 - arr[i]);
            }
            return arr;
        }

        // shows only red channel
        public byte[] setRedFilter(byte[] originalImage)
        {
            /* 
             * TODO: создать новый byte[], который будет содержать инвертированное изображение, т.е. каналы G и B занулить
             * TODO: вернуть это значение
             */
            byte [] arr = new byte[originalImage.Length];
            Array.Copy(originalImage, arr, originalImage.Length);
            for (int i = 0; i < arr.Length; i+=4)
            {
                arr[i] = 0;
                arr[i+1] = 0;
            }
            return arr;
        }
    }
}
