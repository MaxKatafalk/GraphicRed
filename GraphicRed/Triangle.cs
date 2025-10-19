using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

public class TriangleShape : Shape
{
    public int Width { get; set; } = 100;
    public int Height { get; set; } = 60;

    public override void Draw(Graphics graphics)
    {
        Point[] points = new Point[]
        {
            new Point(Location.X + Width / 2, Location.Y),
            new Point(Location.X, Location.Y + Height),
            new Point(Location.X + Width, Location.Y + Height)
        };

        using (Brush brush = new SolidBrush(Color))
        {
            graphics.FillPolygon(brush, points);
        }
        DrawSelection(graphics);
    }

    public override bool ContainsPoint(Point point)
    {
        Rectangle bounds = GetBounds();
        return bounds.Contains(point);
    }

    public override Rectangle GetBounds()
    {
        return new Rectangle(Location.X, Location.Y, Width, Height);
    }
}

