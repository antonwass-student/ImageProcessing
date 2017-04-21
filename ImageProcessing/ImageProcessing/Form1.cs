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
using ObjectIdentification;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        private string _loadedImage;
        public ObjectLibrary ObjectLibrary;

        public Form1()
        {
            InitializeComponent();

            IEnumerable<ObjectView.Perspective> perspectives = Enum.GetValues(typeof(ObjectView.Perspective)).Cast<ObjectView.Perspective>();

            comboBox1.DataSource = perspectives;
            comboBox1.Refresh();

            ObjectLibrary = new ObjectLibrary();
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
            

            int id = Convert.ToInt32(textBox1.Text);
            ObjectView.Perspective perspective = (ObjectView.Perspective) comboBox1.SelectedItem;

            ObjectLibrary.Train(id, _loadedImage, perspective);

            richTextBox1.Text = ObjectLibrary.GetFeatures(id, perspective).GetDescriptorString();
            richTextBox1.Invalidate();
            Debug.WriteLine("Done");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            int id = Convert.ToInt32(textBox1.Text);
            List<ImageSearchResult> result = ObjectLibrary.Search(_loadedImage, id);

            Bitmap newBitmap = new Bitmap(pictureBox1.Image);

            Graphics g = Graphics.FromImage(newBitmap);

            foreach (ImageSearchResult match in result)
            {
                g.DrawImage(match.Homography.Bitmap, new Point(0,0));
            }

            pictureBox2.Image = newBitmap;
            pictureBox1.Invalidate();

            Debug.WriteLine("Search complete");
        }
    }
}
