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

        private string _className;
        private string _methodName;
        private List<string> _methodBody;
        private bool _isDocumentationOpen;
        private bool _isMultilineCommentOpen;
        private int _openFigureBracketsCount;
        private readonly Stack<TabUnit> _tabManager;

        private readonly HashSet<string> _modifiers = new HashSet<string>
        {
            "public",
            "private",
            "protected",
            "internal"
        };

        public FileParser()
        {
            _className = "";
            _methodName = "";
            _methodBody = new List<string>();
            _tabManager = new Stack<TabUnit>();
        }

        public ParsedCodeKeeper ParseCodeListings(List<Document> listings)
        {
            var parseResult = new ParsedCodeKeeper();

            foreach (Document listing in listings)
            {
                List<string> content = listing.GetContent();

                foreach(string line in content)
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
                            FinishDeclaration(parseResult);
                    }
                }
            }
            return parseResult;
        }

        #region Check Conditions
        private bool ShouldSkipLine(string line)
        {
            if (_isMultilineCommentOpen || _isDocumentationOpen)
                return true;

            if (line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Length == 0)
                return true;

            //Шаблон для поиска начала однострочного комментария
            string pattern = @"^\s*\/{2}";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(line))
                return true;

            //Шаблон для поиска начала документации
            pattern = @"^\s*\/{3}\s*<summary>";
            regex = new Regex(pattern);
            if (regex.IsMatch(line))
            {
                _isDocumentationOpen = true;
                return true;
            }

            //Шаблон для поиска конца документации
            pattern = @"^\s*\/{3}\s*</summary>";
            regex = new Regex(pattern);
            if (regex.IsMatch(line))
            {
                _isDocumentationOpen = false;
                return true;
            }

            //Шаблон для поиска начала многострочного комментария
            pattern = @"^\s*\/\*";
            regex = new Regex(pattern);
            if (regex.IsMatch(line))
            {
                _isMultilineCommentOpen = true;
                return true;
            }

            //Шаблон для поиска конца многострочного комментария
            pattern = @"\s*\*\/$";
            regex = new Regex(pattern);
            if (regex.IsMatch(line))
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
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(line))
                return false;

            //Шаблон для поиска ключевого слова void
            pattern = @"\s+void\s+";
            regex = new Regex(pattern);
            if (regex.IsMatch(line))
                return true;

            //Шаблон для поиска объявления метода (с аргументами или без)
            //Пример: public static <T> TestMethod([args])
            //Где на месте [args] может быть любое количество аргументов, в том числе 0
            pattern = @"\s*\(.*\)$";
            regex = new Regex(pattern);
            if (!regex.IsMatch(line))
                return false;

            if (line.Contains(';'))
                return false;

            string varDeclarationPattern = @"^\s*var [_*|\w*]*\s*=\s*";
            Regex varDeclarationRegex = new Regex(varDeclarationPattern);
            MatchCollection matches = varDeclarationRegex.Matches(line);
            if (matches.Count != 0)
                return false;

            return true;
        }

        private bool IsClassDeclaration(string line)
        {
            if (line.Contains('"'))
                return false;

            //Шаблон для поиска ключевого слова class
            string pattern = @"\s+class\s+";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(line))
                return true;
            return false;
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

        private void FinishDeclaration(ParsedCodeKeeper parseResult)
        {
            if (_tabManager.Peek().Type == TabUnit.DeclarationType.Class)
            {
                _tabManager.Pop();
                _className = _tabManager.Count > 0 ? _tabManager.Peek().Name : "";
            }
            else
            {
                parseResult.AddMethodRecord(_className, _methodName, _methodBody);
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
            // Это для того чтобы убрать пробел между именем метода и '('
            // Таким образом все имена методов будут приведены к шаблону
            // [модификатор(ы)] [static] <возвращаемый тип> ИмяМетода ([args])
            methodNameStart = methodNameStart.TrimStart();

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
        }

    }
}
