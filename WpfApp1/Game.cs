using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Game
    {
        public static int[,] bv = new int[Variable.size, Variable.size];
        private static int memoryX = 1;
        private static int memoryY = 2;
        Random rnd = new Random();

        public static void Step(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Down)
            {
                if(Variable.bv[memoryY + 1,memoryX] == true)
                {
                    bv[memoryY, memoryX] = 0;
                    memoryY += 1;
                }
            }
            if (e.Key == System.Windows.Input.Key.Up)
            {
                if (Variable.bv[memoryY - 1, memoryX] == true)
                {
                    bv[memoryY, memoryX] = 0;
                    memoryY -= 1;
                }
            }
            if (e.Key == System.Windows.Input.Key.Right)
            {
                if (Variable.bv[memoryY, memoryX + 1] == true)
                {
                    bv[memoryY, memoryX] = 0;
                    memoryX += 1;
                }
            }
            if (e.Key == System.Windows.Input.Key.Left)
            {
                if (Variable.bv[memoryY, memoryX - 1] == true)
                {
                    bv[memoryY, memoryX] = 0;
                    memoryX -= 1;
                }
            }

            bv[memoryY, memoryX] = 1;
        }
        public void CreatedPerson()
        {
            bv[2, 1] = 1;
            for(int i = 0; i < 5; i++)
            {
                int a = rnd.Next(Variable.size);
                int b = rnd.Next(Variable.size);
                if(Variable.bv[a, b] == true)
                {
                    bv[a, b] = 2;
                }
                else
                {
                    
                }

            }
        }
    }
}
