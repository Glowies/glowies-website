using System;
using System.Numerics;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using MudBlazor;
using SeedSharp;

namespace glowies_website.Shared.Seed
{
    public class Canvas2DSeedRenderer: SeedRendererBase
    {
        public Canvas2DContext Context { get; set; }

        public Canvas2DSeedRenderer(Canvas2DContext context, Plant plant) : base(plant)
        {
            Context = context;
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override async Task RenderAsync()
        {
            await Context.ClearRectAsync(0, 0, 9001, 9001);

            await Context.SetFillStyleAsync("white");
            await Context.SetStrokeStyleAsync("white");

            await Context.SetLineWidthAsync(2);
            await Context.BeginPathAsync();

            await Context.BeginBatchAsync();

            await base.RenderAsync();

            await Context.EndBatchAsync();

            await Context.ClosePathAsync();
            await Context.StrokeAsync();
        }

        public override void DrawLine(Vector2 p1, Vector2 p2)
        {
            throw new NotImplementedException();
        }

        public override async Task DrawLineAsync(Vector2 p1, Vector2 p2)
        {
            if (Context is null)
                return;

            await Context.MoveToAsync(p1.X, p1.Y);
            await Context.LineToAsync(p2.X, p2.Y);
        }

        public override void SetViewportScale(Vector2 scale)
        {
            var scaleFactor = 16;

            var min = MathF.Min(scale.X, scale.Y);

            var translateVector = new Vector2(
                scale.X / min / 2,
                -scale.Y / min
            );
            translateVector *= scaleFactor;
            var scaleVector = new Vector2(min, -min);
            scaleVector /= scaleFactor;

            var translation = Matrix3x2.CreateTranslation(translateVector);
            var scaling = Matrix3x2.CreateScale(scaleVector);

            WorldToViewMatrix = translation * scaling;
        }
    }
}
