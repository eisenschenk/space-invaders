using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Player : GameObject
    {
        public override char CharacterDisplayed => '=';
      
        public Player(int x, int y) : base(5, 2, new Point(x, y), new Vector(1, new Point(0, 0)))
        {
            Life = 3;
        }


    }
}
