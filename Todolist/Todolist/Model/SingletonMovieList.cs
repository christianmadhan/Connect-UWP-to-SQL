using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;
using Newtonsoft.Json;

namespace Todolist.Model
{
    class SingletonMovieList
    {
        private const string ServerUrl = "http://localhost:62394/";
        // The global list of movies
        public static ObservableCollection<Movie> ListOfMovies;

        // Get the instances of the singleton movie class
        private static SingletonMovieList Instance { get; set; }

        // Private constructer for singleton
        private SingletonMovieList()
        {
            LoadIntoMovieList();
        }

        public static string GetStaticMovieList(string url)
        {
            HttpClientHandler handler = new HttpClientHandler() {UseDefaultCredentials = true};
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public static void LoadIntoMovieList()
        {
            try
            {
                var movies = GetStaticMovieList("api/Movies/");
                ListOfMovies = JsonConvert.DeserializeObject<ObservableCollection<Movie>>(movies);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------

        public static SingletonMovieList GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SingletonMovieList();
            }
            return Instance;
        }

        public void SetMovieList(ObservableCollection<Movie> listMovie)
        {
            ListOfMovies = listMovie;
        }

        public ObservableCollection<Movie> GetMovieList()
        {
            return ListOfMovies;
        }

    }
}
