using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CourseworkTask
{
    public class FileParser
    {
        private class TabUnit
        {
            public enum DeclarationType
            {
                Method,

                Class
            }

            public readonly string Name;
            public readonly int BracketsCount;
            public readonly DeclarationType Type;

            public TabUnit(int bracketCount, string name, DeclarationType type)
            {
                BracketsCount = bracketCount;
                Name = name;
                Type = type;
            }

            public override string ToString()
            {
                return Name + " "  + Type;
            }
        }

        #region Fields
        private string _className;
        private string _methodName;
        private List<string> _methodBody;
        private bool _isMultilineCommentOpen;
        private int _openFigureBracketsCount;
        private readonly Stack<TabUnit> _tabManager;
        private readonly HashSet<string> _modifiers = new HashSet<string>
        {
            "public",
            "private",
            "protected",
            "internal"
            //static
        };
        #endregion
        public FileParser()
        {
            _className = "";
            _methodName = "";
            _methodBody = new List<string>();
            _tabManager = new Stack<TabUnit>();
        }

        public ParsedCodeKeeper ParseCodeListings(List<Document> listings)
        {
            ParsedCodeKeeper keeper = InitPrimaryState(listings);
            CreateMethodCallLinks(keeper);
            return keeper;
        }

        #region Init primary methods
        private ParsedCodeKeeper InitPrimaryState(List<Document> listings)
        {
            var parseResult = new ParsedCodeKeeper();

            foreach (Document listing in listings)
            {
                List<string> content = listing.GetContent();
                foreach (string line in content)
                { 
                    if (ShouldSkipLine(line))
                        continue;

                    if (IsClassDeclaration(line))
                    {
                        _className = ParseClassName(line);
                        var newTab = new TabUnit(_openFigureBracketsCount, _className,
                            TabUnit.DeclarationType.Class);
                        _tabManager.Push(newTab);
                    }
                    else if (IsMethodDeclaration(line))
                    {
                        _methodName = ParseMethodName(line);
                        var newTab = new TabUnit(_openFigureBracketsCount, _methodName,
                            TabUnit.DeclarationType.Method);
                        _tabManager.Push(newTab);
                    }


                    if (line.Contains('{'))
                        _openFigureBracketsCount++;

                    if (CheckMethodBodyBoundary(line))
                        _methodBody.Add(TrimMethodLine(line));

                    if (line.Contains('}'))
                    {
                        _openFigureBracketsCount--;
                        if (IsDeclarationEnd())
                            FinishDeclaration(parseResult, listing.IsEntryPoint());
                    }
                }
            }
            return parseResult;
        }

        #region Check Conditions
        private bool ShouldSkipLine(string line)
        {
            if (_isMultilineCommentOpen)
                return true;

            if (line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Length == 0)
                return true;

            //Шаблон для поиска начала однострочного комментария
            string pattern = @"^\s*\/{2}";
            if (Regex.IsMatch(line, pattern))
                return true;

            //Шаблон для поиска начала документации
            pattern = @"^\s*\/{3}\s*<summary>";
            if (Regex.IsMatch(line, pattern))
            {
                _isMultilineCommentOpen = true;
                return true;
            }

            //Шаблон для поиска конца документации
            pattern = @"^\s*\/{3}\s*</summary>";
            if (Regex.IsMatch(line, pattern))
            {
                _isMultilineCommentOpen = false;
                return true;
            }

            //Шаблон для поиска начала многострочного комментария
            pattern = @"^\s*\/\*";
            if (Regex.IsMatch(line, pattern))
            {
                _isMultilineCommentOpen = true;
                return true;
            }

            //Шаблон для поиска конца многострочного комментария
            pattern = @"\s*\*\/$";
            if (Regex.IsMatch(line, pattern))
            {
                _isMultilineCommentOpen = false;
                return true;
            }

            return false;
        }

        private bool IsMethodDeclaration(string line)
        {
            //Шаблон на проверку того, что строка начинается с кавычки
            //Например " public static void main()"
            string pattern = @"^\s+\""+\s+";
            if (Regex.IsMatch(line, pattern))
                return false;

            //Шаблон на проверку, что строка не содержит директивы using
            pattern = @"\s+using\s+";
            if (Regex.IsMatch(line, pattern))
                return false;

            //шаблон на проверку, что строка не содержит if
            pattern = @"\s+if\s+";
            if (Regex.IsMatch(line, pattern))
                return false;

            //шаблон на проверку что строка не содержит while
            pattern = @"\s+while\s+";
            if (Regex.IsMatch(line, pattern))
                return false;

            //шаблон на проверку что строка не содержит foreach
            pattern = @"\s+foreach\s+";
            if (Regex.IsMatch(line, pattern))
                return false;

            //шаблон на проверку что строка не содержит catch
            pattern = @"\s+catch\s+";
            if (Regex.IsMatch(line, pattern))
                return false;

            if (line.Contains(';'))
                return false;

            //Шаблон для поиска объявления переменной с помощью var
            pattern = @"^\s*var [_*|\w*]*\s*=\s*";
            if (Regex.Matches(line, pattern).Count != 0)
                return false;

            //Шаблон для поиска объявления метода (с аргументами или без)
            //Пример: public static <T> TestMethod([args])
            //Где на месте [args] может быть любое количество аргументов, в том числе 0
            pattern = @"\s*\(.*\)$";
            if (!Regex.IsMatch(line, pattern))
                return false;

            //Шаблон для поиска ключевого слова void
            pattern = @"\s+void\s+";
            if (Regex.IsMatch(line, pattern))
                return true;

            return true;
        }

        private bool IsClassDeclaration(string line) 
        {
            if (line.Contains('"'))
                return false;

            //Шаблон для поиска ключевого слова class
            string pattern = @"\s+class\s+";
            return Regex.IsMatch(line, pattern);
        }

        private bool CheckMethodBodyBoundary(string line)
        {
            return _tabManager.Count > 1 &&
                   _openFigureBracketsCount > 2 &&
                   _tabManager.Peek().Type == TabUnit.DeclarationType.Method;
        }

        private bool IsDeclarationEnd()
        {
            return _openFigureBracketsCount > 0 &&
                   _openFigureBracketsCount == _tabManager.Peek().BracketsCount;
        }
        #endregion

        private void FinishDeclaration(ParsedCodeKeeper parseResult, bool isEntryPoint)
        {
            if (_tabManager.Peek().Type == TabUnit.DeclarationType.Class)
            {
                _tabManager.Pop();
                _className = _tabManager.Count > 0 ? _tabManager.Peek().Name : "";
            }
            else
            {
                parseResult.AddMethodRecord(_className, _methodName, _methodBody, isEntryPoint);
                _tabManager.Pop();
                _methodBody = new List<string>();
            }
        }

        private string TrimMethodLine(string line)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 2; i < _openFigureBracketsCount; i++)
                builder.Append("    ");
            string trimResult = line.Trim();
            builder.Append(trimResult);
            return builder.ToString();
        }

        private string ParseClassName(string line)
        {
            string[] splitResult = line.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
            return splitResult[splitResult.Length - 1];
        }

        private string ParseMethodName(string line)
        {
            string[] splitResult = line.Split(new []{'('}, StringSplitOptions.RemoveEmptyEntries);
            string methodNameEnd = splitResult[1];

            splitResult = splitResult[0].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

            string methodNameStart = splitResult[splitResult.Length - 1];
            methodNameStart = methodNameStart.TrimStart();
            return methodNameStart + "()";

            // Здесь сделано допущение, что в одном классе нет методов с одним и тем же именем
            // даже если у этих методов разная сигнатура. Это сделано для упрощения работы и с целью экономии времени.

            /*  Составление имени метода с сигнатурой
            StringBuilder fullName = new StringBuilder();
            for (int i = 0; i < splitResult.Length - 1; i++)
            {
                string part = splitResult[i];
                if (part.CompareTo("static") != 0 &&_modifiers.Contains(part))
                    continue;
                fullName.Append(part).Append(' ');
            }

            fullName.Append(methodNameStart).Append(" (").Append(methodNameEnd);
            return fullName.ToString();
            */
        }
        #endregion

        #region Second Iteration's methods

        private void CreateMethodCallLinks(ParsedCodeKeeper keeper)
        {
            var entryPoint = keeper.EntryPoint;
            string className = entryPoint.Item1;
            string methodName = entryPoint.Item2;
            var recordQueue = new Queue<MethodRecord>();
            var methodRecord = GetMethodRecord(keeper, className, methodName);

            recordQueue.Enqueue(methodRecord);
            while (recordQueue.Count > 0)
            {
                var currentRecord = recordQueue.Dequeue();
                className = currentRecord.ClassName;
                methodName = currentRecord.MethodName;
                ParseMethodBody(keeper, recordQueue, currentRecord);
            }
        }

        private MethodRecord GetMethodRecord(ParsedCodeKeeper keeper, string className, string methodName)
        {
            MethodRecord result = null;
            foreach (MethodRecord record in keeper.Storage[className])
            {
                if (methodName.CompareTo(record.MethodName) == 0)
                {
                    result = record;
                    break;
                }
            }

            return result;
        }

        private void ParseMethodBody(ParsedCodeKeeper keeper,
            Queue<MethodRecord> queue, MethodRecord currentRecord)
        {
            var variableClasses = new Dictionary<string, string>();
            //Проверки на корректность строки излишн
            foreach (string line in currentRecord.MethodBody)
            {
                if (IsClassCreating(line))
                {
                    string[] names = GetClassAndVariableName(line);
                    string varName = names[0];
                    string className = names[1];
                    variableClasses.Add(varName, className);
                }
                else if (IsMethodCall(line))
                {
                    //Как то проверить является ли вызов статичным или динамичным (от класса или переменной)
                    if (IsInnerMethod(line))
                    {
                        var calledMethodName = GetCalledMethodName(line);
                        var methodBody = GetMethodRecord(keeper, currentRecord.ClassName, calledMethodName).MethodBody;
                        var newRecord = new MethodRecord(currentRecord.ClassName, calledMethodName, methodBody);
                        queue.Enqueue(newRecord);
                        currentRecord.CalledMethods.Add(newRecord);
                    }

                    else 
                    {
                        string sourceName = GetCallSource(line);
                        if (variableClasses.ContainsKey(sourceName))
                            sourceName = variableClasses[sourceName];
                        //А если не содержит то это уже имя класса.
                        string className = sourceName;
                        if (!keeper.Storage.ContainsKey(className))
                            continue;
                        var calledMethodName = GetCalledMethodName(line);
                        var methodBody = GetMethodRecord(keeper, className, calledMethodName).MethodBody;
                        var newRecord = new MethodRecord(className, calledMethodName, methodBody);
                        queue.Enqueue(newRecord);
                        currentRecord.CalledMethods.Add(newRecord);
                    }
                }
            }
        }

        private bool IsMethodCall(string line)
        {
            //Вызов [с записью результата в переменную] метода [принадлежащего текущему классу]
            string pattern = @"^(\s*(\b\Dvar|\b\D\w*)\s+\b\D\w*\s*=)?\s*(\b\D\w*\.)?\D\w*\s*\(.*\);$";
            return Regex.IsMatch(line, pattern);
        }

        private bool IsInnerMethod(string line)
        {
            //Вызов метода внутри класса
            string pattern = @"^(\s*(\b\Dvar|\b\D\w*)\s+\b\D\w*\s*=)?\s*\D\w*\s*\(.*\);$";
            return Regex.IsMatch(line, pattern);
        }

        private bool IsClassCreating(string line)
        {
            //Шаблон для поиска создания экземпляра класса
            string pattern = @"^\s*[var|[_*|\w*]+\s+[_*|\w*]+\s*=\s*new\s+[_*|\w*]*\(.*\);";
            return Regex.IsMatch(line, pattern);
        }

        /// <summary>
        /// GetCallSource("testClass.DoWork();") => testClass.
        /// GetCallSource("MyClass.DoWork();") => MyClass
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string GetCallSource(string line)
        {
            string pattern = @"\s*=\s*";
            var splitResult = Regex.Split(line, pattern);
            string afterEqualSign = splitResult[splitResult.Length - 1];
            string sourceName = afterEqualSign.Split('.')[0];
            return sourceName.Trim();
        }

        private string GetCalledMethodName(string line)
        {
            string pattern = @"\s*=\s*";
            var splitResult = Regex.Split(line, pattern);
            string afterEqualSign = splitResult[splitResult.Length - 1];
            splitResult = afterEqualSign.Split('(');
            string methodName = splitResult[0];
            splitResult = methodName.Split('.');
            methodName = splitResult[splitResult.Length - 1] + "()";
            return methodName.Trim();
        }
        private string[] GetClassAndVariableName(string line)
        {
            string[] result = new string[2];
            // Шаблон для разреза строки до имени переменной
            string pattern = @"^\s*[var|[_*|\w*]+\s+";
            string[] splitResult = Regex.Split(line, pattern);
            string lineWithoutVar = splitResult[splitResult.Length - 1];

            pattern = @"\s*=\s*";
            string[] varSeparateClass = Regex.Split(lineWithoutVar, pattern);
            splitResult = Regex.Split(varSeparateClass[varSeparateClass.Length - 1], @"\s*new\s+");
            string classWithoutNew = splitResult[splitResult.Length - 1];

            result[0] = varSeparateClass[0];
            result[1] = classWithoutNew.Split(new[] { '(' })[0];
            return result;
        }
        #endregion
    }
}
