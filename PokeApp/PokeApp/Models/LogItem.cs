using System;

namespace PokeApp.Models
{
#warning Don't try this at home! Share your models between projects

    public class LogItem
    {
        public int Id { get; set; }
        public DateTime CaughtAt { get; set; }
        public Player CaughtBy { get; set; }
        public Pokemon Pokemon { get; set; }

        public string Description
        {
            get { return $"{CaughtAt.ToString("dd-MM-yyyy hh:mm")} by: {CaughtBy?.Name}"; }
        }
    }
}