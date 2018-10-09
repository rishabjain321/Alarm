using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// INotifyPropertyChanged
using System.ComponentModel;

// observable collections
using System.Collections.ObjectModel;

// Timer.Timer
using System.Timers;

using System.Windows;

namespace Alarm_Front_End
{
    partial class model : INotifyPropertyChanged
    {
        
        public class AlarmEventArgs : EventArgs
        {
            public bool alarm;
            public AlarmEventArgs(bool set)
            {
                alarm = set;
            }
        }

        public delegate void AlarmEventHandler(object sender, AlarmEventArgs e);

        public event AlarmEventHandler AlarmEvent;
        public AlarmEventHandler handler;


        public ObservableCollection<SevenSegment> Segments;

        System.Timers.Timer secondsTimer;
        System.Timers.Timer alarmTimer;

        int curSeconds, curMinutes, curHours;


        int alarmSeconds, alarmMinutes, alarmHours;
        bool alarmSet;
        bool alarmTomorrow;


        public Visibility _alarmVisible;
        public Visibility alarmVisible
        {
            get { return _alarmVisible; }
            set { _alarmVisible = value; OnPropertyChanged("alarmVisible"); }
        }
        

        public bool initialize()
        {
            TimeSpan curTime = DateTime.Now.TimeOfDay;

            curSeconds = curTime.Seconds;
            curMinutes = curTime.Minutes;
            curHours = curTime.Hours;

            alarmSeconds = 0;
            alarmMinutes = 0;
            alarmHours = 0;
            alarmSet = false;
            alarmTomorrow = false;

            alarmVisible = Visibility.Hidden;

            Segments = new ObservableCollection<SevenSegment>();

            Segments.Clear(); // make sure segments are clear

            // create and set segments
            Segments.Add(new SevenSegment(260, 55, curSeconds % 10 )); // seconds
            Segments.Add(new SevenSegment(215,55, curSeconds/10 ));

            Segments.Add(new SevenSegment(155, 55, curMinutes % 10)); // minutes
            Segments.Add(new SevenSegment(110, 55, curMinutes / 10));
            
            Segments.Add(new SevenSegment(55, 55, curHours % 10)); // hours
            Segments.Add(new SevenSegment(10, 55, curHours / 10));
            
            //setAlarm(curSeconds + 5, curMinutes, curHours);

            if(!initalizeNetwork())
            {
                // error
                MessageBox.Show("Error creating sockets", "Network Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return(false);
            }
            handler = AlarmEvent;

            handler += new AlarmEventHandler(turnOnAlarm); // set up alarm event

            // create timers
            secondsTimer = new System.Timers.Timer(1000);
            secondsTimer.Elapsed += new ElapsedEventHandler(incrementTime);
            secondsTimer.Start();

            alarmTimer = new System.Timers.Timer(10000);
            alarmTimer.Elapsed += new ElapsedEventHandler(turnOffAlarm);
            return (true);
        }

        public void cleanup()
        {
            networkStop();
        }
        
        private void setSegments()
        {
            Segments[0].setSegments(curSeconds % 10); // set segments
            Segments[1].setSegments(curSeconds / 10);
            Segments[2].setSegments(curMinutes % 10);
            Segments[3].setSegments(curMinutes / 10);
            Segments[4].setSegments(curHours % 10);
            Segments[5].setSegments(curHours / 10);
        }

        private void incrementTime(object source, ElapsedEventArgs Args)
        {
            // increment seconds, and increment throut on overflow
            curSeconds++;
            if(curSeconds >= 60)
            {
                curSeconds = 0;
                curMinutes++;
                if(curMinutes >= 60)
                {
                    curMinutes = 0;
                    curHours++;
                    if(curHours >= 24)
                    {
                        curHours = 0;
                    }
                }
            }
            
            setSegments();
            
            if (alarmSet)
            {
                // check alaram if alarm time is greater or equal to curTime
                if (compareTime(curSeconds, curMinutes, curHours, alarmSeconds, alarmMinutes, alarmHours) <= 0)
                {
                    if (!alarmTomorrow) // don't execute alarm if it is supposed to ring tomorrow
                    {
                        // supposed to execute today
                        if (handler != null) // send event
                            handler(this, new AlarmEventArgs(true));
                    }
                }
            }
            return;
        }
        
        private bool setTime(int hours, int minutes, int seconds)
        {
            if (seconds < 0 || seconds >= 60)
                return (false);
            if (minutes < 0 || minutes >= 60)
                return (false);
            if (hours < 0 || hours >= 24)
                return (false);

            curSeconds = seconds;
            curMinutes = minutes;
            curHours = hours;

            setSegments();

            return (true);
        }

        private bool setAlarm(int seconds, int minutes, int hours)
        {
            if (seconds < 0 || seconds >= 60)
                return (false);
            if (minutes < 0 || minutes >= 60)
                return (false);
            if (hours < 0 || hours >= 24)
                return (false);


            alarmSeconds = seconds;
            alarmMinutes = minutes;
            alarmHours = hours;


            // check if alarm is suposed to be tomorrow
            if (compareTime(alarmSeconds, alarmMinutes, alarmHours, curSeconds, curMinutes, curHours) >= 0)
            {
                alarmSet = true;
                alarmTomorrow = true;
            }
            else
            {
                alarmSet = true;
                alarmTomorrow = false;
            }

            return (true);

        }

        private void turnOffAlarm(object source, ElapsedEventArgs Args)
        {
            alarmVisible = Visibility.Hidden;
        }

        public void turnOnAlarm(object sender, AlarmEventArgs e)
        {
            alarmVisible = Visibility.Visible;
            alarmSet = false; // stop alarm
            alarmTimer.Stop();
            alarmTimer.Start();
        }

        /*private bool checkAlarm()
        {
            if()
        }*/

        // returns -1 if time1 is greater
        // returns 1 if time2 is greater
        // returns 0 if equal
        private int compareTime(int sec1, int min1, int hour1, int sec2, int min2, int hour2)
        {
            if(hour1 > hour2)
                return (-1);
            if(hour1 < hour2)
                return (1);
            
            if (min1 > min2)
                return (-1);
            if (min1 < min2)
                return (1);

            if (sec1 > sec2)
                return (-1);
            if (sec1 < sec2)
                return (1);

            return (0);
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
