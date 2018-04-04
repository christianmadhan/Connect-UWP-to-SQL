using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Popups;
using Newtonsoft.Json;
using Todolist.Model;

namespace Todolist.View_Model
{
    class MovieList : NotifyChanged
    {
        private const string ServerUrl = "http://localhost:62394/"; 

        public Movie _selectedMovie;

        private readonly SingletonMovieList _singleton = SingletonMovieList.GetInstance();

        public ObservableCollection<Movie> Movies { get; set; }

        public RelayCommand AddMovie { get; set; }

        public RelayCommand DeleteMovie { get; set; }

        public RelayCommand UpdateMovie { get; set; }

        public Movie AddNewMovie { get; set; }

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

            AddMovie = new RelayCommand(DoAddMovie);
            DeleteMovie = new RelayCommand(DoDeleteMovie);
            
            AddNewMovie = new Movie();

        }

        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set { _selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); }
        }

        public void DoAddMovie()
        {
            try
            {
                PostMovie("api/Movies/", AddNewMovie);
                _singleton.GetMovieList().Add(AddNewMovie);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public  void DoDeleteMovie()
        {
            try
            {
                  DeleteMovieAsync("api/Movies/" + SelectedMovie.ID);
                _singleton.GetMovieList().Remove(SelectedMovie);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



        // Http DeleteMovie Async Method

        public static async Task<string> DeleteMovieAsync(string url)
        {
            HttpClientHandler handler = new HttpClientHandler() {UseDefaultCredentials = true};
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage responseMessage = await client.DeleteAsync(url);
                    if (responseMessage.IsSuccessStatusCode) await responseMessage.Content.ReadAsStringAsync();
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        // Http Post/Add Movie Method.
        public static async Task<string> PostMovie(string url, Movie objectToPost)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var serializedString = JsonConvert.SerializeObject(objectToPost);
                    StringContent content = new StringContent(serializedString, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = await client.PostAsync(url, content);
                    if (responseMessage.IsSuccessStatusCode) return await responseMessage.Content.ReadAsStringAsync();
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
    }
}
