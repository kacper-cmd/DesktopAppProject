using Microsoft.EntityFrameworkCore;
using MusicApp.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.BusinessLogic
{
    public class LikedSongsLengthBL
    {
        public DatabaseContext Database {  get; set; }

        public LikedSongsLengthBL()
        {
            Database = new DatabaseContext();
        }

        public int CalculateLikedSongsLength()
        {
            return Database.LikedSongs.Include(item => item.Song)
                                      .Where(item => item.IsActive)
                                      .Sum(item => item.Song.SongDuration);
        }
    }
}
