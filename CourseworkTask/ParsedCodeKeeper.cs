
using System.Collections.Generic;

namespace CourseworkTask
{
    public class ParsedCodeKeeper
    {
        private readonly Dictionary<string, Dictionary<string, List<string>>> _storage;

        public ParsedCodeKeeper()
        {
            _storage = new Dictionary<string, Dictionary<string, List<string>>>();
        }

        public void AddMethodRecord(string className, string methodName, List<string> methodBody)
        {
            if (!_storage.ContainsKey(className))
                _storage.Add(className, new Dictionary<string, List<string>>());
            _storage[className].Add(methodName, methodBody);
        }

        public Dictionary<string, Dictionary<string, List<string>>> GetStorage()
        {
            return _storage;
        }

        public override string ToString()
        {
            return "keeper has " + _storage.Count + " classes";
        }
    }
}
