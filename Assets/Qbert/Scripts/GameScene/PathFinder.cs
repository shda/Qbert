using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.GameScene
{
    [System.Serializable]
    public class PathFinder
    {
        [System.Serializable]
        public class PathPoint
        {
            public PathPoint parentPathPoint;
            public Cube      currentCube;
            public int G;
            public int H;
            public int F;
        }


        public List<PathPoint> closeList;
        public List<PathPoint> openList;

        public List<Cube> FindPath(Cube start , Cube end)
        {
            closeList = new List<PathPoint>();
            openList = new List<PathPoint>();

            var point = new PathPoint()
            {
                currentCube = start,
                G = 0,
                H = CalcDist(start, end),
                F = CalcDist(start, end),
                parentPathPoint = null
            };

            openList.Add(point);

            int maxIter = 1000;

            int count = 0;
            while (openList.Count > 0 && maxIter > 0)
            {
                PathPoint min = GetMinFInOpen();
                count++;
                if (min.currentCube == end)
                {
                    return CreateArrayPathPoints(min);
                }
                maxIter--;

                FindAllNeighborNodes(RemoveOpenSetToClose(min));
            }

            return null;
        }

        private List<Cube> CreateArrayPathPoints(PathPoint currentPoint)
        {
            List<Cube> path = new List<Cube>();

            path.Add(currentPoint.currentCube);

            while (true)
            {
                if (currentPoint.parentPathPoint == null)
                    break;

                currentPoint = currentPoint.parentPathPoint;

                path.Add(currentPoint.currentCube);
            }

            return path;
        } 

        private PathPoint RemoveOpenSetToClose(PathPoint point)
        {
            closeList.Add(point);
            openList.Remove(point);

            return closeList.Last();
        }

        private PathPoint GetMinFInOpen()
        {
            PathPoint findValue = null;

            foreach (var pathPoint in openList)
            {
                if (findValue == null || findValue.F > pathPoint.F)
                {
                    findValue = pathPoint;
                }
            }

            return findValue;
        }

        private bool IsInCloseList(Cube cube)
        {
            return closeList.Any(x => x.currentCube == cube);
        }

        private PathPoint GetInOpenList(Cube cube)
        {
            return openList.FirstOrDefault(x => x.currentCube == cube);
        }

        private void FindAllNeighborNodes(PathPoint point)
        {
            foreach (var node in point.currentCube.nodes)
            {
                if(IsInCloseList(node))
                    continue;

                int Gscore = 10 + point.G;

                var findPoint = GetInOpenList(node);

                if (findPoint != null)
                {
                    if (Gscore < findPoint.G)
                    {
                        findPoint.parentPathPoint = point;
                        findPoint.G = Gscore;
                    }
                }
                else
                {
                    var pathPoint = new PathPoint()
                    {
                        currentCube = node,
                        G = Gscore,
                        H = CalcDist(node, point.currentCube),
                        F = CalcDist(node, point.currentCube) + Gscore,
                        parentPathPoint = point,
                    };

                    openList.Add(pathPoint);
                }
            }
        }

        public int CalcDist(Cube start, Cube end)
        {
            return 10 * Mathf.Abs(start.currentPosition.position - end.currentPosition.position)
                   + Mathf.Abs(start.currentPosition.line - end.currentPosition.line);
        }
    }
}
