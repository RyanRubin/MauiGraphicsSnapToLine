namespace MauiGraphicsSnapToLine;

public class ImageTracerXyz
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
}

public class ImageTracerXyzFileParser
{
    public static async Task<List<ImageTracerXyz>> Parse(string file)
    {
        var result = new List<ImageTracerXyz>();

        try
        {
            string[] lines = await File.ReadAllLinesAsync(file);

            foreach (string line in lines)
            {

            }
        }
        catch (Exception ex)
        {

        }

        return result;
    }
}
