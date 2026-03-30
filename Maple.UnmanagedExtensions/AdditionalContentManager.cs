using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace Maple.UnmanagedExtensions
{
    public class AdditionalContentManager : AdditionalContentManager<object>
    {

        public void Set<T>(string key, T content) where T : class
        {
            this.AdditionalContent[key] = new WeakReference<object>(content);
        }

        public bool TryGet<T>(string key, [MaybeNullWhen(false)] out T content) where T : class
        {
            Unsafe.SkipInit(out content);
            if (TryGet(key, out var weakReference))
            {
                if (weakReference is T weakReference2)
                {
                    content = weakReference2;
                    return true;
                }
            }
            return false;
        }


    }

    public class AdditionalContentManager<T>   where T : class
    {
        protected Dictionary<string, WeakReference<T>> AdditionalContent { get; } = [];

        

        public void Set(string key, T content)
        {
            this.AdditionalContent[key] = new WeakReference<T>(content);
        }

        public bool TryGet(string key, [MaybeNullWhen(false)] out T content)
        {
            Unsafe.SkipInit(out content);
            if (this.AdditionalContent.TryGetValue(key, out var weakReference))
            {
                return weakReference.TryGetTarget(out content);
            }
            return false;
        }
        public void Clear()
        {
            this.AdditionalContent.Clear();
        }

    }

}
