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
            
            nestingLevel = dict.TryGetValue("nesting", out val) ? int.Parse(val) : 20;
            numRecords = dict.TryGetValue("records", out val) ? int.Parse(val) : 10000;
            iterations = dict.TryGetValue("iterations", out val) ? int.Parse(val) : 20;

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
            test.Configure(nestingLevel, numRecords);
            test.PreTest();
            for (int i = 0; i < iterations; i++)
            {
                st.Reset();
                st.Start();
                test.Test(i);
                addTime = st.ElapsedMilliseconds;
                this.Dispatcher.Invoke(DispatcherPriority.Loaded, stopTimer);
                totalTime = st.ElapsedMilliseconds;
                test.Reset();
                this.Dispatcher.Invoke(DispatcherPriority.Loaded, renderComplete);
                AddTimeList.Add(addTime);
                RenderTimeList.Add(totalTime - addTime);
            }
        }

        private void AnalyzeAndLog()
        {
            double addTimeMean = AddTimeList.Average();
            double renderTimeMean = RenderTimeList.Average();

            File.AppendAllText(logFile, $"{testCase},{nestingLevel},{numRecords},{iterations},{addTimeMean:N2},{renderTimeMean:N2}\n");
            //File.AppendAllLines(logFile, new List<string>
            //{
            //    $"Test Case : {testCase}\t\tNesting Level : {nestingLevel}\tRecords : {numRecords}\tIterations : {iterations}",
            //    $"Mean Addition Time : {addTimeMean}\t\t\tMean Render Time : {renderTimeMean}"
            //});
        }

        //private void RunTestOnce(int testCase, int nestingLevel, int numRecords)
        //{
        //    st.Reset();
        //    st.Start();

        //    ItemsChange(testCase, nestingLevel, numRecords);

        //    addTime = st.ElapsedMilliseconds;
        //    this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, renderComplete);
        //    Console.WriteLine("End Test !!");
        //}

        //private void ItemsChange(int testCase, int nestingLevel, int numRecords)
        //{

        //}

        #region Action Helpers


        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
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

        private enum Pos { Top, Bottom };

        private void ScrollTo(Pos pos)
        {
            var scrollViewer = FindVisualChild<ScrollViewer>(treeView);
            if (scrollViewer != null)
            {
                switch(pos)
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

        #endregion

    }


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
                    break;
            }
            return test;
        }
    }

    public class ItemsChangeBaseTest
    {
        protected ObservableCollection<IItem> _items;

        protected int nestingLevel;
        protected int numRecords;

        public ItemsChangeBaseTest(ObservableCollection<IItem> items, Stopwatch st)
        {
            _items = items;
        }

        public virtual void Run(int iterations, int nestingLevel, int numRecords)
        {
            PreTest();
            for(int i = 0; i < iterations; i++)
            {
                Test(i);
            }
        }

        public virtual void Configure(int nestingLevel, int numRecords)
        {
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
                    _items.Add(curr);
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

    #endregion


    #region View Model

    public interface IItem
    {
        string Number { get; set; }
    }

    public class Node : INotifyPropertyChanged, IItem
    {
        public Node(int number, int iter)
        {
            num = $"{iter.ToString()}-{number.ToString()}";
            childItems = new ObservableCollection<IItem>();
        }

        private string num;
        public string Number
        {
            get => num;
            set
            {
                num = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<IItem> childItems;
        public ObservableCollection<IItem> ChildItems
        {
            get => childItems;
            set => childItems = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class LeafNode : INotifyPropertyChanged, IItem
    {
        public LeafNode(int number, int click)
        {
            num = $"{click.ToString()}-{number.ToString()}";
        }

        private string num;
        public string Number
        {
            get => num;
            set
            {
                num = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    #endregion
}
