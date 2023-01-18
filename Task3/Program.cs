using System;
using System.Drawing;
using System.IO;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к папке:");
            string strPath = Console.ReadLine();

            DirectoryInfo directoryInfo = new DirectoryInfo(strPath);

            if (directoryInfo.Exists)
            {
                long FirstSize = CalculationFolderSize(directoryInfo);
                Console.WriteLine($"Исходный размер папки: {FirstSize} байт");
                var infoAboutDeleteFile = CleanFolder(directoryInfo);
                Console.WriteLine($"Количество удаленных файлов: {infoAboutDeleteFile.amountFile}");
                long LastSize = CalculationFolderSize(directoryInfo);
                Console.WriteLine($"Освобождено: {infoAboutDeleteFile.fileSize} байт");
                Console.WriteLine($"Текущий размер папки: {LastSize} байт");
            }
            else
                Console.WriteLine("По указанному пути папки не существует!");
        }

        static public (int amountFile, long fileSize) CleanFolder(DirectoryInfo directoryInfo)
        {
            TimeSpan interval = TimeSpan.FromMinutes(30);
            (int amountFile, long fileSize) infoAboutDeleteFile;
            infoAboutDeleteFile.amountFile = 0;
            infoAboutDeleteFile.fileSize = 0;

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                try
                {
                    if ((fileInfo.LastAccessTime + interval) < DateTime.Now)
                    {
                        infoAboutDeleteFile.fileSize += fileInfo.Length;
                        fileInfo.Delete();
                        infoAboutDeleteFile.amountFile++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не удалось получить доступ к файлу: {fileInfo.FullName} по причине: {ex.ToString()}");
                }
            }

            foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
            {
                var cort = CleanFolder(dir);
                infoAboutDeleteFile.amountFile += cort.amountFile;
                infoAboutDeleteFile.fileSize += cort.fileSize;
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

            return infoAboutDeleteFile;
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
                catch (Exception ex)
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
