using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Maple.UnmanagedExtensions
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnsafePtr<T>(nint ptr) where T : unmanaged
    {
        [MarshalAs(UnmanagedType.SysInt)]
        readonly nint _ptr = ptr;

        public ref T Raw => ref Unsafe.AsRef<T>(_ptr.ToPointer());

        public static implicit operator nint(UnsafePtr<T> ptr) => ptr._ptr;
        public static implicit operator UnsafePtr<T>(nint ptr) => new(ptr);
    }
}
