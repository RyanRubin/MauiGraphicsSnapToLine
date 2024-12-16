namespace MauiGraphicsSnapToLine;

public partial class MainPage : ContentPage
{
    public HolesDiagramSnapToLineViewModel SnapToLineVM { get; set; }

    public MainPage()
    {
        InitializeComponent();
        SnapToLineVM = new() { GraphicsView = graphicsView };
        graphicsView.Drawable = new MainPageDrawable { SnapToLineVM = SnapToLineVM };

        Task.Run(async () =>
        {
            string? path = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            if (string.IsNullOrWhiteSpace(path)) return;
            path = Path.Combine(path, "Image Tracer 1.xyz");
            var result = await ImageTracerXyzFileParser.Parse(path);
        });
    }

    private void GraphicsView_StartInteraction(object sender, TouchEventArgs e)
    {
        SnapToLineVM.IsSnapHolesToLine = true; // TODO

        if (SnapToLineVM.IsSnapHolesToLine)
        {
            SnapToLineVM.StartSnapHolesToLine(e.Touches[0]);
        }
    }

    private void GraphicsView_DragInteraction(object sender, TouchEventArgs e)
    {
        if (SnapToLineVM.IsSnapHolesToLine)
        {
            SnapToLineVM.SnapHolesToLine(e.Touches[0]);
        }
    }

    private void GraphicsView_EndInteraction(object sender, TouchEventArgs e)
    {
        if (SnapToLineVM.IsSnapHolesToLine)
        {
            SnapToLineVM.EndSnapHolesToLine();
        }
    }
}
