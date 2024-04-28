using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace XrayGUI.Modle
{
    [Table("Subscription")]
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
        public void Save()
        {
            if (SubcriptionId == default || Global.DBService.Find<SubscriptionItem>(SubcriptionId) == default)
            {
                Global.DBService.Insert(this);
            }
            else
            {
                Global.DBService.Update(this);
            }
        }
        public bool Delate() => Global.DBService.Delete(this) > 0;

        public SubscriptionItem Copy()
        {
            return new SubscriptionItem
            {
                SubcriptionName = SubcriptionName,
                LastUpdateTime = LastUpdateTime,
                Url = Url,
                IsProxyUpdate = IsProxyUpdate,
                IsAutoUpdate = IsAutoUpdate,
                AutoUpdateInterval = AutoUpdateInterval
            };
        }
    }
}
