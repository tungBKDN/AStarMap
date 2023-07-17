using DirectMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectionMap
{
    public class PriorityQueue
    {
        public List<ProcessNode> Queues { get; set; }
        public PriorityQueue()
        {
            Queues = new List<ProcessNode>();
        }
        public PriorityQueue(List<ProcessNode> queues)
        {
            Queues = queues;
        }
        public bool IsEmpty()
        {
            return Queues.Count == 0;
        }
        public void Put(ProcessNode node)
        {
            Queues.Add(node);
            Queues = Queues.OrderBy(p => p.DistanceCost + p.HeuristicLength).ToList();
        }
        public void Put(StreetNode node, double cost = 0)
        {
            ProcessNode processNode = new ProcessNode(node, null, cost);
            processNode.HeuristicLength = BLL.Instance.DistanceCal(node.StreetNodeX, node.StreetNodeY, ProcessNode.TargetLocX, ProcessNode.TargetLocY);
            Queues.Add(processNode);
            Queues = Queues.OrderBy(p => p.DistanceCost).ToList();
        }
        public ProcessNode Get()
        {
            if (IsEmpty()) return null;
            var node = Queues[0];
            Queues.RemoveAt(0);
            return node;
        }

        public override string ToString()
        {
            string res = "";
            foreach (ProcessNode pn in Queues)
            {
                res = res + pn.StreetNodeID + " ";
            }
            return res;
        }

        public int Count()
        {
              return Queues.Count;
        }

        public bool HasExisted(ProcessNode pn)
        {
            foreach (ProcessNode p in Queues)
            {
                if (p.Parent == null) continue;
                if (p.Parent.StreetNodeID == pn.Parent.StreetNodeID && p.StreetNodeID == pn.StreetNodeID)
                    return true;
            }
            return false;          
        }

        public List<ProcessNode> ToList()
        {
            return Queues;
        }

        public ProcessNode[] ToArray()
        {
            return Queues.ToArray();
        }
    }
}
