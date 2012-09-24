using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.Specialized;
using Windows.Data.Xml.Dom;
using System.Xml.Linq;
using Windows.UI.Xaml;
using System.IO;
using Windows.Storage;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace Reyx.Win8.PeriodicTable.Data
{
    /// <summary>
    /// Base class for <see cref="SampleDataItem"/> and <see cref="SampleDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    public class Element : Reyx.Win8.PeriodicTable.Common.BindableBase
    {
        public Element() { }

        public Element(String number, String name, String symbol, String electronDistrib, String atomicWeight, ElementCategory category, String group, String period, String history)
        {
            this._number = number;
            this._name = name;
            this._symbol = symbol;
            this._electronDistrib = electronDistrib;
            this._atomicWeight = atomicWeight;
            this._category = category;
            this._group = group;
            this._period = period;
            this._history = history;
        }

        private string _number = string.Empty;
        public string Number
        {
            get { return this._number; }
            set { this.SetProperty(ref this._number, value); }
        }

        private string _name = string.Empty;
        public string Name
        {
            get { return this._name; }
            set { this.SetProperty(ref this._name, value); }
        }

        private string _symbol = string.Empty;
        public string Symbol
        {
            get { return this._symbol; }
            set { this.SetProperty(ref this._symbol, value); }
        }

        private string _electronDistrib = string.Empty;
        public string EletronDistrib
        {
            get { return this._electronDistrib.Replace("~~", "\n"); }
            set { this.SetProperty(ref this._electronDistrib, value); }
        }

        private string _atomicWeight = string.Empty;
        public string AtomicWeight
        {
            get { return this._atomicWeight; }
            set { this.SetProperty(ref this._atomicWeight, value); }
        }

        private ElementCategory _category;
        public ElementCategory Category
        {
            get { return this._category; }
            set { this.SetProperty(ref this._category, value); }
        }

        private string _group = string.Empty;
        public int Group
        {
            get { return string.IsNullOrWhiteSpace(this._group) ? 0 : int.Parse(this._group); }
            set { this.SetProperty(ref this._group, value.ToString()); 
            }
        }

        private string _period = string.Empty;
        public int Period
        {
            get { return string.IsNullOrWhiteSpace(this._period) ? 0 : int.Parse(this._period); }
            set { this.SetProperty(ref this._period, value.ToString()); }
        }

        private string _history = string.Empty;
        public string History
        {
            get { return this._history; }
            set { this.SetProperty(ref this._history, value); }
        }

        private ObservableCollection<ElementProperty> _properties= new ObservableCollection<ElementProperty>();
        public ObservableCollection<ElementProperty> Properties
        {
            get { return this._properties; }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class ElementCategory : Reyx.Win8.PeriodicTable.Common.BindableBase
    {
        public ElementCategory(String name)
        {
            this._name = name;
        }

        private string _name = string.Empty;
        public string Name
        {
            get { return this._name; }
            set { this.SetProperty(ref this._name, value); }
        }

        public string HumanName
        {
            get
            {
                //Regex.Replace("ThisIsMyCapsDelimitedString", "(\\B[A-Z])", " $1");
                string upper = Capitalize(this._name);
                return Regex.Replace(upper, "(\\B[A-Z])", " $1");
            }
        }

        public SolidColorBrush Color
        {
            get
            {
                return App.Current.Resources[Capitalize(_name)] as SolidColorBrush;
            }
            set
            {
                SolidColorBrush brush = App.Current.Resources[Capitalize(_name)] as SolidColorBrush;
                this.SetProperty(ref brush, value);
            }
        }

        private string Capitalize(String color)
        {
            return String.Concat(color[0].ToString().ToUpper(), color.Substring(1));
        }

        public override string ToString()
        {
            return this.Name;
        }

        private ObservableCollection<Element> _elements= new ObservableCollection<Element>();
        public ObservableCollection<Element> Elements
        {
            get { return this._elements; }
        }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class ElementProperty : Reyx.Win8.PeriodicTable.Common.BindableBase
    {
        public ElementProperty(String label, String value)
        {
            this._label = label;
            this._value = value;
        }

        private string _label = string.Empty;
        public string Label
        {
            get { return this._label; }
            set { this.SetProperty(ref this._label, value); }
        }

        private string _value = string.Empty;
        public string Value
        {
            get { return this._value; }
            set { this.SetProperty(ref this._value, value); }
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// 
    /// SampleDataSource initializes with placeholder data rather than live production
    /// data so that sample data is provided at both design-time and run-time.
    /// </summary>
    public sealed class ElementsDataSource
    {
        private List<Element> _elements = new List<Element>();
        public List<Element> Elements
        {
            get { return this._elements; }
        }

        private ObservableCollection<ElementCategory> _categories = new ObservableCollection<ElementCategory>();
        public ObservableCollection<ElementCategory> Categories
        {
            get { return this._categories; }
        }

        public static Element GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            // var matches = Elements.Where((item) => item.UniqueId.Equals(uniqueId));
            // if (matches.Count() == 1) return matches.First();
            return null;
        }

        public async Task<bool> LoadElements()
        {
            try
            {
                String ITEM_CONTENT = String.Format("{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                        "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

                string DataFolder = Path.Combine("Assets", "Data");

                string InitMainChartFileName = Path.Combine(DataFolder, (string)Application.Current.Resources["properties-initMainChart"]);
                string InitSeriesChartFileName = Path.Combine(DataFolder, (string)Application.Current.Resources["properties-initSeriesChart"]);

                string AtomicFileName = Path.Combine(DataFolder, (string)Application.Current.Resources["properties-atomic"]);
                string GeneralFileName = Path.Combine(DataFolder, (string)Application.Current.Resources["properties-general"]);

                string MiscFileName = Path.Combine(DataFolder, (string)Application.Current.Resources["properties-misc"]);

                StorageFolder folder = Windows.ApplicationModel.Package.Current.InstalledLocation;

                ElementCategory category;

                // Main Elements
                StorageFile file = await folder.GetFileAsync(InitMainChartFileName);
                XDocument doc = await GetXDocument(file);

                Element element;

                var groups = doc.Element("data").Elements("element").GroupBy(t => t.Element("category").Value);
                foreach (var categoryNode in groups)
                {
                    category = new ElementCategory(categoryNode.Key);

                    foreach (var node in categoryNode)
                    {
                        element = new Element(
                            node.Element("number").Value,
                            node.Element("name").Value,
                            node.Element("symbol").Value,
                            node.Element("electron_distrib").Value,
                            node.Element("atomic_weight").Value,
                            category,
                            node.Element("group").Value,
                            node.Element("period").Value,
                            ITEM_CONTENT);

                        _elements.Add(element);

                        category.Elements.Add(element);
                    }

                    _categories.Add(category);
                }

                _elements = _elements.OrderBy(t => int.Parse(t.Number)).ToList();

                // Series Elementss
                file = await folder.GetFileAsync(InitSeriesChartFileName);
                doc = await GetXDocument(file);
                foreach (var categoryNode in doc.Element("data").Elements("element").GroupBy(t => t.Element("category").Value))
                {
                    category = new ElementCategory(categoryNode.Key);

                    foreach (var node in categoryNode)
                    {
                        element = new Element(
                            node.Element("number").Value,
                            node.Element("name").Value,
                            node.Element("symbol").Value,
                            node.Element("electron_distrib").Value,
                            node.Element("atomic_weight").Value,
                            category,
                            node.Element("group").Value,
                            node.Element("period").Value,
                            ITEM_CONTENT);

                        _elements.Add(element);

                        category.Elements.Add(element);
                    }

                    _categories.Add(category);
                }

                // General Properties
                file = await folder.GetFileAsync(GeneralFileName);
                doc = await GetXDocument(file);
                foreach (var node in doc.Element("data").Elements("element"))
                {
                    element = _elements.FirstOrDefault(t => t.Number == node.Element("number").Value);
                    if (element != null)
                    {
                        foreach (var propertyNode in node.Elements("property"))
                        {
                            element.Properties.Add(new ElementProperty(
                                propertyNode.Element("label").Value,
                                propertyNode.Element("value").Value));
                        }
                    }
                }

                // Atomic Properties
                file = await folder.GetFileAsync(AtomicFileName);
                doc = await GetXDocument(file);
                foreach (var node in doc.Element("data").Elements("element"))
                {
                    element = _elements.FirstOrDefault(t => t.Number == node.Element("number").Value);
                    if (element != null)
                    {
                        foreach (var propertyNode in node.Elements("property"))
                        {
                            element.Properties.Add(new ElementProperty(
                                propertyNode.Element("label").Value,
                                propertyNode.Element("value").Value));
                        }
                    }
                }

                // Miscelaneous Properties
                file = await folder.GetFileAsync(MiscFileName);
                doc = await GetXDocument(file);
                foreach (var node in doc.Element("data").Elements("element"))
                {
                    element = _elements.FirstOrDefault(t => t.Number == node.Element("number").Value);
                    if (element != null)
                    {
                        foreach (var propertyNode in node.Elements("property"))
                        {
                            element.Properties.Add(new ElementProperty(
                                propertyNode.Element("label").Value,
                                propertyNode.Element("value").Value));
                        }
                    }
                }

                // Ajuste de posições no grid
                int maxPeriod = _elements.Max(t => t.Period);
                int maxGroup = _elements.Max(t => t.Group);
                int level = 0;
                for (int i = 1; i <= maxPeriod; i++)
                {
                    for (int j = 1; j <= maxGroup; j++)
                    {
                        if (!_elements.Any(t => t.Period == i && t.Group == j))
                            _elements.Insert(level, new Element("", "", "", "", "", new ElementCategory("empty"), "", "", ""));

                        level++;
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private async Task<XDocument> GetXDocument(StorageFile file)
        {
            XmlDocument xmlDoc = await XmlDocument.LoadFromFileAsync(file);
            return XDocument.Parse(xmlDoc.GetXml());
        }

        public static List<T> CreateDefaultList<T>(int entries)
        {
            return new List<T>(new T[entries]);
        }
    }
}
