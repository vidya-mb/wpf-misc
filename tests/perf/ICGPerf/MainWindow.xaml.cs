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
using System.Windows.Threading;

namespace ICGPerf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int clickCount = 0;
        int nestingLevel;
        int numRecords;
        long timeAddItems;
        long totalTime;

        private Stopwatch stopwatch;

        public ObservableCollection<IItem> Items
        {
            get => items;
            set => items = value;
        }

        private ObservableCollection<IItem> items;

        public MainWindow()
        {
            InitializeComponent();
            nestingLevel = 20;
            numRecords = 10000;
            Items = new ObservableCollection<IItem>();
            stopwatch = new Stopwatch();
            this.DataContext = this;
        }

        //private void CollapseAll()
        //{
        //    treeView.Visibility = Visibility.Collapsed;
        //    //foreach (TreeViewItem node in treeView.Items)
        //    //{
        //    //    node.IsExpanded = false;
        //    //}

        //    this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
        //        new Action(() =>
        //        {
        //            Debug.WriteLine("Collapsed All !!");
        //        }));
        //}

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

        private void ScrollToBottomButton_Click(object sender, RoutedEventArgs e)
        {

            var scrollViewer = FindVisualChild<ScrollViewer>(treeView);
            
            if(scrollViewer != null)
            {
                scrollViewer.ScrollToBottom();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Items.Clear();
            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Loaded,
                new Action(() =>
                {
                    Debug.WriteLine("Render events completed !!");
                }));
        }

        private void Test1Button_Click(object sender, RoutedEventArgs e)
        {
            clickCount++;
            nestingLevel = int.Parse(nestingLevelTextBox.Text);
            numRecords = int.Parse(numRecordsTextBox.Text);
            stopwatch.Reset();
            stopwatch.Start();
            AddItemsTest();
            //stopwatch.Stop();
            //wpfTextBlock.Text = ts.TotalMilliseconds.ToString();
            //wpfTextBlock.Text = wpfTextBlock.Text + "( " + stopwatch.ElapsedMilliseconds.ToString() + " ms +";
            //Debug.WriteLine("Add Items Time : " + stopwatch.ElapsedMilliseconds.ToString() + " ms");
            timeAddItems = stopwatch.ElapsedMilliseconds;

            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Loaded,
                  new Action(() =>
                  {
                      stopwatch.Stop();
                      totalTime = stopwatch.ElapsedMilliseconds;
                      Debug.WriteLine($"{nestingLevel.ToString()},{numRecords.ToString()},{timeAddItems.ToString()},{(totalTime - timeAddItems).ToString()},{totalTime.ToString()}");
                      //Debug.WriteLine("Total Time ( include render ) : " + stopwatch.ElapsedMilliseconds.ToString() + " ms");
                      //wpfTextBlock.Text = wpfTextBlock.Text + stopwatch.ElapsedMilliseconds.ToString() + " ms ) , ";
                      //CollapseAll();
                  }));
        }

        private void Test2Button_Click(object sender, RoutedEventArgs e)
        {
            clickCount++;
            nestingLevel = int.Parse(nestingLevelTextBox.Text);
            numRecords = int.Parse(numRecordsTextBox.Text);

            stopwatch.Reset();
            stopwatch.Start();
            AddItemsTest2();
            //stopwatch.Stop();
            //wpfTextBlock.Text = ts.TotalMilliseconds.ToString();
            //wpfTextBlock.Text = wpfTextBlock.Text + "( " + stopwatch.ElapsedMilliseconds.ToString() + " ms +";
            //Debug.WriteLine("Add Items Time : " + stopwatch.ElapsedMilliseconds.ToString() + " ms");
            timeAddItems = stopwatch.ElapsedMilliseconds;

            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Loaded,
                  new Action(() =>
                  {
                      stopwatch.Stop();
                      totalTime = stopwatch.ElapsedMilliseconds;
                      Debug.WriteLine($"{nestingLevel.ToString()},{numRecords.ToString()},{timeAddItems.ToString()},{(totalTime - timeAddItems).ToString()},{totalTime.ToString()}");
                      //Debug.WriteLine("Total Time ( include render ) : " + stopwatch.ElapsedMilliseconds.ToString() + " ms");
                      //wpfTextBlock.Text = wpfTextBlock.Text + stopwatch.ElapsedMilliseconds.ToString() + " ms ) , ";
                      //CollapseAll();
                  }));
        }

        private void AddItemsTest()
        {
            Node prev = null, curr = null;
            for (int level = 0; level < nestingLevel; level++)
            {
                curr = new Node(level.ToString());
                if (prev != null)
                {
                    prev.ChildItems.Add(curr);
                }
                else
                {
                    Items.Add(curr);
                }
                prev = curr;
            }

            for (int i = 0; i < numRecords; i++)
            {
                var node = new LeafNode(i.ToString());
                curr.ChildItems.Add(node);
            }
        }

        private void AddItemsTest2()
        {
            Node prev = null, curr = null;
            for (int level = 0; level < nestingLevel; level++)
            {
                curr = new Node(clickCount.ToString() + "," + level.ToString());
                if (prev != null)
                {
                    prev.ChildItems.Add(curr);
                }
                else
                {
                    Items.Insert(0,curr);
                }
                prev = curr;
            }
        
            for (int i = 0; i < numRecords/2; i++)
            {
                var node = new LeafNode(clickCount.ToString() + "," + i.ToString());
                curr.ChildItems.Add(node);
            }


            for (int i = 0; i < numRecords / 2; i++)
            {
                var node = new LeafNode(clickCount.ToString() + "," + i.ToString());
                curr.ChildItems.Insert(0, node);
            }
        }
    }

    public interface IItem
    {
        string Number { get; set; }
    }

    public class Node : INotifyPropertyChanged, IItem
    {
        public Node(string number)
        {
            num = number;
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
        public LeafNode(string number)
        {
            num = number;
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
}
