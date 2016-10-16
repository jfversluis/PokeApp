using FreshMvvm;
using PokeApp.Interfaces;
using PokeApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PokeApp.PageModels
{
    internal class MainPageModel : FreshBasePageModel
    {
        private readonly IPokeApiService _pokeApiService;

        public ObservableCollection<LogItem> LogItems { get; set; } = new ObservableCollection<LogItem>();
        public ICommand ItemSelectedCommand { get; private set; }

        public LogItem SelectedItem { get; set; }

        public MainPageModel(IPokeApiService pokeApiService)
        {
            _pokeApiService = pokeApiService;

            ItemSelectedCommand = new Command(ItemSelected);
        }

        public override async void Init(object initData)
        {
            base.Init(initData);

            var results = await _pokeApiService.GetFeedAsync();

            LogItems.Clear();

            foreach (var logitem in results)
                LogItems.Add(logitem);
        }

        private void ItemSelected()
        {
            // Open the claim page.
            if (SelectedItem != null)
            {
                CoreMethods.PushPageModel<LogItemDetailPageModel>(SelectedItem);

                SelectedItem = null;
            }
        }
    }
}