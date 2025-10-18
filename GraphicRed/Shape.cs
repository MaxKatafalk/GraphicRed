using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

public abstract class Shape
{
    public Point Location { get; set; }
    public Color Color { get; set; }
    public bool IsSelected { get; set; }
    public abstract Rectangle GetBounds();

    public abstract void Draw(Graphics graphics);
    public abstract bool ContainsPoint(Point point);

    public virtual void DrawSelection(Graphics graphics)
    {
        if (IsSelected)
        {
            using (Pen selectionPen = new Pen(Color.Black, 1))
            {
                selectionPen.DashPattern = new float[] { 2, 2 };
                Rectangle bounds = GetBounds();
                graphics.DrawRectangle(selectionPen, bounds);
            }
        }

    }
    
    public virtual void Move(int deltaX, int deltaY)
    {
        Location = new Point(deltaX + Location.X, deltaY + Location.Y);
    }
}
