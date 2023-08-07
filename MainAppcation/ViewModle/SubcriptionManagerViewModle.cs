using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.Input;
using NetProxyController.Handler;
using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using NetProxyController.View;

namespace NetProxyController.ViewModle
{
    internal class SubcriptionManagerViewModle : ViewModleBase
    {
        private List<SubcriptionItemViewModle> ListViewItems;
        private readonly CollectionViewSource listViewDataSource;
        public List<SubcriptionItemViewModle> SelectedItems;
        public CollectionViewSource ListViewDataSource
        {
            get => listViewDataSource;
        }
        public SubcriptionManagerViewModle()
        {
            ListViewItems = (from SubscriptionItem item in SubscriptionItem.SubscriptionItemDataList select new SubcriptionItemViewModle(item)).ToList();
            listViewDataSource = new() { Source = ListViewItems };
            SelectedItems = new();
            editSubcriptionItemCmd = new(EditSubcriptionItemExcute);
            createSubcriptionItemCmd = new(CreateSubcriptionItemExcute);
            deleteSubcriptionItemsCmd = new(DeleteSubcriptionItemsExcute);
            updateSubcriptionItemsCmd = new(UpdateSubcriptionItemsExcute);
        }
        private bool listViewHasSelectedItems;
        public bool ListViewHasSelectedItems
        {
            get => listViewHasSelectedItems;
            set
            {
                listViewHasSelectedItems = value;
                OnPropertyChanged();
            }
        }
        private bool listViewSeletedItemsIsSingle;
        public bool ListViewSeletedItemsIsSingle
        {
            get => listViewSeletedItemsIsSingle;
            set
            {
                listViewSeletedItemsIsSingle = value;
                OnPropertyChanged();
            }
        }
        private RelayCommand editSubcriptionItemCmd;
        public RelayCommand EditSubcriptionItemCmd
        {
            get => editSubcriptionItemCmd;
            set => _ = value;
        }
        private RelayCommand createSubcriptionItemCmd;
        public RelayCommand CreateSubcriptionItemCmd
        {
            get => createSubcriptionItemCmd;
            set => _ = value;
        }
        private RelayCommand deleteSubcriptionItemsCmd;
        public RelayCommand DeleteSubcriptionItemsCmd
        {
            get => deleteSubcriptionItemsCmd;
            set => _ = value;
        }
        private RelayCommand updateSubcriptionItemsCmd;
        public RelayCommand UpdateSubcriptionItemsCmd
        {
            get => updateSubcriptionItemsCmd;
            set => _ = value;
        }
        private void CreateSubcriptionItemExcute()
        {
            var itemVm = new SubcriptionItemViewModle(new());
            if (itemVm.EditSubItem())
            {
                ListViewItems.Add(itemVm);
                listViewDataSource.View.Refresh();
            }                       
        }
        private void EditSubcriptionItemExcute()
        {
            if(SelectedItems.Count > 0)
            {
                SelectedItems[0].EditSubItem();
            }
        }
        private void DeleteSubcriptionItemsExcute()
        {
            if (HandyControl.Controls.MessageBox.Show(messageBoxText: $"是否删除选中项(共{SelectedItems.Count()}项)？", button: System.Windows.MessageBoxButton.YesNo,
                icon: System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.Yes)
            {
                foreach (var item in SelectedItems)
                {
                    item.DeleteSubItem();
                    ListViewItems.Remove(item);
                }               
                listViewDataSource.View.Refresh();
            }
        }
        private void UpdateSubcriptionItemsExcute()
        {
            foreach(var item in SelectedItems)
            {
                item.UpdateSubItem();
            }
        }
    }
}
