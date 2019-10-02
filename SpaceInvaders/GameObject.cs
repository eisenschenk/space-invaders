using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    abstract class GameObject : Rectangle
    {
        public Vector Vector;
        public int Life;
        public GameObject(int width, int height, Point point, Vector vector) : base(width, height, point)
        {
            Vector = vector;
        }

        public abstract char CharacterDisplayed { get; }
        public bool IsMovementValid(Vector direction, Rectangle boundary)
        {
            int leftOfElement = PPoint.X + direction.Direction.X;
            int rightOfElement = PPoint.X + Width + direction.Direction.X;
            int leftBoundary = boundary.PPoint.X;
            int rightBoundary = boundary.PPoint.X + boundary.Width;

            return leftOfElement > leftBoundary && rightOfElement < rightBoundary;
        }

        public bool IsInRow(int row) => PPoint.Y <= row && row < PPoint.Y + Height;


        //public bool IsInRow(int row)
        //{
        //    for (int index = PositionY; index < PositionY + Height; index++)
        //        if (index == row)
        //            return true;
        //    return false;
        //}
    }
}
