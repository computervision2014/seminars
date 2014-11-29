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
            return null;
        }

        // inverts image
        public byte[] setInvert(byte[] originalImage)
        {   
            /* 
             * TODO: создать новый byte[], который будет содержать инвертированное изображение, т.е. 255 минус текущее значение
             * TODO: вернуть это значение
             */         
            return null;
        }

        // shows only red channel
        public byte[] setRedFilter(byte[] originalImage)
        {
            /* 
             * TODO: создать новый byte[], который будет содержать инвертированное изображение, т.е. каналы G и B занулить
             * TODO: вернуть это значение
             */  
            return null;
        }
    }
}
