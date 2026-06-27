using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class PathExist : IPathExist
    {
        private readonly IBFS _bfs;

        public PathExist(IBFS bfs)
        {
            _bfs = bfs;
        }
        public PathExistResult Execute(SocialGraph graph, int userA, int userB)
        {
            PathExistResult result = new();
            BFSResult bfsResult = _bfs.Execute(graph, userA);
            bool pathFind = false;

            foreach (var visitedNode in bfsResult.VisitedNodes)
            {
                if (visitedNode == userB)
                { 
                    pathFind = true;
                    break;
                }
            }

            result.isPathExist = pathFind;

            return result;
        }
    }
}
