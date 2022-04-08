using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace ShellCodeExecutor
{
    internal class MemoryWriter
    {
        public enum WRITE_METHOD
        {
            RtlMoveMemory,
            WriteProcessMemory,
            MarshalCopy
        }
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern void RtlMoveMemory(IntPtr destData, IntPtr srcData, int size);
        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcess();

        public static void WriteToMemory(IntPtr memAddress, byte[] shellCode, WRITE_METHOD writeType){
            
                switch (writeType){
                    case WRITE_METHOD.RtlMoveMemory:
                        unsafe{
                            fixed (byte* p = shellCode){
                                IntPtr shellCodePtr = (IntPtr)p;
                                RtlMoveMemory(memAddress, shellCodePtr, shellCode.Length);
                            }
                        }
                        break;
                    case WRITE_METHOD.MarshalCopy:
                        Marshal.Copy(shellCode, 0, memAddress, shellCode.Length);
                        break;
                    case WRITE_METHOD.WriteProcessMemory:
                        IntPtr outSize;
                        WriteProcessMemory(GetCurrentProcess(), memAddress, shellCode, shellCode.Length, out outSize);
                    break;
                }
            }
        }
    }

