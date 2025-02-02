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
       
            //двумерный массив для ввода тарифа
            int[,] cost = new int[proposal, demand];
            Console.Write("Введите тарифный план (стоимость доставки) для каждой пары Предложение - Спрос");
            Console.WriteLine("\n");

            for (int i = 0; i < proposal; i++)
            {
                for (int j = 0; j < demand; j++)
                {
                    Console.Write($"Стоимость от Предложения {i + 1} к Спросу {j + 1}: ");
                    cost[i, j] = int.Parse(Console.ReadLine());
                }
            }
                
         
            Console.WriteLine("\n");
            for (int i = 0; i < proposal; i++)
            {
                for (int j = 0; j < demand; j++)
                {
                    Console.Write(cost[i, j] + "\t");
                }
                Console.WriteLine();
            }

            //предлоежние

         
            int[] suppliers = new int[proposal];
            Console.WriteLine("Введите предложение (количество товаров) для каждого поставщика");
            Console.WriteLine("\n");
            for (int i = 0; i < proposal; i++)
            {
               Console.WriteLine($"Предложение для каждого поставщика {i + 1}: ");
                suppliers[i] = int.Parse(Console.ReadLine());    
            }

            //спрос 
            int[] consumers = new int[demand];
            Console.WriteLine("Введите cпрос (количество товаров) для каждого потребителя");
            Console.WriteLine("\n");
            for (int j = 0; j < demand; j++)
            {
                Console.WriteLine($"Спрос для каждого потребителя {j + 1}: ");
                consumers[j] = int.Parse(Console.ReadLine());
            }

            

         




           


        }



    }
}