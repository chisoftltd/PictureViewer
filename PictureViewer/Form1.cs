using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public DialogResult ofdResults { get; private set; }
        public int numOfFiles { get; private set; }
        public string[] aryFilePaths { get; private set; }
        public int counter { get; private set; }
        public string bigFileName { get; private set; }
        public Bitmap bmp { get; private set; }
        public int newWidth { get; private set; }
        public int newHeight { get; private set; }
        public Bitmap bmpNew { get; private set; }
        public Graphics gr { get; private set; }

        private void btnLoadImages_Click(object sender, EventArgs e)
        {
            imageList1.Images.Clear();
            listView1.Clear();

            openFD1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFD1.Title = "Open Image Files";
            openFD1.Filter = "JPEGS|*.jpg|GIFS|*.gif|PNG|*.png";

            ofdResults = openFD1.ShowDialog();
            if (ofdResults==DialogResult.Cancel)
            {
                return;
            }

            try
            {
                numOfFiles = openFD1.FileName.Length;
                aryFilePaths = new string[numOfFiles];
                counter = 0;

                foreach (string singleFile in openFD1.FileNames)
                {
                    aryFilePaths[counter] = singleFile;
                    imageList1.Images.Add(Image.FromFile(singleFile));
                    counter++;
                }

                listView1.LargeImageList = imageList1;

                for (int i = 0; i < counter; i++)
                {
                    listView1.Items.Add(aryFilePaths[i], i);
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetOrignalImage();
        }

        private void GetOrignalImage()
        {
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                bigFileName = listView1.SelectedItems[i].Text;
                pictureBox1.Image = Image.FromFile(bigFileName);
                panel1.AutoScrollMinSize = new Size(pictureBox1.Image.Width, pictureBox1.Image.Height);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Right)
            {
                GetZoom(2);
            }
            else if (e.Button==MouseButtons.Left)
            {
                GetOrignalImage();
            }
        }

        private void GetZoom(int zoomSize)
        {
            bmp = new Bitmap(pictureBox1.Image);

            newWidth = pictureBox1.Image.Width / zoomSize;
            newHeight = pictureBox1.Image.Height / zoomSize;

            bmpNew = new Bitmap(newWidth, newHeight);

            gr = Graphics.FromImage(bmpNew);
            gr.DrawImage(bmp, 0, 0, bmpNew.Width, bmpNew.Height);

            pictureBox1.Image = bmpNew;
            panel1.AutoScrollMinSize = new Size(pictureBox1.Image.Width, pictureBox1.Image.Height);
        }
    }
}
