using System;
using System.IO;
using System.Reflection;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string strPath;
            long size;

            Console.WriteLine("Введите путь к папке, для расчета размера:");
            strPath = Console.ReadLine();

            DirectoryInfo directoryInfo = new DirectoryInfo(strPath);

            if (directoryInfo.Exists)
            {
                size = CalculationFolderSize(directoryInfo);
                Console.WriteLine($"Размер директории - {size} байт");
            }
            else
                Console.WriteLine("По указанному пути папки не существует!");
        }

        static public long CalculationFolderSize(DirectoryInfo directoryInfo)
        {
            long size = 0;

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                try
                {
                    size += fileInfo.Length;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Не удалось получить доступ к файлу: {fileInfo.FullName} по причине: {ex.ToString()}. Файл в расчете учавстовать не будет!");
                }
            }

            foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
            {
                try
                {
                    size += CalculationFolderSize(dir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не удалось получить доступ к lbhtrnjhbb: {dir.FullName} по причине: {ex.ToString()}. Файл в директории в расчете учавстовать не будут!");
                }
            }

            return size;
        }
    }
}
