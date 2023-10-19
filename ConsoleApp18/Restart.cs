using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    partial class Program
    {
        private static void Restart()
        {
            Console.WriteLine("Желаете начать игру заново? Да/Нет");
            string answer = Console.ReadLine();
            if (answer == "Да")
                Main(null);
            else if (answer == "Нет")
            {
                gameRunning = false;
            }

        }
    }
}
