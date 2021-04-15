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
        int m; // Я знаю, магические числа - зло. Это количество клеток вертикль/горизонталь.
        int n;
        string[,] name;
        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
            m = 27;
            n = 23;
            name = new string[m, n];
            bv = new bool[m, n];

            // Генерация случайного заполнения массива. Позже в этом блоке будет писаться сам алгоритм процедурной генерации.

            for(int i =0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (random.Next(0, 1) == 0) bv[i, j] = false;
                    else bv[i, j] = true;
                }
            }

            
            // Генеруем имена для позднего свзывания с xaml. Имена точно совпадают со всем названиями полей (ячеек) в конструкторе xaml.
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    name[i, j] = $"Box{i}_{j}";
                }
            }

            // а здесь должен вызываться метод, передающий наш процедурно сгенерированный массив вля отборажения в форму.
            // однако, мне пока неизвестно, как сделать поля открытыми для осуществления позднего связываения. Работаю над этим
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Эта строка вызвывает exeption, из-за того, что поля в xaml закрытые.
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
