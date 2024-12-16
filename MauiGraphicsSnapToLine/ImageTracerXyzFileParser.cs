namespace MauiGraphicsSnapToLine;

public class ImageTracerData
{
    public string? PatternName { get; set; }
    public List<ImageTracerPoints> Points { get; set; } = new();
}

public class ImageTracerPoints
{
    public double X { get; set; }
    public double Y { get; set; }
}

public class InvalidImageTracerXyzFileException : Exception
{
    public InvalidImageTracerXyzFileException() : base("Invalid image tracer xyz file.") { }
}

public class ImageTracerXyzFileParser
{
    public static async Task<ImageTracerData> Parse(string file)
    {
        var result = new ImageTracerData();
        var splitOpt = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

        string[] lines = await File.ReadAllLinesAsync(file);
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (i == 0)
            {
                if (line.Length == 0) throw new InvalidImageTracerXyzFileException();
            }
            else if (i == 1)
            {
                string[] lineArr = line.Split(':', splitOpt);
                if (!string.Equals(lineArr[0], "Pattern Name", StringComparison.OrdinalIgnoreCase)) throw new InvalidImageTracerXyzFileException();
                result.PatternName = lineArr[1];
            }
            else if (i == 2)
            {
                string[] lineArr = line.Split(':', splitOpt);
                if (!string.Equals(lineArr[0], "Current Date", StringComparison.OrdinalIgnoreCase)) throw new InvalidImageTracerXyzFileException();
            }
            else if (i == 3)
            {
                if (!line.Equals("Units: US", StringComparison.OrdinalIgnoreCase)) throw new InvalidImageTracerXyzFileException();
            }
            else
            {
                string[] lineArr = line.Split(' ', splitOpt);
                if (!double.TryParse(lineArr[0], out double x) || !double.TryParse(lineArr[1], out double y)) throw new InvalidImageTracerXyzFileException();
                result.Points.Add(new ImageTracerPoints { X = x, Y = y });
            }
        }

        return result;
    }
}
