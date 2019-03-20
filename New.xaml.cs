using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BooksWPFClient
{
    /// <summary>
    /// Interaction logic for New.xaml
    /// </summary>
    public partial class New : Window
    {
        public New()
        {
            MainWindow wnd = new MainWindow();
            InitializeComponent();
            btnNewNew.Click += NewTitle;
            btnNewNew.Click += wnd.btnAdd_Click;
        }
        public void NewTitle(object obj, RoutedEventArgs e)
        {
                Title title = new Title()
                {
                    Id = TxtISBNNew,
                    BookTitle = TxtBookTitleNew,
                    EditionNumber = int.Parse(TxtEditionNumberNew),
                    Copyright = TxtCopyrightNew
                };

            e.Source = title;


        }
        public  string TxtISBNNew
        {
            get { return txtISBNNew.Text; }
        }
        public string TxtBookTitleNew
        {
            get { return txtBookTitleNew.Text; }
        }
        public string TxtEditionNumberNew
        {
            get { return txtEditionNumberNew.Text; }
        }
        public string TxtCopyrightNew
        {
            get { return txtCopyrightNew.Text; }
        }
    }
}
