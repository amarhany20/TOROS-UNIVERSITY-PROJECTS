using System;

namespace Average_of_students_grade_between_0_and_20__first_
{
    class Program
    {
        static void Main(string[] args)
        {
            int grade = 0;
            int counter = 0;
            int total = 0;
            string gradeIn;
            do
            {
                Console.WriteLine("Please Enter the student Grade");
                gradeIn = Console.ReadLine();
                if (int.TryParse(gradeIn, out grade))
                {
                    grade = int.Parse(gradeIn);
                    if (grade < 0 || grade > 20)
                    {
                        if (grade == -1)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("the grade must be between 0 and 20 .. try again ...\n .");
                            continue;
                        }
                    }
                    else
                    {
                        total += grade;
                        counter++;
                    }
                }
                else
                {
                    Console.WriteLine("Please make sure that you entered the number correctly");
                }
            } while (grade != -1);
            Console.WriteLine(total);
            Console.WriteLine(total / counter);
        }
    }
}
