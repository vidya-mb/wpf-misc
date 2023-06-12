using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICGPerfAutomated
{
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
