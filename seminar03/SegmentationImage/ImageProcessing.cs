using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegmentationImage
{
    class ImageProcessing
    {

        // converts image to gray-scale
        private byte[] setGrayscale(byte[] originalImage)
        {
            /* 
             * TODO: создать новый byte[], который будет создрежать градации серого
             * TODO: записать в него значения по формуле: x = 0.299R + 0.587G + 0.114B
             * TODO: вернуть это значение
             * note: в С# дефолтным сичтается формат Bgra
             */
            return null;
        }

        // applies erosion to the image
        public byte[] setBinary(byte[] originalImage)
        {
            /* 
             * TODO: создать новый byte[]
             * TODO: задать порог для бинаризации. по дефолту - 150. потом подобрать.
             * TODO: все пиксле > 150 заменить на белый, иначе - черный
             * TODO: прияменять это к черно-белому изображению
             */
            return null;
        }

        // applies erosion to the image
        public byte[] setErosion(byte[] originalImage)
        {
            /* 
             * TODO: создать новый byte[]
             * TODO: применить операцию эрозии к бинарному изображению
             * структурный элемент - крест 3х3
             */
            return null;
        }

        // applies dilatation to the image
        public byte[] setDilatation(byte[] originalImage)
        {
            /* 
             * TODO: создать новый byte[]
             * TODO: применить операцию дилатации к бинарному изображению
             * структурный элемент - кольцо 5х5             
             */
            return null;
        }

        // detectes edges at the image
        public byte[] setEdges(byte[] originalImage)
        {
            /* 
             * TODO: создать новый byte[]
             * TODO: придумать, как выделить края
             * видимо, знания морфологических операций не помешают ^_^
             */
            return null;
        }
    }
}
