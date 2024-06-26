﻿using HandyWMS_ClassLibrary.Enum.Browser;

namespace HandyWMS_ClassLibrary.Models.Browser
{
    public class Edge : BaseBrowser
    {
        private readonly string _agent;

        public Edge(string agent)
        {
            _agent = agent.ToLower();
            var edge = BrowserType.Edge.ToString().ToLower();

            if (_agent.Contains(edge))
            {
                var first = _agent.IndexOf(edge);
                var version = _agent.Substring(first + edge.Length + 1);
                Version = ToVersion(version);
                Type = BrowserType.Edge;
            }
        }
    }
}
