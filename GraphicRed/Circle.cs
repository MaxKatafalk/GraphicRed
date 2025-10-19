using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

public class CircleShape : Shape
{
    public int Radius { get; set; } = 50;

    public override void Draw(Graphics graphics)
    {
        int diameter = Radius * 2;
        using (Brush brush = new SolidBrush(Color))
        {
            graphics.FillEllipse(brush, Location.X, Location.Y, diameter, diameter);
        }
        DrawSelection(graphics);
    }

    public override bool ContainsPoint(Point point)
    {
        Point center = new Point(Location.X + Radius, Location.Y + Radius);
        double distance = Math.Sqrt(Math.Pow(point.X - center.X, 2) + Math.Pow(point.Y - center.Y, 2));
        return distance <= Radius;
    }

    public override Rectangle GetBounds()
    {
        int diameter = Radius * 2;
        return new Rectangle(Location.X, Location.Y, diameter, diameter);
    }
}
