using Shaper;
using Shaper.Shapes;

namespace ShaperClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<Shape> Shapes;

        private void button1_Click(object sender, EventArgs e)
        {
            var count = decimal.ToInt32(numericUpDown1.Value);
            var find = decimal.ToInt32(numericUpDown2.Value);
            var width = decimal.ToInt32(numericUpDown3.Value);
            var height = decimal.ToInt32(numericUpDown4.Value);
            var minSize = decimal.ToInt32(numericUpDown5.Value);
            var maxSize = decimal.ToInt32(numericUpDown6.Value);

            var generator = new Generator();
            Shapes = generator.GetShapes(count, width, height, minSize, maxSize).ToList();

            pictureBox1.Refresh();
            var image = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(image);
            using var pen = new Pen(Color.FromKnownColor(KnownColor.Black), 2);
            using var brush = new SolidBrush(Color.FromKnownColor(KnownColor.White));

            DrawShapes(graphics, pen, brush, Shapes);

            pictureBox1.Image = image;

        }

        private void DrawShapes(Graphics graphics, Pen pen, Brush brush, IEnumerable<Shape> shapes)
        {
            foreach (var shape in shapes)
            {
                if (shape is Circle c) DrawCircle(graphics, pen, brush, c);

                if (shape is Shaper.Shapes.Rectangle r) DrawRectangle(graphics, pen, brush, r);

                if (shape is Line l) DrawLine(graphics, pen, brush, l);

                if (shape is Triangle t) DrawTriangle(graphics, pen, brush, t);
            }
        }

        private void DrawCircle(Graphics graphics, Pen pen, Brush brush, Circle c)
        {
            graphics.FillEllipse(brush, new System.Drawing.Rectangle((int)(c.Center.X - c.Radius), (int)(c.Center.Y - c.Radius), (int)(c.Radius * 2), (int)(c.Radius * 2)));
            graphics.DrawEllipse(pen, new System.Drawing.Rectangle((int)(c.Center.X - c.Radius), (int)(c.Center.Y - c.Radius), (int)(c.Radius * 2), (int)(c.Radius * 2)));
        }

        private void DrawLine(Graphics graphics, Pen pen, Brush brush, Line l)
        {
            graphics.DrawLine(pen, new Point((int)l.A.X, (int)l.A.Y), new Point((int)l.B.X, (int)l.B.Y));
        }

        private void DrawRectangle(Graphics graphics, Pen pen, Brush brush, Shaper.Shapes.Rectangle r)
        {
            graphics.FillRectangle(brush, new System.Drawing.Rectangle((int)r.TopLeft.X, (int)r.TopLeft.Y, (int)r.Width, (int)r.Height));
            graphics.DrawRectangle(pen, new System.Drawing.Rectangle((int)r.TopLeft.X, (int)r.TopLeft.Y, (int)r.Width, (int)r.Height));
        }

        private void DrawTriangle(Graphics graphics, Pen pen, Brush brush, Triangle t)
        {
            graphics.FillPolygon(brush,
                       new Point[] {
                            new Point((int)t.A.X, (int)t.A.Y),
                            new Point((int)t.B.X, (int)t.B.Y),
                            new Point((int)t.C.X, (int)t.C.Y)
                       });

            graphics.DrawPolygon(pen,
                        new Point[] {
                            new Point((int)t.A.X, (int)t.A.Y),
                            new Point((int)t.B.X, (int)t.B.Y),
                            new Point((int)t.C.X, (int)t.C.Y)
                        });

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Shapes == null || pictureBox1.Image == null) return;

            var shaper = new ShapeSeeker();
            var result = shaper.FindAllForegroundShapes(decimal.ToInt32(numericUpDown2.Value), Shapes);

            var shapesToDraw = result.Select(i => Shapes[i]);

            var image = pictureBox1.Image.Clone() as Image;
            using var graphics = Graphics.FromImage(image!);
            using var pen = new Pen(Color.FromKnownColor(KnownColor.DarkGreen), 2);
            using var brush = new SolidBrush(Color.FromKnownColor(KnownColor.Green));

            DrawShapes(graphics, pen, brush, shapesToDraw);

            pictureBox2.Image = image;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;

            saveFileDialog1.Filter = "JPEG Image|*.jpeg|PNG Image|*.png|BMP Image|*.bmp";
            saveFileDialog1.DefaultExt = "png";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null) return;

            saveFileDialog1.Filter = "JPEG Image|*.jpeg|PNG Image|*.png|BMP Image|*.bmp";
            saveFileDialog1.DefaultExt = "png";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image.Save(saveFileDialog1.FileName);
            }
        }
    }
}