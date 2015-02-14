using OpenTK;
using Reactor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace _2___Init_Form
{
    public partial class Form1 : Form
    {
        REngine engine;
        bool isRunning = true;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;

            this.FormClosing += Form1_FormClosing;

            this.Resize += Form1_Resize;

            
        }

        void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = false;
            //this.BackColor = Color.Transparent;
            engine = REngine.Instance;  // This will actually instantiate the singleton instance and all dependencies!

            engine.InitForm(Handle);  // Init the engine to our form's surface.

            engine.SetViewport(new RViewport(0, 0, this.ClientSize.Width, this.ClientSize.Height));  //Set the initial viewport to the size of our pictureBox.

            this.Show();
            while(isRunning)
            {
                engine.Clear();

                //Draw everything here...

                engine.Present();
                Application.DoEvents();
            }
        }

        void Form1_Resize(object sender, EventArgs e)
        {
            /*
             * We resize the viewport here.  This is called whenever the form is resized because it's the form resize event handler.
             */
            engine.SetViewport(new RViewport(0, 0, this.ClientSize.Width, this.ClientSize.Height));
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false;
            Thread.Sleep(100);
            engine.Dispose();  // clean up all allocated memory and unmanaged opengl memory!
        }
    }
}
