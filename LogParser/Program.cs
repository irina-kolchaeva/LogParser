using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace LogParser
{
    internal class Program
    {
        static void Main(string[] args) //cd /d D:\Users\kolch\VisualStudio\LogParser\LogParser\bin\Debug
                                        // LogParser.exe D:\kolch
                                        // LogParser.exe D:\kolch\test.log
        {
            //   ДЛЯ ТЕРМИНАЛА
            string path = args[0];
            List<string> res = new List<string>();
            if (File.Exists(path)) //Это файл
            {
                Console.WriteLine("Это файл!!!");
            }
            else if (Directory.Exists(path)) //Это папка
            {
                Console.WriteLine("Это папка!!!");
            }

            //  ДЛЯ КОНСОЛИ
            //string path = @"D:\kolch\test.log"; //\test.log

            Console.ReadLine();
        }
    }
}