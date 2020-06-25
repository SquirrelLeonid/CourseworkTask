using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkTask
{
    public class MethodRecord
    {
        public string ClassName;
        public string MethodName;
        public List<string> MethodBody;
        public List<MethodRecord> CalledMethods = new List<MethodRecord>();

        public MethodRecord(string className, string methodName, List<string> methodBody)
        {
            ClassName = className;
            MethodName = methodName;
            MethodBody = methodBody;
        }

        public void SetMethodRecord(List<MethodRecord> calledMethods)
        {
            CalledMethods = calledMethods;
        }

        public override string ToString()
        {
            return ClassName + "." + MethodName;
        }
    }
}
