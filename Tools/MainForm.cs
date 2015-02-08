using Reactor.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RPKEditor
{
    public partial class MainForm : Form
    {
        RPackage package;
        bool pending = false;
        FileDragDrop fileDragDrop;
        public MainForm()
        {
            InitializeComponent();
            fileDragDrop = new FileDragDrop();
            fileDragDrop.Dock = DockStyle.Fill;
            this.Controls.Add(fileDragDrop);
            fileDragDrop.Visible = false;
            panel1.Visible = false;
            //menuStrip1.BackColor = Color.FromArgb(45, 45, 45);
            //menuStrip1.ForeColor = Color.White;
            //menuStrip1.Renderer = new MyRenderer();
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            fileDragDrop.Reset();
        }

        void fileDragDrop_DroppedFiles(object sender, EventArgs args)
        {
            treeView1.PathSeparator = "/";
            TreeNode rootNode = treeView1.Nodes.Add("Archive");
            
            foreach (KeyValuePair<string, FileStream> p in fileDragDrop.Files)
            {
                string key = p.Key.Replace(fileDragDrop.RelativePath, "");
                package.AddEntry(key, p.Value);
            }
            PopulateTreeNodes(rootNode, package.GetEntries(), '/');
            panel1.Visible = true;
            fileDragDrop.Visible = false;
            pending = true;
        }
        

        void MainForm_Load(object sender, EventArgs e)
        {
            fileDragDrop.DroppedFiles += fileDragDrop_DroppedFiles;
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            package = new RPackage();
            pending = true;
            this.Text = "RPK Editor : New Archive (unsaved)";
            treeView1.Nodes.Clear();
            
            panel1.Visible = false;
            fileDragDrop.Visible = true;
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            var result = openFileDialog1.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                treeView1.Nodes.Clear();
                package = new RPackage(openFileDialog1.FileName);
                pending = false;
                this.Text = "RPK Editor : " + Path.GetFileName(openFileDialog1.FileName);
                TreeNode root = treeView1.Nodes.Add(Path.GetFileNameWithoutExtension(openFileDialog1.FileName));
                PopulateTreeNodes(root, package.GetEntries(), '/');
                panel1.Visible = true;
                fileDragDrop.Visible = false;
            }
        }

        public static void PopulateTreeNodes(TreeNode rootNode, IEnumerable<string> paths, char pathSeparator)
        {
            TreeNode lastNode = null;
            string subPathAgg;
            foreach (string path in paths)
            {
                var p = path.Replace("\\", "/");
                subPathAgg = string.Empty;
                foreach (string subPath in p.Split(pathSeparator))
                {
                    subPathAgg += subPath + pathSeparator;
                    TreeNode[] nodes = rootNode.Nodes.Find(subPathAgg, true);
                    if (nodes.Length == 0)
                        if (lastNode == null)
                            lastNode = rootNode.Nodes.Add(subPathAgg, subPath);
                        else
                            lastNode = lastNode.Nodes.Add(subPathAgg, subPath);
                    else
                        lastNode = nodes[0];
                }
                lastNode = null;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.AddExtension = false;
            saveFileDialog1.ValidateNames = false;
            saveFileDialog1.SupportMultiDottedExtensions = true;
            saveFileDialog1.DefaultExt = "rpk";
            saveFileDialog1.Filter = "Reactor Package|*.rpk|Zip Archive|*.zip|Custom|*.*";
            var result = saveFileDialog1.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                
                package.Save(saveFileDialog1.FileName);
                pending = false;
            }
            this.Text = "RPK Editor : " + Path.GetFileName(saveFileDialog1.FileName);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(pending)
            {
                var result = MessageBox.Show("There are pending changes to this archive, do you still wish to quit?", "Warning! Pending Changes...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                    Application.Exit();
                }
                else
                {
                    return;
                }
            }
            this.Close();
            Application.Exit();
        }








        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (!e.Item.Selected)
                {

                    e.Item.BackColor = Color.FromArgb(45, 45, 45);
                    e.Item.ForeColor = Color.White;
                    base.OnRenderMenuItemBackground(e);
                }
                else
                {
                    Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                    e.Graphics.FillRectangle(new Pen(Color.FromArgb(85,85,85)).Brush, rc);
                    e.Graphics.DrawRectangle(Pens.Black, 0, 0, rc.Width + 2, rc.Height + 1);
                    e.Item.BackColor = Color.FromArgb(45, 45, 45);
                    e.Item.ForeColor = Color.Yellow;
                }
            }
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                Rectangle rc = new Rectangle();
                rc = e.ConnectedArea;
                e.Graphics.FillRectangle(new Pen(Color.FromArgb(45, 45, 45)).Brush, rc);
            }
            
            
            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                base.OnRenderToolStripBackground(e);
                Rectangle rc = e.AffectedBounds;
                rc.Inflate(2,2);
                e.Graphics.FillRectangle(new Pen(Color.FromArgb(45, 45, 45)).Brush, rc);
                //e.Graphics.DrawRectangle(Pens.Black, 1, 0, rc.Width, rc.Height);
            }
            protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                
                if (!e.Item.Selected)
                {

                    e.Item.BackColor = Color.FromArgb(18, 18, 18);
                    e.Item.ForeColor = Color.White;
                    e.Graphics.FillRectangle(new Pen(Color.FromArgb(45, 45, 45)).Brush, rc);
                    e.Graphics.DrawRectangle(Pens.Black, 0, 0, rc.Width, rc.Height);
                    //base.OnRenderItemBackground(e);
                }
                else
                {
                    
                    e.Item.BackColor = Color.FromArgb(45, 45, 45);
                    e.Item.ForeColor = Color.Yellow;
                    e.Graphics.FillRectangle(new Pen(Color.FromArgb(85, 85, 85)).Brush, rc);
                    e.Graphics.DrawRectangle(Pens.Black, 0, 0, rc.Width, rc.Height);
                    
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                if (!e.Item.Selected)
                {
                    e.Item.ForeColor = Color.Yellow;
                    base.OnRenderItemText(e);
                }
                else
                {
                    e.Item.ForeColor = Color.White;
                    base.OnRenderItemText(e);
                }
            }
        }

        private class MyColors : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return Color.FromArgb(18,18,18); }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.SlateGray; }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.Yellow; }
            }
        }

        
    }
}
