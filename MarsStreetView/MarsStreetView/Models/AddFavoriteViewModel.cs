using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsStreetView.Models
{
    public class AddFavoriteViewModel
    {
        public bool Existing { get; set; }
        public Favorite Favorite { get; set; }
        public List<FavoriteList> Lists { get; set; }
    }
}
