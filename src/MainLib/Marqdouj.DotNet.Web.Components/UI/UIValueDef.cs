namespace Marqdouj.DotNet.Web.Components.UI
{
    public interface IUIValueDef
    {
        string? Description { get; set; }
        bool HasDescription { get; }
        string Name { get; }
        string? NameAlias { get; set; }
        string NameDisplay { get; }
        bool ReadOnly { get; set; }
        int SortOrder { get; set; }
        bool Visible { get; set; }

        void UpdateDescription(Dictionary<string, string?> items);
    }

    /// <summary>
    /// Provides information used to display the value in the UI
    /// </summary>
    /// <param name="name"></param>
    public class UIValueDef(string name) : IUIValueDef
    {
        #region Name

        /// <summary>
        /// Typically, the property name.
        /// </summary>
        public string Name { get; } = name;

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

        #region Description

        /// <summary>
        /// Describes the Value.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Flag for if the Description has a value.
        /// Normally used to dynamically add/remove UI elements
        /// that require a Description (i.e. table column, tooltip, etc.)
        /// </summary>
        public bool HasDescription => !string.IsNullOrWhiteSpace(Description);

        /// <summary>
        /// Updates the Description by key(Name)/value.
        /// </summary>
        /// <param name="items"></param>
        public void UpdateDescription(Dictionary<string, string?> items)
        {
            var kvp = items.FirstOrDefault(kvp => kvp.Key.Equals(Name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(kvp.Value))
                Description = kvp.Value;
        }

        #endregion

        /// <summary>
        /// If true, the UI should not allow editing of the value.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// If sorted, normally by SortOrder, NameDisplay
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Typically used to dynamically add/remove UI elements
        /// </summary>
        public bool Visible { get; set; } = true;
    }
}
