using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkTask
{
    public class Document
    {
        private readonly List<string> _content;
        private readonly string Name;
        private readonly bool _isEntryPoint;

        public Document(List<string> content, string name, bool isEntryPoint)
        {
            _content = content;
            Name = name;
            _isEntryPoint = isEntryPoint;
        }

        public void ShowContent()
        {
            Console.WriteLine("--|Start of file content|--");
            foreach (string line in _content)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("--|End of file content|--\r\n");
        }

        public List<string> GetContent()
        {
            return _content;
        }

        public bool IsEntryPoint()
        {
            return _isEntryPoint;
        }

        public string GetName()
        {
            return Name;
        }

        public override string ToString()
        {
            if (_isEntryPoint)
                return Name + " has Main()";
            return Name;
        }
    }
}
