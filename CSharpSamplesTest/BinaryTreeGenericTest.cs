using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsAndDataStructures;

namespace AlgorithmsAndDataStructuresTest
{
    [TestClass]
    public class BinaryTreeGenericTest
    {
        private BinaryTree<string> tree;

        private BinaryTree<string> InitializeTreeString(int count)
        {
            BinaryTree<string> tree = new BinaryTree<string>();
            for (int i = 0; i < count; i++)
            {
                Guid guid = Guid.NewGuid();
                tree.Add(guid.ToString());
            }

            return tree;
        }

        [TestInitialize]
        public void Initialize()
        {
            tree = new BinaryTree<string>();
        }

        [TestMethod]
        public void AddSingleItem()
        {
            tree.Add("Ernie");

            Assert.IsTrue(tree.Contains("Ernie"));
            Assert.IsFalse(tree.Contains("Bert"));
        }

        [TestMethod]
        public void AddMultipleItems()
        {
            tree.Add("Bert");
            tree.Add("Ernie");
            tree.Add("BigBird");

            Assert.IsTrue(tree.Contains("Bert"));
            Assert.IsTrue(tree.Contains("Ernie"));
            Assert.IsTrue(tree.Contains("BigBird"));
            Assert.IsFalse(tree.Contains("CookieMonster"));
        }

        [TestMethod]
        public void ICollectionCount()
        {
            Assert.AreEqual(0, tree.Count);

            tree.Add("Bert");
            Assert.AreEqual(1, tree.Count);

            int itemCount = new Random().Next(10, 30);
            tree = InitializeTreeString(itemCount);

            Assert.AreEqual(itemCount, tree.Count);
        }

        [TestMethod]
        public void ICollectionCopyTo()
        {
            int itemCount = new Random().Next(5, 20);
            string[] array = new string[itemCount];

            tree = InitializeTreeString(itemCount);
            tree.CopyTo(array, 0);

            Assert.AreEqual(itemCount, array.Length);

            foreach (string value in array)
                Assert.IsTrue(tree.Contains(value));
        }

        [TestMethod]
        public void IEnumerableGetEnumerator()
        {
            int itemCount = new Random().Next(5, 20);
            //string[] array = new string[itemCount];

            tree = InitializeTreeString(itemCount);

            foreach (string value in tree)
                Assert.IsTrue(tree.Contains(value));
        }

        [TestMethod]
        public void Clear()
        {
            int itemCount = new Random().Next(10, 30);
            tree = InitializeTreeString(itemCount);

            Assert.AreEqual(itemCount, tree.Count);

            tree.Clear();
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void SortOrder()
        {
            //itemCount = 50;
            //lowerBound = 1;
            //upperBound = 1000;
            //tree = AddValues(lowerBound, upperBound, itemCount);

            //int previous = 0;
            //foreach (int current in tree)
            //{
            //    Assert.IsTrue(previous <= current);
            //    previous = current;
            //}
        }

        [TestMethod]
        public void RemoveRootNoChildren()
        {
            BinaryTree<int> bTree = new BinaryTree<int>();

            bTree.Add(4);
            bool result = bTree.Remove(4);

            Assert.IsTrue(result);
            Assert.IsFalse(bTree.Contains(4));
            Assert.AreEqual(0, bTree.Count);
        }

        [TestMethod]
        public void RemoveRootNoChildrenThenAdd()
        {
            RemoveRootNoChildren();

            BinaryTree<int> bTree = new BinaryTree<int>();

            bTree.Add(6);
            Assert.IsTrue(bTree.Contains(6));
            Assert.AreEqual(1, bTree.Count);
        }

        [TestMethod]
        public void RemoveRootSingleLeftChild()
        {
            BinaryTree<int> bTree = new BinaryTree<int>();

            bTree.Add(4);
            bTree.Add(1);
            bTree.Remove(4);
            Assert.IsFalse(bTree.Contains(4));
            Assert.IsTrue(bTree.Contains(1));
            Assert.AreEqual(1, bTree.Count);
        }

        [TestMethod]
        public void RemoveRootSingleRightChild()
        {
            BinaryTree<int> bTree = new BinaryTree<int>();

            bTree.Add(4);
            bTree.Add(6);
            bTree.Remove(4);
            Assert.IsFalse(bTree.Contains(4));
            Assert.IsTrue(bTree.Contains(6));
            Assert.AreEqual(1, bTree.Count);
        }

        [TestMethod]
        public void RemoveLeftChildLeaf()
        {
            BinaryTree<int> bTree = new BinaryTree<int>();

            bTree.Add(4);
            bTree.Add(2);
            bTree.Remove(2);
            Assert.IsFalse(bTree.Contains(2));
            Assert.IsTrue(bTree.Contains(4));
            Assert.AreEqual(1, bTree.Count);
        }

        [TestMethod]
        public void RemoveRightChildLeaf()
        {
            BinaryTree<int> bTree = new BinaryTree<int>();

            bTree.Add(4);
            bTree.Add(6);
            bTree.Remove(6);
            Assert.IsFalse(bTree.Contains(6));
            Assert.IsTrue(bTree.Contains(4));
            Assert.AreEqual(1, bTree.Count);
        }

        [TestMethod]
        public void RemoveRootWithTwoChildLeafNodes()
        {
            BinaryTree<int> bTree = new BinaryTree<int>();

            bTree.Add(4);
            bTree.Add(2);
            bTree.Add(6);
            bTree.Remove(4);
            Assert.IsFalse(bTree.Contains(4));
            Assert.IsTrue(bTree.Contains(2));
            Assert.IsTrue(bTree.Contains(6));
            Assert.AreEqual(2, bTree.Count);
        }

        [TestMethod]
        public void RemoveRootWithTwoChildren()
        {
            BinaryTree<int> bTree = new BinaryTree<int>();

            bTree.Add(6);
            bTree.Add(2);
            bTree.Add(10);
            bTree.Add(14);
            bTree.Add(8);
            bTree.Add(12);
            bTree.Add(7);
            bTree.Remove(6);
            Assert.AreEqual(6, bTree.Count);

            int[] array = new int[6];
            bTree.CopyTo(array, 0);

            Assert.AreEqual(6, array.Length);

            foreach (int value in array)
                Assert.IsTrue(bTree.Contains(value));
        }

    }
}
