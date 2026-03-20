using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Maple.UnmanagedExtensions
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnsafeRef<T>(nint ptr) where T : unmanaged
    {
        [MarshalAs(UnmanagedType.SysInt)]
        readonly nint _Ptr = ptr;

        public UnsafeRef(scoped ref T data) : this(Unsafe.AsPointer(ref data)) { }

        public UnsafeRef(void* ptr) : this(new nint(ptr)) { }


        public ref T Raw => ref Unsafe.AsRef<T>(_Ptr.ToPointer());
        public readonly nint Pointer => this._Ptr;

        public static UnsafeRef<T> FromRef(scoped ref T data)
        {
            return new UnsafeRef<T>(ref data);
        }


        public static implicit operator nint(UnsafeRef<T> data) => data._Ptr;
        public static implicit operator UnsafePtr<T>(UnsafeRef<T> data) => data._Ptr;
        public static implicit operator UnsafePtr(UnsafeRef<T> data) => data._Ptr;
        public static implicit operator bool(UnsafeRef<T> v) => v._Ptr != nint.Zero;

        public readonly override string ToString()
        {
            return _Ptr.ToString("X8");
        }
    }

}
