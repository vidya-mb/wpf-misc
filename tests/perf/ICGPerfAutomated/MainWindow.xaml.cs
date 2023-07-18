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

namespace ItemsCollectionChangePerfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string logFile = null;

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

            renderComplete = new Action(() =>
            {
                Thread.Sleep(100);
            });

            stopTimer = new Action(() =>
            {
                st.Stop();
            });
        }

        private void RunTestButtonClick(object sender, RoutedEventArgs e)
        {
            RunTest();
            AnalyzeAndLog();
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
            if(dict.TryGetValue("debug", out val))
            {
                while(!Debugger.IsAttached) { Thread.Sleep(100); }
                Debugger.Break();
            }

            if(!dict.TryGetValue("type", out val))
            {
                testCase = "AddTestCase0";
                test = TestFactory.CreateTestInstance("AddTestCase0", Items, st);
            }
            else
            {
                testCase = val;
                test = TestFactory.CreateTestInstance(testCase, Items, st);
            }
            
            nestingLevel = dict.TryGetValue("nesting", out val) ? int.Parse(val) : 10;
            numRecords = dict.TryGetValue("records", out val) ? int.Parse(val) : 1000;
            iterations = dict.TryGetValue("iterations", out val) ? int.Parse(val) : 1;

            string path = Directory.GetCurrentDirectory();
            logFile = System.IO.Path.Combine(path, $"perf-{testCase}-{numRecords}.csv");

            if (!File.Exists(logFile))
            {
                using (File.Create(logFile)) { }
                File.AppendAllText(logFile, "TestCase\tNesting\tRecords\tIterations\tMean Add Time\tMean Render Time\n");
            }


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
            test.Startup();
            WaitRenderComplete(renderComplete);
            for (int i = 0; i < iterations; i++)
            {
                test.PreTest();
                WaitRenderComplete(renderComplete);
                st.Reset();
                st.Start();
                test.Test(i);
                addTime = st.ElapsedMilliseconds;
                WaitRenderComplete(stopTimer);
                totalTime = st.ElapsedMilliseconds;
                test.Reset();
                WaitRenderComplete(renderComplete);
                AddTimeList.Add(addTime);
                RenderTimeList.Add(totalTime - addTime);
            }
            test.Clean();
        }

        private void AnalyzeAndLog()
        {
            double addTimeMean = AddTimeList.Average();
            double renderTimeMean = RenderTimeList.Average();

            File.AppendAllText(logFile, $"{testCase}\t{nestingLevel}\t{numRecords}\t{iterations}\t{addTimeMean:N2}\t{renderTimeMean:N2}\n");
        
        }

        private void WaitRenderComplete(Action action)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Loaded, action);
        }

    }
}
