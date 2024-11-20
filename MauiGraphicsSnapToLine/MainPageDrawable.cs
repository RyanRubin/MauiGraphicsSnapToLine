namespace MauiGraphicsSnapToLine;

public class MainPageDrawable : IDrawable
{
    public HolesDiagramSnapToLineViewModel SnapToLineVM { get; set; } = default!;

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        DrawSnapHolesToLineMarker(canvas);
    }

    private void DrawSnapHolesToLineMarker(ICanvas canvas)
    {
        if (!SnapToLineVM.IsSnapHolesToLine)
        {
            return;
        }

        canvas.StrokeColor = Colors.Blue;
        canvas.StrokeSize = 3;
        canvas.DrawLine(SnapToLineVM.SnapHolesToLinePoint1.X, SnapToLineVM.SnapHolesToLinePoint1.Y, SnapToLineVM.SnapHolesToLinePoint2.X, SnapToLineVM.SnapHolesToLinePoint2.Y);
    }
}
