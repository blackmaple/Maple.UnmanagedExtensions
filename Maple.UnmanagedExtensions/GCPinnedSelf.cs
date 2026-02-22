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
