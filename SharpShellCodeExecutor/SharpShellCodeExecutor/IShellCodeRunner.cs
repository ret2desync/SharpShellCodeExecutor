using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellCodeExecutor
{
    public interface IShellCodeRunner
    {
        void RunShellCode(IntPtr rwxMemory);
    }
}
