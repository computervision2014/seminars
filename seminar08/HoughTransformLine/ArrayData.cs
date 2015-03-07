using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoughTransformLine
{
    public class ArrayData
  {
    public byte[] dataArray;
    public int width;
    public int height;
 
    public ArrayData(int width, int height)
    {
      this.dataArray = new byte[width * height];
      this.width = width;
      this.height = height;
    }
 
    public ArrayData(byte[] dataArray, int width, int height)
    {
      this.dataArray = dataArray;
      this.width = width;
      this.height = height;
    }
 
    public int get(int x, int y)
    {  return dataArray[y * width + x];  }
 
    public void set(int x, int y, byte value)
    {  dataArray[y * width + x] = value;  }
 
    public void accumulate(int x, int y, byte delta)
    {  set(x, y, (byte)(get(x, y) + delta));  }
 
    public bool contrast(int x, int y, int minContrast)
    {
      int centerValue = get(x, y);
      for (int i = 8; i >= 0; i--)
      {
        if (i == 4)
          continue;
        int newx = x + (i % 3) - 1;
        int newy = y + (i / 3) - 1;
        if ((newx < 0) || (newx >= width) || (newy < 0) || (newy >= height))
          continue;
        if (Math.Abs(get(newx, newy) - centerValue) >= minContrast)
          return true;
      }
      return false;
    }
 
    public int getMax()
    {
      int max = dataArray[0];
      for (int i = width * height - 1; i > 0; i--)
        if (dataArray[i] > max)
          max = dataArray[i];
      return max;
    }
  }
}
