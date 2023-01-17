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

            DirectoryInfo directoryInfo = new DirectoryInfo(strPath);

            if (directoryInfo.Exists)
            {
                CleanFolder(directoryInfo);
                Console.WriteLine("директория успешно очищена от файлов не используемых в течении 30 минут!");
            }
            else
                Console.WriteLine("По указанному пути папки не существует!");
        }

        static public void CleanFolder(DirectoryInfo directoryInfo)
        {
            TimeSpan interval = TimeSpan.FromMinutes(30);

            
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                try
                {
                    if ((fileInfo.LastAccessTime + interval) < DateTime.Now)
                        fileInfo.Delete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не удалось получить доступ к файлу: {fileInfo.FullName} по причине: {ex.ToString()}");
                }
            }

            foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
            {
                CleanFolder(dir);
                try
                {
                    if ((directoryInfo.LastAccessTime + interval) < DateTime.Now)
                        directoryInfo.Delete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не удалось получить доступ к каталогу: {dir.FullName} по причине: {ex.ToString()}");
                }
            }
        }
    }
}
