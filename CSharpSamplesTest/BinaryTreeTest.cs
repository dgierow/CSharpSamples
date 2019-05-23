using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsAndDataStructures;

namespace AlgorithmsAndDataStructuresTest
{
    [TestClass]
    public class BinaryTreeTest
    {
        private BinaryTree tree;
        private int itemCount = 10;
        private int lowerBound = 1;
        private int upperBound = 100;

        private BinaryTree AddValues(int lowerBound, int upperBound, int count)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException("Count must be greater than zero.");

            if (lowerBound < 0 || upperBound < lowerBound)
                throw new ArgumentOutOfRangeException("Parameters must be greater than zero. Parameter lowerBound must be greater than upperBound.");

            BinaryTree b = new BinaryTree();
            Random r = new Random();

            for (int i = 0; i < count; i++)
                b.Add(r.Next(lowerBound, upperBound));

            return b;
        }

        [TestInitialize]
        public void Initialize()
        {
            itemCount = 10;
            lowerBound = 1;
            upperBound = 100;
            tree = new BinaryTree();
        }

        [TestMethod]
        public void AddSingleValue()
        {
            tree.Add(4);
            Assert.IsTrue(tree.Contains(4));
            Assert.IsFalse(tree.Contains(8));
        }

        [TestMethod]
        public void AddMultipleValues()
        {
            tree.Add(5);
            tree.Add(4);
            tree.Add(6);
            tree.Add(3);
            tree.Add(8);
            tree.Add(1);
            tree.Add(2);
            tree.Add(7);
            tree.Add(10);
            tree.Add(9);

            Assert.IsTrue(tree.Contains(1));
            Assert.IsTrue(tree.Contains(2));
            Assert.IsTrue(tree.Contains(3));
            Assert.IsTrue(tree.Contains(4));
            Assert.IsTrue(tree.Contains(5));
            Assert.IsTrue(tree.Contains(6));
            Assert.IsTrue(tree.Contains(7));
            Assert.IsTrue(tree.Contains(8));
            Assert.IsTrue(tree.Contains(9));
            Assert.IsTrue(tree.Contains(10));
        }

        #region ICollection Tests

        [TestMethod]
        public void ICollectionCount()
        {
            Assert.AreEqual(0, tree.Count);

            tree.Add(4);
            Assert.AreEqual(1, tree.Count);

            tree = new BinaryTree();
            itemCount = new Random().Next(10, 50);

            tree = AddValues(lowerBound, upperBound, itemCount);
            Assert.AreEqual(itemCount, tree.Count);
        }

        [TestMethod]
        public void ICollectionCopyTo()
        {
            int[] array = new int[itemCount];
            tree = AddValues(lowerBound, upperBound, itemCount);
            tree.CopyTo(array, 0);

            Assert.AreEqual(itemCount, array.Length);

            foreach (int value in array)
                Assert.IsTrue(tree.Contains(value));
        }

        [TestMethod]
        public void ICollectionIsSynchronized()
        {
            Assert.IsFalse(tree.IsSynchronized);
        }

        [TestMethod]
        public void ICollectionSyncRoot()
        {
            Node root = tree.SyncRoot as Node;
            Assert.IsNull(root);

            tree.Add(4);
            tree.Add(1);
            tree.Add(6);

            root = tree.SyncRoot as Node;
            Assert.AreEqual(4, root.Value);
            Assert.AreEqual(1, root.Left.Value);
            Assert.AreEqual(6, root.Right.Value);
        }

        [TestMethod]
        public void IEnumerableGetEnumerator()
        {
            tree = AddValues(lowerBound, upperBound, itemCount);

            foreach (int value in tree)
                Assert.IsTrue(tree.Contains(value));
        }

        #endregion

        [TestMethod]
        public void Clear()
        {
            tree = AddValues(lowerBound, upperBound, itemCount);
            Assert.AreEqual(itemCount, tree.Count);

            tree.Clear();
            Assert.AreEqual(0, tree.Count);

            Node root = tree.SyncRoot as Node;
            Assert.IsNull(root);
        }

        [TestMethod]
        public void SortOrder()
        {
            itemCount = 50;
            lowerBound = 1;
            upperBound = 1000;
            tree = AddValues(lowerBound, upperBound, itemCount);

            int previous = 0;
            foreach (int current in tree)
            {
                Assert.IsTrue(previous <= current);
                previous = current;
            }
        }

        [TestMethod]
        public void RemoveRootNoChildren()
        {
            tree.Add(4);
            tree.Remove(4);

            Assert.IsFalse(tree.Contains(4));
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void RemoveRootNoChildrenThenAdd()
        {
            RemoveRootNoChildren();

            tree.Add(6);
            Assert.IsTrue(tree.Contains(6));
            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void RemoveRootSingleLeftChild()
        {
            tree.Add(4);
            tree.Add(1);
            tree.Remove(4);
            Assert.IsFalse(tree.Contains(4));
            Assert.IsTrue(tree.Contains(1));
            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void RemoveRootSingleRightChild()
        {
            tree.Add(4);
            tree.Add(6);
            tree.Remove(4);
            Assert.IsFalse(tree.Contains(4));
            Assert.IsTrue(tree.Contains(6));
            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void RemoveLeftChildLeaf()
        {
            tree.Add(4);
            tree.Add(2);
            tree.Remove(2);
            Assert.IsFalse(tree.Contains(2));
            Assert.IsTrue(tree.Contains(4));
            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void RemoveRightChildLeaf()
        {
            tree.Add(4);
            tree.Add(6);
            tree.Remove(6);
            Assert.IsFalse(tree.Contains(6));
            Assert.IsTrue(tree.Contains(4));
            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void RemoveRootWithTwoChildLeafNodes()
        {
            tree.Add(4);
            tree.Add(2);
            tree.Add(6);
            tree.Remove(4);
            Assert.IsFalse(tree.Contains(4));
            Assert.IsTrue(tree.Contains(2));
            Assert.IsTrue(tree.Contains(6));
            Assert.AreEqual(2, tree.Count);
        }

        [TestMethod]
        public void RemoveRootWithTwoChildren()
        {
            tree.Add(6);
            tree.Add(2);
            tree.Add(10);
            tree.Add(14);
            tree.Add(8);
            tree.Add(12);
            tree.Add(7);
            tree.Remove(6);
            Assert.AreEqual(6, tree.Count);

            int[] array = new int[6];
            tree.CopyTo(array, 0);

            Assert.AreEqual(6, array.Length);

            foreach (int value in array)
                Assert.IsTrue(tree.Contains(value));
        }
    }
}
