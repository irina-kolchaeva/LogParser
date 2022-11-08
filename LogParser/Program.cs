using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LogParser
{
    internal class Program
    {
        static void Main(string[] args) // cd /d D:\Users\kolch\VisualStudio\LogParser\LogParser\bin\Debug
                                        // LogParser.exe D:\kolch "^Error:\s.*"
                                        // LogParser.exe D:\kolch\test.log "^Error:\s.*"
        {
            //   ДЛЯ ТЕРМИНАЛА
            string path = args[0];
            List<string> res = new List<string>();
            if (File.Exists(path)) //Это файл
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Для файла {0}:", path);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  Все строки в файле, удовлетворяющие указанной маске:");
                res = Search(path, args[1]);
                Console.ForegroundColor = ConsoleColor.White;
                foreach (string str in res)
                    Console.WriteLine(str);
            }
            else if (Directory.Exists(path)) //Это папка
            {
                var files = Directory.GetFiles(path, "*.*").Where(s => s.EndsWith(".log") || s.EndsWith(".txt") || s.EndsWith(".text") || s.EndsWith(".err"));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Все текстовые файлы в папке {0}:", path);
                foreach (string file in files)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("  " + file);
                    res = Search(file, args[1]);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    if (res.Count != 0)
                        Console.WriteLine("  Все строки в файле, удовлетворяющие указанной маске:");
                    else
                        Console.WriteLine("  В данном файле не найдены строки, удовлетворяющие указанной маске :(");
                    Console.ForegroundColor = ConsoleColor.White;
                    foreach (string str in res)
                        Console.WriteLine(str);
                }
            }

            //  ДЛЯ КОНСОЛИ
            /*string path = @"D:\kolch"; //\test.log
            List<string> res = new List<string>();

            var files = Directory.GetFiles(path, "*.*").Where(s => s.EndsWith(".log") || s.EndsWith(".txt") || s.EndsWith(".text") || s.EndsWith(".err"));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Все текстовые файлы в папке {0}:", path);
            foreach (string file in files)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  " + file);
                res = Search(file);
                Console.ForegroundColor = ConsoleColor.Blue;
                if (res.Count != 0)
                    Console.WriteLine("  Все строки в файле, удовлетворяющие указанной маске:");
                else
                    Console.WriteLine("  В данном файле не найдены строки, удовлетворяющие указанной маске :(");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (string str in res)
                    Console.WriteLine(str);
            }
            */

            //Console.ReadLine();
        }


        static List<string> Search(string path, string mask) //ДЛЯ ТЕРМИНАЛА
        //static List<string> Search(string path) //ДЛЯ КОНСОЛИ
        {
            List<string> list = new List<string>();
            //string mask = @"^Error:\s.*";    // ДЛЯ КОНСОЛИ

            string s;
            StreamReader f = new StreamReader(path);
            while ((s = f.ReadLine()) != null)
            {
                //if (Regex.IsMatch(s.ToLower(), mask.ToLower())) //ДЛЯ КОНСОЛИ
                if (Regex.IsMatch(s, mask))  //ДЛЯ ТЕРМИНАЛА
                    list.Add(s);
            }
            f.Close();
            return list;
        }
    }
}