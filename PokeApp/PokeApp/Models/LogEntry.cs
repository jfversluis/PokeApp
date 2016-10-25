using System;

namespace PokeApp.Models
{
    internal class LogEntry
    {
        public int Id { get; set; }
        public DateTime CaughtAt { get; set; } = DateTime.Now;
        public int PokemonId { get; set; }
        public int PlayerId { get; set; }
    }
}