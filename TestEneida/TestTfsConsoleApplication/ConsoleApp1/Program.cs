using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static void testMethod()
        {
            Console.WriteLine("Hello from test method from Bohdan Lyesiko");
        }

        static void Main(string[] args)
        {
            testMethod();
            Console.WriteLine("Who am i? I`m a champion!!");//Dima
            Console.WriteLine("Hello from Bohdan Lyseiko and " + DimaSaySomething());
            Console.WriteLine("Hello from Bohdan Lyseiko. asdflsfnckl");
            Console.WriteLine();
            TarasSaysSomething();
            Console.WriteLine("Input name: ");
            string name;
            name = Console.ReadLine();
            Console.WriteLine("First Comment");
            Console.WriteLine("Second Comment");
            Console.WriteLine("Thirth Name");
            Console.WriteLine("Fourth Comment");
            Console.WriteLine("Fifth Comment");
            Console.Read();
        }

        static void testMethod2()
        {
            Console.WriteLine("Hello from test method from Bohdan Lyesiko 2");
        }
        private static string DimaSaySomething()
        {
            return "Keep calm and write code like a hero))";
        }

        private static void TarasSaysSomething()
        {
            Console.WriteLine("Test hello from Taras");
        }

    }
}
