using Humanizer;
using System;

namespace PokeApp.Models
{
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

        public string HumanDescription
        {
            get { return $"{CaughtAt.Humanize()} by: {CaughtBy?.Name}"; }
        }
    }
}