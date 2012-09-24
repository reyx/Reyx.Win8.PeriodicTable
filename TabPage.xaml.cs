using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Reyx.Win8.PeriodicTable
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class TabPage : Reyx.Win8.PeriodicTable.Common.LayoutAwarePage
    {
        private List<Questao> questaoList = new List<Questao>();

        public TabPage()
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
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var tabList = new List<Tab>();
            tabList.Add(new Tab("0", "Consulta de Débitos", true));
            tabList.Add(new Tab("1", "Crédito Consignado"));
            tabList.Add(new Tab("2", "Tempo de espera"));

            questaoList.Add(new Questao("questao 1", "resposta 1", tabList[0]));
            questaoList.Add(new Questao("questao 2", "resposta 2", tabList[0]));
            questaoList.Add(new Questao("questao 3", "resposta 3", tabList[0]));
            questaoList.Add(new Questao("questao 4", "resposta 4", tabList[0]));
            questaoList.Add(new Questao("questao 5", "resposta 5", tabList[0]));
            questaoList.Add(new Questao("questao 6", "resposta 6", tabList[0]));
            questaoList.Add(new Questao("questao 7", "resposta 7", tabList[0]));
            questaoList.Add(new Questao("questao 8", "resposta 8", tabList[0]));
            questaoList.Add(new Questao("questao 9", "resposta 9", tabList[0]));
            questaoList.Add(new Questao("questao 10", "resposta 10", tabList[0]));
            questaoList.Add(new Questao("questao 11", "resposta 11", tabList[0]));

            questaoList.Add(new Questao("questao 12", "resposta 12", tabList[1]));
            questaoList.Add(new Questao("questao 13", "resposta 13", tabList[1]));
            questaoList.Add(new Questao("questao 14", "resposta 14", tabList[1]));
            questaoList.Add(new Questao("questao 15", "resposta 15", tabList[1]));
            questaoList.Add(new Questao("questao 16", "resposta 16", tabList[1]));

            questaoList.Add(new Questao("questao 17", "resposta 17", tabList[2]));
            questaoList.Add(new Questao("questao 18", "resposta 18", tabList[2]));
            questaoList.Add(new Questao("questao 19", "resposta 19", tabList[2]));
            questaoList.Add(new Questao("questao 20", "resposta 20", tabList[2]));
            questaoList.Add(new Questao("questao 21", "resposta 21", tabList[2]));
            questaoList.Add(new Questao("questao 22", "resposta 22", tabList[2]));
            questaoList.Add(new Questao("questao 23", "resposta 23", tabList[2]));
            questaoList.Add(new Questao("questao 24", "resposta 24", tabList[2]));
            questaoList.Add(new Questao("questao 25", "resposta 25", tabList[2]));
            questaoList.Add(new Questao("questao 26", "resposta 26", tabList[2]));
            questaoList.Add(new Questao("questao 27", "resposta 27", tabList[2]));
            questaoList.Add(new Questao("questao 28", "resposta 28", tabList[2]));
            questaoList.Add(new Questao("questao 29", "resposta 29", tabList[2]));
            questaoList.Add(new Questao("questao 30", "resposta 30", tabList[2]));
            questaoList.Add(new Questao("questao 31", "resposta 31", tabList[2]));
            questaoList.Add(new Questao("questao 32", "resposta 32", tabList[2]));
            questaoList.Add(new Questao("questao 33", "resposta 33", tabList[2]));

            this.DefaultViewModel["Tabs"] = tabList;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Invoked when a filter is selected using a RadioButton when not snapped.
        /// </summary>
        /// <param name="sender">The selected RadioButton instance.</param>
        /// <param name="e">Event data describing how the RadioButton was selected.</param>
        void Tab_Checked(object sender, RoutedEventArgs e)
        {
            // Mirror the change into the CollectionViewSource used by the corresponding ComboBox
            // to ensure that the change is reflected when snapped
            //if (filtersViewSource.View != null)
            //{
            // var filter = (sender as FrameworkElement).DataContext;S
            //    filtersViewSource.View.MoveCurrentTo(filter);
            //}

            RadioButton radio = sender as RadioButton;
            radio.IsChecked = true;

            this.DefaultViewModel["Questions"] = questaoList.Where(t => t.Tab.Id == AutomationProperties.GetAutomationId(radio));
        }

        /// <summary>
        /// View model describing one of the filters available for viewing search results.
        /// </summary>
        private sealed class Tab : Reyx.Win8.PeriodicTable.Common.BindableBase
        {
            private String _name;
            private bool _active;
            private string _id;

            public Tab(String id, String name, bool active = false)
            {
                this.Name = name;
                this.Active = active;
                this.Id = id;
            }

            public override String ToString()
            {
                return Description;
            }

            public String Id
            {
                get { return _id; }
                set { if (this.SetProperty(ref _id, value)) this.OnPropertyChanged("Id"); }
            }

            public String Name
            {
                get { return _name; }
                set { if (this.SetProperty(ref _name, value)) this.OnPropertyChanged("Description"); }
            }

            public bool Active
            {
                get { return _active; }
                set { this.SetProperty(ref _active, value); }
            }

            public String Description
            {
                get { return _name; }
            }
        }

        /// <summary>
        /// View model describing one of the filters available for viewing search results.
        /// </summary>
        private sealed class Questao : Reyx.Win8.PeriodicTable.Common.BindableBase
        {
            private String _pergunta;
            private String _resposta;
            private Tab _tab;

            public Questao(String pergunta, String resposta, Tab tab)
            {
                this.Pergunta = pergunta;
                this.Resposta = resposta;
                this.Tab = tab;
            }

            public override String ToString()
            {
                return Description;
            }

            public String Pergunta
            {
                get { return _pergunta; }
                set { if (this.SetProperty(ref _pergunta, value)) this.OnPropertyChanged("Pergunta"); }
            }

            public String Resposta
            {
                get { return _resposta; }
                set { if (this.SetProperty(ref _resposta, value)) this.OnPropertyChanged("Resposta"); }
            }

            public Tab Tab
            {
                get { return _tab; }
                set { if (this.SetProperty(ref _tab, value)) this.OnPropertyChanged("Tab"); }
            }

            public String Description
            {
                get { return string.Format("{0}: {1}", _pergunta, _resposta); }
            }
        }

        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Border border = (Border)sender;
            Grid parent = VisualTreeHelper.GetParent(border) as Grid;
            Border sibling = VisualTreeHelper.GetChild(parent, 1) as Border;

            if (sibling.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                sibling.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                sibling.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
