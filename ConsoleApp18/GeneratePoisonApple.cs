using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    partial class Program
    {
        private static void GeneratePoisonApple()
        {
            for (int i = 0; i < 2; i++)
                poisonApple[i] = random.Next(0, 40);
            poisonApple[0] = poisonApple[0] * 10;
            poisonApple[1] = poisonApple[1] * 10;
            graphics.FillEllipse(System.Drawing.Brushes.Yellow, poisonApple[0], poisonApple[1], 10, 10);
        }
    }
}