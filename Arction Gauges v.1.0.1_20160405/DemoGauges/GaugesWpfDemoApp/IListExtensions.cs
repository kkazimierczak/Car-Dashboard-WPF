using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaugesWpfDemoApp {

    /// <summary>
    ///  Extension methods for classes that implement IList interface
    /// </summary>
    public static class IListExtensions {

        /// <summary>
        ///  Inserts item by counting the index from the end. 
        /// </summary>
        /// 
        /// <param name="list">List to insert the item</param>
        /// <param name="Index">Index to insert the item, calculated fromt the end.
        /// Values 0 or less (negative values) result to insertion to the last of the list, 
        /// Positive values cause insertion counting form the end of the list. 
        /// 1 results the item to be the second last item in the list, 
        /// list size and larger values results on the item to be the first item of the list.</param>
        /// <returns>True if the insertion was a success. Captures 
        /// \li ArgumentOutOfRangeException and
        /// \li NotSupportedException
        /// exceptions</returns>
        /// \todo Maby change to internal and set libs as friends
        public static bool InsertEnd(this IList list, int Index, Object item) {
            try {
                if (item == null) { 
                    return false; 
                }

                // Determine proper index 
                int Count = list.Count;
                int i = Count - Index;
                i = Math.Max(i,0);
                i = Math.Min(i,Count);
                
                // insert
                list.Insert(i, item);

                return true;
            } catch (ArgumentOutOfRangeException err) {
                return false;
            } catch (NotSupportedException err) {
                return false;
            }
        }

        /// <summary>
        ///  Insert items from the given array to the list. 
        /// </summary>
        /// <remarks>
        /// Implemented mainly to ease up fast implementation of debugging elements, not
        /// for any real or fast work. 
        /// </remarks>
        /// <param name="list"></param>
        /// <param name="Objects"></param>
        /// <returns></returns>
        public static bool AddItems(this IList list, Object[] Objects) {
            try {
                foreach (Object o in Objects) {
                    list.Add(o);
                }
                return true;
            }
            catch (NotSupportedException Err) { 
                return false;
            }
        }

        public static int InsertAfter(this IList list, Object ExistingItem, Object NewItem) {
            return list.InsertRelative(ExistingItem, NewItem, 1);
        }
        
        public static int InsertBefore(this IList list, Object ExistingItem, Object NewItem) {
            return list.InsertRelative(ExistingItem, NewItem, 0);
        }
        
        /// <summary>
        ///  Insert item to list relative to position of some other element.
        ///  Used by InsertAfter and InsertBefore functions. 
        /// </summary>
        /// <remarks>
        ///  Currently this function has not protection of checnking given values, or checking if the resulting index 
        ///  is valid
        /// </remarks>
        /// <param name="list"></param>
        /// <param name="ex">Existing item. If null, insertion happends using add function = to the end of list.
        /// </param>
        /// <param name="ni">New item to insert. No internal checks, list itself should throw error if null.</param>
        /// <param name="id">Index difference. Value to add to the found ex items index for Insert function.</param>
        /// <returns></returns>
        private static int InsertRelative(this IList list, Object ex, Object ni, int id = 1) {

            // Try to get the index for (some of) the reference item. 
            int exi = list.IndexOf(ex); // Documenatation does not specify what is returned if the item does not exist. 

            if (exi < 0 || exi > list.Count - 1) {
                list.Add(ni);
            } else {
                list.Insert(exi+id, ni);
            }

            return exi;
        }
    }
}
