using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Rectangle
    {
        public int Width;
        public int Height;
        public Point PPoint;


        public Rectangle(int width, int height, Point point)
        {
            Width = width;
            Height = height;
            PPoint = point;
        }

        public bool IncludesPoint(Point point)
        {
            return (point.X >= PPoint.X && point.X <= PPoint.X + Width && point.Y >= PPoint.Y && point.Y <= PPoint.Y + Height);
        }
        public bool IncludesRectangle(Rectangle rectangle)
        {
            return (IncludesPoint(rectangle.PPoint) && IncludesPoint(new Point(rectangle.PPoint.X + rectangle.Width, rectangle.PPoint.Y + rectangle.Height)));
        }
        public bool OverlapsRectangle(Rectangle rectangle)
        {
            return (IncludesPoint(rectangle.PPoint) || IncludesPoint(new Point(rectangle.PPoint.X + rectangle.Width, rectangle.PPoint.Y + rectangle.Height)));
        }
        
    }
}
