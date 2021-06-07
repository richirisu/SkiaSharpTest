//#define WithGradient

using System;
using System.Diagnostics;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using SkiaSharpTest.Services;
using Xamarin.Forms;

namespace SkiaSharpTest
{
    public class MainPage : ContentPage
    {
        SKCanvasView CanvasView;

        public MainPage()
        {
            Content = new StackLayout {
                Children = {
                    (CanvasView = new SKCanvasView {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        EnableTouchEvents = true,
                    }),
                }
            };
            CanvasView.PaintSurface += (sender, eventArgs) => {
                Debug.WriteLine(eventArgs.Info.Size);
                SKSize canvasSize = eventArgs.Info.Size;
                SKCanvas canvas = eventArgs.Surface.Canvas;
                Random r = new Random();
                SKColor randomColor() {
                    uint x = unchecked((uint) r.Next()) | 0xFF000000;
                    return new SKColor(x);
                }
                #if WithGradient
                using SKPaint paint = new SKPaint();
                paint.Shader = SKShader.CreateLinearGradient(SKPoint.Empty, canvasSize.ToPoint(), new[] { randomColor(), randomColor() }, SKShaderTileMode.Clamp);
                canvas.DrawPaint(paint);
                #else
                canvas.Clear(randomColor());
                #endif
            };
            CanvasView.Touch += async (sender, eventArgs) => {
                await Navigation.PushAsync(new MainPage());
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.BeginInvokeOnMainThread(() => {
                Title = $"{Navigation.NavigationStack.Count} · {Memory.UsedBytes >> 20} MB";
            });
        }

        private static readonly IMemoryService Memory = DependencyService.Get<IMemoryService>();
    }
}

