using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RFont_Generator
{
    public partial class Viewer : Form
    {
        string filename = String.Empty;
        Font font;
        public Viewer()
        {
            InitializeComponent();
            label4.Text = "";
            this.Load += Viewer_Load;
        }

        void Viewer_Load(object sender, EventArgs e)
        {
            for(int i =6; i<=40; i++)
            {
                sizeDropDown.Items.Add(i);
            }
            sizeDropDown.SelectedIndex = 0;
            dpiDropDown.Items.Add(72);
            dpiDropDown.Items.Add(96);
            dpiDropDown.Items.Add(220);
            sizeDropDown.SelectedIndexChanged += (o, ex) =>
            {
                label4.Text = "";
            };
            dpiDropDown.SelectedIndexChanged += (o, ex) =>
                {
                    label4.Text = "";
                };
        }

        private void fileLoadButton_Click(object sender, EventArgs e)
        {
            var result = fontFileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                fileTextBox.Text = Path.GetFileName(fontFileDialog.FileName);
                filename = fontFileDialog.FileName;
                GoButton.Enabled = true;
                saveButton.Enabled = false;
            }
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            FontGenerator generator = new FontGenerator();
            font = generator.Build(filename, (int)sizeDropDown.SelectedItem, (int)dpiDropDown.SelectedItem);
            pictureBox1.Image = font.Bitmap;
            saveButton.Enabled = true;
            label4.Text = String.Format("Optimal Texture Size: {0}x{1}", font.Bitmap.Width, font.Bitmap.Height);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var result = saveFileDialog.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                if(File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }
                if(File.Exists(saveFileDialog.FileName+".bmp"))
                {
                    File.Delete(saveFileDialog.FileName + ".bmp");
                }
                BinaryWriter writer = new BinaryWriter(File.Open(saveFileDialog.FileName, FileMode.CreateNew));
                font.Save(ref writer);
                font.Bitmap.Save(saveFileDialog.FileName + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);

                MessageBox.Show("Saved Successfully!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
