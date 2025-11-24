using System.Collections.ObjectModel;

namespace Marqdouj.DotNet.Web.Components.Css
{
    /// <summary>
    /// Represents a collection of <see cref="HtmlColorNameListItem"/>,
    /// optionally including an item with a 'null' value to the start of the list to represent 'none' or 'not selected'.
    /// This collection is useful as the Items source for Lists and Dropdowns, etc.
    /// </summary>
    public class HtmlColorNameCollection
    {
        private readonly List<HtmlColorNameListItem> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlColorNameCollection"/> class, optionally including a null
        /// item.
        /// </summary>
        /// <remarks>The collection is populated with all values of the <see cref="HtmlColorName"/>
        /// enumeration, and optionally includes a null item. The resulting collection is read-only.</remarks>
        /// <param name="nullItem">A value indicating whether to include a null item in the collection.  If <see langword="true"/>, a null item
        /// is added at the beginning of the collection; otherwise, the collection contains only color names.</param>
        /// <param name="nullItemAlias">An optional alias for the null item, used for display purposes. 
        /// If not provided, the null item will have an empty string as its name.</param>
        public HtmlColorNameCollection(bool nullItem = false, string? nullItemAlias = null)
        {
            items = [.. Enum.GetValues<HtmlColorName>().Select(e => new HtmlColorNameListItem(e)).OrderBy(o => o.Value)];

            if (nullItem)
                items.Insert(0, new HtmlColorNameListItem(null) { NameAlias = nullItemAlias });

            Items = new ReadOnlyCollection<HtmlColorNameListItem>(items);
        }

        public ReadOnlyCollection<HtmlColorNameListItem> Items { get; }
    }

    /// <summary>
    /// Represents an item in a list of HTML color names, including its name, hexadecimal value, and associated
    /// enumeration value.
    /// </summary>
    /// <remarks>This class provides convenient access to the name and hexadecimal representation of an HTML
    /// color based on the <see cref="HtmlColorName"/> enumeration. If the <see cref="Value"/> is null, the  <see
    /// cref="Name"/> and <see cref="Hex"/> properties will also return null.</remarks>
    /// <param name="value"></param>
    public class HtmlColorNameListItem(HtmlColorName? value)
    {
        public HtmlColorName? Value { get; } = value;

        #region Name

        /// <summary>
        /// Typically, the property name.
        /// </summary>
        public string Name => Value?.ToString() ?? "";

        /// <summary>
        /// Alternate Name for display purposes.
        /// </summary>
        public string? NameAlias { get; set; }

        /// <summary>
        /// The name to be displayed in the UI.
        /// Display NameAlias if not null; otherwise Name.
        /// </summary>
        public string NameDisplay => NameAlias ?? Name;

        #endregion

        public string Hex => Value?.ToHex() ?? "";

        public override string ToString() => $"{Name}";
    }
}
