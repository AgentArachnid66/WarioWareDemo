using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NodeEdge
{
    public int NodeID;
    public float EdgeWeight;
}

[System.Serializable]
public struct GraphNode
{
    public List<NodeEdge> edges;
    public int Id;
}


public class DataTypes : MonoBehaviour
{
    public List<int> List = new List<int>();
    public int[] array = new int[10];

    [SerializeField]
    public List<GraphNode> GraphNodes = new List<GraphNode>();


    [ContextMenu("Breadth First Search")]
    public void TestBFS()
    {
        Debug.Log($"Starting Breadth First Search");
        AssignNodeIDs();
        BreadthFirstSearch(0);
    }

    [ContextMenu("Depth First Search")]
    public void TestDFS()
    {
        Debug.Log($"Starting Depth First Search");
        AssignNodeIDs();
        bool[] visited = new bool[GraphNodes.Count];
        DepthFirstSearch(0, visited);
    }

    private void AssignNodeIDs()
    {
        for(int i = 0; i < GraphNodes.Count; i++)
        {
            GraphNode node = GraphNodes[i];
            node.Id = i;
            GraphNodes[i] = node;
        }
    }


    void BreadthFirstSearch(int InitialNode)
    {
        Debug.Log($"Initial {InitialNode}");
        Queue<GraphNode> NodeQueue = new Queue<GraphNode>();
        for (int i = 0; i < GraphNodes[InitialNode].edges.Count; i++)
        {
            // Enqueue the Connected Edges to the Initial Node
            Debug.Log($"Enqueuing {GraphNodes[InitialNode].edges[i].NodeID}");
            NodeQueue.Enqueue(GraphNodes[GraphNodes[InitialNode].edges[i].NodeID]);
        }

        bool[] visited = new bool[GraphNodes.Count];
        visited[InitialNode] = true;

        while(NodeQueue.Count > 0)
        {
            GraphNode _Node = NodeQueue.Dequeue();
            GraphNodes.FindIndex(node =>
            {
                return node.Id == _Node.Id;
            });
            Debug.Log($"Visiting {_Node.Id}");

            for(int i = 0; i < GraphNodes[_Node.Id].edges.Count; i++)
            {
                int EdgeID = GraphNodes[_Node.Id].edges[i].NodeID;

                if (!visited[EdgeID])
                {
                    Debug.Log($"Enqueuing {EdgeID}");
                    NodeQueue.Enqueue(GraphNodes[EdgeID]);
                    visited[EdgeID] = true;
                }
            }
        }

    }

    void DepthFirstSearch(int InitialNode, bool[] visitedNodes)
    {
        visitedNodes[InitialNode] = true;

        Debug.Log($"Visited {InitialNode}");

        
        foreach(NodeEdge Edge in GraphNodes[InitialNode].edges)
        {
            if (!visitedNodes[Edge.NodeID])
            {
                DepthFirstSearch(Edge.NodeID, visitedNodes);
            }
        }
    }
}
