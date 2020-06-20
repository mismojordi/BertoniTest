using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BertoniTest.Models
{
    public class AlbumModel
    {
        public IEnumerable<SelectListItem> Albums { get; set; }

        public int Album { get; set; }
    }
}
