using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Minion : GameObject
    {
        public override char CharacterDisplayed => '#';
        
        public Minion(int x, int y) : base(4, 3, new Point(x, y), new Vector(1, new Point(-1, 0)))
        {
            Life = 1;
        }


    }
}
