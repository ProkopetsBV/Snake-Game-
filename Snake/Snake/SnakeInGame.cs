using System;
using System.Collections.Generic;

namespace Snake
{
    internal class SnakeInGame
    {
        private readonly ConsoleColor headColor;
        private readonly ConsoleColor bodyColor;

        /// <summary>
        /// консткуктор
        /// </summary>
        /// <param name="initX"></param>
        /// <param name="initY"></param>
        /// <param name="headColor"></param>
        /// <param name="bodyColor"></param>
        /// <param name="startLenghtBody"></param>
        public SnakeInGame(int initX, int initY, ConsoleColor headColor, ConsoleColor bodyColor, int startLenghtBody = 3)
        {
            this.headColor = headColor;
            this.bodyColor = bodyColor;

            Head = new Pixel(initX, initY, headColor);

            // цикл для відображення змії на екрані 
            for (int i = startLenghtBody; i >= 0; i--)
            {
                Body.Enqueue(new Pixel(Head.X - i - 1, initY, bodyColor));
            }
            // викликаємо метод відображення зміюки 
            Draw();
        }

        // Піксель що відповідає за голову змії 
        public Pixel Head { get; private set; }
        // створюєсо обєкт черги, який буде відображати тільо змії
        public Queue<Pixel> Body { get; } = new Queue<Pixel>();

        // метод для вивиоду змії на екран 
        public void Draw()
        {
            Head.Drow();
            foreach (Pixel pixel in Body)
            {
                pixel.Drow();
            }
        }
        // метод для очищення екрану від елементів змії 
        public void Clear()
        {
            Head.Clear();
            foreach (Pixel pixel in Body)
            {
                pixel.Clear();
            }
        }

        // метод для того щоб зміюка повзалла 
        public void Move(Direction direction, bool eat = false)
        {
            // видаляємо зміюку з екрану
            Clear();

            // добавляємо перший піксель в чергу 
            Body.Enqueue(new Pixel(Head.X, Head.Y, bodyColor));

            // видаляємо останній піксель з черги 
            if (!eat)
                Body.Dequeue();

            Head = direction switch
            {
                Direction.Right => new Pixel(Head.X + 1, Head.Y, headColor),
                Direction.Left => new Pixel(Head.X - 1, Head.Y, headColor),
                Direction.Up => new Pixel(Head.X, Head.Y - 1, headColor),
                Direction.Down => new Pixel(Head.X, Head.Y + 1, headColor),
                _ => Head
            };
            Draw();
        }
    }
}
