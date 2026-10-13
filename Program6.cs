using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Создание матрицы склада 5x5 (техническое требование: динамическое считывание размеров)
            int rows = 5;
            int cols = 5;
            char[,] warehouse = new char[rows, cols];
            int[,] cargoWeights = new int[rows, cols];
            Random rnd = new Random();
            // 2. Заполнение четных строк стеллажами 'B', нечетных — проездами '.'
            // 3. Заполнение весов грузов от 10 до 500 кг
            for (int i = 0; i < warehouse.GetLength(0); i++)
            {
                for (int j = 0; j < warehouse.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        warehouse[i, j] = 'B'; // Коробка
                    }
                    else
                    {
                        warehouse[i, j] = '.'; // Проезд
                    }
                    cargoWeights[i, j] = rnd.Next(10, 500);
                }
            }
            // 4. Размещение робота-погрузчика 'R' в ячейке [0, 0]
            warehouse[0, 0] = 'R';
            // 5. Форматированный вывод плана склада с вертикальными разделителями
            Console.WriteLine("--- План склада ---");
            PrintWarehouse(warehouse);
            // 6. Расчет суммарного веса коробок для каждого стеллажа (строки)
            Console.WriteLine("\n--- Аналитический блок: Вес по стеллажам ---");
            for (int i = 0; i < cargoWeights.GetLength(0); i++)
            {
                int rowSum = 0;
                for (int j = 0; j < cargoWeights.GetLength(1); j++)
                {
                    rowSum += cargoWeights[i, j];
                }
                Console.WriteLine($"Стеллаж (ряд) {i}: {rowSum} кг");
            }
            // 7. Поиск ячейки с самым тяжелым грузом (глобальный экстремум)
            int maxWeight = -1;
            int maxRow = 0, maxCol = 0;
            for (int i = 0; i < cargoWeights.GetLength(0); i++)
            {
                for (int j = 0; j < cargoWeights.GetLength(1); j++)
                {
                    if (cargoWeights[i, j] > maxWeight)
                    {
                        maxWeight = cargoWeights[i, j];
                        maxRow = i;
                        maxCol = j;
                    }
                }
            }
            Console.WriteLine($"\nСамый тяжелый груз: {maxWeight} кг в ячейке [{maxRow}, {maxCol}]");
            // 8. Подсчет общего количества свободных для проезда клеток '.'
            int freePassages = 0;
            for (int i = 0; i < warehouse.GetLength(0); i++)
            {
                for (int j = 0; j < warehouse.GetLength(1); j++)
                {
                    if (warehouse[i, j] == '.') freePassages++;
                }
            }
            Console.WriteLine($"Количество свободных проездов: {freePassages}");
            // 9. Запрос координат для списания груза в брак 'X' (безопасность индексов)
            Console.WriteLine("\n--- Списание в брак ---");
            Console.Write("Введите номер строки (x): ");
            int targetX = int.Parse(Console.ReadLine());
            Console.Write("Введите номер столбца (y): ");
            int targetY = int.Parse(Console.ReadLine());
            // Техническое требование: проверка безопасности индекса
            if (targetX >= 0 && targetX < warehouse.GetLength(0) &&
                targetY >= 0 && targetY < warehouse.GetLength(1))
            {
                warehouse[targetX, targetY] = 'X';
                Console.WriteLine("Ячейка успешно помечена как брак (X).");
            }
            else
            {
                Console.WriteLine("Ошибка: Координаты вне диапазона!");
            }
            // 10. Обнуление весов на побочной диагонали (сканирование диагоналей)
            Console.WriteLine("\nОбнуление весов на побочной диагонали...");
            for (int i = 0; i < cargoWeights.GetLength(0); i++)
            {
                // Побочная диагональ: индекс столбца = (размер - 1) - индекс строки
                int j = (cargoWeights.GetLength(1) - 1) - i;
                cargoWeights[i, j] = 0;
            }
            // Итоговый вывод для проверки
            Console.WriteLine("\n--- Итоговый план склада ---");
            PrintWarehouse(warehouse);
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
        // Метод для отрисовки сетки (Техническое требование №2)
        static void PrintWarehouse(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    // Вертикальные разделители и ровные отступы
                    Console.Write($"| {matrix[i, j]} ");
                }
                Console.WriteLine("|"); // Перевод строки после каждого ряда

            }
        }
    }
}
