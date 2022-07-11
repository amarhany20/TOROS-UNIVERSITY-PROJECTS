using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DemoProjectForOOB
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Dictionary<int, string> numberNames = new Dictionary<int, string>(); 
            List<string> lst = new List<string>();
            sw.Start();
            for (int f = 0; f <= 10000; f++)
            {
                lst.Add(f.ToString() + " HEX: " + f.ToString("X"));
            }
            sw.Stop();
            string listwrite = ("Write Time List: "+sw.ElapsedMilliseconds);
            sw.Reset();
            sw.Start();
            for (int i = 0; i <= 10000; i++)
            {
                numberNames.Add(i, i.ToString("X"));

            }
            sw.Stop();
            string dictwrite = "Write Time Dict: " +sw.ElapsedMilliseconds.ToString();
            sw.Reset();
            sw.Start();
            foreach (KeyValuePair<int, string> kvp in numberNames)
            { Console.WriteLine("Dictionary:  Key: {0}, Value: {1}", kvp.Key, kvp.Value); }
            sw.Stop();
            string dictread = "Dictionary Read Time:" + sw.ElapsedMilliseconds.ToString();
            sw.Reset();
            sw.Start();
            foreach (var item in lst)
            {
                Console.WriteLine(item);
            }
            sw.Stop();
            string listread = "List Read Time : " + sw.ElapsedMilliseconds.ToString();
            Console.WriteLine($"{listwrite}\n{listread}\n{dictwrite}\n{dictread}");
            sw.Reset();
            sw.Start();
            Console.WriteLine("Dic Signle Selection: "+numberNames[5000]);
            sw.Stop();
            Console.WriteLine("It took: "+sw.ElapsedMilliseconds);
            sw.Reset();
            sw.Start();
            Console.WriteLine("Dic Signle Selection: " + lst[5000]);
            sw.Stop();
            Console.WriteLine("It took: " + sw.ElapsedMilliseconds);
            sw.Reset();
            Console.WriteLine();
            Console.ReadLine();
               
        }
    }
}
