using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace SpaceInvaders
{
    class Gameboard
    {
        public int Width;
        public int Height => Width;
        public int Margin = 4;
        public Rectangle MinionBoundary;
        public Rectangle GameBoard;
        public Player Player;
        private DateTime MinionShotCreated = DateTime.Now;
        private DateTime PlayerShotCreated = DateTime.Now;
        private TimeSpan PlayerShotInterval = TimeSpan.FromMilliseconds(1500);
        private TimeSpan MinionShotInterval = TimeSpan.FromMilliseconds(2500);


        public Gameboard(int width, List<GameObject> gameObjects)
        {
            Width = width;
            MinionBoundary = new Rectangle(Width - Margin, Height - Margin, new Point(1, 1));
            GameBoard = new Rectangle(Width, Height, new Point(0, 0));
        }
        public void CreateGameBoard(List<GameObject> gameObjects)
        {
            Console.SetWindowSize(Width + 1, Width + 1);
            Console.SetBufferSize(Width + 1, Width + 1);
            Console.CursorVisible = false;
            CreatePlayer(gameObjects);
            CreateMinions(gameObjects);
        }
        private void CreatePlayer(List<GameObject> gameObjects)
        {
            Player = new Player(Width / 2, Width - 10);
            gameObjects.Add(Player);
        }
        private void CreateMinions(List<GameObject> gameObjects)
        {
            for (int minionRow = 0; minionRow < 5; minionRow++)
                for (int minionCount = 0; minionCount < 10; minionCount++)
                    gameObjects.Add(new Minion(Margin + ((Width - Margin * 2) / 10) * minionCount, Margin + minionRow * 5));
        }
        public void CreateShot(List<GameObject> gameObjects, Player player)
        {
            Shot shot = new Shot(player.PPoint.X + player.Width / 2, player.PPoint.Y + -1);
            if (PlayerShotCreated <= DateTime.Now)
            {
                gameObjects.Add(shot);
                PlayerShotCreated = DateTime.Now + PlayerShotInterval;
            }
        }
        public void MoveShots(List<GameObject> gameObjects)
        {
            foreach (Shot element in gameObjects.OfType<Shot>())
            {
                if (element.IsMovementValid(element.Vector, GameBoard))
                    element.Vector.ApplyVector(element.PPoint);
            }
            ShotHitsGameObject(gameObjects);
        }
        public void ShotHitsGameObject(List<GameObject> gameObjects)
        {
            foreach (GameObject gameObject in gameObjects.ToArray())
                foreach (Shot shot in gameObjects.OfType<Shot>().ToArray())
                    if (shot != gameObject && gameObject.OverlapsRectangle(shot))
                    {
                        RemoveLifeOfGameObjects(gameObjects, shot);
                        RemoveLifeOfGameObjects(gameObjects, gameObject);
                    }
        }
        public void RemoveLifeOfGameObjects(List<GameObject> gameObjects, GameObject gameObject)
        {
            gameObject.Life -= 1;
            if (gameObject.Life == 0)
                gameObjects.Remove(gameObject);
        }
        public int CalcMinionSpeed(List<GameObject> gameObjects)
        {
            return gameObjects.OfType<Minion>().Count() * 15; //.Count(o => o is Minion)
        }
        public void MovePlayer(Player player)
        {
            if (player.IsMovementValid(player.Vector, GameBoard))
                player.Vector.ApplyVector(player.PPoint);
        }
        public bool LastInCol(List<GameObject> gameObjects, Minion minion)
        {
            foreach (Minion element in gameObjects.OfType<Minion>().Where(o => o.PPoint.X == minion.PPoint.X))
                if (minion.PPoint.Y < element.PPoint.Y)
                {
                    return false;
                }

            return true;
        }
        public void CreateMinionShots(List<GameObject> gameObjects)
        {
            Random random = new Random();
            int range = 30;
            if (MinionShotCreated > DateTime.Now)
                return;

            foreach (Minion minion in gameObjects.OfType<Minion>().ToArray())
                if (LastInCol(gameObjects, minion) && random.Next(range) > range / 2)
                {
                    Shot shot = new Shot(minion.PPoint.X + minion.Width / 2, minion.PPoint.Y + minion.Height + +1);
                    shot.Vector.Direction.X = 0;
                    shot.Vector.Direction.Y = 1;
                    gameObjects.Add(shot);
                }
            MinionShotCreated = DateTime.Now + MinionShotInterval;

        }
        public void MoveMinions(List<GameObject> gameObjects)
        {
            GetMinionDirection(gameObjects);
            MoveMinionsDirection(gameObjects);
            CreateMinionShots(gameObjects);
        }
        public void MoveMinionsDirection(List<GameObject> gameObjects)
        {
            foreach (Minion element in gameObjects.OfType<Minion>())
                element.Vector.ApplyVector(element.PPoint);
        }
        public void GetMinionDirection(List<GameObject> gameObjects)
        {
            foreach (Minion element in gameObjects.OfType<Minion>())
            {
                if (!element.IsMovementValid(element.Vector, MinionBoundary))
                {
                    foreach (Minion item in gameObjects.OfType<Minion>())
                        item.Vector.Direction.X *= -1;
                    return;
                }
            }
        }

        public string GetRowString(List<GameObject> gameObjects, int row)
        {
            StringBuilder result = new StringBuilder();
            var lineObjects = gameObjects.Where(o => o.IsInRow(row)).OrderBy(o => o.PPoint.X).ToArray();
            foreach (var GameObject in lineObjects)
            {
                result.Append(' ', GameObject.PPoint.X - result.Length);
                result.Append(GameObject.CharacterDisplayed, GameObject.Width);
            }
            result.Append(' ', Width - result.Length);
            return result.ToString();
        }
        public void RendergameboardObjects(List<GameObject> gameObjects)
        {
            Console.SetCursorPosition(0, 0);
            for (int index = 0; index < Width; index++)
            {
                Console.WriteLine(GetRowString(gameObjects, index));
            }
        }
    }
}
