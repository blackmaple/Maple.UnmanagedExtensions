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
