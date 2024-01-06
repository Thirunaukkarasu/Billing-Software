using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BillingSoftware.Domain.Extentions
{
    public static class ObservableCollectionExtention
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> source)
        {
            var obcoll = new ObservableCollection<T>(); 
            if(source != null && source.Count >0 ) 
            { 
                    foreach (var item in source)
                    {
                        obcoll.Add(item);
                    }
            }
            return obcoll;
        }
    }
}
