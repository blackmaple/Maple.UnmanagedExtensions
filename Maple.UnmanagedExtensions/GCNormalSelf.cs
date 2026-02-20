using System.Runtime.InteropServices;

namespace Maple.UnmanagedExtensions
{
    public class GCNormalSelf<T> : IDisposable where T : GCNormalSelf<T>
    {
        GCHandle<GCNormalSelf<T>> Handle { get; }
        public nint HandlePointer => GCHandle<GCNormalSelf<T>>.ToIntPtr(Handle);

        public GCNormalSelf()
        {
            Handle = new GCHandle<GCNormalSelf<T>>(this);
        }

        public void Dispose()
        {
            this.Handle.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
