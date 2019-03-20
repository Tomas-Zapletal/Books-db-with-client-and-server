using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
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

namespace BooksWPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public static class CustomCommands
    {
        public static readonly RoutedUICommand CopyAll = new RoutedUICommand
                (
                        "Copy all to clipboard",
                        "CopyAll",
                        typeof(CustomCommands),
                        new InputGestureCollection()
                                {
                                        new KeyGesture(Key.C, ModifierKeys.Alt)
                                }
                );

        //Define more commands here, just like the one above
    }
    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();
        static List<Title> titles = new List<Title>();
        static ViewModel vm;

        public MainWindow()
        {

            InitializeComponent();
            GetData();

        }

        public string SearchText
        {
            get { return txtSearch.Text.ToUpper(); }
        }
        public Title SelectedTitle
        {
            get
            {
                Title st = lvBooks.SelectedItem as Title;
                return st;
            }
        }
        public List<Title> DataSource
        {
            set => lvBooks.ItemsSource = value;
            get { return (List<Title>)lvBooks.ItemsSource; }
        }


        public string SearchByProperty
    {
        get { return cbxSearchBy.SelectedValue.ToString().ToLower(); }
    }
    private async void GetData(string searchExpression = null, string searchByProperty = null)
    {
        //Initialize HTTP Client 
        client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:3466");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = await client.GetAsync("/api/titles");
                response.EnsureSuccessStatusCode(); // Throw on error code. 
                var responseData = await response.Content.ReadAsStringAsync();
                //JsonSerializer j = new JsonSerializer();
                //TextReader r = new StringReader(responseData);
                //JsonTextReader reader = new JsonTextReader(r);
                //j.DefaultValueHandling = DefaultValueHandling.Populate;

                //titles = j.Deserialize<List<Title>>(reader);

                //j.Deserialize()
                //j.Formatting = Formatting.Indented;

                titles = JsonConvert.DeserializeObject<List<Title>>(responseData);
                //titles = JsonConvert.DeserializeObject<List<Title>>(responseData);
                vm = new ViewModel();

                BindtitlesWithVMTitles();

                if (searchExpression != null)
                {
                    if (searchByProperty.Equals("title"))
                    {
                        titles = titles.Where(title => title.BookTitle.StartsWith(searchExpression)).ToList<Title>();
                        vm.Titles.Clear();
                        BindtitlesWithVMTitles();
                    }
                    else
                    {
                        titles = titles.Where(title => title.Id.StartsWith(searchExpression)).ToList<Title>();
                        vm.Titles.Clear();
                        BindtitlesWithVMTitles();
                    }
                }
                vm.SelectedTitle = (Title)lvBooks.SelectedItem;
                lvBooks.ItemsSource = vm.Titles;


            }
            catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

        private  void BindtitlesWithVMTitles()
        {
            foreach (var title in titles)
            {
                vm.Titles.Add(title);
            }
            lvBooks.ItemsSource = vm.Titles;
        }

    //    private  void ApplySearchfilter(string searchExpression, string searchByProperty)
    //{

    //    if (searchExpression != null)
    //    {
    //            GetData();
    //        if (searchByProperty.Equals("title"))
    //        {
    //            titles = titles.Where(title => title.BookTitle.StartsWith(searchExpression)).ToList<Title>();
    //                vm.Titles.Clear();
    //                BindtitlesWithVMTitles();
    //        }
    //        else
    //        {
    //                titles = titles.Where(title => title.Id.StartsWith(searchExpression)).ToList<Title>();
    //                vm.Titles.Clear();
    //                BindtitlesWithVMTitles();
    //        }
    //    }
    //    else
    //    {
    //            GetData();

    //    }
    //        lvBooks.ItemsSource = vm.Titles;

    //}

    private async void btnDelete_Click(object sender, RoutedEventArgs e)
    {

        client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:3466/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var id = txtISBN.Text;

        var url = "api/titles/" + id;

        HttpResponseMessage response = client.DeleteAsync(url).Result;

        if (response.IsSuccessStatusCode)
        {
            GetData();

        }
        else
        {
            MessageBox.Show("Error Code" +
            response.StatusCode + " : Message - " + response.ReasonPhrase);
        }
    }
        New newWindow;
        private void ShowNewWindow(object sender, RoutedEventArgs e)
        {
            newWindow = new New();
            newWindow.Show();
            


        }
        public async void btnAdd_Click(object sender, RoutedEventArgs e)
    {
            
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:3466/");


        client.DefaultRequestHeaders.Accept.Add(
           new MediaTypeWithQualityHeaderValue("application/json"));

            //načte údaje o nově zadané knize
            Title title = e.Source as Title;

        HttpResponseMessage response = new HttpResponseMessage();
        try
        {
            response = await client.PostAsJsonAsync("api/titles", title);

        }
        catch (Exception ex)
        {

            string message = ex.Message;
        }

        if (response.IsSuccessStatusCode)
        {
            MessageBox.Show("titul přidán");

            GetData();
        }
        else
        {
            MessageBox.Show("Error Code" +
            response.StatusCode + " : Message - " + response.ReasonPhrase);
        }
    }
    private async void btnSearch_Click(object sender, RoutedEventArgs e)
    {
        string searchExpression = SearchText;
        string searchByProperty = cbxSearchBy.Text;
        GetData(searchExpression, searchByProperty);
    }

    private void cbxSearchBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //ComboBox cbxSearchBy  = sender as ComboBox;
        //cbxSearchBy.SelectedValue == title
    }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3466/");


            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            Title title = new Title()
            {
                Id = txtISBN.Text,
                BookTitle = txtBookTitle.Text,
                EditionNumber = int.Parse(txtEditionNumber.Text),
                Copyright = txtCopyright.Text
            };
            HttpResponseMessage responseMessage = new HttpResponseMessage(); 
            try
            {
                responseMessage = await client.PutAsJsonAsync("api/titles/" + title.Id, title);

            }
            catch (Exception ex)
            {

                string message = ex.Message;
            }
            if (responseMessage.IsSuccessStatusCode)
            {

                GetData();
            }
        }

        private void btnViewAll_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }
    }
}
