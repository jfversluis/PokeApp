using FreshMvvm;
using PokeApp.Interfaces;
using PokeApp.Models;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PokeApp.PageModels
{
    internal class PickPokemonPageModel : FreshBasePageModel
    {
        private readonly IPokeApiService _pokeApiService;

        private IEnumerable<Pokemon> _pokemon;

        private string _searchTerm;

        public ObservableCollection<Pokemon> Pokemon { get; set; } = new ObservableCollection<Pokemon>();

        [AlsoNotifyFor(nameof(IsNotLoading))]
        public bool IsLoading { get; set; } = true;

        public bool IsNotLoading
        {
            get { return !IsLoading; }
        }

        public Pokemon SelectedPokemon { get; set; }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;

                SearchPokemon(_searchTerm);
            }
        }

        public ICommand ItemSelectedCommand { get; private set; }

        public PickPokemonPageModel(IPokeApiService pokeApiService)
        {
            _pokeApiService = pokeApiService;

            ItemSelectedCommand = new Command(PokemonSelected);
        }

        private void SearchPokemon(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                PopulatePokemon();

                return;
            }

            PopulatePokemon(_pokemon.Where(p => p.Name.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant())));
        }

        private async void PokemonSelected()
        {
            await CoreMethods.PopPageModel(SelectedPokemon, true);
        }

        public async void PopulatePokemon(IEnumerable<Pokemon> source = null)
        {
            IsLoading = true;

            if (source == null)
                _pokemon = await _pokeApiService.GetPokemonAsync();
            else
                _pokemon = source;

            Pokemon.Clear();

            foreach (var p in _pokemon)
                Pokemon.Add(p);

            IsLoading = false;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            PopulatePokemon();

            IsLoading = false;
        }
    }
}