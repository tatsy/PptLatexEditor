using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPointLatex
{
    class TrieTree
    {
        private TrieNode root = null;

        public TrieTree(String[] words)
        {
            if (words.Length == 0) return;

            root = new TrieNode();
            construct(root, words, 0);            
        }

        public TrieTree(List<String> words) : this(words.ToArray())
        {
        }

        private void construct(TrieNode node, String[] words, int depth)
        {
            List<String>[] lists = new List<String>[256];
            for (int i = 0; i < 256; i++)
            {
                lists[i] = new List<String>();
            }

            foreach (String s in words)
            {
                if (s.Length < depth)
                {
                    continue;
                }

                if (s.Length == depth)
                {
                    lists[0].Add(s);
                    continue;
                }

                Char c = s[depth];
                lists[c].Add(s);
            }

            for (int i = 0; i < 256; i++)
            {
                if (lists[i].Count != 0)
                {
                    node.Children[i] = new TrieNode();
                    construct(node.Children[i], lists[i].ToArray(), depth + 1);
                }
            }
        }

        public bool Contain(String word)
        {
            return containSub(root, word, 0);
        }

        private bool containSub(TrieNode node, String word, int depth)
        {
            if (node == null)
            {
                return false;
            }

            if (word.Length < depth)
            {
                return false;
            }

            if (word.Length == depth)
            {
                return node.Children[0] != null;
            }

            Char c = word[depth];
            return containSub(node.Children[c], word, depth + 1);
        }

        public void Clear()
        {
            root = null;
        }

        public bool Empty
        {
            get
            {
                return root == null;
            }
        }
    }

    internal class TrieNode
    {
        public TrieNode[] Children = new TrieNode[256];

        public TrieNode()
        {
            for (int i = 0; i < 256; i++)
            {
                Children[i] = null;
            }
        }
    }
}
