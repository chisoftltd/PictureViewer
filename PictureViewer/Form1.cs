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
                    listView1.LargeImageList = imageList1;
                    counter++;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
