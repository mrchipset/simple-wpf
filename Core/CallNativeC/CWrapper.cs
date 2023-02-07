using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CallNativeC
{
    class CWrapper
    {
    [DllImport("dll_demo.dll", EntryPoint = "Foo", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Foo();

    [DllImport("dll_demo.dll", EntryPoint = "Add", CallingConvention = CallingConvention.Cdecl)]
    public static extern int Add(int a, int b);

    [DllImport("dll_demo.dll", EntryPoint = "Sum", CallingConvention = CallingConvention.Cdecl)]
    public static extern double Sum(double[] arr, int n);
    [DllImport("dll_demo.dll", EntryPoint = "ChangeContent", CallingConvention = CallingConvention.Cdecl)]
    public static extern double Change(double[] arr, int n);
    }
}
