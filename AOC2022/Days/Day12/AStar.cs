using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2022.Days.Day12
{
    public static class AStar<T> where T : PriorityQueueNode
    {
        public static bool PerformSearch<TIndex>(
            List<T> initialNodes,
            Func<T, TIndex> getNodeIndex,
            Func<T, bool> isNotEnd,
            Func<T, List<T>> getNeighbors,
            Func<T, T, ulong> getNeighborDistance,
            Func<T, ulong> getEstimatedDistance,
            out ulong distance)
                where TIndex : notnull
        {
            distance = ulong.MaxValue;
            var foundResult = false;
            var States = new Dictionary<TIndex, T>();
            var queue = new PriorityQueue<T>();
            foreach (var node in initialNodes)
            {
                queue.InsertNode(node);
                States.Add(getNodeIndex(node), node);
            }
            List<T> neighbors;
            TIndex neighborIndex;
            ulong tentativeGScore;
            var currentNode = queue.PopMinPriority();

            while (currentNode != null && isNotEnd(currentNode))
            {
                neighbors = getNeighbors(currentNode);
                foreach (var neighbor in neighbors)
                {
                    tentativeGScore = currentNode.BestDistance + getNeighborDistance(currentNode, neighbor);
                    neighborIndex = getNodeIndex(neighbor);
                    if (States.ContainsKey(neighborIndex) && States[neighborIndex].BestDistance <= tentativeGScore)
                    {
                        continue;
                    }

                    if (!States.ContainsKey(neighborIndex))
                    {
                        if (queue.ShouldExpand(out var capacity))
                        {
                            queue.UpdateCapacity(2 * capacity);
                        }
                        neighbor.BestDistance = tentativeGScore;
                        neighbor.Priority = tentativeGScore + getEstimatedDistance(neighbor);
                        States.Add(neighborIndex, neighbor);
                        queue.InsertNode(States[neighborIndex]);
                    }
                    else
                    {
                        States[neighborIndex].BestDistance = tentativeGScore;
                        queue.UpdatePriority(States[neighborIndex], tentativeGScore + getEstimatedDistance(neighbor));
                    }
                }

                currentNode = queue.PopMinPriority();
            }

            if (currentNode != null)
            {
                distance = currentNode.BestDistance;
                foundResult = true;
            }

            return foundResult;
        }
    }

    public class PriorityQueue<T> where T : PriorityQueueNode
    {
        private long _capacity;
        private long _lastPos = -1;
        private T[] _nodes;

        public PriorityQueue() : this(1024) { }

        public PriorityQueue(long capacity)
        {
            if (capacity < 0)
            {
                throw new Exception("Cannot set capacity to value less than 0");
            }

            _capacity = capacity;
            _nodes = new T[capacity];
        }

        public T PopMinPriority()
        {
            if (_lastPos < 0)
            {
                return default;
            }

            var result = _nodes[0];
            _nodes[0] = _nodes[_lastPos];
            _nodes[0].Index = 0;
            _nodes[_lastPos--] = default;
            CascadeDown(0);
            return result;
        }

        public void InsertNode(T node)
        {
            if (_lastPos >= _capacity - 1)
            {
                throw new Exception("Maximum capacity reached");
            }

            node.Index = ++_lastPos;
            _nodes[_lastPos] = node;
            CascadeUp(_lastPos);
        }

        public void UpdatePriority(T node, ulong priority)
        {
            if (node == default || node.Index > _lastPos)
            {
                throw new Exception("Node not found");
            }

            var cascadeUp = _nodes[node.Index].Priority > priority;
            _nodes[node.Index].Priority = priority;
            if (cascadeUp)
            {
                CascadeUp(node.Index);
            }
            else
            {
                CascadeDown(node.Index);
            }
        }

        public void UpdateCapacity(long capacity)
        {
            if (capacity <= _lastPos)
            {
                throw new Exception("Cannot reduce capacity below current size");
            }

            var nodes = new T[capacity];
            Array.Copy(_nodes, nodes, _lastPos + 1);
            _nodes = nodes;
            _capacity = capacity;
        }

        public T Get(long index)
        {
            if (index > _lastPos)
            {
                throw new Exception("Index out of range");
            }

            return _nodes[index];
        }

        public bool ShouldExpand(out long currentSize)
        {
            currentSize = _capacity;
            return _lastPos >= _capacity - 1;
        }

        private void CascadeUp(long index)
        {
            var parent = _nodes[(index - 1) / 2];
            if (parent.Priority <= _nodes[index].Priority)
            {
                return;
            }

            _nodes[(index - 1) / 2] = _nodes[index];
            _nodes[(index - 1) / 2].Index = (index - 1) / 2;
            parent.Index = index;
            _nodes[index] = parent;
            CascadeUp((index - 1) / 2);
        }

        private void CascadeDown(long index)
        {
            if (2 * index + 1 > _lastPos)
            {
                return;
            }

            var childL = _nodes[index * 2 + 1];
            var childR = _nodes[index * 2 + 2];
            var current = _nodes[index];
            if (current.Priority <= childL.Priority && (childR == default || current.Priority <= childR.Priority))
            {
                return;
            }

            if (childR == default || childL.Priority <= childR.Priority)
            {
                current.Index = index * 2 + 1;
                childL.Index = index;
                _nodes[index * 2 + 1] = current;
                _nodes[index] = childL;
                CascadeDown(index * 2 + 1);
            }
            else
            {
                current.Index = index * 2 + 2;
                childR.Index = index;
                _nodes[index * 2 + 2] = current;
                _nodes[index] = childR;
                CascadeDown(index * 2 + 2);
            }
        }
    }

    public abstract class PriorityQueueNode
    {
        public ulong Priority { get; set; }
        public ulong BestDistance { get; set; }
        public long Index { get; set; }
    }
}
