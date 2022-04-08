using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ShellCodeExecutor
{
    delegate int ShellCodeFunc();
    internal class DelegateShellCodeRunner : IShellCodeRunner
    {
        public void RunShellCode(IntPtr rwxMemory)
        {
            ShellCodeFunc shellCodeFunc = Marshal.GetDelegateForFunctionPointer<ShellCodeFunc>(rwxMemory);
            shellCodeFunc();
        }
        override
        public String ToString()
        {
            return "Delegate";
        }
    }
}
