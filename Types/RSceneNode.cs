namespace Reactor.Types
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class RSceneNode
    {
        private List<RSceneNode> _children = new List<RSceneNode>();

        public List<RSceneNode> Children { get { return _children; } }

        private RSceneNode _parent;

        public RSceneNode Parent { get { return _parent; } }

        internal RSceneNode() { }

        public RSceneNode(RSceneNode parent)
        {
            _parent = parent;
        }

    }
}