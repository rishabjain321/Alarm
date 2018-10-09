using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// INotifyPropertyChanged
using System.ComponentModel;

using TimeDataDLL;

namespace FinalProject
{
    public partial class Model : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Initialize() {
            TimeSpan curTime = DateTime.Now.TimeOfDay;
            Sec = curTime.Seconds;
            Hr = curTime.Hours;
            Min = curTime.Minutes;
        }
        private int _Hr;
        public int Hr
        {
            get { return _Hr; }
            set
            {
                _Hr = value;
                OnPropertyChanged("Hr");
            }
        }
        private int _Min;
        public int Min
        {
            get { return _Min; }
            set
            {
                _Min = value;
                OnPropertyChanged("Min");
            }
        }
        private int _Sec;
        public int Sec
        {
            get { return _Sec; }
            set
            {
                _Sec = value;
                OnPropertyChanged("Sec");
            }
        }
        private String _Status;
        public String Status
        {
            get { return _Status; }
            set { _Status = value;
                OnPropertyChanged("Status");
            }
        }


        public void ValidTime(bool isAlarm) {
            DateTime today = DateTime.Today;
            if ((Hr < 0) || (Min < 0) || (Sec < 0) ||(Hr > 23) || (Min > 59) || (Sec > 59))
            {
                Status = "Invalid Entry. Time Cannot be Set.";
            }
            else if ((Hr > 12))
            {
                //Hr = Hr - 12;
                Status = today.Date.Year + "-" + today.Date.Month+ "-" + today.Date.Day + " " + (Hr-12) + ":" + Min + ":" + Sec + " PM";
                if (isAlarm)
                    Status += " Alarm update sent successfully.";
                else
                    Status += " Time of day update sent successfully.";

            }
            else
            {
                Status = today.Date.Year + "-" + today.Date.Month + "-" + today.Date.Day + " " + Hr + ":" + Min + ":" + Sec + " AM";
                if (isAlarm)
                    Status += " Alarm update sent successfully.";
                else
                    Status += " Time of day update sent successfully.";

            }

            SendMessage(new TimeData.StructTimeData(Hr,Min, Sec, isAlarm));
        }

        public void NowTime() {
           TimeSpan curTime = DateTime.Now.TimeOfDay;
            Sec = curTime.Seconds;
            Hr = curTime.Hours;
            Min = curTime.Minutes;
            ValidTime(false);
        }

        public void SetAlarm() {

            ValidTime(true);
           
        }

    }
}
