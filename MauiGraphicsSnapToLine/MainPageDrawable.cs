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

        const int HolesCount = 5; // TODO

        const int LineSpacing = 40;

        float x1 = SnapToLineVM.SnapHolesToLinePoint1.X;
        float y1 = SnapToLineVM.SnapHolesToLinePoint1.Y;
        float x2 = SnapToLineVM.SnapHolesToLinePoint2.X;
        float y2 = SnapToLineVM.SnapHolesToLinePoint2.Y;
        float dx = x2 - x1;
        float dy = y2 - y1;

        float angleRad = (float)Math.Atan2(dy, dx);
        float angleDeg = angleRad * 180 / (float)Math.PI;

        bool isFlip = angleDeg < -90 || angleDeg > 90;

        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

        float distanceBetween2Holes = distance / HolesCount;

        canvas.StrokeColor = Colors.Blue;
        canvas.StrokeSize = 3;

        canvas.SaveState();
        canvas.Rotate(angleDeg, x1, y1);

        float y;

        y = y1 + (isFlip ? LineSpacing : -LineSpacing);
        DrawLineWithArrow(canvas, x1 + distance - distanceBetween2Holes, y,
            x1 + distance, y,
            new float[] { 5, 5 }, distanceBetween2Holes.ToString("n2"), isFlip);

        DrawLineWithArrow(canvas, x1, y1,
            x1 + distance, y1,
            new float[] { 15, 15 });

        y = y1 + (isFlip ? -LineSpacing : LineSpacing);
        DrawLineWithArrow(canvas, x1, y,
            x1 + distance, y,
            null, distance.ToString("n2"), isFlip);

        canvas.RestoreState();
    }

    private void DrawLineWithArrow(ICanvas canvas, float x1, float y1, float x2, float y2, float[]? strokeDashPattern, string? text = null, bool? isFlip = null)
    {
        const int Spacing = 10;

        float mx = (x1 + x2) / 2;

        canvas.StrokeDashPattern = strokeDashPattern;
        canvas.DrawLine(x1, y1, x2, y2);

        // draw t shaped arrow
        DrawArrow(canvas, x1, y1, 10, 90, LeftOrRight.Left);
        DrawArrow(canvas, x2, y2, 10, 90, LeftOrRight.Right);

        DrawArrow(canvas, x1, y1, 15, 30, LeftOrRight.Left);
        DrawArrow(canvas, x2, y2, 15, 30, LeftOrRight.Right);

        if (!string.IsNullOrWhiteSpace(text))
        {
            canvas.FontColor = Colors.Blue;
            canvas.FontSize = 15;
            canvas.Font = Microsoft.Maui.Graphics.Font.Default;

            if (isFlip == true)
            {
                canvas.SaveState();
                canvas.Rotate(180, mx, y1 - Spacing);
                canvas.DrawString(text, mx, y1 - Spacing * 3, HorizontalAlignment.Center);
                canvas.RestoreState();
            }
            else
            {
                canvas.DrawString(text, mx, y1 - Spacing, HorizontalAlignment.Center);
            }
        }
    }

    private enum LeftOrRight { Left, Right }
    private void DrawArrow(ICanvas canvas, float x, float y, float size, float angleDeg, LeftOrRight direction)
    {
        float sx = x;
        float ex = x + (direction == LeftOrRight.Left ? size : -size);

        canvas.StrokeDashPattern = null;

        canvas.DrawLine(sx, y, ex, y);

        canvas.SaveState();
        canvas.Rotate(-angleDeg, sx, y);
        canvas.DrawLine(sx, y, ex, y);
        canvas.RestoreState();

        canvas.SaveState();
        canvas.Rotate(angleDeg, sx, y);
        canvas.DrawLine(sx, y, ex, y);
        canvas.RestoreState();
    }
}
