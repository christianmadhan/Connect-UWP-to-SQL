using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todolist.Model
{
    class Movie : NotifyChanged
    {
        private int _id;
        private string _title;
        private int _year;
        private int _runningTimeinMins;
        private int _studioId;

        public Movie(int id, string title, int year, int runningTimeinMins, int studioID)
        {
            _id = id;
            _title = title;
            _year = year;
            _runningTimeinMins = runningTimeinMins;
            _studioId = studioID;
        }

        // Empty Constructer for RelayCommand.
        public Movie() { }

        //----------------------
        // Get and set
        //---------------------

        public int ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(ID));  }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }
        public int Year
        {
            get { return _year; }
            set { _year = value; OnPropertyChanged(nameof(Year)); }
        }

        public int RunningTimeInMins
        {
            get { return _runningTimeinMins; }
            set { _runningTimeinMins = value; OnPropertyChanged(nameof(RunningTimeInMins)); }
        }

        public int StudioID
        {
            get { return _studioId; }
            set { _studioId = value; OnPropertyChanged(nameof(StudioID)); }
        }


    }
}
