using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List2
{
    class Node
    {
        public Node Next { get; set; }
        public Node Previous { get; set; }
        public object Value { get; set; }

        public Node(object value)
        {
            Value = value;
        }
    }
    class LinkedList
    {
        public Node First { get; private set; }
        public Node Last { get; private set; }
        public int Count { get; private set; }


        public void AddLast(object newItem)
        {
            Count++;
            if (First == null)
            {
                First = Last = new Node(newItem);
            }
            else
            {
                Node node = new Node(newItem);
                Last.Next = node;
                node.Previous = Last;
                Last = node;
            }
        }

        public void AddFirst(object newItem)
        {
            Count++;
            if (First == null)
            {
                First = Last = new Node(newItem);
            }
            else
            {
                Node node = new Node(newItem);
                First.Next = First;
                node.Previous = First;
                First = node;
            }
        }

        public void Remove(object newItem)
        {
            if (First == null)
                return;
            if (Last == First && newItem.Equals(First))
            {
                First = Last = null;
                Count--;
            }
            else if (First.Value.Equals(newItem))
            {
                First.Previous = null;
                First = First.Next;
                Count--;
            }
            else if (Last.Value.Equals(newItem))
            {
                Last = Last.Previous;
                Count--;
            }
            else
            {
                Node current = First;
                do
                {
                    if (current.Value.Equals(newItem))
                    {
                        var previous = current.Previous;
                        var next = current.Next;

                        previous.Next = next;
                        next.Previous = previous;
                        Count--;
                    }
                    else
                        current = First.Next;
                }
                while (current != null);

            }

        }

        public object RemoveLast()
        {
            object removed = Last;
            Last.Previous = Last;
            Last.Next = null;
            Count--;
            return removed;
        }

        public object RemoveFirst()
        {
            object removed = First;
            First.Previous = null;
            First = First.Next;
            Count--;
            return removed;
        }

        public bool Contains(object newObj)
        {
            Node current = First;
            do
            {
                if (current.Value.Equals(newObj))
                    return true;
                current = First.Next;
            }
            while (current != null);
            return false;
        }

        public object[] ToArray()
        {
            object[] newArr = new object[Count];
            Node current = First;
            int i = 0;
            do
            {
                newArr[i] = First;
                current = First.Next;
                i++;
            }
            while (current != null);
            return newArr;
        }

        public void Clear()
        {
            Count = 0;
            First = null;
            Last = null;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < Count; i++)
            {
                stringBuilder.AppendLine($"LinkedList{i} = {First}");
                First = First.Next;
            }
            return stringBuilder.ToString();
        }
    }

    class Queue
    {
        private LinkedList linkedList = new LinkedList();

        public void Enqueue(object newItem)
        {
            linkedList.AddFirst(newItem);
        }

        public object Dequeue()
        {
            return linkedList.RemoveFirst();
        }

        public void Clear()
        {
            linkedList.Clear();
        }

        public object ToArray()
        {
            return linkedList.ToArray();
        }

        public bool Contains(object newObj)
        {
            return linkedList.Contains(newObj);
        }

        public object Peek()
        {
            if (linkedList.First == null)
                return null;
            return linkedList.First;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            Node current = linkedList.First;
            do
            {
                stringBuilder.AppendLine($"Queue{i} = {current}");
                current = linkedList.First.Next;
                i++;
            }
            while (current != null);
            return stringBuilder.ToString();
        }
    }

    class Stack
    {
        private LinkedList stack = new LinkedList();

        public void Clear()
        {
            stack.Clear();
        }

        public bool Contains(object newObj)
        {
            return stack.Contains(newObj);
        }

        public object ToArray()
        {
            return stack.ToArray();
        }

        public object Peek()
        {
            if (stack.Last == null)
                return null;
            return stack.Last;
        }

        public void Push(object newObj)
        {
            stack.AddLast(newObj);
        }

        public object Pop()
        {
            return stack.RemoveLast();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            Node current = stack.First;
            do
            {
                stringBuilder.AppendLine($"Stack{i} = {current}");
                current = stack.First.Next;
                i++;
            }
            while (current != null);
            return stringBuilder.ToString();
        }
    }

    public class NodeBT
    {
        public int Data { get; private set; }
        public NodeBT Left { get; set; }
        public NodeBT Right { get; set; }

        public NodeBT(int data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    class BinaryTree
    {
        public NodeBT Root { get; private set; }
        public int Count { get; private set; }

        public BinaryTree()
        {
            Root = null;
            Count = 0;
        }

        public BinaryTree(int data)
        {
            Root = new NodeBT(data);
        }

        public void Add(int data)
        {
            if (Root == null)
            {
                NodeBT newNode = new NodeBT(data);
                Root = newNode;
                Count++;
                return;
            }

            NodeBT currentNode = Root;
            bool addData = false;
            do
            {
                if (data < currentNode.Data)
                {
                    if (currentNode.Left == null)
                    {
                        NodeBT newNode = new NodeBT(data);
                        currentNode.Left = newNode;
                        addData = true;
                        Count++;
                    }
                    else
                    {
                        currentNode = currentNode.Left;
                    }
                }
                if (data > currentNode.Data)
                {
                    if (currentNode.Right == null)
                    {
                        NodeBT newNode = new NodeBT(data);
                        currentNode.Right = newNode;
                        addData = true;
                        Count++;
                    }
                    else
                    {
                        currentNode = currentNode.Right;
                    }
                }
            }
            while (!addData);
        }

        public bool Contains(int data)
        {
            NodeBT currentNode = Root;
            bool containsData = false;
            do
            {
                if (currentNode.Data == data)
                {
                    containsData = true;
                    return containsData;
                }
                if (data < currentNode.Data)
                {
                    currentNode = currentNode.Left;
                }
                if (data > currentNode.Data)
                {
                    currentNode = currentNode.Right;
                }
            }
            while (!containsData);
            return containsData;
        }

        public int[] ToArray()
        {
            int[] newArr = new int[Count];
            NodeBT current = Root;
            newArr[0] = Root.Data;
            int i = 1;
            do
            {
                if (current.Data!=Root.Data)
                newArr[i] = current.Data;
                i++;
                if (current.Left != null)
                    current = current.Left;
                else if (current.Right != null)
                    current = current.Right;
                else if (current.Left == null && current.Right == null)
                    current = Root;
            }
            while (i<Count);
            return newArr;
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }
    }
}


