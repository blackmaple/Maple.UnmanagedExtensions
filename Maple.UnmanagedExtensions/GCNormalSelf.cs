using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Maple.UnmanagedExtensions
{
    public class GCNormalSelf : IDisposable
    {
        GCHandle Obj { get; }
        public nint Handle => GCHandle.ToIntPtr(Obj);

        public GCNormalSelf()
        {
            this.Obj = GCHandle.Alloc(this, GCHandleType.Normal);
        }
        public static bool TryGet<T>(nint ptr, [MaybeNullWhen(false)] out T obj) where T:GCNormalSelf
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
