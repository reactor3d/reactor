using Reactor.Types;

namespace Reactor
{
    public class RScene : RSingleton<RScene>
    {
        private RSceneNode _root;
        private RScene()
        {
            _root = new RSceneNode();
        }
    }
}