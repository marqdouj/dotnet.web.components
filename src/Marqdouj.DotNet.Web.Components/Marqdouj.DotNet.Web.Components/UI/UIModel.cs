using System.Collections.ObjectModel;
using System.Reflection;

namespace Marqdouj.DotNet.Web.Components.UI
{
    public interface IUIModel
    {
        bool ContainsItem(string name);
        IUIModelValue? GetItem(string name);
        ReadOnlyCollection<IUIModelValue> Items { get; }
        bool RemoveItem(string name);
        List<IUIModelValue> ToUIList();
    }

    public class UIModel<TSource> : IUIModel where TSource : class
    {
        private readonly List<IUIModelValue> items;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bindingAttr">
        /// BindingFlags used to generate the initial Items list.
        /// Default BindingFlags.Public | BindingFlags.Instance
        /// </param>
        public UIModel(BindingFlags? bindingAttr = null)
        {
            var attr = bindingAttr ?? BindingFlags.Public | BindingFlags.Instance;
            var props = typeof(TSource).GetProperties(attr).Where(p => p.CanRead);
            items = [.. props.OrderBy(p => p.Name).Select(p => new UIModelValue<TSource>(p)).Cast<IUIModelValue>()];
            Items = new ReadOnlyCollection<IUIModelValue>(items);
        }

        public bool ContainsItem(string name) => Items.Any(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        public IUIModelValue? GetItem(string name) => Items.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        
        public ReadOnlyCollection<IUIModelValue> Items { get; }

        public bool RemoveItem(string name)
        {
            var item = GetItem(name);
            return item != null && items.Remove(item);
        }

        private TSource? source;
        public virtual TSource? Source
        {
            get => source;
            set
            {
                source = value;
                foreach (var item in Items.Cast<IUIModelValue<TSource>>())
                {
                    item.Source = source;
                }
            }
        }

        /// <summary>
        /// Creates a list of visible items, sorted by SortOrder, NameDisplay.
        /// </summary>
        /// <returns></returns>
        public virtual List<IUIModelValue> ToUIList()
            => [.. Items.Where(e => e.Visible).OrderBy(e => e.SortOrder).ThenBy(e => e.NameDisplay)];
    }
}
