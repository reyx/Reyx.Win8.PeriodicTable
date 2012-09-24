using Reyx.Win8.PeriodicTable.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace Reyx.Win8.PeriodicTable
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class Categories : Reyx.Win8.PeriodicTable.Common.LayoutAwarePage
    {
        public Categories()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected async override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            ElementsDataSource model = new ElementsDataSource();

            if (await model.LoadElements())
            {
                this.DefaultViewModel["Categories"] = model.Categories;
                this.DefaultViewModel["Elements"] = model.Elements;
            }
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Reyx.Win8.PeriodicTable.Data.Element element = (Reyx.Win8.PeriodicTable.Data.Element)e.ClickedItem;
            if (!string.IsNullOrWhiteSpace(element.Number))
                this.Frame.Navigate(typeof(Element), element);
        }

        private void GridViewButton_Click(object sender, RoutedEventArgs e)
        {
            Reyx.Win8.PeriodicTable.Data.Element element = (Reyx.Win8.PeriodicTable.Data.Element)((Button)sender).DataContext;
            if (!string.IsNullOrWhiteSpace(element.Number))
                this.Frame.Navigate(typeof(Element), element);
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TabPage));
        }
    }
}
