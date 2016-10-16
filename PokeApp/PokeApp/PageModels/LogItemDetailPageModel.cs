using FreshMvvm;
using PokeApp.Models;

namespace PokeApp.PageModels
{
    internal class LogItemDetailPageModel : FreshBasePageModel
    {
        public LogItem CurrentLogItem { get; private set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            var selectedLogItem = initData as LogItem;

            if (selectedLogItem == null)
                return;

            CurrentLogItem = selectedLogItem;
        }
    }
}