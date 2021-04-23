using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>


    // Задача: создать алгоритм процедурной геренации лабиранта на произвольной площади. Один вход и выход. 


    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            Random random = new Random();
            InitializeComponent();
            int size = 30;
            int bustOfWhites = (int)((size * size / 3) * 2);
            int[] x = new int[bustOfWhites];
            int[] y = new int[bustOfWhites];
            int[] g = new int[bustOfWhites];
            string[,] name = new string[size, size];
            bool[,] bv = new bool[size, size];
            int memoryGroup = 0;
            int[] x1 = new int[bustOfWhites];
            int[] y1 = new int[bustOfWhites];
            int[] x2 = new int[bustOfWhites];
            int[] y2 = new int[bustOfWhites];

            //GroupAnalysis
            int z = 0;
            int sequence = 0;
            int group = 0;
            bool flag1 = true;
            bool flag2 = true;

            //Interconnection
            int number1 = 0;
            int number2 = 0;
            int resultX = 0;
            int resultY = 0;
            double allResult = 100;
            int point1 = 0;
            int point2 = 0;
            int directionX = 0;
            int directionY = 0;
            int numberToGenerateX = 1;
            int numberToGenerateY = 1;

            //meta
            int[] metagroups = new int[bustOfWhites];
            int metaNumber = 0;
            bool metaflag = false;

            //
            int memoryI;
            int memoryJ;


            //Yura, please, don't hit me!...
            //This is really needed ... 

            void СontourAndFuel()
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        // проверка на мнимые крайноси
                        if (i == 0 || i == size - 1 || j == 0 || j == size - 1)
                        {
                            bv[i, j] = false;
                        }
                        // проверка на видимые крайности
                        else if (i == 1 || i == size - 2 || j == 1 || j == size - 2 || i == size - 3 || j == size - 3)
                        {
                            //генерация старта, выхода и стенок
                            if (i == 2 && j == 1 || i == size - 3 && j == size - 2 || i == size - 3 && j == size - 3) bv[i, j] = true;
                            else bv[i, j] = false;
                        }
                        else
                        {
                            if (random.Next(0, 20) > 18) bv[i, j] = true;
                            else bv[i, j] = false;
                        }
                    }
                }
            }
            void V2()
            {
                for (int i = 2; i < size - 2; i++)
                {
                    for (int j = 2; j < size - 2; j++)
                    {
                        if (i == size - 3 && j == size - 3)
                        {
                        }
                        //find the left neighbors 
                        else if (bv[i, j - 1] == true)
                        {
                            //find out how to do with them 
                            if (bv[i, j - 2] == false && bv[i + 1, j - 1] == false && bv[i - 1, j - 1] == false)
                            {
                                bv[i, j] = true;
                            }
                            else if (random.Next(0, 10) == 0) bv[i, j] = true;
                            else bv[i, j] = false;
                        }
                        // find the top neighbors 
                        else if (bv[i + 1, j] == true)
                        {
                            //find out how to do with them
                            if (bv[i + 2, j] == false && bv[i + 1, j - 1] == false && bv[i + 1, j + 1] == false)
                            {
                                bv[i, j] = true;
                            }
                            else if (random.Next(0, 2) == 0) bv[i, j] = true;
                            else bv[i, j] = false;
                        }
                        // if the neighbor is below
                        else if (bv[i - 1, j] == true)
                        {
                            //find out how to do with them
                            if (bv[i - 2, j] == false && bv[i - 1, j - 1] == false && bv[i - 1, j + 1] == false)
                            {
                                bv[i, j] = true;
                            }
                            else if (random.Next(0, 2) == 0) bv[i, j] = true;
                            else bv[i, j] = false;
                        }
                        // if the neighbor is right
                        // if neighbors not find
                        else
                        {
                            if (random.Next(0, 10) >= 8) bv[i, j] = true;
                            else bv[i, j] = false;
                        }

                    }
                }
            }
            void Cliner()
            {
                for (int i = 2; i < size - 2; i++)
                {
                    for (int j = 2; j < size - 2; j++)
                    {
                        if (bv[i, j] == true && bv[i - 1, j] == true && bv[i, j + 1] == true && bv[i - 1, j + 1] == true)
                        {
                            if (bv[i - 1, j + 2] != true && bv[i - 2, j + 1] != true) { bv[i - 1, j + 1] = false; }
                            else if (bv[i, j + 2] != true && bv[i + 1, j + 1] != true)
                            {
                                bv[i, j + 1] = false;
                            }
                            else if (bv[i - 1, j - 1] != true && bv[i - 2, j] != true) { bv[i - 1, j] = false; }
                            else if (bv[i, j - 1] != true && bv[i + 1, j] != true)
                            {
                                bv[i, j] = false;
                            }

                            else if (bv[i, j + 2] != true || bv[i + 1, j + 1] != true) { bv[i, j + 1] = false; }
                            else if (bv[i - 1, j + 2] != true || bv[i - 2, j + 1] != true) { bv[i - 1, j + 1] = false; }
                            else if (bv[i - 1, j - 1] != true || bv[i - 2, j] != true) { bv[i - 1, j] = false; }
                            else if (bv[i, j - 1] != true || bv[i + 1, j] != true) { bv[i, j] = false; }
                        }
                    }
                }
            }
            void Сomplementofdiagonals()
            {
                for (int i = 2; i < size - 2; i++)
                {
                    for (int j = 2; j < size - 2; j++)
                    {
                        //elimination of diagonals - blakades. Сarriage up 
                        if (bv[i, j] == true && bv[i - 1, j + 1] == true && bv[i, j + 1] == false || bv[i, j] == true && bv[i - 1, j - 1] == true && bv[i, j - 1] == false)
                        {
                            bv[i - 1, j] = true;
                        }
                    }
                }
            }
            void GroupAnalysisClean()
            {
                for (int i = 0; i < bustOfWhites; i++)
                {
                    x[i] = 0;
                    y[i] = 0;
                    g[i] = 0;
                    x1[i] = 0;
                    x2[i] = 0;
                    y1[i] = 0;
                    y2[i] = 0;
                    metagroups[i] = 0;
                }
            }
            void GroupAnalysis()
            {
                //gathering whites together 
                z = 0;
                sequence = 0;
                group = 0;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (bv[i, j] == true)
                        {
                            flag1 = true;
                            flag2 = true;
                            y[z] = i;
                            x[z] = j;
                            g[z] = group;
                            int assimilationLeft = 0;
                            int assimilationUp = 0;

                            // if the group is on the left
                            if (bv[i, j - 1] == true)
                            {
                                for (int u = 0; u <= z; u++)
                                {
                                    if (i == y[u] && j - 1 == x[u])
                                    {
                                        g[z] = g[u];
                                        assimilationLeft = g[u];
                                        flag1 = false;
                                        break;
                                    }
                                }
                            }
                            //if the group is on top 
                            //меняем группу нашей переменной на имя топа
                            if (bv[i - 1, j] == true)
                            {
                                for (int u = 0; u <= z; u++)
                                {
                                    if (i - 1 == y[u] && j == x[u])
                                    {
                                        g[z] = g[u];
                                        flag2 = false;
                                        assimilationUp = g[u];
                                        break;
                                    }
                                }
                            }
                            //assimilation
                            if (flag1 == false && flag2 == false)
                            {
                                for (int h = 0; h < z; h++)
                                {
                                    if (g[h] == assimilationLeft)
                                    {
                                        g[h] = assimilationUp;
                                    }
                                }
                            }
                            else if (flag1 == false || flag2 == false) { }
                            else group++;
                            z++;
                        }
                    }
                }

                // Меняем названия групп на последовательные
                //Для этого подбирает исходные имена групп
                for (int i = 0; i < y.Length; i++)
                {
                    // сверяет сген. имена с имеющимися
                    for (int j = 0; j < y.Length; j++)
                    {
                        if (g[j] == i)
                        {
                            // меняет имена всей группы
                            for (int w = 0; w < y.Length; w++)
                            {
                                if (g[w] == i) g[w] = sequence;
                            }
                            sequence++;
                            break;
                        }
                    }
                }
            }
            void Interconnection()
            {
                number1 = 0;
                number2 = 0;
                numberToGenerateX = 0;
                numberToGenerateY = 0;
                allResult = 100;
                // Проверяем, есть ли нулевая группа
                for (int i = 0; i < group; i++)
                {
                    if (g[i] == 0)
                    {
                        break;
                    }
                    memoryGroup = 1;
                }

                // делим ячейки на наши/не наши
                for (int i = 0; i < bustOfWhites; i++)
                {
                    if (g[i] == memoryGroup)
                    {
                        x1[number1] = x[i];
                        y1[number1] = y[i];
                        number1++;
                    }
                    else
                    {
                        x2[number2] = x[i];
                        y2[number2] = y[i];
                        number2++;
                    }
                }

                // находим для нашей ячейки ближайшую не нашу

                // перебираем наши ячейки
                for (int i = 0; i < bustOfWhites; i++)
                {
                    if (y1[i] == 0)
                    {
                        break;
                    }
                    // перибераем не наши ячейки
                    for (int j = 0; j < bustOfWhites; j++)
                    {
                        if (y2[j] == 0)
                        {
                            break;
                        }
                        resultX = x1[i] - x2[j];
                        resultY = y1[i] - y2[j];
                        if (resultX < 0) resultX = -resultX;
                        if (resultY < 0) resultY = -resultY;
                        if (Math.Sqrt((double)Math.Pow(resultX, 2) + (double)Math.Pow(resultY, 2)) < allResult)
                        {
                            allResult = (double)Math.Sqrt((double)Math.Pow(resultX, 2) + (double)Math.Pow(resultY, 2));
                            point1 = i;
                            point2 = j;
                        }
                    }

                }
                // Расстояние, которое необходимо отложить
                directionX = x2[point2] - x1[point1];
                directionY = y1[point1] - y2[point2];


                if (directionX > 0)
                {

                    for (int i = 0; i < directionX; i++)
                    {
                        bv[y1[point1], x1[point1] + numberToGenerateX] = true;
                        numberToGenerateX++;
                    }
                    // генерить вправо
                }
                else if (directionX < 0)
                {
                    for (int i = 0; i < -directionX; i++)
                    {
                        bv[y1[point1], x1[point1] + numberToGenerateX] = true;
                        numberToGenerateX--;
                    }
                }
                else
                {
                    numberToGenerateX = 0;
                }
                if (directionY < 0)
                // генерить вниз
                {
                    for (int i = 0; i < -directionY; i++)
                    {
                        bv[y1[point1] + numberToGenerateY, x1[point1] + numberToGenerateX] = true;
                        numberToGenerateY++;
                    }
                }
                else
                {
                    // генерить вверх
                    for (int i = 0; i < directionY; i++)
                    {
                        bv[y1[point1] - numberToGenerateY, x1[point1] + numberToGenerateX] = true;
                        numberToGenerateY++;
                    }

                }
            }
            void FillingEmpty()
                {
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (i > 2 && j > 2 && i < size-3 && j < size - 3)
                            {
                                if (bv[i, j] == true)
                                {
                                    // если выглядывает право
                                    if (bv[i + 1, j] == false && bv[i - 1, j] == false && bv[i, j + 1] == false)
                                    {
                                        memoryI = i;
                                        memoryJ = j;
                                        for (int f = 0; f < 100; f++)
                                        {
                                            //движение вправо
                                            if (bv[i - 1, j + 1] == false && bv[i, j + 1] == false && bv[i + 1, j + 1] == false && bv[i - 1, j + 2] == false && bv[i, j + 2] == false && bv[i + 1, j + 2] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                {
                                                    bv[i, j + 1] = true;
                                                    j++;
                                                }
                                                else { continue; }
                                            }
                                            //движение влево
                                            else if (bv[i - 1, j - 1] == false && bv[i, j - 1] == false && bv[i + 1, j - 1] == false && bv[i - 1, j - 2] == false && bv[i, j - 2] == false && bv[i + 1, j - 2] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                            {
                                                    bv[i, j - 1] = true;
                                                    j--;
                                                }
                                                else { continue; }
                                            }
                                            //движение вверх
                                            else if (bv[i - 1, j - 1] == false && bv[i - 1, j] == false && bv[i - 1, j + 1] == false && bv[i - 2, j - 1] == false && bv[i - 2, j] == false && bv[i - 2, j + 1] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                            {
                                                    bv[i - 1, j] = true;
                                                    i--;
                                                }
                                                else { continue; }
                                            }
                                            //движение вниз
                                            else if (bv[i + 1, j - 1] == false && bv[i + 1, j] == false && bv[i + 1, j + 1] == false && bv[i + 2, j - 1] == false && bv[i + 2, j] == false && bv[i + 2, j + 1] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                {
                                                    bv[i + 1, j] = true;
                                                    i++;
                                                }
                                                else { continue; }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        i = memoryI;
                                        j = memoryJ;
                                    }
                                    // если выглядывает влево
                                    if (bv[i + 1, j] == false && bv[i - 1, j] == false && bv[i, j - 1] == false)
                                    {
                                        memoryI = i;
                                        memoryJ = j;
                                        for (int f = 0; f < 100; f++)
                                        {
                                            //движение вправо
                                            if (bv[i - 1, j + 1] == false && bv[i, j + 1] == false && bv[i + 1, j + 1] == false && bv[i - 1, j + 2] == false && bv[i, j + 2] == false && bv[i + 1, j + 2] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                {
                                                    bv[i, j + 1] = true;
                                                    j++;
                                                }
                                                else { continue; }
                                            }
                                            //движение влево
                                            else if (bv[i - 1, j - 1] == false && bv[i, j - 1] == false && bv[i + 1, j - 1] == false && bv[i - 1, j - 2] == false && bv[i, j - 2] == false && bv[i + 1, j - 2] == false)
                                            {
                                            if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                {
                                                    bv[i, j - 1] = true;
                                                    j--;
                                                }
                                                else { continue; }
                                            }
                                            //движение вверх
                                            else if (bv[i - 1, j - 1] == false && bv[i - 1, j] == false && bv[i - 1, j + 1] == false && bv[i - 2, j - 1] == false && bv[i - 2, j] == false && bv[i - 2, j + 1] == false)
                                            {
                                            if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                 {
                                                    bv[i - 1, j] = true;
                                                    i--;
                                                }
                                                else { continue; }
                                            }
                                            //движение вниз
                                            else if (bv[i + 1, j - 1] == false && bv[i + 1, j] == false && bv[i + 1, j + 1] == false && bv[i + 2, j - 1] == false && bv[i + 2, j] == false && bv[i + 2, j + 1] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                   {
                                                    bv[i + 1, j] = true;
                                                    i++;
                                                }
                                                else { continue; }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        i = memoryI;
                                        j = memoryJ;
                                    }
                                    // если выглядывает вверх
                                    if (bv[i - 1, j] == false && bv[i, j + 1] == false && bv[i, j - 1] == false)
                                    {
                                        memoryI = i;
                                        memoryJ = j;
                                        for (int f = 0; f < 100; f++)
                                        {
                                            //движение вправо
                                            if (bv[i - 1, j + 1] == false && bv[i, j + 1] == false && bv[i + 1, j + 1] == false && bv[i - 1, j + 2] == false && bv[i, j + 2] == false && bv[i + 1, j + 2] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                {
                                                    bv[i, j + 1] = true;
                                                    j++;
                                                }
                                                else { continue; }
                                            }
                                            //движение влево
                                            else if (bv[i - 1, j - 1] == false && bv[i, j - 1] == false && bv[i + 1, j - 1] == false && bv[i - 1, j - 2] == false && bv[i, j - 2] == false && bv[i + 1, j - 2] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                {
                                                    bv[i, j - 1] = true;
                                                    j--;
                                                }
                                                else { continue; }
                                            }
                                            //движение вверх
                                            else if (bv[i - 1, j - 1] == false && bv[i - 1, j] == false && bv[i - 1, j + 1] == false && bv[i - 2, j - 1] == false && bv[i - 2, j] == false && bv[i - 2, j + 1] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                {
                                                    bv[i - 1, j] = true;
                                                    i--;
                                                }
                                                else { continue; }
                                            }
                                            //движение вниз
                                            else if (bv[i + 1, j - 1] == false && bv[i + 1, j] == false && bv[i + 1, j + 1] == false && bv[i + 2, j - 1] == false && bv[i + 2, j] == false && bv[i + 2, j + 1] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                {
                                                    bv[i + 1, j] = true;
                                                    i++;
                                                }
                                                else { continue; }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        i = memoryI;
                                        j = memoryJ;
                                    }
                                    // если выглядывает вниз
                                    if (bv[i + 1, j] == false && bv[i, j + 1] == false && bv[i, j - 1] == false)
                                    {
                                        memoryI = i;
                                        memoryJ = j;
                                        for (int f = 0; f < 100; f++)
                                        {
                                            //движение вправо
                                            if (bv[i - 1, j + 1] == false && bv[i, j + 1] == false && bv[i + 1, j + 1] == false && bv[i - 1, j + 2] == false && bv[i, j + 2] == false && bv[i + 1, j + 2] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                {
                                                    bv[i, j + 1] = true;
                                                    j++;
                                                }
                                                else { continue; }
                                            }
                                            //движение влево
                                            else if (bv[i - 1, j - 1] == false && bv[i, j - 1] == false && bv[i + 1, j - 1] == false && bv[i - 1, j - 2] == false && bv[i, j - 2] == false && bv[i + 1, j - 2] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                {
                                                    bv[i, j - 1] = true;
                                                    j--;
                                                }
                                                else { continue; }
                                            }
                                            //движение вверх
                                            else if (bv[i - 1, j - 1] == false && bv[i - 1, j] == false && bv[i - 1, j + 1] == false && bv[i - 2, j - 1] == false && bv[i - 2, j] == false && bv[i - 2, j + 1] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                {
                                                    bv[i - 1, j] = true;
                                                    i--;
                                                }
                                                else { continue; }
                                            }
                                            //движение вниз
                                            else if (bv[i + 1, j - 1] == false && bv[i + 1, j] == false && bv[i + 1, j + 1] == false && bv[i + 2, j - 1] == false && bv[i + 2, j] == false && bv[i + 2, j + 1] == false)
                                            {
                                                if (i > 2 && j > 2 && i < size - 3 && j < size - 3)
                                                {
                                                    bv[i + 1, j] = true;
                                                    i++;
                                                }
                                                else { continue; }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        i = memoryI;
                                        j = memoryJ;
                                    }
                                }
                            }
                        }

                    }
                }

                void MetaWorkBlok()
                {
                    Interconnection();
                    Сomplementofdiagonals();
                }

                СontourAndFuel();
                V2();
                Сomplementofdiagonals();
                Cliner();

                //узнать, есть ли ещё хоть одна группа
                for (int a = 0; a < 400; a++)
                {
                    if (metaflag == true) break;

                    for (int i = 0; i < bustOfWhites; i++)
                    {
                        if (i == 0) metaNumber = g[i];
                        if (i == 299)
                        {
                            metaflag = true;
                            Cliner();
                            break;
                        }
                        GroupAnalysisClean();
                        GroupAnalysis();
                        if (g[i] == metaNumber || g[i] == 0) { }
                        else
                        {
                            MetaWorkBlok();
                            break;
                        }
                    }
                }
                FillingEmpty();
                Сomplementofdiagonals();

            // Генеруем имена для позднего свзывания с xaml. Имена точно совпадают со всем названиями полей (ячеек) в конструкторе xaml.
            for (int i = 1; i < size - 1; i++)
                {
                    for (int j = 1; j < size - 1; j++)
                    {
                        name[i, j] = $"Box{i - 1}_{j - 1}";
                    }
                }
                // Графическое отображение
                for (int i = 1; i < size - 1; i++)
                {
                    for (int j = 1; j < size - 1; j++)
                    {
                        // Эта строка является магией
                        FieldInfo field = typeof(MainWindow).GetField(name[i, j],
                            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        if (field != null && field.GetValue(this) is TextBox textBox)
                        {
                            textBox.Background = bv[i, j] ? Brushes.White : Brushes.Black;
                        }
                    }
                }
            }
        }
    }