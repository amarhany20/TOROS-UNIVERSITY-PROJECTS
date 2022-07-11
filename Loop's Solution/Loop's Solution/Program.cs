using System;

namespace Loop_s_Solution
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "0";
            int count = 0;
            int total = 0;
            int currentNumber = 0;

            while (input != "-1")
            {
                Console.WriteLine("The last entered number was : {0}", currentNumber);
                Console.WriteLine("Current amount of enteries is : {0}", count);
                Console.WriteLine("Note : To calcualte the average please enter -1 ");
                Console.WriteLine("Please Enter the next score");
                input = Console.ReadLine();
                if (input == "-1" || input.Equals("-1"))
                {
                    Console.WriteLine("----------------------------------------------");

                }
                if (int.TryParse(input, out currentNumber) && currentNumber >= 0 && currentNumber <= 20)
                {
                    total += currentNumber;

                }
                else if (!(input == "-1"))
                {
                    Console.WriteLine("Error!!\nPlease enter a number between 0 and 20 ");
                    continue;
                }
                count++;
            }
            Console.WriteLine("Average = " + (total / count));
            Console.WriteLine("Press Enter the end");
            Console.Read();
        }
    }
}
