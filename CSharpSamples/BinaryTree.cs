using System;
using System.Collections;

namespace AlgorithmsAndDataStructures
{
    public class BinaryTree : ICollection
    {
        private Node root;
        private Node parent;

        #region ICollection

        public int Count { get; private set; }

        public void CopyTo(Array array, int index)
        { 
            int[] intArray = array as int[];

            foreach (int value in this)
                intArray[index++] = value;
        }
        
        public IEnumerator GetEnumerator()
        {
            return TraverseTree(root).GetEnumerator();
        }

        public bool IsSynchronized { get => false; }

        public object SyncRoot { get => root; }

        #endregion

        private IEnumerable TraverseTree(Node node)
        {
            if (node.Left != null)
            {
                foreach (var value in TraverseTree(node.Left))
                    yield return value;
            }

            yield return node.Value;

            if (node.Right != null)
            {
                foreach (var value in TraverseTree(node.Right))
                    yield return value;
            }
        }

        private Node Find(Node node, int value)
        {
            if (node == null || node.Value == value)
                return node;

            parent = node;
            return (value < node.Value ? Find(node.Left, value) : Find(node.Right, value));
        }

        public void Add(int value)
        {
            Add(ref root, value);
            Count++;
        }

        private void Add(ref Node node, int value)
        {
            if (node == null)
                node = new Node(value);
            else if (value < node.Value)
                Add(ref node.Left, value);
            else
                Add(ref node.Right, value);
        }

        public void Clear()
        {
            root = null;
            parent = null;
            Count = 0;
        }

        public bool Contains(int value)
        {
            return Find(root, value) != null;
        }

        public void Remove(int value)
        {
            Node node = Find(root, value);
            if (node == null)
                return;

            Remove(node);
            Count--;
        }

        private void Remove(Node node)
        {
            //check for leaf node
            if (node.Left == null && node.Right == null)
            {
                ReplaceNode(node, null);
                return;
            }

            //non-leaf node
            if (node.Right == null)
            {
                ReplaceNode(node, node.Left);
            }
            else
            {
                ReplaceNode(node, node.Right);
                if (node.Left != null)
                    UpdateSmallestOfTheLargeChildren(node);
            }
        }

        private void ReplaceNode(Node nodeToReplace, Node replaceWith)
        {
            if (nodeToReplace == root)
                root = replaceWith;
            else if (nodeToReplace == parent.Right)
                parent.Right = replaceWith;
            else
                parent.Left = replaceWith;
        }

        private void UpdateSmallestOfTheLargeChildren(Node nodeToRemove)
        {
            //of the provided node's large children, find the node with the smallest value
            //then connect the small children of the node being removed, to the smallest child node
            Node smallest = nodeToRemove.Right;
            while (smallest.Left != null)
                smallest = smallest.Left;
            smallest.Left = nodeToRemove.Left;
        }

    }
}