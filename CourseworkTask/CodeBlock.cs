using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseworkTask
{
    public class CodeBlock
    {
        private static readonly Size GroupBoxDefaultSize = new Size(200, 200);
        private static readonly Size TextBoxDefaultSize = new Size(190, 160);

        private string ClassName { get; }
        private string MethodName { get; }
        private GroupBox Block { get; }

        public CodeBlock(string className, string methodName, List<string> methodBody, CodeBlock caller)
        {
            ClassName = className;
            MethodName = methodName;
            Block = CreateGroupBox();
            Block.Controls.Add(CreateTextBox(methodBody));
        }

        private GroupBox CreateGroupBox()
        {
            var box = new GroupBox();
            box.Size = GroupBoxDefaultSize;
            box.Text = MethodName;
            return box;
        }

        private TextBox CreateTextBox(List<string> methodBody)
        {
            var box = new TextBox();
            box.Enabled = false;
            box.Multiline = true;
            box.Dock = DockStyle.Top;
            box.Size = TextBoxDefaultSize;
            box.Lines = methodBody.ToArray();
            box.ScrollBars = ScrollBars.Vertical;
            return box;
        }
    }
}