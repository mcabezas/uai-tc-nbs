using System.Collections.Generic;
using System.Linq;

namespace Http.Router
{
    internal class Node : INode
    {
        internal Node()
        {
            _methodHandlers = new Dictionary<string, IHttpHandler>();
            _leaves = new Dictionary<string, INode>();
        }

        private Node(string resourceName)
        {
            ResourceName = resourceName;
            _methodHandlers = new Dictionary<string, IHttpHandler>();
            _leaves = new Dictionary<string, INode>();
        }

        private readonly Dictionary<string, INode> _leaves;
        private readonly Dictionary<string, IHttpHandler> _methodHandlers;
        private string ResourceName { get; set; }

        public void AddHandler(string[] paths, string method, IHttpHandler handler)
        {
            if (paths.Length > 0)
            {
                PushToLeaves(paths, method, handler);
                return;
            }

            PushAtThisNode(method, handler);
        }

        public IHttpHandler GetHandler(string[] paths, string method)
        {
            if (paths.Length > 0)
            {
                var basePath = paths[0];
                if (!_leaves.ContainsKey(basePath))
                {
                    return null;
                }

                paths = paths.Skip(1).ToArray();

                return _leaves[basePath].GetHandler(paths, method);
            }

            if (_methodHandlers.ContainsKey(method))
            {
                return _methodHandlers[method];
            }

            return null;
        }

        #region Protected Helpers

        private void PushAtThisNode(string method, IHttpHandler handler)
        {
            _methodHandlers.Add(method, handler);
        }

        private void PushToLeaves(string[] paths, string method, IHttpHandler handler)
        {
            var basePath = paths[0];
            paths = paths.Skip(1).ToArray();
            INode node;
            if (_leaves.ContainsKey(basePath))
            {
                node = _leaves[basePath];
            }
            else
            {
                node = new Node(basePath);
                _leaves.Add(basePath, node);
            }

            node.AddHandler(paths, method, handler);
        }

        #endregion
    }
}