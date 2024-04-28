using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayGUI.Modle
{
    public interface IDataBaseItem
    {
        public Guid Identity { get; set;}
        public Guid ParentID { get; set; }
        public void Save();
        public void Delate();
        public IDataBaseItem Copy();
        public void LoadDBItemProperty();
    }
}
