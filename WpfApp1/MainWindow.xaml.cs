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
            m = 28;
            n = 24;
            name = new string[n, m];
            bv = new bool[n, m];

            // Генерация случайного заполнения массива. Позже в этом блоке будет писаться сам алгоритм процедурной генерации.

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if (random.Next(0, 2) == 0) bv[i, j] = false;
                    else bv[i, j] = true;
                }
            }

            
            // Генеруем имена для позднего свзывания с xaml. Имена точно совпадают со всем названиями полей (ячеек) в конструкторе xaml.
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    name[i, j] = $"Box{i}_{j}";
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    // Эта строка является магией
                    FieldInfo field = typeof(MainWindow).GetField(name[i, j],
                        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (field != null && field.GetValue(this) is TextBox textBox)
                    {
                        textBox.Background = bv[i, j] ? Brushes.Black : Brushes.Red;
                    }
                }
            }
        }
    }
}
