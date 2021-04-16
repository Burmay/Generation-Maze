using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>


    // Задача: создать алгоритм процедурной геренации лабиранта на произвольной площади. Один вход и выход. 


    public partial class MainWindow : Window
    {
        public static bool[,] bv;
        int m; // Я знаю, магические переменные - зло. Это количество клеток вертикль/горизонталь.
        int n;
        string[,] name;
        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
            n = 26;
            m = 30;
            name = new string[n, m];
            bv = new bool[n, m];

            void V1()
            {
                for (int i = 2; i < n - 2; i++)
                {
                    for (int j = 2; j < m - 2; j++)
                    {
                        //find the left neighbors 
                        if (bv[i - 1, j] == true)
                        {
                            //find out how to do with them 
                            if (bv[i - 2, j] == false && bv[i - 1, j + 1] == false && bv[i - 1, j - 1] == false)
                            {
                                bv[i, j] = true;
                            }
                            else if (random.Next(0, 2) == 0) bv[i, j] = true;
                            else bv[i, j] = false;
                        }
                        // find the top neighbors 
                        else if (bv[i, j + 1] == true)
                        {
                            //find out how to do with them
                            if (bv[i, j + 2] == false && bv[i - 1, j + 1] == false && bv[i + 1, j + 1] == false)
                            {
                                bv[i, j] = true;
                            }
                            else if (random.Next(0, 2) == 0) bv[i, j] = true;
                            else bv[i, j] = false;
                        }
                        // if neighbors not find
                        else
                        {
                            if (random.Next(0, 10) >= 9) bv[i, j] = true;
                            else bv[i, j] = false;
                        }
                    }
                }
            }

            // генерация контура
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    // проверка на мнимые крайноси
                    if (i == 0 || i == 25 || j == 0 || j == 29)
                    {
                        bv[i, j] = false;
                    }
                    // проверка на видимые крайности
                    else if (i == 1 || i == 24 || j == 1 || j == 28)
                    {
                        //генерация старта, выхода и стенок
                        if (i == 2 && j == 1 || i == 23 && j == 28) bv[i, j] = true;
                        else bv[i, j] = false;
                    }
                    else
                    {
                        //if (random.Next(0, 10) > 8) bv[i, j] = true;
                        bv[i, j] = false;
                    }
                }
            }
            // main block of logic of the algorithm
            V1();

                    // Генеруем имена для позднего свзывания с xaml. Имена точно совпадают со всем названиями полей (ячеек) в конструкторе xaml.
                    for (int i = 1; i < n-1; i++)
                          {
                          for (int j = 1; j < m-1; j++)
                          {
                              name[i, j] = $"Box{i-1}_{j-1}";
                          }
                    }

            for (int i = 1; i < n-1; i++)
            {
                for (int j = 1; j < m-1; j++)
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
