using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Maple.UnmanagedExtensions
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnsafeRef<T>(scoped ref T data) where T : unmanaged
    {
        [MarshalAs(UnmanagedType.SysInt)]
        readonly nint _ptr = new(Unsafe.AsPointer(ref data));

        //public ref T Raw => ref Unsafe.AsRef<T>(_ptr.ToPointer());

        public static UnsafeRef<T> FromRef(scoped ref T data)
        {
            return new UnsafeRef<T>(ref data);
        }


        public static implicit operator nint(UnsafeRef<T> data) => data._ptr;
    }

}
