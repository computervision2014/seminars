using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Collections;

namespace EmguFormsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                Image<Bgr, Byte> originalImage = new Image<Bgr, byte>(Openfile.FileName);
                Image<Gray, Byte> grayImage = originalImage.Convert<Gray, Byte>();

                grayImage._SmoothGaussian(3);
                CvInvoke.cvAdaptiveThreshold(grayImage, grayImage, 255, ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_MEAN_C, THRESH.CV_THRESH_BINARY, 75, 10);
                grayImage._Not();
                //ArrayList lines = new ArrayList();
                LineSegment2D[] lines = grayImage.HoughLinesBinary(
                    1, //Distance resolution in pixel-related units
                    Math.PI / 45.0, //Angle resolution measured in radians.
                    20, //threshold
                    30, //min Line width
                    10 //gap between lines
                    )[0]; //Get the lines from the first channel

                #region draw lines                
                foreach (LineSegment2D line in lines)
                    originalImage.Draw(line, new Bgr(Color.Red), 1);
                #endregion

                pictureBox1.Image = originalImage.ToBitmap();
            }
        }

    }
}