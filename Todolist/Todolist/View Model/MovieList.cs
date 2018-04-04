using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Popups;
using Todolist.Model;

namespace Todolist.View_Model
{
    class MovieList : NotifyChanged
    {
        public Movie _selectedMovie;

        private readonly SingletonMovieList _singleton = SingletonMovieList.GetInstance();

        public ObservableCollection<Movie> Movies { get; set; }

        public RelayCommand AddMovie { get; set; }

        public RelayCommand DeleteMovie { get; set; }

        public RelayCommand UpdateMovie { get; set; }

        //------------------------------------------------------------
        // Constructer
        public  MovieList()
        {
            try
            {
                Movies = new ObservableCollection<Movie>();

                var movieList = _singleton.GetMovieList();

                Movies = movieList;

            }
            catch (Exception e)
            {
              Console.Write(e.Message);
            }
        }


        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set { _selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); }
        }
    }
}
