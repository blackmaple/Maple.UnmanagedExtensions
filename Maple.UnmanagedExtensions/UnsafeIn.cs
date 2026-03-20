using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Maple.UnmanagedExtensions
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnsafeIn<T>(nint ptr) where T : unmanaged
    {
        [MarshalAs(UnmanagedType.SysInt)]
        readonly nint _Ptr = ptr;


        public UnsafeIn(scoped in T data) : this(Unsafe.AsPointer(ref Unsafe.AsRef(in data))) { }

        public UnsafeIn(void* ptr) : this(new nint(ptr)) { }

        public ref T Raw => ref Unsafe.AsRef<T>(_Ptr.ToPointer());
        public readonly nint Pointer => this._Ptr;

        public static UnsafeIn<T> FromIn(scoped in T data)
        {
            return new UnsafeIn<T>(in data);
        }


        public static implicit operator nint(UnsafeIn<T> data) => data._Ptr;
        public static implicit operator UnsafePtr<T>(UnsafeIn<T> data) => data._Ptr;
        public static implicit operator UnsafePtr(UnsafeIn<T> data) => data._Ptr;
        public static implicit operator bool(UnsafeIn<T> v) => v._Ptr != nint.Zero;

        public readonly override string ToString()
        {
            return _Ptr.ToString("X8");
        }
    }

}
