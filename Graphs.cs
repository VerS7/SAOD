using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAOD
{
    class MST
    {
        static int V = 15;
        static int minKey(int[] key, bool[] mstSet)
        {
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    min_index = v;
                }
            return min_index;
        }
        static void printMST(int[] parent, int[,] graph)
        {
            for (int i = 1; i < V; i++)
                Console.WriteLine($"{parent[i]} -> {graph[i, parent[i]]} : {parent[i]}");
        }
        public static void primMST(int[,] graph)
        {
            int[] parent = new int[V];
            int[] key = new int[V];
            bool[] mstSet = new bool[V];
            for (int i = 0; i < V; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }
            key[0] = 0;
            parent[0] = -1;
            for (int count = 0; count < V - 1; count++)
            {
                int u = minKey(key, mstSet);
                mstSet[u] = true;
                for (int v = 0; v < V; v++)
                    if (graph[u, v] != 0 && mstSet[v] == false
                        && graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
            }
            printMST(parent, graph);
        }
    }
    class RandomGraph
    {
        public static int Size = 15;
        public int[,] Graph = new int[Size, Size];
        public RandomGraph(int Size)
        {
            RandomGraph.Size = Size;
            this.Graph = RandomMatrix(Size);
        }
        private int[,] RandomMatrix(int Size)
        {
            Random rnd = new();
            int[,] matrix = new int[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    matrix[i, j] = rnd.Next(1, 9);
                    matrix[i, i] = 0;
                }
            }
            for (int i = 0; i < Size; i++)
            {
                for (int j = i; j < Size; j++)
                {
                    matrix[j, i] = matrix[i, j];
                }
            }
            return matrix;
        }
        public void PrintGraph()
        {
            for (int i = 0; i < Graph.GetLength(0); i++)
            {
                for (int j = 0; j < Graph.GetLength(0); j++)
                {
                    Console.Write(Graph[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
    public class Edge : IComparable<Edge>
    {
        public int EdgeWeight { get; set; }
        public string VertexA { get; set; }
        public string VertexB { get; set; }


        public Edge(string vertexA, string vertexB, int weight)
        {
            VertexA = vertexA;
            VertexB = vertexB;
            EdgeWeight = weight;
        }

        public int CompareTo(Edge other)
        {
            if (other == null) return 1;
            return EdgeWeight.CompareTo(other.EdgeWeight);
        }
    }
    public class Graph_ : IEnumerable<Edge>
    {
        private List<Edge> _graph;

        public Graph_()
        {
            _graph = new List<Edge>();
        }

        public Graph_(Edge val)
        {
            Edge[] value = new Edge[] { val };

            _graph = new List<Edge>(value);
        }

        public void Add(Graph_ graph)
        {
            foreach (Edge edge in graph)
            {
                _graph.Add(edge);
            }
        }

        public void Add(Edge edge)
        {
            _graph.Add(edge);
        }

        public int GetWeight()
        {
            int weight = 0;
            foreach (Edge edge in _graph)
            {
                weight += edge.EdgeWeight;
            }
            return weight;
        }

        public Graph_ FindMinimumSpanningTree()
        {
            Sort();
            var disjointSets = new SystemOfDisjointSets();
            foreach (Edge edge in _graph)
            {
                disjointSets.AddEdgeInSet(edge);
            }

            return disjointSets.Sets.First().SetGraph;
        }

        public override string ToString()
        {
            string result = string.Empty;

            foreach (Edge edge in _graph)
            {
                if (!(result.Contains($"[{ edge.VertexB}] -> [{ edge.VertexA}]")))
                result += $"[{edge.VertexA}] -> [{edge.VertexB}] : {edge.EdgeWeight}\n";
            }

            return result;
        }

        public void Sort()
        {
            _graph.Sort();
        }

        public IEnumerator<Edge> GetEnumerator()
        {
            return _graph.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _graph.GetEnumerator();
        }
    }
    public class Set
    {
        public Graph_ SetGraph;
        public List<string> Vertices;

        public Set(Edge edge)
        {
            SetGraph = new Graph_(edge);

            Vertices = new List<string>();
            Vertices.Add(edge.VertexA);
            Vertices.Add(edge.VertexB);
        }

        public void Union(Set set, Edge connectingEdge)
        {
            SetGraph.Add(set.SetGraph);
            Vertices.AddRange(set.Vertices);
            SetGraph.Add(connectingEdge);
        }

        public void AddEdge(Edge edge)
        {
            SetGraph.Add(edge);
            Vertices.Add(edge.VertexA);
            Vertices.Add(edge.VertexB);
        }

        public bool Contains(string vertex)
        {
            return Vertices.Contains(vertex);
        }
    }
    class SystemOfDisjointSets
    {
        public List<Set> Sets;

        public SystemOfDisjointSets()
        {
            Sets = new List<Set>();
        }

        public void AddEdgeInSet(Edge edge)
        {
            Set setA = Find(edge.VertexA);
            Set setB = Find(edge.VertexB);

            if (setA != null && setB == null)
            {
                setA.AddEdge(edge);
            }
            else if (setA == null && setB != null)
            {
                setB.AddEdge(edge);
            }
            else if (setA == null && setB == null)
            {
                Set set = new Set(edge);
                Sets.Add(set);
            }
            else if (setA != null && setB != null)
            {
                if (setA != setB)
                {
                    setA.Union(setB, edge);
                    Sets.Remove(setB);
                }
            }
        }

        public Set Find(string vertex)
        {
            foreach (Set set in Sets)
            {
                if (set.Contains(vertex)) return set;
            }
            return null;
        }
    }
    public class GraphVertexInfo
    {
        /// <summary>
        /// Вершина
        /// </summary>
        public GraphVertex Vertex { get; set; }

        /// <summary>
        /// Не посещенная вершина
        /// </summary>
        public bool IsUnvisited { get; set; }

        /// <summary>
        /// Сумма весов ребер
        /// </summary>
        public int EdgesWeightSum { get; set; }

        /// <summary>
        /// Предыдущая вершина
        /// </summary>
        public GraphVertex PreviousVertex { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="vertex">Вершина</param>
        public GraphVertexInfo(GraphVertex vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
        }
    }
    public class Dijkstra
    {
        Graph graph;
        List<GraphVertexInfo> infos;
        public Dijkstra(Graph graph)
        {
            this.graph = graph;
        }
        void InitInfo()
        {
            infos = new List<GraphVertexInfo>();
            foreach (var v in graph.Vertices)
            {
                infos.Add(new GraphVertexInfo(v));
            }
        }
        GraphVertexInfo GetVertexInfo(GraphVertex v)
        {
            foreach (var i in infos)
            {
                if (i.Vertex.Equals(v))
                {
                    return i;
                }
            }

            return null;
        }
        public GraphVertexInfo FindUnvisitedVertexWithMinSum()
        {
            var minValue = int.MaxValue;
            GraphVertexInfo minVertexInfo = null;
            foreach (var i in infos)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }
        public string FindShortestPath(string startName, string finishName)
        {
            return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName));
        }
        public string FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
        {
            InitInfo();
            var first = GetVertexInfo(startVertex);
            first.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }

                SetSumToNextVertex(current);
            }

            return GetPath(startVertex, finishVertex);
        }
        void SetSumToNextVertex(GraphVertexInfo info)
        {
            info.IsUnvisited = false;
            foreach (var e in info.Vertex.Edges)
            {
                var nextInfo = GetVertexInfo(e.ConnectedVertex);
                var sum = info.EdgesWeightSum + e.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
        }
        string GetPath(GraphVertex startVertex, GraphVertex endVertex)
        {
            var path = endVertex.ToString();
            while (startVertex != endVertex)
            {
                endVertex = GetVertexInfo(endVertex).PreviousVertex;
                path = endVertex.ToString() + path;
            }
            return path;
        }
    }
    public class GraphEdge
    {
        public GraphVertex ConnectedVertex { get; }
        public int EdgeWeight { get; }
        public GraphEdge(GraphVertex connectedVertex, int weight)
        {
            ConnectedVertex = connectedVertex;
            EdgeWeight = weight;
        }
    }
    public class GraphVertex
    {
        public string Name { get; }
        public List<GraphEdge> Edges { get; }
        public GraphVertex(string vertexName)
        {
            Name = vertexName;
            Edges = new List<GraphEdge>();
        }
        public void AddEdge(GraphEdge newEdge)
        {
            Edges.Add(newEdge);
        }
        public void AddEdge(GraphVertex vertex, int edgeWeight)
        {
            AddEdge(new GraphEdge(vertex, edgeWeight));
        }
        public override string ToString() => Name;
    }
    public class Graph
    {
        public List<GraphVertex> Vertices { get; }
        public Graph()
        {
            Vertices = new List<GraphVertex>();
        }
        public void AddVertex(string vertexName)
        {
            Vertices.Add(new GraphVertex(vertexName));
        }
        public GraphVertex FindVertex(string vertexName)
        {
            foreach (var v in Vertices)
            {
                if (v.Name.Equals(vertexName))
                {
                    return v;
                }
            }

            return null;
        }
        public void AddEdge(string firstName, string secondName, int weight)
        {
            var v1 = FindVertex(firstName);
            var v2 = FindVertex(secondName);
            if (v2 != null && v1 != null)
            {
                v1.AddEdge(v2, weight);
                v2.AddEdge(v1, weight);
            }
        }
    }
    public class DijkstaHandler
    {
        public List<object> nodes = new List<object> {
            (4, 6, 9),
            (4, 3, 6),
            (4, 5, 3),
            (3, 2, 5),
            (3, 7, 1),
            (3, 6, 7),
            (2, 7, 3),
            (2, 1, 1),
            (1, 7, 2),
            (1, 8, 6),
            (8, 9, 7),
            (8, 13, 2),
            (9, 6, 9),
            (9, 13, 3),
            (6, 10, 4),
            (5, 10, 5),
            (10, 13, 2),
            (13, 12, 3),
            (13, 15, 5),
            (12, 15, 4),
            (10, 11, 8),
            (10, 12, 3),
            (11, 15, 1),
            (15, 14, 3),
            (11, 14, 7),
            (5, 14, 10),
            (7, 6, 3),
            (11, 12, 3)
        };
        string[] nodenames = Enumerable.Range(1, 15).ToArray().Select(x => $"({x.ToString()})").ToArray();

        public DijkstaHandler()
        {
            Graph graph = new();
            foreach (string name in nodenames)
            {
                graph.AddVertex(name);
            }
            foreach (object node in nodes)
            {
                var nd = node.ToString().Replace("(", "").Replace(")", "").Split(",");
                graph.AddEdge($"({nd[0]})", $"({nd[1].Replace(" ", "")})", Convert.ToInt32(nd[2]));
            }
            Dijkstra dijalg = new(graph);
            foreach (string name in nodenames)
            {
                Console.WriteLine(dijalg.FindShortestPath("(5)", $"{name}"));
            }
        }
    }
}


