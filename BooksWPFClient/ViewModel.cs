using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWPFClient
{
    class ViewModel : ObservableCollection<Title>
    {
        public ObservableCollection<Title> Titles
        {
            get; set;
        }

        public Title SelectedTitle
        {
            get; set;
        }
        public ViewModel()
        {
            Titles = new ObservableCollection<Title>();
        }
    }
}
