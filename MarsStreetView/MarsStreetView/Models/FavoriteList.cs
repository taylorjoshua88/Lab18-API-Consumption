using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsStreetView.Models
{
    public class FavoriteList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Favorite> Favorites { get; set; }
    }
}
