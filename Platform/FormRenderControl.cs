using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reactor.Platform
{
    public class FormRenderControl : RenderControl
    {
        public Form Form { get; internal set; }
        public override void Init()
        {
            throw new NotImplementedException();
        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }

        public override void MakeCurrent()
        {
            throw new NotImplementedException();
        }

        public override void SwapBuffers()
        {
            throw new NotImplementedException();
        }
    }
}
