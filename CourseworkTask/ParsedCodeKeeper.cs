
using System;
using System.Collections.Generic;

namespace CourseworkTask
{
    public class ParsedCodeKeeper
    {
        public Dictionary<string, List<MethodRecord>> Storage { get; }
        public Tuple<string,string> EntryPoint { get; protected set; }

        public ParsedCodeKeeper()
        {
            Storage = new Dictionary<string, List<MethodRecord>>();
        }

        public void AddMethodRecord(string className, string methodName, List<string> methodBody, bool isEntryPoint)
        {
            if (!Storage.ContainsKey(className))
                Storage.Add(className, new List<MethodRecord>());
            Storage[className].Add(new MethodRecord(className, methodName, methodBody));
            if (isEntryPoint && EntryPoint == null)
                EntryPoint = new Tuple<string, string>(className, methodName);
                
        }

        public override string ToString()
        {
            return "keeper has " + Storage.Count + " classes";
        }
    }
}
