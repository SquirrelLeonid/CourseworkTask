using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace CourseworkTask
{
    //TODO прибраться в названиях переменных
    public class CodeBlockFormer
    {
        private delegate CodeBlock CreateBlock(string className, string methodName,
            List<string> methodBody, CodeBlock caller);

        //переменная - ее класс (кроме примитивов)
        private Dictionary<string, string> _variables = new Dictionary<string, string>();

        /*public List<CodeBlock> FormCodeBlocks(ParsedCodeKeeper parsedResult)
        {
            var result = new List<CodeBlock>();
            var content = parsedResult.GetStorage();
            var callQueue = new Queue<CreateBlock>();

            CreateBlock entryPointCall = (a, b, c, d) =>
                new CodeBlock(a,b, c, d);
            callQueue.Enqueue(entryPointCall);

            CodeBlock prevBlock = null;
            var className = parsedResult.EntryPoint.Key;
            var methodName = parsedResult.EntryPoint.Value;
            while (callQueue.Count != 0)
            {
                var methodBody = content[className][methodName];
                var currentBlock = callQueue.Dequeue().Invoke(className, methodName, methodBody, prevBlock);
                //Есть созданный блок, но нужно заполнить очередь новыми методами
                foreach (CreateBlock del3gate in FindMethodCalls(methodBody, currentBlock))
                    callQueue.Enqueue(del3gate);

            }

            return result;
        }

        private IEnumerable<CreateBlock> FindMethodCalls(List<string> methodBody, CodeBlock currentBlock)
        {
            foreach (string line in methodBody)
            {
                string methodName = null;
                if (IsClassCreating(line))
                {
                    string[] names = GetClassAndVariableName(line);
                    string varName = names[0];
                    string className = names[1];
                    _variables.Add(varName, className);
                }

                if (IsMethodCall(line))
                {
                    if (IsInnerMethod(line))
                    {
                    }
                }
            }
            yield break;
        }

        private bool IsClassCreating(string line)
        {
            //Шаблон для поиска создания экземпляра класса
            string pattern = @"^\s*[var|[_*|\w*]+\s+[_*|\w*]+\s*=\s*new\s+[_*|\w*]*\(.*\);";
            return Regex.IsMatch(line, pattern);
        }

        private string[] GetClassAndVariableName(string line)
        {
            string[] result = new string[2];
            // Шаблон для разреза строки до имени переменной
            string pattern = @"^\s*[var|[_*|\w*]+\s+";

            //a = TestClass(...);
            string lineWithoutVar = Regex.Split(line, pattern)[1];
            string[] varSeparateClass = Regex.Split(lineWithoutVar, @"\s*=\s*");
            string classWithoutNew = Regex.Split(varSeparateClass[1], @"\s*new\s+")[1];
            result[0] = varSeparateClass[0];
            result[1] = classWithoutNew.Split(new[] {'('})[0];
            return result;
        }

        private bool IsMethodCall(string line)
        {
            // Вызов метода с присваиванием переменной
            string pattern = @"^\s*[var|[_*|\w*]+\s+[_*|\w*]+\s*=\s*[_*|\w*]+\.[_*|\w*]+\s*\(.*\);";
            if (Regex.IsMatch(line, pattern))
                return true;

            // Вызов метода без присваивания переменной;
            pattern = @"^\s*[_*|\w*]+\.[_*|\w*]+\s*\(.*\);";
            if (Regex.IsMatch(line, pattern))
                return true;

            //Вызов метода внутри класса
            pattern = @"^\s*[_*|\w*]+\s*\(.*\);";
            if (Regex.IsMatch(line, pattern))
                return true;

            return false;
        }

        private bool IsInnerMethod(string line)
        {
            //Вызов метода внутри класса
            string pattern = @"^\s*[_*|\w*]+\s*\(.*\);";
            return Regex.IsMatch(line, pattern);
        }*/
    }
}
