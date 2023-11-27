using System;
using System.Linq;
using System.Net;

namespace DotNetLab6
{
    internal class Program
    {
        static String @class = "MyClass";

        static void Main(string[] args)
        {    
            (String name, String surname, int age, double salary) t = (name: "Adam", surname: "Thomson", age: 24, salary: 160.0);
            TupleDemonstration.PrintInfo(t);

            Console.WriteLine("Use class variable = " + @class);

            var info = new {name= "Adam", surname= "Thomson", age= 24, salary= 160.0};
            AnonymousClassDemonstration.PrintInfo(info);

            DrawCardClass.DrawCardDemonstration();
            CountTypesClass.CountTypesDemonstration(2, 2.4, -123.4, -190, 4, "Hello", "X", 'x', 7);
            ArraysClass.ArrayMethodsDemonstration();
        }
    }

    class TupleDemonstration 
    { 
        static public void PrintInfo((String name, String surname, int age, double salary) t)
        {
            Console.WriteLine($"Use named fields: Name = {t.name}, surname = {t.surname}, age = {t.age}, salary = {t.salary}");
            Console.WriteLine($"Use ItemN: Name = {t.Item1}, surname = {t.Item2}, age = {t.Item3}, salary = {t.Item4}");
            (String name, String surname, int age, double salary) = t;
            Console.WriteLine($"Unpack: Name = {name}, surname = {surname}, age = {age}, salary = {salary}");
        }
    } 

    class AnonymousClassDemonstration 
    { 
 
        static public void PrintInfo(dynamic info)
        {
            Console.WriteLine($"Use named fields: Name = {info.name}, surname = {info.surname}, age = {info.age}, salary = {info.salary}");
        }

    }

    class ArraysClass
    {
        public static void ArrayMethodsDemonstration()
        {
            String[] strArray = new string[5] { "Strawberry", "Blueberry", "Mango", "Banana", "Orange" };
            Object[] objArray = new Object[5] { 1, 2.3, "XX", null, '*' };

            Console.WriteLine($"Exists: {System.Array.Exists(strArray, x => { return x.Length > 5; })}");

            int[] rangeInts = new int[5] { 12, 2, 4, 10, 1 };
            Console.Write($"Before copying to object array");
            PrintingValues.PrintValues(objArray);
            System.Array.Copy(rangeInts, rangeInts.GetLowerBound(0) + 1, objArray, objArray.GetUpperBound(0) - 2, 2);
            Console.Write($"After copying to object array");
            PrintingValues.PrintValues(objArray);

            Console.WriteLine($"Index of Blueberry = {System.Array.IndexOf(strArray, "Blueberry")}");
            Console.Write("String array: ");
            PrintingValues.PrintValues(strArray);

            Console.Write("Before sorting ");
            System.Array.Sort(rangeInts);
            PrintingValues.PrintValues(rangeInts);

            Console.Write("Set 0s");
            System.Array.Fill(rangeInts, 0, rangeInts.GetLowerBound(0) + 1, 3);
            PrintingValues.PrintValues(rangeInts);
        }
    }

    class PrintingValues
    {

        public static void PrintValues<T>(T[] myArr)
        {
            foreach (T i in myArr)
            {
                Console.Write("\t{0}", i);
            }
            Console.WriteLine();
        }

    }

    public static class MyStringExtension
    {
        public static string Mult(this string str, int times)
            {
                return String.Concat(Enumerable.Repeat(str, times));
            }
    }

    class DrawCardClass
    {

        public static void DrawCardDemonstration()
        {
            Console.WriteLine(DrawCardClass.DrawCard("Ryszard", "Rys", "~", 3, 20));
            Console.WriteLine();
            Console.WriteLine(DrawCardClass.DrawCard("Merry", borderWidth: 3,secLine: "Xmas", borderSign: "*"));
            Console.WriteLine();
            Console.WriteLine(DrawCardClass.DrawCard("Hello", "World", "-"));
            Console.WriteLine();
            Console.WriteLine(DrawCardClass.DrawCard("Carpe", "Diem", minWidth: 30));
            Console.WriteLine();
            Console.WriteLine(DrawCardClass.DrawCard("LaLaLand", borderSign: "^"));
        }

        public static String DrawCard(String firstLine, String secLine = "", String borderSign = "X",
            int borderWidth = 2, int minWidth = 15)
        {
            firstLine = firstLine.Trim();
            secLine = secLine.Trim();

            int contentWidth = Math.Max(firstLine.Length, secLine.Length);
            int width = Math.Max(minWidth, contentWidth + 2 * borderWidth);

            String line = (borderSign.Mult(width) + "\n").Mult(2);
            String firstContentLine = FormatCardContent(firstLine, borderSign, borderWidth, width) + "\n";
            String secContentLine = "";

            if (secLine.Length > 0)
            {
                secContentLine = FormatCardContent(secLine, borderSign, borderWidth, width) + "\n";
            }
            String card = (line + firstContentLine + secContentLine + line).Trim();

            return card;
        }

        private static String FormatCardContent(String line, String borderSign,
            int borderWidth, int width)
        {
            int whiteSpaces = width - 2 * (borderWidth) - line.Length;
            String border = borderSign.Mult(borderWidth);
            String leftFillingSpace = " ".Mult(whiteSpaces / 2 + whiteSpaces % 2);
            String rightFillingSpace = " ".Mult(whiteSpaces / 2);

            return border + leftFillingSpace + line + rightFillingSpace + border;
        }


    }

    class CountTypesClass
    {
        public static void CountTypesDemonstration(params object[] objects)
        {
            Console.Write("Array Elements: ");
            PrintingValues.PrintValues(objects);
            (int evenIntNums, int realPositiveNums, int stringsLenGtEq5, int others) = CountMyTypes(objects);
            Console.WriteLine($"Even integers = {evenIntNums}");
            Console.WriteLine($"Positive reals = {realPositiveNums}");
            Console.WriteLine($"Strings of length >= 5 = {stringsLenGtEq5}");
            Console.WriteLine($"Others = {others}");
        }
        public static (int evenIntNums, int realPositiveNums, int stringsLenGtEq5, int others) CountMyTypes(params object[] objects)
        {

            int evenIntNums = 0;
            int realPositiveNums = 0;
            int stringsLenGtEq5 = 0;
            int others = 0;

            foreach (var obj in objects)
            {
                switch (obj)
                {
                    case int integer when (integer) % 2 == 0:
                        evenIntNums++;
                        break;
                    case double dob when dob > 0:
                        realPositiveNums++;
                        break;
                    case String str when str.Length >= 5:
                        stringsLenGtEq5++;
                        break;
                    default:
                        others++;
                        break;
                }
            }

            return (evenIntNums, realPositiveNums, stringsLenGtEq5, others);
        }
    }
}
