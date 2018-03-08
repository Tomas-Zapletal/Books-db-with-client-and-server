using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWPFClient
{
    public class Title : INotifyPropertyChanged
    {
        private int editionNumber;
        private string id;
        private string bookTitle;
        private string copyright;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                 OnPropertyChanged("Id"); 
            }
        }

        public string BookTitle
        {
            get { return bookTitle; }
            set
            {
                bookTitle = value;
                 OnPropertyChanged("BookTitle"); 
            }
        }
        public int EditionNumber
        {
            get { return editionNumber; }
            set
            {
                editionNumber = value;
                 OnPropertyChanged("EditionNumber"); 
            }
        }
        public string Copyright
        {
            get { return copyright; }
            set
            {
                copyright = value;
                OnPropertyChanged("Copyright"); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged; if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));

            }
        }

    }

}
