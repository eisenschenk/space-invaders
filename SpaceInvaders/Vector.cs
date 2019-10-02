using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Vector
    {
        public int Speed;
        public Point Direction;

        public Vector(int speed, Point direction)
        {
            Speed = speed;
            Direction = direction;            
        }

        public void ApplyVector(Point point)
        {
            point.X += Direction.X * Speed;
            point.Y += Direction.Y * Speed;
        }

    }
}
