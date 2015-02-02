using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactor.Math;
namespace Reactor
{
    public class RCamera2d : RCamera
    {
        public RCamera2d():base()
        {

            this.OnUpdate += RCamera2d_OnUpdate;
        }

        void RCamera2d_OnUpdate(object sender, EventArgs e)
        {
            RViewport viewport = REngine.Instance._viewport;
            float x_max = viewport.Width - 1.0f;
            float y_max = viewport.Height - 1.0f;
            
            /*this.Projection = new Matrix(
                2 / y_max, 0, 0, -1,
                0, 2 / x_max, 0, -1,
                0, 0, -1, 0,
                0, 0, 0, 1);
             */
            this.Projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height,0, -1, 1);
            this.View = Matrix.Identity;
        }
    }
}
