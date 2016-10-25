using FreshMvvm;
using PokeApp.Interfaces;
using PokeApp.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PokeApp.PageModels
{
    internal class AddLogItemPageModel : FreshBasePageModel
    {
        private readonly IPokeApiService _pokeApi;

        public Pokemon SelectedPokemon { get; set; }

        public DateTime CaughtAt { get; set; }

        public ICommand PickPokemonCommand { get; private set; }

        public ICommand SaveLogEntryCommand { get; private set; }

        public AddLogItemPageModel(IPokeApiService pokeApi)
        {
            PickPokemonCommand = new Command(PickPokemon);
            SaveLogEntryCommand = new Command(SaveLogEntry);

            _pokeApi = pokeApi;

            CaughtAt = DateTime.Now;
        }

        private async void PickPokemon()
        {
            await CoreMethods.PushPageModel<PickPokemonPageModel>(null, true);
        }

        private async void SaveLogEntry()
        {
            await _pokeApi.PostFeedItem(new LogEntry
            {
                CaughtAt = CaughtAt,
                Id = 0,
                PlayerId = 195824080,
                PokemonId = SelectedPokemon.Id
            });

            await CoreMethods.DisplayAlert("Hooray!", "Saved the log entry, sir!", "K THX BYE!");
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            var selectedPokemon = returnedData as Pokemon;

            if (selectedPokemon == null)
                return; // TODO We probably want to handle this better..

            SelectedPokemon = selectedPokemon;
        }
    }
}