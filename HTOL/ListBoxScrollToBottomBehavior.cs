using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Interactivity;

#region File Information

/* ----------------------------------------------------------------------
* File Name: ListBoxScrollToBottomBehavior
* Create Author: ZDK
* Create DateTime: 4/9/2020 7:09:40 PM
* Description:
*----------------------------------------------------------------------*/

#endregion File Information

namespace HTOL
{
    public class ListBoxScrollToBottomBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            ((ICollectionView)AssociatedObject.Items).CollectionChanged += ListBoxScrollToBottomBehavior_CollectionChanged;
        }

        private void ListBoxScrollToBottomBehavior_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (AssociatedObject.HasItems)
                AssociatedObject.ScrollIntoView(AssociatedObject.Items[AssociatedObject.Items.Count - 1]);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            ((ICollectionView)AssociatedObject.Items).CollectionChanged -= ListBoxScrollToBottomBehavior_CollectionChanged;
        }
    }
}