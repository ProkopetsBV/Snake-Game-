using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Snake
{
    internal class Program
    {
        // налаштовуємо голобальні змінні 
        // висота ігрової карти 
        private const int mapHeight = 20;
        // ширина ігрової карти 
        private const int mapWidth = 30;
        // глобальна змінна яка зберігає колір бортику 
        private const ConsoleColor colorBorder = ConsoleColor.Green;

        private const ConsoleColor bodyColor = ConsoleColor.Yellow;
        private const ConsoleColor headColor = ConsoleColor.Blue;

        private const ConsoleColor foodColor = ConsoleColor.Red;

        private const int miliSecFrame = 100;

        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            // налаштовуємо ширину та висоту для буфера та вікна 
            // добавляємо одиницю щоб те що ми малюємо поміщалося в нашу робочу область
            SetWindowSize(mapWidth * 3 + 1, mapHeight * 3 + 1);
            SetBufferSize(mapWidth * 3 + 1, mapHeight * 3 + 1);
            // відключаємо видимість курсору
            CursorVisible = false;

            while (true)
            {
                StartGame();
                Thread.Sleep(2000);
                ReadKey();
            }

            Console.ReadKey();
        }

        static void StartGame()
        {
            Clear();
            // виводимо рамку гри 
            DrowBorder();

            Direction curentMovement = Direction.Right;
            // Створюємо екземпляр класу зміюки 
            SnakeInGame snake = new SnakeInGame(10, 5, headColor, bodyColor);

            Pixel food = GenarateFood(snake);
            food.Drow();

            Stopwatch sw = new Stopwatch();

            // рахунок
            int score = 0;

            // start game =) 
            while (true)
            {
                sw.Restart();

                Direction oldMove = curentMovement;

                while (sw.ElapsedMilliseconds <= miliSecFrame)
                {
                    if (curentMovement == oldMove)
                    {
                        curentMovement = ReadMovement(curentMovement);
                    }

                }
                if (snake.Head.X == food.X && snake.Head.Y == food.Y )
                {
                    // якщо голова потрапила на жрачку то параметер їсти = true
                    snake.Move(curentMovement, true);
                    // якщо зжерли їжу, то генеримо нову їжу 
                    food = GenarateFood(snake);
                    food.Drow();
                    // збільшуємо рахунок 
                    ++score;

                    Task.Run(() => Beep(1200, 250));
                }
                else
                {
                    // якщо жрачки немає то просто повземо далі 
                    snake.Move(curentMovement);
                }


                // gameover 
                if (snake.Head.X == mapWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == mapHeight - 1
                    || snake.Head.Y == 0
                    || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y)
                    ) { break; }

            }
            Task.Run(() => Beep(200, 450));

            Clear();
            SetCursorPosition(mapWidth, mapHeight);
            Console.WriteLine($"Game Over !!!  SCORE : {score}");
        }

        // метод для вивиоду границь вікна 
        static void DrowBorder()
        {
            //-лінія
            for (int i = 0; i < mapWidth; i++)
            {
                new Pixel(i, 0, colorBorder).Drow();
                new Pixel(i, mapHeight - 1, colorBorder).Drow();
            }
            //-стовбчик
            for (int i = 0; i < mapHeight; i++)
            {
                new Pixel(0, i, colorBorder).Drow();
                new Pixel(mapWidth - 1, i, colorBorder).Drow();
            }


        }

        // метод для курування стрілочками
        static Direction ReadMovement(Direction curentDirection)
        {
            if (!KeyAvailable)
                return curentDirection;
            ConsoleKey key = Console.ReadKey(true).Key;

            curentDirection = key switch
            {
                ConsoleKey.UpArrow when curentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when curentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when curentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when curentDirection != Direction.Left => Direction.Right,
                _ => curentDirection
            };
            return curentDirection;


        }

        // food for snake 
        static Pixel GenarateFood(SnakeInGame snake)
        {
            Pixel food;

            do
            {
                food = new Pixel(random.Next(1, mapWidth - 2), random.Next(1, mapHeight - 2), foodColor);
            } while (snake.Head.X == food.X && snake.Head.Y == food.Y
                        || snake.Body.Any(b => b.X == food.X && b.Y == food.Y));
            return food;    
        }
    }
}
