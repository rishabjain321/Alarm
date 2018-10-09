using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// INotifyPropertyChanged
using System.ComponentModel;

using System.Windows;


// Segements layout
//
//  --a--
// |     |
// f     b
// |     |
//  --g--
// |     |
// e     c
// |     |
//  --d-- 

namespace Alarm_Front_End
{
    public partial class SevenSegment : INotifyPropertyChanged
    {
        

        public SevenSegment(int number = 0)
        {
            setSegments(number);
            left = 0;
            top = 0;

        }

        public SevenSegment(double Left, double Top, int number = 0)
        {
            setSegments(number);
            left = Left;
            top = Top;
        }


        private double _left;
        public double left
        {
            get { return _left; }
            set { _left = value; OnPropertyChanged("left"); }
        }

        private double _top;
        public double top
        {
            get { return _top; }
            set { _top = value; OnPropertyChanged("top"); }
        }


        private Visibility _aVisible;
        public Visibility aVisible
        {
            get { return _aVisible; }
            set {_aVisible = value; OnPropertyChanged("aVisible");}
        }

        private Visibility _bVisible;
        public Visibility bVisible
        {
            get { return _bVisible; }
            set { _bVisible = value; OnPropertyChanged("bVisible"); }
        }

        private Visibility _cVisible;
        public Visibility cVisible
        {
            get { return _cVisible; }
            set { _cVisible = value; OnPropertyChanged("cVisible"); }
        }

        private Visibility _dVisible;
        public Visibility dVisible
        {
            get { return _dVisible; }
            set { _dVisible = value; OnPropertyChanged("dVisible"); }
        }

        private Visibility _eVisible;
        public Visibility eVisible
        {
            get { return _eVisible; }
            set { _eVisible = value; OnPropertyChanged("eVisible"); }
        }

        private Visibility _fVisible;
        public Visibility fVisible
        {
            get { return _fVisible; }
            set { _fVisible = value; OnPropertyChanged("fVisible"); }
        }

        private Visibility _gVisible;
        public Visibility gVisible
        {
            get { return _gVisible; }
            set { _gVisible = value; OnPropertyChanged("gVisible"); }
        }


        public void setSegments(int number)
        {
            switch(number)
            {
                case(0):
                    aVisible = Visibility.Visible;
                    bVisible = Visibility.Visible;
                    cVisible = Visibility.Visible;
                    dVisible = Visibility.Visible;
                    eVisible = Visibility.Visible;
                    fVisible = Visibility.Visible;
                    gVisible = Visibility.Hidden;
                    break;
                case (1):
                    aVisible = Visibility.Hidden;
                    bVisible = Visibility.Visible;
                    cVisible = Visibility.Visible;
                    dVisible = Visibility.Hidden;
                    eVisible = Visibility.Hidden;
                    fVisible = Visibility.Hidden;
                    gVisible = Visibility.Hidden;
                    break;
                case (2):
                    aVisible = Visibility.Visible;
                    bVisible = Visibility.Visible;
                    cVisible = Visibility.Hidden;
                    dVisible = Visibility.Visible;
                    eVisible = Visibility.Visible;
                    fVisible = Visibility.Hidden;
                    gVisible = Visibility.Visible;
                    break;
                case (3):
                    aVisible = Visibility.Visible;
                    bVisible = Visibility.Visible;
                    cVisible = Visibility.Visible;
                    dVisible = Visibility.Visible;
                    eVisible = Visibility.Hidden;
                    fVisible = Visibility.Hidden;
                    gVisible = Visibility.Visible;
                    break;
                case (4):
                    aVisible = Visibility.Hidden;
                    bVisible = Visibility.Visible;
                    cVisible = Visibility.Visible;
                    dVisible = Visibility.Hidden;
                    eVisible = Visibility.Hidden;
                    fVisible = Visibility.Visible;
                    gVisible = Visibility.Visible;
                    break;
                case (5):
                    aVisible = Visibility.Visible;
                    bVisible = Visibility.Hidden;
                    cVisible = Visibility.Visible;
                    dVisible = Visibility.Visible;
                    eVisible = Visibility.Hidden;
                    fVisible = Visibility.Visible;
                    gVisible = Visibility.Visible;
                    break;
                case (6):
                    aVisible = Visibility.Visible;
                    bVisible = Visibility.Hidden;
                    cVisible = Visibility.Visible;
                    dVisible = Visibility.Visible;
                    eVisible = Visibility.Visible;
                    fVisible = Visibility.Visible;
                    gVisible = Visibility.Visible;
                    break;
                case (7):
                    aVisible = Visibility.Visible;
                    bVisible = Visibility.Visible;
                    cVisible = Visibility.Visible;
                    dVisible = Visibility.Hidden;
                    eVisible = Visibility.Hidden;
                    fVisible = Visibility.Hidden;
                    gVisible = Visibility.Hidden;
                    break;
                case (8):
                    aVisible = Visibility.Visible;
                    bVisible = Visibility.Visible;
                    cVisible = Visibility.Visible;
                    dVisible = Visibility.Visible;
                    eVisible = Visibility.Visible;
                    fVisible = Visibility.Visible;
                    gVisible = Visibility.Visible;
                    break;
                case (9):
                    aVisible = Visibility.Visible;
                    bVisible = Visibility.Visible;
                    cVisible = Visibility.Visible;
                    dVisible = Visibility.Visible;
                    eVisible = Visibility.Hidden;
                    fVisible = Visibility.Visible;
                    gVisible = Visibility.Visible;
                    break;
            }
        }

        #region Data Binding Stuff

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
