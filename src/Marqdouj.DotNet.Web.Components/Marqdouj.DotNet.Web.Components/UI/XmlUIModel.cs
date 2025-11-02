namespace Marqdouj.DotNet.Web.Components.UI
{
    /// <summary>
    /// Represents a UI model that initializes item descriptions using XML documentation summaries for the specified
    /// type.
    /// </summary>
    /// <remarks>This class uses an XML documentation reader to retrieve summary information for each item of
    /// type T and updates their descriptions accordingly. The XML summaries are loaded once per application run and
    /// shared across all instances. If no XML documentation reader is provided, item descriptions will not be updated
    /// from XML documentation.</remarks>
    /// <typeparam name="T">The type of items contained in the UI model. Must be a reference type.</typeparam>
    public class XmlUIModel<T> : UIModel<T> where T : class
    {
        protected private static Dictionary<string, string?> xmlDisplay = [];
        protected private static bool xmlWasNotSet = true;

        public XmlUIModel(IXmlDocReader? docReader)
        {
            if (xmlWasNotSet)
            {
                if (docReader == null) return;
                xmlWasNotSet = false;
                xmlDisplay = docReader?.GetSummary<T>() ?? [];
            }

            foreach (var item in Items)
                item.UpdateDescription(xmlDisplay);
        }
    }
}
