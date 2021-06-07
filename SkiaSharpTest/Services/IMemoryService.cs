using System;

namespace SkiaSharpTest.Services
{
    public interface IMemoryService
    {
        long FreeBytes { get; }
        long UsedBytes { get; }
        long TotalBytes { get; }
    }
}
