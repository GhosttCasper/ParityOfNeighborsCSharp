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

            long evenCount = 0; // Количество чётных чисел
            long oddCount = 0; // Количество нечётных чисел
            List<CustomLong> numbers = new List<CustomLong>();

            str = Console.ReadLine();
            array = str.Split();

            int i = 0;
            int oddOutPlaceCount = 0;
            int evenOutPlaceCount = 0;

            foreach (var item in array)
            {
                long longVar = long.Parse(item);
                bool isEven = longVar % 2 == 0;
                numbers.Add(new CustomLong(longVar, isEven));

                if ((i % 2 == 0) == isEven)
                    oddOutPlaceCount += 1;
                else
                    evenOutPlaceCount += 1;
                i++;

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

            int count = 0;

            if (oddCount == evenCount)
            {
                Rearrange(ref numbers, ref count, oddOutPlaceCount < evenOutPlaceCount);
            }
            else
            {
                Rearrange(ref numbers, ref count, oddCount > evenCount);
            }

            Console.WriteLine(count);
            StringBuilder output = new StringBuilder();
            foreach (var number in numbers)
            {
                output.Append(number.Number.ToString() + " ");
            }
            Console.WriteLine(output);
        }

        private static void CountOutPlace(List<CustomLong> numbers, ref int oddOutPlaceCount, ref int evenOutPlaceCount)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                if ((i % 2 == 0) == numbers[i].IsEven)
                    oddOutPlaceCount += 1;
                else
                    evenOutPlaceCount += 1;
            }
        }


        private static void Rearrange(ref List<CustomLong> numbers, ref int count, bool oddGreater)
        {
            int length = numbers.Count;
            for (int i = 0; i < length - 1; i++)
            {
                if ((i % 2 == 0) == numbers[i].IsEven ^ !oddGreater)
                    for (int j = i + 1; j < length; j++)
                        if (j % 2 == 0 == numbers[j].IsEven ^ !oddGreater && numbers[i].IsEven != numbers[j].IsEven)
                        {
                            CustomLong oldFirst = numbers[i];
                            numbers[i] = numbers[j];
                            numbers[j] = oldFirst;
                            count += 2;
                            break;
                        }
            }
        }

    }

    public class CustomLong
    {
        public CustomLong(long number, bool isEven)
        {
            Number = number;
            IsEven = isEven;
        }

        public long Number { get; set; }
        public bool IsEven { get; set; }
    }
}
