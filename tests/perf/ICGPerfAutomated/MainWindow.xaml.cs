using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.Xml.Serialization;
using System.IO;

namespace ICGPerfAutomated
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string logFile = "C:\\work\\perftest.csv";


        string testCase;
        int iterations,nestingLevel, numRecords;
        
        long addTime, totalTime;
        List<long> AddTimeList, RenderTimeList;

        private ItemsChangeBaseTest test;
        private Stopwatch st;

        private Action renderComplete;
        private Action stopTimer;

        private delegate void TestCase(int nestingLevel, int numRecords);

        public ObservableCollection<IItem> Items
        {
            get => items;
            set => items = value;
        }

        private ObservableCollection<IItem> items;

        public MainWindow()
        {
            InitializeComponent();

            AddTimeList = new List<long>();
            RenderTimeList = new List<long>();

            Items = new ObservableCollection<IItem>();
            st = new Stopwatch();
            this.DataContext = this;

            if(!File.Exists(logFile))
            {
                using (File.Create(logFile)) { }
                File.AppendAllText(logFile, "TestCase,Nesting,Records,Iterations,Mean Add Time,Mean Render Time\n");
            }

            renderComplete = new Action(() =>
            {
                Thread.Sleep(100);
            });

            stopTimer = new Action(() =>
            {
                st.Stop();
            });
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            Dictionary<string, string> dict = new Dictionary<string, string>();

            for(int index=1;index<args.Length;index+=2)
            {
                dict.Add(args[index].Substring(1), args[index+1]);
            }

            string val;
            if(!dict.TryGetValue("type", out val))
            {
                testCase = "AddTestCase0";
                test = TestFactory.CreateTestInstance("AddTestCase0", Items, st);
                //throw new ArgumentException(" Test Type Missing ");
            }
            else
            {
                testCase = val;
                test = TestFactory.CreateTestInstance(testCase, Items, st);
            }
            
            nestingLevel = dict.TryGetValue("nesting", out val) ? int.Parse(val) : 10;
            numRecords = dict.TryGetValue("records", out val) ? int.Parse(val) : 1000;
            iterations = dict.TryGetValue("iterations", out val) ? int.Parse(val) : 1;

            //test.Run(iterations, nestingLevel, numRecords);

            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                RunTest();
                AnalyzeAndLog();
                Application.Current.Shutdown();
            }));

        }

        private void RunTest()
        {
            test.Configure(this, nestingLevel, numRecords);
            test.PreTest();
            WaitRenderComplete();
            for (int i = 0; i < iterations; i++)
            {
                st.Reset();
                st.Start();
                test.Test(i);
                addTime = st.ElapsedMilliseconds;
                this.Dispatcher.Invoke(DispatcherPriority.Loaded, stopTimer);
                totalTime = st.ElapsedMilliseconds;
                test.Reset();
                WaitRenderComplete();
                AddTimeList.Add(addTime);
                RenderTimeList.Add(totalTime - addTime);
            }
        }

        private void AnalyzeAndLog()
        {
            double addTimeMean = AddTimeList.Average();
            double renderTimeMean = RenderTimeList.Average();

            File.AppendAllText(logFile, $"{testCase},{nestingLevel},{numRecords},{iterations},{addTimeMean:N2},{renderTimeMean:N2}\n");
        
        }

        private void WaitRenderComplete()
        {
            this.Dispatcher.Invoke(DispatcherPriority.Loaded, renderComplete);
        }

    }

    #region Action Helpers

    public class ActionHelpers
    {

        public static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T visualChild)
                {
                    return visualChild;
                }
                else
                {
                    var foundChild = FindVisualChild<T>(child);
                    if (foundChild != null)
                        return foundChild;
                }
            }
            return null;
        }

        public enum Pos { Top, Bottom };

        public static void ScrollTo(ScrollViewer scrollViewer, Pos pos)
        {
            //var scrollViewer = FindVisualChild<ScrollViewer>(treeView);
            if (scrollViewer != null)
            {
                switch (pos)
                {
                    case Pos.Top:
                        scrollViewer.ScrollToTop();
                        break;
                    case Pos.Bottom:
                        scrollViewer.ScrollToBottom();
                        break;
                }
            }
        }
    }
    #endregion


    #region Test Classes

    public class TestFactory
    {
        public static ItemsChangeBaseTest CreateTestInstance(string testType, ObservableCollection<IItem> items, Stopwatch st)
        {
            ItemsChangeBaseTest test = null;
            switch(testType)
            {
                case "AddTestCase0":
                    test = new AddTestCase0(items, st);
                    break;
                case "AddTestCase1":
                    test = new AddTestCase1(items, st);
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
            for(int i = 0; i < iterations; i++)
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

        public virtual void PreTest() { }
       
        public virtual void Test(int iterIndex) { }

        public virtual void Reset() { }
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

            for(int level=0; level<nestingLevel; level++)
            {
                curr = new Node(level, iterIndex);
                if(prev == null)
                {
                    _items.Insert(0, curr);
                    prev = curr;
                    continue;
                }
                prev.ChildItems.Add(curr);
                prev = curr;
            }

            for(int i=0; i<numRecords; i++)
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

        public override void PreTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                _items.Add(new LeafNode(0, 0));
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
        public AddTestCase2(ObservableCollection<IItem> items, Stopwatch st) : base(items, st)
        {
        }

        public override void PreTest()
        {
            base.PreTest();

            // now I need scrollviewer here ??
            var scrollViewer = ActionHelpers.FindVisualChild<ScrollViewer>(this.mainWindow.treeView);
            ActionHelpers.ScrollTo(scrollViewer, ActionHelpers.Pos.Bottom);
        }

        public override void Reset()
        {
            base.Reset();
        }
    }

    public class RemoveTestCase0 : ItemsChangeBaseTest
    {

    }

    #endregion



}
