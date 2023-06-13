using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ICGPerfAutomated
{
    public class TestFactory
    {
        public static ItemsChangeBaseTest CreateTestInstance(string testType, ObservableCollection<IItem> items, Stopwatch st)
        {
            ItemsChangeBaseTest test = null;
            switch (testType)
            {
                case "AddTestCase0":
                    test = new AddTestCase0(items, st);
                    break;
                case "AddTestCase1":
                    test = new AddTestCase1(items, st);
                    break;
                case "AddTestCase2":
                    test = new AddTestCase2(items, st);
                    break;
                case "RemoveTestCase0":
                    test = new RemoveTestCase0(items, st);
                    break;  
                case "RemoveTestCase1":
                    test = new RemoveTestCase1(items, st);
                    break;
                case "RemoveTestCase2":
                    test = new RemoveTestCase2(items, st);
                    break;
                case "ReplaceTestCase0":
                    test = new ReplaceTestCase0(items, st);
                    break;
                case "MoveTestCase0":
                    test = new MoveTestCase0(items, st);
                    break;
                case "MoveTestCase1":
                    test = new MoveTestCase1(items, st);
                    break;
            }
            return test;
        }
    }

    public class ItemsChangeBaseTest
    {
        protected ObservableCollection<IItem> _items;
        protected Stopwatch st;

        protected int nestingLevel;
        protected int numRecords;
        protected MainWindow mainWindow;

        public ItemsChangeBaseTest(ObservableCollection<IItem> items, Stopwatch st)
        {
            _items = items;
            this.st = st;
        }

        public virtual void Run(int iterations, int nestingLevel, int numRecords)
        {
            PreTest();
            for (int i = 0; i < iterations; i++)
            {
                Test(i);
            }
        }

        public virtual void Configure(MainWindow mainWindow, int nestingLevel, int numRecords)
        {
            this.mainWindow = mainWindow;
            this.nestingLevel = nestingLevel;
            this.numRecords = numRecords;
        }

        public virtual void Startup() { }

        public virtual void Clean() { }

        public virtual void PreTest() { }

        public virtual void Test(int iterIndex) { }

        public virtual void Reset() { }

        public virtual Node CreateNesting(int index, int id)
        {
            Node prev = null;
            Node curr = null;

            for (int level = 0; level < nestingLevel; level++)
            {
                curr = new Node(level, id);
                if (prev == null)
                {
                    _items.Insert(index, curr);
                    prev = curr;
                    continue;
                }
                prev.ChildItems.Add(curr);
                prev = curr;
            }

            return curr;
        }

        public virtual void AddRecords(Node node, int id)
        {
            for (int i = 0; i < numRecords; i++)
            {
                var child = new LeafNode(i, id);
                node.ChildItems.Add(child);
            }
        }

    }

    public class AddTestCase0 : ItemsChangeBaseTest
    {
        public AddTestCase0(ObservableCollection<IItem> items, Stopwatch st) : base(items, st)
        {
        }

        public override void Test(int iterIndex)
        {
            Node prev = null;
            Node curr = null;

            for (int level = 0; level < nestingLevel; level++)
            {
                curr = new Node(level, iterIndex);
                if (prev == null)
                {
                    _items.Insert(0, curr);
                    prev = curr;
                    continue;
                }
                prev.ChildItems.Add(curr);
                prev = curr;
            }

            for (int i = 0; i < numRecords; i++)
            {
                var node = new LeafNode(i, iterIndex);
                curr.ChildItems.Add(node);
            }
        }

        public override void Reset()
        {
            _items.Clear();
        }
    }

    public class AddTestCase1 : AddTestCase0
    {
        public AddTestCase1(ObservableCollection<IItem> items, Stopwatch st) : base(items, st)
        {
        }

        public override void Startup()
        {
            for (int i = 0; i < numRecords; i++)
            {
                _items.Add(new LeafNode(i, i));
            }
        }

        public override void Test(int iterIndex)
        {
            base.Test(iterIndex);
        }

        public override void Reset()
        {
            _items.Remove(_items.First());
        }
    }

    public class AddTestCase2 : AddTestCase1
    {
        private static ScrollViewer scrollViewer = null;

        public AddTestCase2(ObservableCollection<IItem> items, Stopwatch st) : base(items, st)
        {
        }

        public override void Startup()
        {
            base.Startup();

            if(scrollViewer == null)
            {
                scrollViewer = Helpers.FindVisualChild<ScrollViewer>(this.mainWindow.treeView);
            }
            Helpers.ScrollTo(scrollViewer, Helpers.Pos.Bottom);
        }

        public override void Test(int iterIndex)
        {
            base.Test(iterIndex);
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Loaded, new Action(() => { }));
            Helpers.ScrollTo(scrollViewer, Helpers.Pos.Top);
        }

        public override void Reset()
        {
            base.Reset();
            Helpers.ScrollTo(scrollViewer, Helpers.Pos.Bottom);
        }
    }

    public class RemoveTestCase0 : ItemsChangeBaseTest
    {
        protected static int index = 0;
        protected Node lastNonLeafNode = null;

        public RemoveTestCase0(ObservableCollection<IItem> items, Stopwatch st) : base(items, st)
        {
        }

        public override void PreTest()
        {
            Node prev = null;
            Node curr = null;

            for (int level = 0; level < nestingLevel; level++)
            {
                curr = new Node(level, index);
                if (prev == null)
                {
                    _items.Insert(0, curr);
                    prev = curr;
                    continue;
                }
                prev.ChildItems.Add(curr);
                prev = curr;
            }

            for (int i = 0; i < numRecords; i++)
            {
                var node = new LeafNode(i, index);
                curr.ChildItems.Add(node);
            }

            lastNonLeafNode = curr;
        }

        public override void Test(int iterIndex)
        {
            while(lastNonLeafNode.ChildItems.Count > 0)
            {
                lastNonLeafNode.ChildItems.RemoveAt(0);
            }
            index = iterIndex;
        }

        public Node GetLastNonLeafNode(IItem node)
        {
            Node prev = null;
            Node curr = node as Node;

            // If the nesting level is 0, the the code will crash

            while (curr != null)
            {
                prev = curr;
                curr = curr.ChildItems.Last() as Node;
            }

            return prev;
        }

        public override void Reset()
        {
            _items.Clear();
        }
    }

    public class RemoveTestCase1 : RemoveTestCase0
    {
        private int count = 0;

        public RemoveTestCase1(ObservableCollection<IItem> items, Stopwatch st) : base(items, st)
        {
        }

        public override void Test(int iterIndex)
        {
            count = lastNonLeafNode.ChildItems.Count;
            while (count > 0)
            {
                lastNonLeafNode.ChildItems.RemoveAt(count-1);
            }
            index = iterIndex;
        }
    }

    public class RemoveTestCase2 : RemoveTestCase0
    {
        private static ScrollViewer scrollViewer = null;

        public RemoveTestCase2(ObservableCollection<IItem> items, Stopwatch st) : base(items, st)
        {
        }

        public override void Startup()
        {
            for (int i = 0; i < 10000; i++)
            {
                _items.Add(new LeafNode(0, 0));
            }

            if (scrollViewer == null)
            {
                scrollViewer = Helpers.FindVisualChild<ScrollViewer>(this.mainWindow.treeView);
            }
        }

        public override void PreTest()
        {
            base.PreTest();
            Helpers.ScrollTo(scrollViewer, Helpers.Pos.Bottom);
        }

        public override void Reset()
        {
            base.Reset();
            Helpers.ScrollTo(scrollViewer, Helpers.Pos.Bottom);
        }   
    }

    public class ReplaceTestCase0 : ItemsChangeBaseTest
    {
        protected static Node lastNonLeafNode = null;

        public ReplaceTestCase0(ObservableCollection<IItem> items, Stopwatch st) : base(items, st)
        {
        }
        
        public override void Startup()
        {
            Node prev = null;
            Node curr = null;

            for (int level = 0; level < nestingLevel; level++)
            {
                curr = new Node(level, 0);
                if (prev == null)
                {
                    _items.Insert(0, curr);
                    prev = curr;
                    continue;
                }
                prev.ChildItems.Add(curr);
                prev = curr;
            }

            for (int i = 0; i < numRecords; i++)
            {
                var node = new LeafNode(i, -1);
                curr.ChildItems.Add(node);
            }

            lastNonLeafNode = curr;
        }   

        public override void Test(int iterIndex)
        {
            for(int i=0;i<numRecords;i++)
            {
                lastNonLeafNode.ChildItems[i] = new LeafNode(i, iterIndex);
            }
        }
    }

    public class MoveTestCase0 : ItemsChangeBaseTest
    {

        protected static List<Node> nodes = new List<Node>();   
        protected static ScrollViewer scrollViewer = null;

        public MoveTestCase0(ObservableCollection<IItem> items, Stopwatch st) : base(items, st)
        {
        }

        public override void PreTest()
        {
            Node node0 = CreateNesting(0, 0);
            AddRecords(node0, 0);
            AddRecords(node0, 0);
            nodes.Add(node0);

            Node node1 = CreateNesting(1, 1);
            AddRecords(node1, 1);
            AddRecords(node1, 1);
            nodes.Add(node1);

            Node node2 = CreateNesting(2, 2);
            AddRecords(node2, 2);
            AddRecords(node2, 2);
            nodes.Add(node2);

            if (scrollViewer == null)
            {
                scrollViewer = Helpers.FindVisualChild<ScrollViewer>(this.mainWindow.treeView);
            }

            scrollViewer.ScrollToVerticalOffset(numRecords*3); // Places us somewhere in the node 1 region
        }

        public override void Test(int iterIndex)
        {
            _items.Move(0, 2);
        }

        public override void Reset()
        {
            _items.Clear();
        } 
    }

    public class MoveTestCase1 : MoveTestCase0
    {
        public MoveTestCase1(ObservableCollection<IItem> items, Stopwatch st) : base(items, st)
        {
        }

        public override void Test(int iterIndex)
        {
            _items.Move(0,1);
        }
    }
}
