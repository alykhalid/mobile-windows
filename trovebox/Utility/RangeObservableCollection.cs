using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace trovebox.Utility
{
    /// <summary>
    /// The purpose of this class is to add a "AddRange" method to a ObservableCollection class. Only a single notification is issued for all the new elements.
    /// </summary>
    /// <typeparam name="T">The type of collection</typeparam>
    /// <remarks>
    /// http://peteohanlon.wordpress.com/2008/10/22/bulk-loading-in-observablecollection/
    /// </remarks>
    public class RangeObservableCollection<T> : ObservableCollection<T> 
    { 
        private bool _suppressNotification = false; 
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) 
        { 
            if (!_suppressNotification)      
                base.OnCollectionChanged(e); 
        } 
        
        public void AddRange(IEnumerable<T> list) 
        { 
            if (list == null)
                throw new ArgumentNullException("list"); 
           
            _suppressNotification = true; 
            
            foreach (T item in list) 
            { 
                Add(item);
            } 
            
            _suppressNotification = false; 
          
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)); 
        } 
    }
}
