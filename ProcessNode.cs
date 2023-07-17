using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectionMap
{
    public class ProcessNode : StreetNode
    {
        public static double TargetLocX { get; set; }
        public static double TargetLocY { get; set; }
        public double DistanceCost { get; set; }
        public double HeuristicLength { get; set; }
        public ProcessNode Parent { get; set; }

        public ProcessNode() { }
        public ProcessNode(StreetNode sn, ProcessNode parent = null, double cost = 0, double costHeur = 0)
        {
            this.StreetNodeID = sn.StreetNodeID;
            this.StreetNodeX = sn.StreetNodeX;
            this.StreetNodeY = sn.StreetNodeY;
            this.DistanceCost = cost;
            this.Parent = parent;
            this.HeuristicLength = costHeur;
            this.ConnectedNodes = sn.ConnectedNodes;
        }
        
        public static bool IsEqual(ProcessNode s, ProcessNode g)
        {
            if (s.StreetNodeID == g.StreetNodeID)
                return true;
            return false;
        }

        public StreetNode Convert()
        {
            return new StreetNode()
            {
                StreetNodeID = this.StreetNodeID,
                StreetNodeX = this.StreetNodeX,
                StreetNodeY = this.StreetNodeY,
                ConnectedNodes = this.ConnectedNodes
            };
        }
    }
}
