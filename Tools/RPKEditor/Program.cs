using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RPKEditor
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            MainForm form = new MainForm();
            Application.Run(form);
        }
    }
}
