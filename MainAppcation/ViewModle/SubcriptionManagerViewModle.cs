using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CommunityToolkit.Mvvm.Input;
using NetProxyController.Modle;
using NetProxyController.View;

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
            editSubcriptionItemCmd = new(EditSubcriptionItemExcute);
        }
        private RelayCommand editSubcriptionItemCmd;
        public RelayCommand EditSubcriptionItemCmd
        {
            get => editSubcriptionItemCmd;
            set => _ = value;
        }
        public void EditSubcriptionItemExcute()
        {
            new EditSubcriptionItemView().ShowDialog();
        }
    }
}
