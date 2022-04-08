using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace ShellCodeExecutor
{

  
    internal class MemoryAllocator
    {
      
        [DllImport("kernel32")]
        private static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocExNuma(IntPtr hProcess, IntPtr lpAddress, uint dwSize, UInt32 flAllocationType, UInt32 flProtect, UInt32 nndPreferred);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcess();

        [DllImport("ntdll.dll")]
        public static extern UInt32 NtCreateSection(ref IntPtr section,UInt32 desiredAccess,IntPtr pAttrs,ref long MaxSize,uint pageProt,uint allocationAttribs,IntPtr hFile);

        // NtMapViewOfSection
        [DllImport("ntdll.dll")]
        public static extern UInt32 NtMapViewOfSection(IntPtr SectionHandle,IntPtr ProcessHandle,ref IntPtr BaseAddress,IntPtr ZeroBits,IntPtr CommitSize,ref long SectionOffset,ref long ViewSize,uint InheritDisposition,uint AllocationType,uint Win32Protect);

        // NtUnmapViewOfSection
        [DllImport("ntdll.dll", SetLastError = true)]
        static extern uint NtUnmapViewOfSection(
            IntPtr hProc,
            IntPtr baseAddr);

        // NtClose
        [DllImport("ntdll.dll", ExactSpelling = true, SetLastError = false)]
        static extern int NtClose(IntPtr hObject);

        public enum ALLOCATE_TYPES
        {
            VirtualAlloc,
            VirtualAllocEx,
            VirtualAllocExNuma,
            NtCreateSection
        }
        private static UInt32 MEM_COMMIT = 0x1000;
        private static UInt32 PAGE_EXECUTE_READWRITE = 0x40;
        private static uint SEC_COMMIT = 0x08000000;
      
        public static IntPtr AllocateMemory(uint size, ALLOCATE_TYPES allocateType)
        {
            switch (allocateType)
            {
                case ALLOCATE_TYPES.VirtualAlloc:
                    return VirtualAlloc(IntPtr.Zero, size, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
                case ALLOCATE_TYPES.VirtualAllocEx:
               
                    return VirtualAllocEx(GetCurrentProcess(), IntPtr.Zero, size, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
                case ALLOCATE_TYPES.VirtualAllocExNuma:
                    return VirtualAllocExNuma(GetCurrentProcess(), IntPtr.Zero, size, MEM_COMMIT, PAGE_EXECUTE_READWRITE, 0);
                case ALLOCATE_TYPES.NtCreateSection:
                    IntPtr sectionHandle = IntPtr.Zero;
                    long sizeLong = Convert.ToInt64(size);
                    NtCreateSection(ref sectionHandle, 0xe, IntPtr.Zero, ref sizeLong, PAGE_EXECUTE_READWRITE, SEC_COMMIT, IntPtr.Zero);
                    long localSectionOffset = 0;
                    IntPtr ptrLocalSectionAddress = IntPtr.Zero;
                    NtMapViewOfSection(sectionHandle, GetCurrentProcess(), ref ptrLocalSectionAddress, IntPtr.Zero, IntPtr.Zero, ref localSectionOffset, ref sizeLong, 0x2, 0, PAGE_EXECUTE_READWRITE);
                    return ptrLocalSectionAddress;
            }
            return IntPtr.Zero;
        }
      
    }
}
