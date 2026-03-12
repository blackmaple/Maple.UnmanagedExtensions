using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Maple.UnmanagedExtensions
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnsafeOut<T>(scoped ref T data) where T : unmanaged
    {
        [MarshalAs(UnmanagedType.SysInt)]
        readonly nint _ptr = new(Unsafe.AsPointer(ref data));

        //public ref T Raw => ref Unsafe.AsRef<T>(_ptr.ToPointer());

        public static UnsafeOut<T> FromOut(scoped out T data)
        {
            Unsafe.SkipInit(out data);
            return new UnsafeOut<T>(ref data);
        }


        public static implicit operator nint(UnsafeOut<T> data) => data._ptr;

    }

}
