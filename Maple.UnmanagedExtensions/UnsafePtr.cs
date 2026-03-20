using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Maple.UnmanagedExtensions
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnsafePtr<T>(nint ptr) where T : unmanaged
    {
        [MarshalAs(UnmanagedType.SysInt)]
        readonly nint _Ptr = ptr;

        public ref T Raw => ref Unsafe.AsRef<T>(_Ptr.ToPointer());
        public readonly nint Pointer => this._Ptr;

        public static implicit operator nint(UnsafePtr<T> ptr) => ptr._Ptr;
        public static implicit operator void*(UnsafePtr<T> ptr) => ptr._Ptr.ToPointer();
        public static implicit operator UnsafePtr<T>(nint ptr) => new(ptr);
        public static implicit operator UnsafeIn<T>(UnsafePtr<T> ptr) => new(ptr._Ptr);
        public static implicit operator UnsafeRef<T>(UnsafePtr<T> ptr) => new(ptr._Ptr);
        public static implicit operator UnsafeOut<T>(UnsafePtr<T> ptr) => new(ptr._Ptr);
        public readonly override string ToString()
        {
            return _Ptr.ToString("X8");
        }
    }


    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnsafePtr(nint ptr)
    {
        [MarshalAs(UnmanagedType.SysInt)]
        readonly nint _Ptr = ptr;

        public UnsafePtr(void* ptr) : this(new nint(ptr))
        {
        }


        public static implicit operator nint(UnsafePtr v) => v._Ptr;
        public static implicit operator void*(UnsafePtr v) => v._Ptr.ToPointer();
        public static implicit operator UnsafePtr(nint v) => new(v);
        public static implicit operator UnsafePtr(void* v) => new(v);
        public static implicit operator bool(UnsafePtr v) => v._Ptr != nint.Zero;

        public readonly ref T GetRaw<T>() where T : unmanaged => ref Unsafe.AsRef<T>(_Ptr.ToPointer());
        public readonly nint Pointer => this._Ptr;

        public readonly UnsafeIn<T> GetUnsafeIn<T>() where T : unmanaged => new (in GetRaw<T>());
        public readonly UnsafeOut<T> GetUnsafeOut<T>() where T : unmanaged => new(ref GetRaw<T>());
        public readonly UnsafeRef<T> GetUnsafeRef<T>() where T : unmanaged => new(ref GetRaw<T>());
 

        public readonly override string ToString()
        {
            return _Ptr.ToString("X8");
        }
    }

}
