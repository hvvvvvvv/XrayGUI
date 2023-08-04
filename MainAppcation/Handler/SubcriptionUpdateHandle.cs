using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetProxyController.Handler
{
    internal class SubcriptionUpdateHandle
    {
        public SubcriptionUpdateHandle()
        {

        }
        public async void UpdateSubcriptionItem(Modle.SubscriptionItem subItem, CancellationToken token)
        {
            using HttpClient http = new();
            var subContent = await http.GetStringAsync(subItem.Url, token);
            if(!string.IsNullOrEmpty(subContent))
            {
                var serverItems = SubscriptionResolveHandle.ResolveSubFromSubContent(subContent);
                
            }
        }
    }
}
