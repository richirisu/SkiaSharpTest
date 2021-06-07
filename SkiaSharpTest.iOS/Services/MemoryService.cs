using System;
using SkiaSharpTest.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SkiaSharpTest.iOS.Services.MemoryService))]

namespace SkiaSharpTest.iOS.Services
{
    public class MemoryService : IMemoryService
    {
        public long FreeBytes => throw new NotImplementedException();

        public long UsedBytes => MachMemoryHelper.GetResidentSize();

        public long TotalBytes => throw new NotImplementedException();
    }
}
