using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Maple.UnmanagedExtensions
{
    public class GCPinnedSelf : IDisposable
    {
        GCHandle Obj { get; }
        public nint Handle => GCHandle.ToIntPtr(Obj);
        public nint AddressPointer => Obj.AddrOfPinnedObject();
        public GCPinnedSelf()
        {
            this.Obj = GCHandle.Alloc(this, GCHandleType.Pinned);
        }

        public static bool TryGetFromHandle<T>(nint ptr, [MaybeNullWhen(false)] out T obj) where T : GCPinnedSelf
        {
            Unsafe.SkipInit(out obj);
            if (ptr != nint.Zero)
            {
                var handle = GCHandle.FromIntPtr(ptr);
                if (handle.IsAllocated && handle.Target is T item)
                {
                    obj = item;
                    return true;
                }
            }
            return false;
        }

        public static bool TrtGetFromPointer<T>(nint ptr, [MaybeNullWhen(false)] out T obj) where T : GCPinnedSelf
        {
            Unsafe.SkipInit(out obj);
            if (ptr != nint.Zero)
            {
                obj = Unsafe.As<nint, T>(ref ptr);
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            if (this.Obj.IsAllocated)
            {
                this.Obj.Free();
            }
            GC.SuppressFinalize(this);
        }
    }

}
