using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Reyx.Win8.PeriodicTable.UserControls
{
    class Selector : GridView
    {
        public DataTemplate TemplateAntigoDownloaded { get; set; }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var obj = item as Reyx.Win8.PeriodicTable.Data.Element;
            var gi = element as GridViewItem;

            gi.Background = obj.Category.Color;

            base.PrepareContainerForItemOverride(gi, item);
        }
    }
}
