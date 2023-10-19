using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    partial class Program
    {
        private static void GameOver()
        {
            // назначить gameRunning в значение false
            // стереть все через graphics.Clear
            // вывести итоговое количество очков
            gameRunning = false;
            graphics.Clear(Color.Black);
            Console.WriteLine($"Итоговое количество очков: {gameScore}");
            Restart();
        }
    }
}
