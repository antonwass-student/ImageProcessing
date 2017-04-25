using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Util;
using ObjectIdentification;


namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        private string _loadedImage;
        private string _imageTop, _imageBot, _imageFront, _imageLeft, _imageRight, _imageBack;
        public ObjectLibrary ObjectLibrary;

        public Form1()
        {
            InitializeComponent();

            IEnumerable<ObjectView.Perspective> perspectives = Enum.GetValues(typeof(ObjectView.Perspective)).Cast<ObjectView.Perspective>();

            ObjectLibrary = new ObjectLibrary();

            comboBox1.DataSource = ObjectLibrary.WorldObjects.Keys.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    _loadedImage = dlg.FileName;
                    Image img = Image.FromFile(dlg.FileName);

                    pictureBox1.Image = img;

                    pictureBox1.Invalidate();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _imageTop = dlg.FileName;
                    Image img = Image.FromFile(dlg.FileName);

                    pictureBox2.Image = img;

                    pictureBox2.Invalidate();
                }
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _imageBot = dlg.FileName;
                    Image img = Image.FromFile(dlg.FileName);

                    pictureBox8.Image = img;

                    pictureBox8.Invalidate();
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _imageLeft = dlg.FileName;
                    Image img = Image.FromFile(dlg.FileName);

                    pictureBox3.Image = img;

                    pictureBox3.Invalidate();
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _imageRight = dlg.FileName;
                    Image img = Image.FromFile(dlg.FileName);

                    pictureBox4.Image = img;

                    pictureBox4.Invalidate();
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _imageFront = dlg.FileName;
                    Image img = Image.FromFile(dlg.FileName);

                    pictureBox5.Image = img;

                    pictureBox5.Invalidate();
                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _imageBack = dlg.FileName;
                    Image img = Image.FromFile(dlg.FileName);

                    pictureBox6.Image = img;

                    pictureBox6.Invalidate();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //top
            int id = Convert.ToInt32(comboBox1.SelectedValue);
            ObjectLibrary.Train(id, _imageTop, ObjectView.Perspective.Up);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //bot
            int id = Convert.ToInt32(comboBox1.SelectedValue);
            ObjectLibrary.Train(id, _imageBot, ObjectView.Perspective.Down);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //left
            int id = Convert.ToInt32(comboBox1.SelectedValue);
            ObjectLibrary.Train(id, _imageLeft, ObjectView.Perspective.Left);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //right
            int id = Convert.ToInt32(comboBox1.SelectedValue);
            ObjectLibrary.Train(id, _imageRight, ObjectView.Perspective.Right);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //front
            int id = Convert.ToInt32(comboBox1.SelectedValue);
            ObjectLibrary.Train(id, _imageFront, ObjectView.Perspective.Front);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //bot
            int id = Convert.ToInt32(comboBox1.SelectedValue);
            ObjectLibrary.Train(id, _imageBack, ObjectView.Perspective.Back);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ObjectLibrary.CreateEmptyObject();
            comboBox1.DataSource = ObjectLibrary.WorldObjects.Keys.ToList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(comboBox1.SelectedValue);
            List<ImageSearchResult> result = ObjectLibrary.Search(_loadedImage, id);

            Bitmap newBitmap = new Bitmap(pictureBox1.Image);

            Graphics g = Graphics.FromImage(newBitmap);

            foreach (ImageSearchResult match in result)
            {
                //g.DrawImage(match.Homography.Bitmap, new Point(0,0));
                Point[] toDraw = perspectiveTransform(match);

                g.DrawPolygon(Pens.Red, toDraw);
            }

            pictureBox1.Image = newBitmap;
            pictureBox1.Invalidate();

            Debug.WriteLine("Search complete");
        }

        /// <summary>
        /// TODO: Move to other project.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private Point[] perspectiveTransform(ImageSearchResult match)
        {
            
            Rectangle rect = new Rectangle(Point.Empty, match.MatchingView.Features.Image.Size);
            PointF[] pts = new PointF[]
            {
                new PointF(rect.Left, rect.Bottom),
                new PointF(rect.Right, rect.Bottom),
                new PointF(rect.Right, rect.Top),
                new PointF(rect.Left, rect.Top)    
            };

            pts = CvInvoke.PerspectiveTransform(pts, match.Homography);

            return Array.ConvertAll<PointF, Point>(pts, Point.Round);
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
