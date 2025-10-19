using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicRed
{
    public partial class Form1 : Form
    {
        private List<Shape> shapes = new List<Shape>();
        private Shape selectedShape;
        private Color currentColor = Color.Red;
        private string currentTool = "Rectangle";

        private Point startPoint;
        private bool isDrawing = false;

        private bool isDragging = false;
        private Point lastMousePosition;
        public Form1()
        {
            InitializeComponent();
            SetupInterface();
        }
        private void SetupInterface()
        {
            this.Text = "Графический редактор";
            this.Size = new Size(800, 600);

            ToolStrip toolStrip = new ToolStrip();
            toolStrip.Dock = DockStyle.Top;

            ToolStripButton rectButton = new ToolStripButton("Rectangle");
            ToolStripButton circleButton = new ToolStripButton("Circle");
            ToolStripButton triangleButton = new ToolStripButton("Triangle");
            ToolStripButton deleteButton = new ToolStripButton("Delete");
            ToolStripButton colorButton = new ToolStripButton("Color");

            toolStrip.Items.AddRange(new ToolStripItem[] {
                rectButton, circleButton, triangleButton,
                new ToolStripSeparator(),
                colorButton, deleteButton
            });

            rectButton.Click += RectButton_Click;
            circleButton.Click += CircleButton_Click;
            triangleButton.Click += TriangleButton_Click;
            deleteButton.Click += DeleteButton_Click;
            colorButton.Click += ColorButton_Click;

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.BackColor = Color.White;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;

            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;

            this.Controls.Add(pictureBox); 
            this.Controls.Add(toolStrip);
        }
        private Shape CreateShape(string shapeType, Point start, Point end)
        {
            int x = Math.Min(start.X, end.X);
            int y = Math.Min(start.Y, end.Y);
            int width = Math.Abs(end.X - start.X);
            int height = Math.Abs(end.Y - start.Y);

            switch (shapeType)
            {
                case "Rectangle":
                    return new RectangleShape { Location = new Point(x, y), Width = width, Height = height };
                case "Circle":
                    return new CircleShape { Location = new Point(x, y), Radius = Math.Min(width, height) / 2 };
                case "Triangle":
                    return new TriangleShape { Location = new Point(x, y), Width = width, Height = height };
                default:
                    return null;
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape shape in shapes)
            {
                shape.Draw(e.Graphics);
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (Shape shape in shapes)
                {
                    shape.IsSelected = false;
                }

                selectedShape = null;
                for (int i = shapes.Count - 1; i >= 0; i--) 
                {
                    if (shapes[i].ContainsPoint(e.Location))
                    {
                        selectedShape = shapes[i];
                        selectedShape.IsSelected = true;
                        break;
                    }
                }

                if (selectedShape == null)
                {

                    isDrawing = true;
                    startPoint = e.Location;
                }
                else
                {
                    isDragging = true;
                    lastMousePosition = e.Location;
                }
                RefreshPicture();
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && selectedShape != null)
            {
                int deltaX = e.X - lastMousePosition.X;
                int deltaY = e.Y - lastMousePosition.Y;

                selectedShape.Move(deltaX, deltaY);
                lastMousePosition = e.Location;

                RefreshPicture();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                Shape newShape = CreateShape(currentTool, startPoint, e.Location);
                if (newShape != null)
                {
                    newShape.Color = currentColor;
                    shapes.Add(newShape);
                }

                isDrawing = false;
                RefreshPicture();
            }

            isDragging = false;
        }
        private void CircleButton_Click(object sender, EventArgs e)
        {
            currentTool = "Circle";
        }

        private void RectButton_Click(object sender, EventArgs e)
        {
            currentTool = "Rectangle";
        }

        private void TriangleButton_Click(object sender, EventArgs e)
        {
            currentTool = "Triangle";
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                shapes.Remove(selectedShape);
                selectedShape = null;
                RefreshPicture();
            }
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = currentColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
            }
        }

        private void RefreshPicture()
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox)
                {
                    control.Invalidate();
                    break;
                }
            }
        }
    }
}
