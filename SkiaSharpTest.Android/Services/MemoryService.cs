using System;
using SkiaSharpTest.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SkiaSharpTest.Droid.Services.MemoryService))]

namespace SkiaSharpTest.Droid.Services
{
    public class MemoryService : IMemoryService
    {
        public long FreeBytes => Java.Lang.Runtime.GetRuntime().FreeMemory();

        public long UsedBytes => Java.Lang.Runtime.GetRuntime().TotalMemory();

        public long TotalBytes => Java.Lang.Runtime.GetRuntime().MaxMemory();
    }
}
