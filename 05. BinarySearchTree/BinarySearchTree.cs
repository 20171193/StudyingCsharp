using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private Node root;

        public BinarySearchTree()
        {
            this.root = null;
        }

        public bool Add(T item)
        {
            Node newNode = new Node(item, null, null, null);

            if (root == null)
            {
                root = newNode;
                return true;
            }

            Node current = root;
            while (current != null)
            {
                if (item.CompareTo(current.item) < 0)
                {
                    if (current.left != null)
                    {
                        current = current.left;
                    }
                    else
                    {
                        current.left = newNode;
                        newNode.parent = current;
                        break;
                    }
                }
                else if (item.CompareTo(current.item) > 0)
                {
                    if (current.right != null)
                    {
                        current = current.right;
                    }
                    else
                    {
                        current.right = newNode;
                        newNode.parent = current;
                        break;
                    }
                }
                else // if (item.CompareTo(current.item) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Remove(T item)
        {
            Node findNode = FindNode(item);
            if (findNode != null)
            {
                EraseNode(findNode);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Contains(T item)
        {
            Node findNode = FindNode(item);
            return findNode != null ? true : false;
        }

        public void Clear()
        {
            root = null;
        }

        private Node FindNode(T item)
        {
            if (root == null)
                return null;

            Node current = root;
            while (current != null)
            {
                if (item.CompareTo(current.item) < 0)
                {
                    current = current.left;
                }
                else if (item.CompareTo(current.item) > 0)
                {
                    current = current.right;
                }
                else // if (item.CompareTo(current.item) == 0)
                {
                    return current;
                }
            }

            return null;
        }

        private void EraseNode(Node node)
        {
            if (node.HasNoChild)
            {
                if (node.IsLeftChild)
                    node.parent.left = null;
                else if (node.IsRightChild)
                    node.parent.right = null;
                else
                    root = null;
            }
            else if (node.HasLeftChild || node.HasRightChild)
            {
                Node parent = node.parent;
                Node child = node.HasLeftChild ? node.left : node.right;

                if (node.IsLeftChild)
                {
                    parent.left = child;
                    child.parent = parent;
                }
                else if (node.IsRightChild)
                {
                    parent.right = child;
                    child.parent = parent;
                }
                else // if (node.IsRootNode)
                {
                    root = child;
                    child.parent = null;
                }
            }
            else // if (node.HasBothChild)
            {
                Node nextNode = node.right;
                while (nextNode.left != null)
                {
                    nextNode = nextNode.left;
                }
                node.item = nextNode.item;
                EraseNode(nextNode);
            }
        }

        private class Node
        {
            public T item;
            public Node parent;
            public Node left;
            public Node right;

            public Node(T item, Node parent, Node left, Node right)
            {
                this.item = item;
                this.parent = parent;
                this.left = left;
                this.right = right;
            }


            public bool IsRootNode    { get { return parent == null; } }
            public bool IsLeftChild   { get { return parent != null && parent.left == this; } }
            public bool IsRightChild  { get { return parent != null && parent.right == this; } }

            public bool HasNoChild    { get { return left == null && right == null; } }
            public bool HasLeftChild  { get { return left != null && right == null; } }
            public bool HasRightChild { get { return left != null && right != null; } }
            public bool HasBothChild  { get { return left != null && right != null; } }
        }
    }
}