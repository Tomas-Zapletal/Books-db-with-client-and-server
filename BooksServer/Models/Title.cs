using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BooksServer.Models
{
    public class Title
    {


        [Column("ISBN")]
        public string Id
        {
            get; set;
        }
        public string BookTitle
        {
            get; set;
        }
        public int EditionNumber
        {
            get; set;
        }
        public string Copyright
        {
            get; set;
        }
    }
}


