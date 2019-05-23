using System;

namespace AlgorithmsAndDataStructures
{
    public class Node
    {
        public Node Left;
        public Node Right;
        public int Value { get; set; }
        public Node(int value) => Value = value;
    }

    public class Node<T> : IComparable<T> where T : IComparable<T>
    {
        public Node<T> Left;
        public Node<T> Right;
        public T Value;

        public Node(T value)
        {
            Value = value;
        }

        public int CompareTo(T value)
        {
            return value.CompareTo(value);
        }
    }
}