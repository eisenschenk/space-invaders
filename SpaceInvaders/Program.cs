using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace SpaceInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<GameObject> gameObjects = new List<GameObject>();
            Gameboard gameboard = new Gameboard(100, gameObjects);
            int timeBetweenMinionRender = 750;
            int timeBetweenShotRender = 75;
            DateTime nextMinionMovement = DateTime.Now.AddMilliseconds(timeBetweenMinionRender);
            DateTime nextShotMovement = DateTime.Now.AddMilliseconds(timeBetweenShotRender);


            gameboard.CreateGameBoard(gameObjects);
           
            

            while (gameObjects.Count > 0)
            {
                timeBetweenMinionRender = gameboard.CalcMinionSpeed(gameObjects);
                if (nextMinionMovement <= DateTime.Now)
                {
                    gameboard.MoveMinions(gameObjects);
                    nextMinionMovement = DateTime.Now.AddMilliseconds(timeBetweenMinionRender); ;
                }
                if (nextShotMovement <= DateTime.Now)
                {
                    gameboard.MoveShots(gameObjects);
                    nextShotMovement = DateTime.Now.AddMilliseconds(timeBetweenShotRender);
                }
                gameboard.RendergameboardObjects(gameObjects);

                if (Console.KeyAvailable)
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.A:
                        case ConsoleKey.LeftArrow:
                           gameboard.Player.Vector.Direction.X = -1;
                            gameboard.MovePlayer(gameboard.Player);
                            break;
                        case ConsoleKey.D:
                        case ConsoleKey.RightArrow:
                            gameboard.Player.Vector.Direction.X = +1;
                            gameboard.MovePlayer(gameboard.Player);
                            break;
                        case ConsoleKey.Spacebar:
                            gameboard.CreateShot(gameObjects, gameboard.Player);
                            break;
                    }
            }
        }
    }
}
