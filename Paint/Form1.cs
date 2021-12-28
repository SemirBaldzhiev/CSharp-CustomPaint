using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Paint
{

    public partial class Form1 : Form
    {
        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point pX;
        Point pY;
        Pen pen = new Pen(Color.Black, 1);
        Pen erase = new Pen(Color.White, 10);

        int index = 0;
        int x, y, sX, sY, cX, cY;

        ColorDialog dlg = new ColorDialog();
        Color newColor;



        public Form1()
        {
            InitializeComponent();
            this.Width = 1200;
            this.Height = 700;
            bm = new Bitmap(this.Width, this.Height);
            g = Graphics.FromImage(bm); 
            g.Clear(Color.White);
            pic.Image = bm;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                if (index == 1)
                {
                    pX = e.Location;
                    g.DrawLine(pen, pX, pY);
                    pY = pX;
                }
                else if(index == 2)
                {

                    pX = e.Location;
                    g.DrawLine(erase, pX, pY);
                    pY = pX;
                }
            }

            pic.Refresh();

            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;

        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            pY = e.Location;

            cX = e.X;
            cY = e.Y;

        }
        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false; 

            sX = x - cX;
            sY = y - cY;

            if (index == 3)
            {
                g.DrawEllipse(pen, cX, cY, sX, sY);
            }
            else if (index == 4)
            {
                g.DrawRectangle(pen, cX, cY, sX, sY);
            }
            else if (index == 5)
            {
                g.DrawLine(pen, cX, cY, x, y);
            }

        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            if (paint)
            {
                if (index == 3)
                {
                    g.DrawEllipse(pen, cX, cY, sX, sY);
                }
                else if (index == 4)
                {
                    g.DrawRectangle(pen, cX, cY, sX, sY);
                }
                else if (index == 5)
                {
                    g.DrawLine(pen, cX, cY, x, y);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pic.Image = bm;
            index = 0;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            dlg.ShowDialog();
            newColor = dlg.Color;
            picColor.BackColor = dlg.Color;
            pen.Color = dlg.Color;

        }

        

        static Point setPoint(PictureBox pb, Point point)
        {
            float pX = 1.0f * pb.Image.Width / pb.Width;
            float pY = 1.0f * pb.Image.Height / pb.Height;

            var newPoint = new Point((int)(point.X * pX), (int)(point.Y * pY));

            return newPoint;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, pic.Width, pic.Height), bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
            }
        }

        private void btnPencil_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void btnEraser_Click(object sender, EventArgs e)
        {
            index = 2;
        }
        private void btnEllipse_Click(object sender, EventArgs e)
        {
            index = 3;
        }
        private void btnRect_Click(object sender, EventArgs e)
        {
            index = 4;
        }
        private void btnLine_Click(object sender, EventArgs e)
        {
            index = 5;
        }

       
    }
}
