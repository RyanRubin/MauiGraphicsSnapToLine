namespace MauiGraphicsSnapToLine;

public class HolesDiagramSnapToLineViewModel
{
    public const int HolesCount = 5; // TODO

    public GraphicsView? GraphicsView { get; set; }

    public bool IsSnapHolesToLine { get; set; }
    public PointF SnapHolesToLinePoint1 { get; set; }
    public PointF SnapHolesToLinePoint2 { get; set; }
    public List<PointF> SnapHolesToLineSpacedPoints { get; set; } = new();

    public void StartSnapHolesToLine(PointF point)
    {
        SnapHolesToLinePoint1 = point;
    }

    public void SnapHolesToLine(PointF point)
    {
        SnapHolesToLinePoint2 = point;
        GraphicsView?.Invalidate();

        float stepX = (SnapHolesToLinePoint2.X - SnapHolesToLinePoint1.X) / (HolesCount - 1);
        float stepY = (SnapHolesToLinePoint2.Y - SnapHolesToLinePoint1.Y) / (HolesCount - 1);

        SnapHolesToLineSpacedPoints.Clear();

        SnapHolesToLineSpacedPoints.Add(SnapHolesToLinePoint1);
        for (int i = 1; i <= HolesCount - 2; i++)
        {
            SnapHolesToLineSpacedPoints.Add(new(SnapHolesToLinePoint1.X + stepX * i, SnapHolesToLinePoint1.Y + stepY * i));
        }
        SnapHolesToLineSpacedPoints.Add(SnapHolesToLinePoint2);
    }

    public void EndSnapHolesToLine()
    {

    }
}
