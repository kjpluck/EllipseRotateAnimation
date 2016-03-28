using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
        public int Width { get; } = 1920;
        public int Height { get; } = 1080;
        public int Fps { get; } = 30;
        public double Duration { get; } = 20;

        public Bitmap RenderFrame(double t)
        {
            var bmp = new Bitmap(Width, Height,PixelFormat.Format24bppRgb);
            var graphics = Graphics.FromImage(bmp);
            var halfHeight = Height/2;
            var halfWidth = Width/2;
            var numOfEllipses = 3*halfHeight/4;
            var center = new PointF(halfWidth, halfHeight);

            using (var rotate = new Matrix())
            {
                var container = graphics.BeginContainer();

                for(var i=0; i < numOfEllipses ; i++)
                {
                    rotate.RotateAt((float)(t*180/1000), center);

                    graphics.Transform = rotate;

                    graphics.DrawEllipseAtCentre(new Pen(Color.FromArgb(50,255,255,255), 1), center.X, center.Y, Height-i, halfHeight-i);
                    
                }

                graphics.EndContainer(container);
            }

            return bmp;
        }
    }

    public static class GraphicsExtension
    {
        public static void DrawEllipseAtCentre(this Graphics g, Pen pen, float x, float y, float width, float height)
        {
            g.DrawEllipse(pen,x - (width/2),y-(height/2),width,height);
        }
    }
}
