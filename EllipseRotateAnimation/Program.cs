using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidja;

namespace EllipseRotateAnimation
{
    class Program
    {
        static void Main(string[] args)
        {
            Vidja.Vidja.Generate(new Ellipses());
        }
    }

    internal class Ellipses : IVidja
    {
        public Bitmap RenderFrame(double t)
        {
            var bmp = new Bitmap(Width, Height,PixelFormat.Format24bppRgb);
            var graphics = Graphics.FromImage(bmp);
            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var halfHeight = Height/2;
            var halfWidth = Width/2;
            var numOfEllipses = 3*halfHeight/4;
            var center = new PointF(halfWidth,halfHeight);
            using (var rotate = new Matrix())
            {
                //used to restore g.Transform previous state
                var container = graphics.BeginContainer();

                for(var i=0; i < numOfEllipses ; i++)
                {
                    rotate.RotateAt((float)(t*180/500), center);
                    //add it to g.Transform
                    graphics.Transform = rotate;

                    //draw what you want
                    graphics.DrawEllipseAtCentre(new Pen(Color.FromArgb(50,255,255,255), 1), center.X, center.Y, Height-i, halfHeight-i);
                    
                }

                //restore g.Transform state
                graphics.EndContainer(container);
            }

            return bmp;
        }

        public int Width { get; } = 1920;
        public int Height { get; } = 1080;
        public int Fps { get; } = 30;
        public double Duration { get; } = 10;
    }

    public static class GraphicsExtension
    {
        public static void DrawEllipseAtCentre(this Graphics g, Pen pen, float x, float y, float width, float height)
        {

            g.DrawEllipse(pen,x - (width/2),y-(height/2),width,height);
        }
    }
}
