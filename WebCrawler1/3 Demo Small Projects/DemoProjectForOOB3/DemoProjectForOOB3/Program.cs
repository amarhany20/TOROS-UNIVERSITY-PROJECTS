using System;

namespace DemoProjectForOOB3
{
    class Program
    {
		enum WeekDays//In C#, an enum (or enumeration type) is used to assign constant names to a group of numeric integer values. It makes constant values more readable, for example, WeekDays.Monday is more readable then number 0 when referring to the day in a week.
		{
			Monday, //0
			Tuesday, //1 
			Wednesday,//2
			Thursday,//3
			Friday,//4
			Saturday,//5
			Sunday//6
		}
		static void Main(string[] args)
        {
			Console.WriteLine(WeekDays.Friday);
			int day = (int)WeekDays.Friday;//this will return 4
			Console.WriteLine(day);

			var wd = (WeekDays)5;//this will return Saturday 
			Console.WriteLine(wd);
		}
    }
}
