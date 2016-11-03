using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class TreeNode<T>//CS260 FTW
{
    private Dictionary<string, TreeNode<T>> allNodes;
    public TreeNode<T> parent;
    private Dictionary<T, TreeNode<T>> children;//Data,
    public T Data;
    private string path;
    private string dataString;
    public int depth;
    public int number;
    public bool end;

    public Dictionary<T, TreeNode<T>> Children
    {
        get
        {
            if (children == null)
            {
                children = new Dictionary<T, TreeNode<T>>();
                Debug.Log(Data + "'s children was null, so it was created.");
            }
            return children;
        }

        set
        {
            children = value;
        }
    }

    public string Path()
    {
        if (parent == null)
        {
            return DataString;
        }
        else {

            return parent.Path() + "/" + DataString;
        }
    }

    public Dictionary<string, TreeNode<T>> AllNodes
    {
        get
        {
            if (allNodes == null) { allNodes = new Dictionary<string, TreeNode<T>>(); }
            if (parent == null)
            {
                if (allNodes == null)
                {
                    allNodes = new Dictionary<string, TreeNode<T>>();
                }
                return allNodes;
            }
            else { return parent.allNodes; }
        }

        set
        {
            if (parent == null)
            {
                allNodes = value;
            }
            else { parent.AllNodes = value; }
        }
    }

    public string DataString
    {
        get
        {
            return Data.ToString();
        }

        set
        {
            dataString = value;
        }
    }

    public void InsertChild(TreeNode<T> newChild)
    {
        newChild.parent = this;
        Children.Add(newChild.Data, newChild);
        newChild.depth = depth + 1;
        newChild.number = Children.Count - 1;
        if (!AllNodes.ContainsKey(Path())) { AllNodes.Add(Path(), newChild); }
    }
    public void RemoveChild(TreeNode<T> child)
    {
        AllNodes.Remove(child.path);
        Children.Remove(child.Data);
    }
    public TreeNode(T d)
    {
        Data = d;
        Children = new Dictionary<T, TreeNode<T>>();
        number = 0;
    }
    public TreeNode<T> Get(T d)
    {
        return Children[d];
    }
    public TreeNode<T> GetChildByIndex(int n)
    {
        foreach (TreeNode<T> t in Children.Values)
        {
            if (t.number == n) { return t; }
        }
        return null;
    }
    public static void DeleteChildren(TreeNode<T> node, bool preserve)
    {//TODO: Make it recursive
        if (node.Children.Count > 0)
        {
            foreach (TreeNode<T> n in node.Children.Values)
            {
                node.Children = null;
            }
        }
        if (!preserve) { node = null; }
    }
    public bool HasChildren()
    {
        if (Children != null)
        {
            if (Children.Count > 0)
            {
                return true;
            }
        }
        return false;
    }
    public void AddHierarchy()
    {

    }
}
