namespace Reactor.Types
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class RSceneNode
    {
        public List<RSceneNode> Children { get; internal set; }

        public RSceneNode Parent { get; internal set; }

        public string Name { get; set; }

        internal RSceneNode() { }


        public RSceneNode(RSceneNode parent)
        {
            Parent = parent;
        }

    }
}