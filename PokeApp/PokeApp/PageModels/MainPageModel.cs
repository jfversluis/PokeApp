using FreshMvvm;
using PokeApp.Interfaces;
using PokeApp.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PokeApp.PageModels
{
    internal class MainPageModel : FreshBasePageModel
    {
        private readonly IPokeApiService _pokeApiService;

        public ObservableCollection<LogItem> LogItems { get; set; } = new ObservableCollection<LogItem>();
        public ICommand ItemSelectedCommand { get; private set; }
        public ICommand NewFeedItemCommand { get; private set; }
        public ICommand FeedRefreshCommand { get; private set; }

        public bool IsLoading { get; set; }

        public LogItem SelectedItem { get; set; }

        public MainPageModel(IPokeApiService pokeApiService)
        {
            _pokeApiService = pokeApiService;

            ItemSelectedCommand = new Command(ItemSelected);
            NewFeedItemCommand = new Command(AddLogItem);
            FeedRefreshCommand = new Command(RefreshList);
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
            if (SelectedItem != null)
            {
                CoreMethods.PushPageModel<LogItemDetailPageModel>(SelectedItem);

                SelectedItem = null;
            }
        }

        private void AddLogItem()
        {
            CoreMethods.PushPageModel<AddLogItemPageModel>();
        }

        private async void RefreshList()
        {
            var results = await _pokeApiService.GetFeedAsync(LogItems.Select(li => li.Id).Max(), 1);

            foreach (var logitem in results)
                LogItems.Insert(0, logitem);

            IsLoading = false;
        }
    }
}