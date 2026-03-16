using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Maple.UnmanagedExtensions
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnsafeIn<T>(scoped in T data) where T : unmanaged
    {
        [MarshalAs(UnmanagedType.SysInt)]
        readonly nint _ptr = new(Unsafe.AsPointer(ref Unsafe.AsRef(in data)));

        public ref T Raw => ref Unsafe.AsRef<T>(_ptr.ToPointer());

        public static UnsafeIn<T> FromIn(scoped in T data)
        {
            return new UnsafeIn<T>(in data);
        }


        public static implicit operator nint(UnsafeIn<T> data) => data._ptr;

    }

}
