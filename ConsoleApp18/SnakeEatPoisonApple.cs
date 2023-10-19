using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    partial class Program
    {
        private static bool SnakeEatPoisonApple()
        {
            return poisonApple[0] == snake[0][0] && poisonApple[1] == snake[0][1];

        }
    }
}
