using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Shot : GameObject
    {
        public override char CharacterDisplayed => '|';
       
        public Shot(int x, int y) : base(1, 1, new Point(x, y), new Vector(2, new Point(0, -1)))
        {
            Life = 1;
        }
    }
}
