using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimumElementMethod
{
    public class Methods
    {

        static void Main()
        {

            Console.WriteLine("Введите количество предложения: ");
            int proposal = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите количество спроса: ");
            int demand = int.Parse(Console.ReadLine());

            // инит двумерного массива для ввода тарифов на доставку
            int[,] cost = new int[proposal, demand];
            Console.WriteLine("Введите тарифный план (стоимость доставки) для каждой пары Предложение - Спрос");
            Console.WriteLine("\n");

            //ввод стоимости доставки для каждой пары "Предложение - Спрос"
            for (int i = 0; i < proposal; i++)
            {
                for (int j = 0; j < demand; j++)
                {
                    Console.Write($"Стоимость от Предложения {i + 1} к Спросу {j + 1}: ");
                    cost[i, j] = int.Parse(Console.ReadLine());
                }
            }

            // вывод полученной матрицы из введеных пользователм данных
            Console.WriteLine("\nМатрица стоимостей:");
            for (int i = 0; i < proposal; i++)
            {
                for (int j = 0; j < demand; j++)
                {
                    Console.Write(cost[i, j] + "\t");
                }
                Console.WriteLine();
            }

            //количество предложения от каждого поставщика
            int[] suppliers = new int[proposal];
            Console.WriteLine("Введите предложение (количество товаров) для каждого поставщика");
            Console.WriteLine("\n");
            for (int i = 0; i < proposal; i++)
            {
                Console.Write($"Предложение для поставщика {i + 1}: ");
                suppliers[i] = int.Parse(Console.ReadLine());
            }

            // количество спроса от каждого потребителя
            int[] consumers = new int[demand];
            Console.WriteLine("Введите спрос (количество товаров) для каждого потребителя");
            Console.WriteLine("\n");
            for (int j = 0; j < demand; j++)
            {
                Console.Write($"Спрос для потребителя {j + 1}: ");
                consumers[j] = int.Parse(Console.ReadLine());

                //применение метода сз угла и вывод результата
                Console.WriteLine("\nМетод Северо-Западного угла:");
                int[,] northwestPlan = NorthwestCornerMethod(suppliers, consumers);
                PrintPlan(northwestPlan);
                Console.WriteLine($"Общая стоимость: {CalculateCost(northwestPlan, cost)}");

                // применение метода мин элементаи вывод результата
                Console.WriteLine("\nМетод Минимального элемента:");
                int[,] minElementPlan = MinimumElementMethod(suppliers, consumers, cost);
                PrintPlan(minElementPlan);
                Console.WriteLine($"Общая стоимость: {CalculateCost(minElementPlan, cost)}");

            }

            // метод сз угла
            static int[,] NorthwestCornerMethod(int[] suppliers, int[] consumers)
            {
                int[,] plan = new int[suppliers.Length, consumers.Length];  // матрица для хранения плана
                int i = 0, j = 0;
                //копирование массивов
                int[] supply = (int[])suppliers.Clone();
                int[] demand = (int[])consumers.Clone();

                // алгоритм
                while (i < suppliers.Length && j < consumers.Length)
                {
                    // определние минимального количества товаров, которое можно поставить (между поставкой и спросом)
                    int quantity = Math.Min(supply[i], demand[j]);
                    plan[i, j] = quantity;  // заполнение плана

                    // Уменьшаем количество оставшихся товаров
                    supply[i] -= quantity;
                    demand[j] -= quantity;

                    // когда поставка для текущего поставщика исчерпана, переходим к следующему поставщику
                    if (supply[i] == 0)
                    {
                        i++;
                    }
                    // Если спрос для текущего потребителя исчерпан, переходим к следующему потребителю
                    else
                    {
                        j++;
                    }
                }

                return plan;  
            }

            // метод минимального элемента
            static int[,] MinimumElementMethod(int[] suppliers, int[] consumers, int[,] cost)
            {
                int[,] plan = new int[suppliers.Length, consumers.Length];
                int[] supply = (int[])suppliers.Clone();
                int[] demand = (int[])consumers.Clone();
                bool[,] used = new bool[suppliers.Length, consumers.Length];

               
                while (true)
                {
                    // Ищем минимальный элемент среди неиспользованных
                    int minCost = int.MaxValue;
                    int minI = -1, minJ = -1; //тк на момент инициализации минимальная ячейка не найдена

                    for (int i = 0; i < suppliers.Length; i++)
                    {
                        for (int j = 0; j < consumers.Length; j++)
                        {
                            // Если элемент не использован, стоимость меньше минимальной и есть потребность и предложение
                            if (!used[i, j] && cost[i, j] < minCost && supply[i] > 0 && demand[j] > 0)
                            {
                                minCost = cost[i, j];
                                minI = i;  
                                minJ = j;  
                            }
                        }
                    }

                    // если не найдено ни одного подходящего элемента (не осталось товаров или спроса), завершаем цикл
                    if (minI == -1 || minJ == -1)
                    {
                        break;
                    }

                    // определяем количество товаров, которое можно поставить
                    int quantity = Math.Min(supply[minI], demand[minJ]);
                    plan[minI, minJ] = quantity;  

                    // уменьшаем количество оставшихся товаров
                    supply[minI] -= quantity;
                    demand[minJ] -= quantity;
                    used[minI, minJ] = true;  // отмечаем ячейку как использованную

                    // если предложение для текущего поставщика исчерпано, помечаем всю строку как использованную
                    if (supply[minI] == 0)
                    {
                        for (int j = 0; j < consumers.Length; j++)
                        {
                            used[minI, j] = true;
                        }
                    }

                    // если спрос для текущего потребителя исчерпался, помечаем весь столбец как использованный
                    if (demand[minJ] == 0)
                    {
                        for (int i = 0; i < suppliers.Length; i++)
                        {
                            used[i, minJ] = true;
                        }
                    }
                }

                return plan; 
            }

            // функция для вывода плана перевозок
            static void PrintPlan(int[,] plan)
            {
                for (int i = 0; i < plan.GetLength(0); i++)
                {
                    for (int j = 0; j < plan.GetLength(1); j++)
                    {
                        Console.Write(plan[i, j] + "\t"); 
                    }
                    Console.WriteLine(); 
                }
            }

           //подсчет целевой функции
            static int CalculateCost(int[,] plan, int[,] cost)
            {
                int totalCost = 0;
                for (int i = 0; i < plan.GetLength(0); i++)
                {
                    for (int j = 0; j < plan.GetLength(1); j++) 
                    {
                        totalCost += plan[i, j] * cost[i, j];  
                    }
                }
                return totalCost; 
            }
        }
    }
}