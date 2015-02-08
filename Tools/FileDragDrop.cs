using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RPKEditor
{
    public delegate void DroppedFilesEventHandler(object sender, EventArgs args);

    [Designer("DragDropPanel")]
    [DesignerCategory("Panels")]
    public partial class FileDragDrop : UserControl
    {
        public string RelativePath { get; set; }
        public Dictionary<string, FileStream> Files { get; set; }

        public event DroppedFilesEventHandler DroppedFiles;
        public FileDragDrop()
        {
            InitializeComponent();
            this.Load += DragDrop_Load;
        }

        void DragDrop_Load(object sender, EventArgs e)
        {
            Files = new Dictionary<string, FileStream>();
            this.AllowDrop = true;
            this.DragEnter += DragDrop_DragEnter;
            this.DragDrop += DragDrop_DragDrop;
        }

        void DragDrop_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        void DragDrop_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if(paths.Length > 1)
            {
                MessageBox.Show("Error, only single folders are supported.  We'll traverse the tree and add all files under it.","Error", MessageBoxButtons.OK);
                return;
            }
            string path = paths[0];
                RelativePath = Path.GetFullPath(path) + Path.DirectorySeparatorChar;

                FileAttributes attr = File.GetAttributes(path);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    AddFiles(path);

            if(DroppedFiles != null)
                DroppedFiles.Invoke(this, new EventArgs());
            
        }
        private void AddFiles(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    AddFiles(d);
                }
                foreach (string f in Directory.GetFiles(sDir))
                {
                    Files.Add(f, File.OpenRead(Path.GetFullPath(f)));
                }
                
                
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        public void Reset()
        {
            if(Files != null)
            {
                foreach (FileStream stream in Files.Values)
                    stream.Dispose();
                Files.Clear();
            }            
        }
    }
}
