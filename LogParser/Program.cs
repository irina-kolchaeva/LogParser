using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LogParser
{
    internal class Program  // Путь до файла/папки подаётся в качестве первого аргумента
                            // Маска регулярного выражения для поиска строк подаётся в качестве второго аргумента
                                // Если нужно вывести строки, начинающиеся с Error
                                    // и имеющие непустую строку после двоеточия -
                                    // в качестве второго аргумента подаётся, например, "^Error:\s.*"
                            // Вывод результатов осуществляется в консоль и в файл
                            // Можно поменять маску для считывания файлов из папки на любую другую, чтобы считывались только определенные файлы
    {
        static void Main(string[] args) // cd /d D:\Users\kolch\VisualStudio\LogParser\LogParser\bin\Debug
                                        // LogParser.exe D:\kolch\log.log "^Error:\s.*"
                                        // LogParser.exe D:\kolch "^Error:\s.*"
        {
            string path = args[0];
            List<string> res = new List<string>();
            StreamWriter sw = new StreamWriter(@"D:\ResultLogParser.txt");
            if (File.Exists(path)) //Это файл
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Для файла {0}:", path);
                sw.WriteLine("Для файла {0}:", path);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  Все строки в файле, удовлетворяющие указанной маске:");
                sw.WriteLine("  Все строки в файле, удовлетворяющие указанной маске:");
                res = Search(path, args[1]);
                Console.ForegroundColor = ConsoleColor.White;
                foreach (string str in res)
                {
                    Console.WriteLine(str);
                    sw.WriteLine(str);
                }
            }
            else if (Directory.Exists(path)) //Это папка
            {
                //var files = Directory.GetFiles(path, "*.log");            // Тут можно поменять маску на любую другую, чтобы считывались только определенные файлы

                var files = Directory.GetFiles(path, "*.*").Where(s => s.EndsWith(".log") || s.EndsWith(".txt") || s.EndsWith(".text") || s.EndsWith(".err"));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Все текстовые файлы в папке {0}:", path);
                sw.WriteLine("Все текстовые файлы в папке {0}:", path);
                foreach (string file in files)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("  " + file);
                    sw.WriteLine("  " + file);
                    res = Search(file, args[1]);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    if (res.Count != 0)
                    {
                        Console.WriteLine("  Все строки в файле, удовлетворяющие указанной маске:");
                        sw.WriteLine("  Все строки в файле, удовлетворяющие указанной маске:");
                    }
                    else
                    {
                        Console.WriteLine("  В данном файле не найдены строки, удовлетворяющие указанной маске :(");
                        sw.WriteLine("  В данном файле не найдены строки, удовлетворяющие указанной маске :(");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    foreach (string str in res)
                    {
                        Console.WriteLine(str);
                        sw.WriteLine(str);
                    }
                }
            }
            sw.Close();
        }


        static List<string> Search(string path, string mask)
        {
            List<string> list = new List<string>();
            string s;
            StreamReader f = new StreamReader(path);
            while ((s = f.ReadLine()) != null)
            {
                if (Regex.IsMatch(s, mask))
                    list.Add(s);
            }
            f.Close();
            return list;
        }
    }
}