using DirectionMap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectMap
{
    public partial class Form1 : Form
    {
        public enum Task
        {
            None = 0,
            Add = 1,
            Connect = 2,
            Direct = 3
        }

        private double North = 16.608;
        private double South = 16.518;
        private double East = 107.589;
        private double West = 107.458;

        private Task task;
        private StreetNode prevLoc = null;
        int connectCount = 0;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            StreetMapRender();
            taskName.Text = "Adding Node";
            task = Task.Add;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            Clear();
            StreetMapRender();
            StreetNodeRender();
            task = Task.Connect;
            taskName.Text = "Connecting Node";
        }

        private void directButton_Click(object sender, EventArgs e)
        {
            task = Task.Direct;
            taskName.Text = "Directing";
            connectCount = 0;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            task = Task.None;
            taskName.Text = "None";
            prevLoc = null;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            double locX = Math.Round(West + (East - West) * e.X / pictureBox1.Width, 5);
            double locY = Math.Round(North - (North - South) * e.Y / pictureBox1.Height, 5);
            this.locX.Text = locX.ToString() + " E";
            this.locY.Text = locY.ToString() + " N";
            this.locX.Text = e.X.ToString();
            this.locY.Text = e.Y.ToString();
        }

        private void Clear()
        {
            //clear all
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.Transparent);
            pictureBox1.Refresh();
        }

        private void DrawNode(double locX, double locY)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.Wheat, 3);
            g.DrawEllipse(pen, (float)((locX - West) / (East - West) * pictureBox1.Width - 5), (float)((North - locY) / (North - South) * pictureBox1.Height - 5), 5, 5);
        }

        public void DrawExcitedNode(double locX, double locY)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.SkyBlue, 3);
            g.DrawEllipse(pen, (float)((locX - West) / (East - West) * pictureBox1.Width - 5), (float)((North - locY) / (North - South) * pictureBox1.Height - 5), 5, 5);
        }

        private void DrawLine(double xStart, double yStart, double xEnd, double yEnd)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.Wheat, 3);
            g.DrawLine(pen, (float)xStart, (float)yStart, (float)xEnd, (float)yEnd);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (task == Task.Add)
            {
                double locX = Math.Round(West + (East - West) * e.X / pictureBox1.Width, 5);
                double locY = Math.Round(North - (North - South) * e.Y / pictureBox1.Height, 5);
                StreetNode newNode = new StreetNode
                {
                    StreetNodeID = BLL.Instance.GetStreetNodesCount(),
                    StreetNodeX = locX,
                    StreetNodeY = locY
                };
                BLL.Instance.AddStreetNode(newNode);
                if (prevLoc != null)
                {
                    BLL.Instance.NewConnection(prevLoc, newNode);
                    BLL.Instance.NewConnection(newNode, prevLoc);
                }
                prevLoc = newNode;
                DrawNode(locX, locY);
            }
            
            if (task == Task.Connect)
            {
                if (connectCount == 0)
                {
                    prevLoc = Connect(e.X, e.Y);
                    if (prevLoc == null) return;
                    connectCount = 1;
                    taskName.Text = "Choose the end node";
                    return;
                } 
                
                if (connectCount == 1)
                {
                    StreetNode endNode = Connect(e.X, e.Y);
                    connectCount = 0;
                    if (endNode == null)
                    {
                        prevLoc = null;
                        Clear();
                        StreetMapRender();
                        StreetNodeRender();
                        return;
                    };
                    if (endNode.StreetNodeID == prevLoc.StreetNodeID)
                    {
                        MessageBox.Show("Cannot connect to itself");
                        prevLoc = null;
                        Clear();
                        StreetMapRender();
                        StreetNodeRender();
                        return;
                    }
                    BLL.Instance.NewConnection(prevLoc, endNode);
                    BLL.Instance.NewConnection(endNode, prevLoc);
                    Clear();
                    StreetMapRender();
                    StreetNodeRender();
                    prevLoc = null;
                    return;
                }
            }

            if (task == Task.None)
            {
                prevLoc = Connect(e.X, e.Y);
                if (prevLoc == null) return;
                taskName.Text = "Node ID: " + prevLoc.StreetNodeID;
                MessageBox.Show(BLL.Instance.GetConnected(prevLoc.StreetNodeID));
                prevLoc = null;
                return;
            }

            if (task == Task.Direct)
            {
                if (connectCount == 0)
                {
                    prevLoc = Connect(e.X, e.Y);
                    if (prevLoc == null) return;
                    connectCount = 1;
                    taskName.Text = "Directing from {ID: " + prevLoc.StreetNodeID + "} to ...";
                    return;
                }

                if (connectCount == 1)
                {
                    StreetNode directTo = Connect(e.X, e.Y);
                    if (directTo == null)
                    {
                        connectCount = 0;
                        prevLoc = null;
                        taskName.Text = "Direction canceled";
                        return;
                    }
                    if (directTo.StreetNodeID == prevLoc.StreetNodeID)
                    {
                        MessageBox.Show("Cannot direct to itself");
                        connectCount = 0;
                        prevLoc = null;
                        taskName.Text = "Direction canceled";
                        return;
                    }
                    taskName.Text = "Directing from {ID: " + prevLoc.StreetNodeID + "} to " + "{ ID: " + directTo.StreetNodeID + "}";
                    Clear();
                    StreetMapRender();
                    StreetNodeRender();
                    /*if (path != null) 
                        foreach (StreetNode node in path)
                        {
                            DrawExcitedNode(node.StreetNodeX, node.StreetNodeY);
                        }*/
                    List<StreetNode> path = BLL.Instance.Direct(prevLoc.StreetNodeID, directTo.StreetNodeID);
                    RenderRoute(path);
                    cost.Text = Math.Round(BLL.Instance.CostInKilometer(path), 2).ToString() + " km";
                }
            }
        }

        private int[] ViewCoorConvert(double x, double y)
        {
            int[] result = new int[2];
            result[0] = (int)((x - West) / (East - West) * pictureBox1.Width);
            result[1] = (int)((North - y) / (North - South) * pictureBox1.Height);
            return result;
        }

        private double[] LocCoorConvert(int x, int y)
        {
            double[] result = new double[2];
            result[0] = West + (East - West) * x / pictureBox1.Width;
            result[1] = North - (North - South) * y / pictureBox1.Height;
            return result;
        }

        private void StreetMapRender()
        {
            List<ConnectedNode> lines = BLL.Instance.GetAllConnection();
            foreach (var line in lines)
            {
                int[] locStart = ViewCoorConvert(line.StreetNode.StreetNodeX, line.StreetNode.StreetNodeY);
                int[] locEnd = ViewCoorConvert(line.StreetNode1.StreetNodeX, line.StreetNode1.StreetNodeY);
                //MessageBox.Show(locStart[0].ToString() + " " + locStart[1].ToString() + " " + locEnd[0].ToString() + " " + locEnd[1].ToString());
                DrawLine(locStart[0], locStart[1], locEnd[0], locEnd[1]);
            }
        }

        private void StreetNodeRender()
        {
            List<StreetNode> streetNodes = BLL.Instance.GetStreetNodes();
            foreach (var node in streetNodes)
            {
                int[] loc = ViewCoorConvert(node.StreetNodeX, node.StreetNodeY);
                DrawNode(node.StreetNodeX, node.StreetNodeY);
            }
        }

        private StreetNode GetOnClosest(int x, int y)
        {
            double[] locCoor = LocCoorConvert(x, y);
            return BLL.Instance.GetClosestNode(locCoor[0], locCoor[1]);
        }

        private StreetNode Connect(int x, int y)
        {
            StreetNode node = GetOnClosest(x, y);
            if (node == null)
            {
                double[] locCoor = LocCoorConvert(x, y);
                MessageBox.Show("No node found near (" + locCoor[0] + " E , " + locCoor[1] + " N)");
                return null;
            }
            DrawExcitedNode(node.StreetNodeX, node.StreetNodeY);
            return node;
        }

        private void DrawExcitedStreet(double x1, double y1, double x2, double y2)
        {
            int[] locStart = ViewCoorConvert(x1, y1);
            int[] locEnd = ViewCoorConvert(x2, y2);
            Graphics g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.Blue, 3);
            g.DrawLine(pen, (float)locStart[0], (float)locStart[1], (float)locEnd[0], (float)locEnd[1]);
        }

        private void RenderRoute(List<StreetNode> route)
        {
            if (route == null) return;
            for (int i = 0; i < route.Count - 1; i++)
            {
                DrawExcitedStreet(route[i].StreetNodeX, route[i].StreetNodeY, route[i + 1].StreetNodeX, route[i + 1].StreetNodeY);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
