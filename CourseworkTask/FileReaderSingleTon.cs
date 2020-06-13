using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseworkTask
{
    public class FileReaderSingleton
    {
        private static readonly FileReaderSingleton FileReaderReference = new FileReaderSingleton();

        private FileReaderSingleton()
        {
        }

        public static FileReaderSingleton GetReference()
        {
            return FileReaderReference;
        }

        public List<Document> ReadFiles(ListBox.ObjectCollection files)
        {
            List<Document> readResult = new List<Document>(files.Count);
            foreach (string file in files)
            {
                string line;
                bool hasMain = false;
                List<string> fileContent = new List<string>();
                try
                {
                    using (StreamReader sr = new StreamReader(file, Encoding.Default))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains("static void Main("))
                                hasMain = true;
                            fileContent.Add(line);
                        }
                    }
                }
                catch (FileNotFoundException exception)
                {
                    // Каким либо образом обработать ошибку: залогировать, показать диалоговое окно с вариантами ДА/НЕТ...
                }

                readResult.Add(new Document(fileContent, GetShortName(file), hasMain));
            }

            return readResult;
        }

        private string GetShortName(string fullName)
        {
            string[] parts = fullName.Split('\\');
            return parts[parts.Length - 1];
        }
    }
}
