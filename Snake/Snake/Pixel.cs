using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    
    public readonly struct Pixel
    {
        // АSCII символ що заповнює консольне значення 
        public const char PixelChar = '█';
        public Pixel(int x, int y, ConsoleColor color, int pixelSize = 3 )
        {
            X = x;
            Y = y;
            Color = color;
            PixelSize = pixelSize;
        }

        public int X { get; } 
        
        public int Y { get; }

        public ConsoleColor Color { get; }
        public int PixelSize { get; }

        // метод що виводить зображення PixelChar
        public void Drow()
        {
            Console.ForegroundColor= Color;
            for(int i = 0; i<PixelSize; i++) 
            { 
                for(int j = 0; j < PixelSize; j++)
                {
                    Console.SetCursorPosition(X * PixelSize + i, Y * PixelSize +j);
                    Console.Write(PixelChar);
                }
            }
        }
        // метод що стирає зображення 
        public void Clear()
        {
            for (int i = 0; i < PixelSize; i++)
            {
                for (int j = 0; j < PixelSize; j++)
                {
                    Console.SetCursorPosition(X * PixelSize + i, Y * PixelSize + j);
                    Console.Write(' ');
                }
            }
        }

    }
}
