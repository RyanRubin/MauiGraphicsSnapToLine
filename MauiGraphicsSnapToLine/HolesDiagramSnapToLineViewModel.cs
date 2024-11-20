namespace MauiGraphicsSnapToLine;

public class HolesDiagramSnapToLineViewModel
{
    public GraphicsView? GraphicsView { get; set; }

    public bool IsSnapHolesToLine { get; set; }
    public PointF SnapHolesToLinePoint1 { get; set; }
    public PointF SnapHolesToLinePoint2 { get; set; }

    public void StartSnapHolesToLine(PointF point)
    {
        SnapHolesToLinePoint1 = point;
    }

    public void SnapHolesToLine(PointF point)
    {
        SnapHolesToLinePoint2 = point;
        GraphicsView?.Invalidate();
    }

    public void EndSnapHolesToLine()
    {

    }
}
