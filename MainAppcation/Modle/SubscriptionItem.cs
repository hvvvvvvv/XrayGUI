using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace NetProxyController.Modle
{
    internal class SubscriptionItem
    {
        [PrimaryKey]
        public Guid SubcriptionId { get; set; }
        public string SubcriptionName { get; set; } = string.Empty;      
        public DateTime LastUpdateTime { get; set; }
        public string Url { get; set; } = string.Empty;
        public bool IsProxyUpdate { get; set; }
        public bool IsAutoUpdate { get; set; }
        public int AutoUpdateInterval { get; set; }
        public void SaveToDataBase()
        {
            lock (_SubscriptionItemDataList)
            {
                if (SubcriptionId == default)
                {
                    Global.DBService.Insert(this);
                    _SubscriptionItemDataList.Add(this);
                }
                else
                {
                    Global.DBService.Update(this);
                }
            }
        }
        public void DelateFormDataBase()
        {
            lock (_SubscriptionItemDataList)
            {
                Global.DBService.Delete(this);
                _SubscriptionItemDataList.Remove(this);
            }
        }        
        private static List<SubscriptionItem> _SubscriptionItemDataList = Global.DBService.Table<SubscriptionItem>().ToList();
        public static readonly ReadOnlyCollection<SubscriptionItem> SubscriptionItemDataList = new(_SubscriptionItemDataList);

    }
}
