using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * В последовательности целых чисел A требуется переставить минимальное число элементов так,
 * чтобы никакие два соседних числа не были одной четности.

Формат ввода
В первой строке входных данных записано целое число n (1 ≤ n ≤ 100 000) — количество элементов в последовательности.

Во второй строке записаны n целых чисел ai (0 ≤ ai ≤ 1 000 000 000) — элементы последовательности.

Формат вывода
В первой строке выведите число k (0 ≤ k ≤ n) — количество элементов последовательности, которые были переставлены.

Во второй строке выведите n чисел bi. Все числа последовательности A должны быть выведены. 
Любые два соседних элемента последовательности должны быть различной четности. 
Ровно k индексов последовательности B должны отличаться от последовательности A. 
Если подходящих последовательностей B несколько, то выведите любую из них.

Если переставить элементы последовательности с требуемым условием невозможно, то выведите одно число -1.
 */

namespace ParityOfNeighborsCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            var str = Console.ReadLine();
            var array = str.Split();
            n = int.Parse(array[0]);

            int evenCount = 0; // Количество чётных чисел
            int oddCount = 0;  // Количество нечётных чисел
            List<int> numbers = new List<int>(n);

            str = Console.ReadLine();
            array = str.Split();

            int amountNumbersInTheirPlacesIfFirstIsEven = 0; // количество чисел на своих местах, если первое число чётное
            int amountNumbersInTheirPlacesIfFirstIsOdd = 0; // количество чисел на своих местах, если первое число нечётное

            for (int i = 0; i < n; i++)
            {
                int intVar = int.Parse(array[i]);
                numbers.Add(intVar);

                bool isEven = intVar % 2 == 0;

                if ((i % 2 == 0) == isEven)
                    amountNumbersInTheirPlacesIfFirstIsEven += 1;
                else
                    amountNumbersInTheirPlacesIfFirstIsOdd += 1;

                if (isEven)
                    evenCount += 1;
                else
                    oddCount += 1;
            }

            if (Math.Abs(evenCount - oddCount) >= 2)
            {
                Console.WriteLine(-1);
                return;
            }

            bool firstIsOdd = oddCount == evenCount ? amountNumbersInTheirPlacesIfFirstIsOdd > amountNumbersInTheirPlacesIfFirstIsEven : oddCount > evenCount;
            int count = Rearrange(numbers, n, firstIsOdd);

            Console.WriteLine(count);
            StringBuilder output = new StringBuilder();
            foreach (var number in numbers)
            {
                output.Append(number + " ");
            }
            Console.WriteLine(output);
        }

        private static int Rearrange(List<int> numbers, int n, bool firstIsOdd)
        {
            Stack<int> outPlaceOdd = new Stack<int>(); // нечетные числа не на своих местах
            Stack<int> outPlaceEven = new Stack<int>(); // четные числа не на своих местах
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == numbers[i] % 2 ^ !firstIsOdd) // если число не своем месте
                    if (numbers[i] % 2 == 1)    // если число нечетное
                        outPlaceOdd.Push(numbers[i]);
                    else                        // иначе число четное
                        outPlaceEven.Push(numbers[i]);
            }

            int count = outPlaceOdd.Count + outPlaceEven.Count;

            for (int j = 0; j < n; j++)
            {
                if (j % 2 == numbers[j] % 2 ^ !firstIsOdd)
                    if (numbers[j] % 2 == 1)
                        numbers[j] = outPlaceEven.Pop();
                    else
                        numbers[j] = outPlaceOdd.Pop();
            }

            return count;
        }
    }
}
