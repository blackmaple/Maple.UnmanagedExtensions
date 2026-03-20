using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Maple.UnmanagedExtensions
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnsafeOut<T>(nint ptr) where T : unmanaged
    {
        [MarshalAs(UnmanagedType.SysInt)]
        readonly nint _Ptr = ptr;

        public UnsafeOut(scoped ref T data) : this(Unsafe.AsPointer(ref data)) { }

        public UnsafeOut(void* ptr) : this(new nint(ptr)) { }


        public ref T Raw => ref Unsafe.AsRef<T>(_Ptr.ToPointer());
        public readonly nint Pointer => this._Ptr;

        public static UnsafeOut<T> FromOut(scoped out T data)
        {
            Unsafe.SkipInit(out data);
            return new UnsafeOut<T>(ref data);
        }


        public static implicit operator nint(UnsafeOut<T> data) => data._Ptr;
        public static implicit operator UnsafePtr<T>(UnsafeOut<T> data) => data._Ptr;
        public static implicit operator UnsafePtr(UnsafeOut<T> data) => data._Ptr;
        public static implicit operator bool(UnsafeOut<T> v) => v._Ptr != nint.Zero;

        public readonly override string ToString()
        {
            return _Ptr.ToString("X8");
        }
    }

}
