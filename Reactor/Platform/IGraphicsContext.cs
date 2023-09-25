using Reactor.Types;

namespace Reactor.Platform
{
    public interface IGraphicsContext
    {
        RDisplayModes SupportedModes();
        RDisplayMode CurrentMode();
        void SetMode(RDisplayMode mode);
        void MakeCurrent();
        void MakeNoneCurrent();
        void SwapBuffers();
    }
}