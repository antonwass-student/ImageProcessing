using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private string loadedImage;

        public Form1()
        {
            InitializeComponent();

            IEnumerable<ObjectView.Perspective> perspectives = Enum.GetValues(typeof(ObjectView.Perspective)).Cast<ObjectView.Perspective>();

            comboBox1.DataSource = perspectives;
            comboBox1.Refresh();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    loadedImage = dlg.FileName;
                    Image img = Image.FromFile(dlg.FileName);

                    pictureBox1.Image = img;

                    pictureBox1.Invalidate();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ObjectLibrary lib = new ObjectLibrary();

            int id = Convert.ToInt32(textBox1.Text);
            ObjectView.Perspective perspective = (ObjectView.Perspective) comboBox1.SelectedItem;

            lib.Train(id, loadedImage, perspective);

            richTextBox1.Text = lib.GetFeatures(id, perspective).getDescriptorString();

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
    }
}
