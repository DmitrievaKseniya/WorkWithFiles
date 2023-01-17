using System;
using System.IO;
using System.Reflection;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string strPath;

            Console.WriteLine("Введите путь к папке, для очистки:");
            strPath = Console.ReadLine();

            CleanFolder(strPath);
            Console.WriteLine("директория успешно очищена от файлов не используемых в течении 30 минут!");
        }

        static public void CleanFolder(string PathFolder)
        {
            TimeSpan interval = TimeSpan.FromMinutes(30);

            if (Directory.Exists(PathFolder))
            {
                foreach (string fileInfo in Directory.GetFiles(PathFolder))
                {
                    try
                    {
                        if ((File.GetLastAccessTime(fileInfo) + interval) < DateTime.Now)
                            File.Delete(fileInfo);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Не удалось получить доступ к файлу: {fileInfo} по причине: {ex.ToString()}");
                    }
                }

                foreach (string dir in Directory.GetDirectories(PathFolder))
                {
                    CleanFolder(dir);
                    try
                    {
                        if ((Directory.GetLastAccessTime(dir) + interval) < DateTime.Now)
                            Directory.Delete(dir);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Не удалось получить доступ к каталогу: {dir} по причине: {ex.ToString()}");
                    }
                }
            }
            else
            {
                Console.WriteLine("По указанному пути папки не существует!");
            }
        }
    }
}
