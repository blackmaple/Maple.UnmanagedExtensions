using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Maple.UnmanagedExtensions
{

    public readonly struct GCPinnedObject<T>(T obj) : IDisposable
       where T : class
    {
        readonly GCHandle _handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
        public readonly nint Handle => GCHandle.ToIntPtr(_handle);
        public readonly nint AddressPointer => _handle.AddrOfPinnedObject();
        public static bool TryGet(nint ptr, [MaybeNullWhen(false)] out T obj)
        {
            Unsafe.SkipInit(out obj);
            if (ptr != nint.Zero)
            {
                var handle = GCHandle.FromIntPtr(ptr);
                if (handle.IsAllocated && handle.Target is T b)
                {
                    obj = b;
                    return true;
                }
            }
            return false;
        }
        public static GCPinnedObject<T> Create(T obj) => new(obj);
        public void Dispose()
        {
            if (_handle.IsAllocated)
            {
                _handle.Free();
            }
        }

        public static implicit operator GCPinnedObject<T>(T obj) => new(obj);
        public static implicit operator nint(GCPinnedObject<T> obj) => obj.Handle;

    }

}
