using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgorithmsAndDataStructures
{
    public class BinaryTree<T> : ICollection<T> where T : IComparable<T>
    {
        private Node<T> root;
        private Node<T> parent;

        private IEnumerable<T> TraverseTree(Node<T> node)
        {
            if (node.Left != null)
            {
                foreach(T value in TraverseTree(node.Left))
                {
                    yield return value;
                }
            }
            yield return node.Value;
            if (node.Right != null)
            {
                foreach (T value in TraverseTree(node.Right))
                {
                    yield return value;
                }
            }
        }

        #region ICollection

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T value)
        {
            Add(ref root, value);
            Count++;
        }

        private void Add(ref Node<T> node, T value)
        {
            if (node == null)
                node = new Node<T>(value);
            else if (node.Value.CompareTo(value) > 0)
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

        public bool Contains(T value)
        {
            return Find(root, value) != null;
        }

        private Node<T> Find(Node<T> node, T value)
        {
            if (node == null || node.Value.CompareTo(value) == 0)
                return node;

            parent = node;
            return (node.Value.CompareTo(value) > 0 ? Find(node.Left, value) : Find(node.Right, value));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (T value in TraverseTree(root))
                array[arrayIndex++] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return TraverseTree(root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(T value)
        {
            bool result = false;

            Node<T> node = Find(root, value);
            if (node != null)
            {
                Remove(node);
                Count--;
                result = true;
            }

            return result;
        }

        private void Remove(Node<T> node)
        {
            if (node.Left == null && node.Right == null)
            {
                //leaf node
                ReplaceNode(node, null);
            }
            else if (node.Right == null)
            {
                //left child only
                ReplaceNode(node, node.Left);
            }
            else if (node.Left == null)
            {
                //right child only
                ReplaceNode(node, node.Right);
            }
            else
            {
                //node to be deleted has 2 child nodes

                //if right child does not have a left subtree, replace the node with the right child
                Node<T> subtreeRoot = node.Right;
                if (subtreeRoot.Left == null)
                {
                    node.Value = subtreeRoot.Value;
                    node.Right = subtreeRoot.Right;
                }
                else
                {
                    //replace the node value with the smallest value of its right subtree and remove the smallest node
                    Node<T> smallest = FindSmallestAndRemove(subtreeRoot);
                    node.Value = smallest.Value;
                }
            }
        }

        private void ReplaceNode(Node<T> nodeToReplace, Node<T> replaceWith)
        {
            if (nodeToReplace == root)
                root = replaceWith;
            else if (nodeToReplace == parent.Right)
                parent.Right = replaceWith;
            else
                parent.Left = replaceWith;
        }

        /// <summary>
        /// Returns node with the smallest value from the subtree of the provided node
        /// and removes it from the tree.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>The smallest node</returns>
        private Node<T> FindSmallestAndRemove(Node<T> node)
        {
            Node<T> smallest = node.Left;
            Node<T> smallestParent = node;

            while (smallest.Left != null)
            {
                smallestParent = smallest;
                smallest = smallest.Left;
            }

            smallestParent.Left = smallest.Right;

            return smallest;
        }

        #endregion

    }
}
