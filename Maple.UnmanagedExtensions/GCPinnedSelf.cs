using System.Runtime.InteropServices;

namespace Maple.UnmanagedExtensions
{
    public class GCPinnedSelf<T> : IDisposable where T : GCPinnedSelf<T>
    {
        PinnedGCHandle<GCPinnedSelf<T>> Handle { get; }
        public nint HandlePointer => PinnedGCHandle<GCPinnedSelf<T>>.ToIntPtr(Handle);

        public GCPinnedSelf()
        {
            Handle = new PinnedGCHandle<GCPinnedSelf<T>>(this);
        }

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
