using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

public class RectangleShape : Shape
{
    public int Width { get; set; } = 100;
    public int Height { get; set; } = 60;

    public override void Draw(Graphics graphics)
    {
        using (Brush brush = new SolidBrush(Color))
        {
            graphics.FillRectangle(brush, Location.X, Location.Y, Width, Height);
        }
        DrawSelection(graphics);
    }

    public override bool ContainsPoint(Point point)
    {
        return point.X >= Location.X && point.X <= Location.X + Width &&
                point.Y >= Location.Y && point.Y <= Location.Y + Height;
    }

    public override Rectangle GetBounds()
    {
        return new Rectangle(Location.X, Location.Y, Width, Height);
    }
}