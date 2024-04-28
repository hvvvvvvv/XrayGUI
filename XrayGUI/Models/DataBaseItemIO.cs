using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace XrayGUI.Modle
{
    public abstract class DataBaseItem<T> : IDataBaseItem where T : new()
    {
        [PrimaryKey, AutoIncrement]
        public Guid Identity { get; set; } = Guid.Empty;
        public Guid ParentID { get; set; } = Guid.Empty;
        public uint SeqNumber { get; set; }
        public void Save()
        {
            _DBService.InsertOrReplace(this);
            foreach (var p in typeof(T).GetProperties())
            {
                if (p.PropertyType.IsSubclassOf(typeof(DataBaseItem<>)))
                {
                    var pValue = p.GetValue(this) as IDataBaseItem;
                    if (pValue != null && pValue.ParentID != Identity)
                    {
                        pValue = pValue.Copy();
                        pValue.ParentID = Identity;
                    }
                    pValue?.Save();
                }
            }
        }
        public void Delate()
        {
            _DBService.Delete<T>(Identity);
            foreach (var p in typeof(T).GetProperties())
            {
                if(p.PropertyType.IsSubclassOf(typeof(DataBaseItem<>)))
                {
                    var pvalue = p.GetValue(this) as IDataBaseItem;
                    pvalue?.Delate();
                }
            }
        }
        public T Copy()
        {
            T data = new();
            foreach (var p in typeof(T).GetProperties())
            {
                if (p.Name == nameof(Identity) || p.Name == nameof(ParentID))
                {
                    continue;
                }
                var pValue = p.GetValue(this);
                if(p.PropertyType.IsSubclassOf(typeof(DataBaseItem<>)))
                {
                    pValue = (pValue as IDataBaseItem)?.Copy();
                }
                p.SetValue(data, pValue);
            }
            return data;
        }
        IDataBaseItem IDataBaseItem.Copy() => (IDataBaseItem)Copy()!;
        public void LoadDBItemProperty()
        {
            foreach (var p in typeof(T).GetProperties())
            {
                if (p.PropertyType.IsSubclassOf(typeof(DataBaseItem<>)))
                {
                    TableMapping tableMapping = _DBService.GetMapping(p.PropertyType);
                    var val = _DBService.Query(tableMapping, $"SELECT * FROM {tableMapping.TableName} WHERE ParentID = {Identity}").FirstOrDefault();
                    (val as IDataBaseItem)?.LoadDBItemProperty();
                    if (val?.GetType() == p.PropertyType)
                    {
                        p.SetValue(this, val);
                    }
                }
            }
        }
        private static readonly SQLiteConnection _DBService = new(Global.DbPath);
        public static T Get(Guid pk)
        {
            var val = _DBService.Get<T>(pk);
            (val as IDataBaseItem)?.LoadDBItemProperty();
            return val;
        }
        public static List<T> GetAll()
        {
            var lst = _DBService.Table<T>().ToList();
            lst.ForEach(i => (i as IDataBaseItem)?.LoadDBItemProperty());
            return lst;
        }
        public static void CreateTable()
        {
            _DBService.CreateTable<T>();
        }
    }
}
