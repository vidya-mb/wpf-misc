using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace ItemsCollectionChangePerfTest
{
    internal class Helpers
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

}
