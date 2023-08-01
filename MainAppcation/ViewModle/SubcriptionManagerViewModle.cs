using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using NetProxyController.Modle;

namespace NetProxyController.ViewModle
{
    internal class SubcriptionManagerViewModle : ViewModleBase
    {
        private List<SubcriptionItemViewModle> ListViewItems;
        private CollectionViewSource listViewDataSource;
        public CollectionViewSource ListViewDataSource
        {
            get => listViewDataSource;
        }
        public SubcriptionManagerViewModle()
        {
            ListViewItems = (from SubscriptionItem item in SubscriptionItem.SubscriptionItemDataList select new SubcriptionItemViewModle(item)).ToList();
            listViewDataSource = new()
            {
                Source = ListViewItems
            };
        }
    }
}
