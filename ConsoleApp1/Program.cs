using System;
using System.Collections.Generic;
using System.Text;

class HuffmanNode
{
    public char Symbol { get; set; }
    public int Frequency { get; set; }
    public HuffmanNode Left { get; set; }
    public HuffmanNode Right { get; set; }
}

class HuffmanTree
{
    private HuffmanNode root;

    public HuffmanTree(Dictionary<char, int> frequencies)
    {
        BuildTree(frequencies);
    }

    private void BuildTree(Dictionary<char, int> frequencies)
    {
        PriorityQueue<HuffmanNode> priorityQueue = new PriorityQueue<HuffmanNode>();

        foreach (var kvp in frequencies)
        {
            priorityQueue.Enqueue(new HuffmanNode { Symbol = kvp.Key, Frequency = kvp.Value });
        }

        while (priorityQueue.Count > 1)
        {
            HuffmanNode left = priorityQueue.Dequeue();
            HuffmanNode right = priorityQueue.Dequeue();

            HuffmanNode mergedNode = new HuffmanNode
            {
                Symbol = '\0',
                Frequency = left.Frequency + right.Frequency,
                Left = left,
                Right = right
            };

            priorityQueue.Enqueue(mergedNode);
        }

        root = priorityQueue.Dequeue();
    }

    public Dictionary<char, string> BuildCodeTable()
    {
        Dictionary<char, string> codeTable = new Dictionary<char, string>();
        BuildCodeTableRecursive(root, "", codeTable);
        return codeTable;
    }

    private void BuildCodeTableRecursive(HuffmanNode node, string currentCode, Dictionary<char, string> codeTable)
    {
        if (node != null)
        {
            if (node.Left == null && node.Right == null)
            {
                codeTable.Add(node.Symbol, currentCode);
            }

            BuildCodeTableRecursive(node.Left, currentCode + "0", codeTable);
            BuildCodeTableRecursive(node.Right, currentCode + "1", codeTable);
        }
    }

    public string Encode(string input)
    {
        Dictionary<char, string> codeTable = BuildCodeTable();
        StringBuilder encodedString = new StringBuilder();

        foreach (char c in input)
        {
            encodedString.Append(codeTable[c]);
        }

        return encodedString.ToString();
    }

    public string Decode(string encodedString)
    {
        StringBuilder decodedString = new StringBuilder();
        HuffmanNode current = root;

        foreach (char bit in encodedString)
        {
            if (bit == '0')
            {
                current = current.Left;
            }
            else if (bit == '1')
            {
                current = current.Right;
            }

            if (current.Left == null && current.Right == null)
            {
                decodedString.Append(current.Symbol);
                current = root;
            }
        }

        return decodedString.ToString();
    }
}

class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> heap = new List<T>();

    public int Count => heap.Count;

    public void Enqueue(T item)
    {
        heap.Add(item);
        int i = heap.Count - 1;

        while (i > 0)
        {
            int parent = (i - 1) / 2;

            if (heap[parent].CompareTo(heap[i]) <= 0)
                break;

            Swap(parent, i);
            i = parent;
        }
    }

    public T Dequeue()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        T root = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        int i = 0;

        while (true)
        {
            int leftChild = 2 * i + 1;
            int rightChild = 2 * i + 2;

            if (leftChild >= heap.Count)
                break;

            int minChild = leftChild;

            if (rightChild < heap.Count && heap[rightChild].CompareTo(heap[leftChild]) < 0)
                minChild = rightChild;

            if (heap[i].CompareTo(heap[minChild]) <= 0)
                break;

            Swap(i, minChild);
            i = minChild;
        }

        return root;
    }

    private void Swap(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}

class HuffmanExample
{
    static void Main()
    {
        string originalText = "hello world";
        Console.WriteLine("Original Text: " + originalText);

        Dictionary<char, int> frequencies = new Dictionary<char, int>();
        foreach (char c in originalText)
        {
            if (frequencies.ContainsKey(c))
            {
                frequencies[c]++;
            }
            else
            {
                frequencies[c] = 1;
            }
        }

        HuffmanTree huffmanTree = new HuffmanTree(frequencies);

        string encodedText = huffmanTree.Encode(originalText);
        Console.WriteLine("Encoded Text: " + encodedText);

        string decodedText = huffmanTree.Decode(encodedText);
        Console.WriteLine("Decoded Text: " + decodedText);
    }
}
