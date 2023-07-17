using DirectionMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;

namespace DirectMap
{
    public class BLL
    {
        //singleton
        private static BLL instance;
        public static BLL Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLL();
                return instance;
            }
        }

        public List<StreetNode> GetStreetNodes()
        {
            var db = new MapDBEntities();
            return db.StreetNodes.ToList();
        }

        public void AddStreetNode(StreetNode node)
        {
            var db = new MapDBEntities();
            db.StreetNodes.Add(node);
            db.SaveChanges();
        }

        public int GetStreetNodesCount()
        {
            var db = new MapDBEntities();
            try
            {
                return db.StreetNodes.Max(p => p.StreetNodeID) + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public void DeleteAll()
        {
            var db = new MapDBEntities();
            db.StreetNodes.RemoveRange(db.StreetNodes);
            db.SaveChanges();
        }

        public void NewConnection(StreetNode parent, StreetNode child)
        {
            var db = new MapDBEntities();
            var newConnect = new ConnectedNode()
            {
                RecordID = GenerateConnectionID(),
                CurrentNode = parent.StreetNodeID,
                ConnectedTo = child.StreetNodeID
            };
            db.ConnectedNodes.Add(newConnect);
            db.SaveChanges();
        }

        public int GenerateConnectionID()
        {
            try
            {
                var db = new MapDBEntities();
                return db.ConnectedNodes.Max(p => p.RecordID) + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public List<ConnectedNode> GetAllConnection()
        {
            var db = new MapDBEntities();
            return db.ConnectedNodes.Select(p => p).ToList();
        }

        public StreetNode GetClosestNode(double x, double y)
        {
            double maxError = 0.0005;
            List<StreetNode> nodes = GetStreetNodes();
            foreach (var node in nodes)
            {
                if (Math.Abs(node.StreetNodeX - x) < maxError && Math.Abs(node.StreetNodeY - y) < maxError)
                    return node;
            }
            return null;
        }

        public double DistanceCal(StreetNode node1, StreetNode node2)
        {
            return Math.Sqrt(Math.Pow(node1.StreetNodeX - node2.StreetNodeX, 2) + Math.Pow(node1.StreetNodeY - node2.StreetNodeY, 2));
        }

        public double DistanceCal(double xStart, double yStart, double xEnd, double yEnd)
        {
            return Math.Sqrt(Math.Pow(xStart - xEnd, 2) + Math.Pow(yStart - yEnd, 2));
        }

        public List<StreetNode> ConvertToStreetNode(List<ProcessNode> nodes)
        {
            var result = new List<StreetNode>();
            foreach (var node in nodes)
            {
                result.Add(node.Convert());
            }
            return result;
        }

        public List<ProcessNode> GetRoute(ProcessNode O, List<ProcessNode> tempList)
        {
            tempList.Add(O);
            if (O.Parent == null)
                return tempList;
            else
                return GetRoute(O.Parent, tempList);
        }

        public StreetNode GetStreetNodeByID(int ID)
        {
            var db = new MapDBEntities();
            return db.StreetNodes.Where(p => p.StreetNodeID == ID).FirstOrDefault();
        }
        
        public List<StreetNode> Direct(int startID, int endID)
        {
            TimeSpan start = DateTime.Now.TimeOfDay;
            int traversingCount = 0;
            var db = new MapDBEntities();
            ProcessNode startNode = new ProcessNode(db.StreetNodes.Where(p => p.StreetNodeID == startID).FirstOrDefault());
            ProcessNode endNode   = new ProcessNode(db.StreetNodes.Where(p => p.StreetNodeID == endID).FirstOrDefault());
            List<ProcessNode> resultList = new List<ProcessNode>();

            //using A*
            ProcessNode.TargetLocX = endNode.StreetNodeX;
            ProcessNode.TargetLocY = endNode.StreetNodeY;
            PriorityQueue openQueue = new PriorityQueue();
            PriorityQueue closeQueue = new PriorityQueue();
            openQueue.Put(startNode);

            while (true)
            {
                if (openQueue.IsEmpty())
                {
                    MessageBox.Show("No route found");
                    return null;
                }
                ProcessNode O = openQueue.Get();
                closeQueue.Put(O);

                if (ProcessNode.IsEqual(O, endNode))
                {
                    MessageBox.Show("Route found: Traversing cost: " + traversingCount);
                    TimeSpan endTime = DateTime.Now.TimeOfDay;
                    MessageBox.Show("Time cost: " + (endTime - start).TotalSeconds.ToString() + "s");
                    return ConvertToStreetNode(GetRoute(O, new List<ProcessNode>()));
                }

                foreach (ConnectedNode cn in O.ConnectedNodes)
                {
                    StreetNode streetNode = GetStreetNodeByID(cn.CurrentNode);
                    double routeLength = O.DistanceCost + DistanceCal(O, streetNode);
                    double heuristicLength = DistanceCal(streetNode, endNode);
                    ProcessNode tempNode = new ProcessNode(streetNode, O, routeLength, heuristicLength);
                    if (!openQueue.HasExisted(tempNode) && !closeQueue.HasExisted(tempNode))
                        openQueue.Put(tempNode);
                    traversingCount++;
                    Console.WriteLine(openQueue.Count().ToString());
                }

                if (traversingCount > 150000)
                {
                    List<ProcessNode> temp = openQueue.ToList();
                    temp.AddRange(closeQueue.ToList());
                    return ConvertToStreetNode(temp);
                }

            }
        }

        public string GetConnected(int ID)
        {
            var db = new MapDBEntities();
            var result = db.ConnectedNodes.Where(p => p.CurrentNode == ID).ToList();
            string resultString = "CURR_ID | CONN_TO | RECD_ID\n";
            foreach (var item in result)
            {
                resultString += item.CurrentNode + " | " + item.ConnectedTo + " | " + item.RecordID + "\n";
            }
            return resultString;
        }

        public double CostInKilometer(List<StreetNode> res)
        {
            double cost = 0;
            for (int i = 0; i < res.Count - 1; i++)
            {
                cost += DistanceCal(res[i], res[i + 1]);
            }
            return cost * 111.12;
        }
    }
}
