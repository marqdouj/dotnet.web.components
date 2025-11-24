using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Xml.Linq;

namespace Marqdouj.DotNet.Web.Components.UI
{
    public interface IXmlDocReader
    {
        bool Loaded { get; }
        Exception? LoadException { get; }

        string? GetMemberSummary(MemberInfo member);
        Dictionary<string, string?> GetSummary<T>() where T : class;
    }

    /// <summary>
    /// Provides functionality to read and extract XML documentation comments from an assembly's XML documentation file.
    /// </summary>
    /// <remarks>The XML documentation file must be present in the application's base directory 
    /// and named according to the assembly (e.g., 'MyAssembly.xml'). 
    /// If the file is missing or cannot be loaded, the Loaded property will be false and 
    /// LoadException will contain any error encountered.</remarks>
    /// <example>
    /// Add this Target to the main project *.csproj file to copy the XML document to the output folder.
    /// <code>
    /// <Target Name="_ResolveCopyLocalNuGetPackageXmls" AfterTargets="ResolveReferences">
    ///   <ItemGroup>
    ///     <ReferenceCopyLocalPaths Include="@(ReferenceCopyLocalPaths->'%(RootDir)%(Directory)%(Filename).xml')" Condition="Exists('%(RootDir)%(Directory)%(Filename).xml')" />
    ///   </ItemGroup>
    /// </Target>
    /// </code>
    /// </example>
    public class XmlDocReader : IXmlDocReader
    {
        private protected readonly XDocument? xmlDoc;

        /// <summary>
        /// Initializes a new instance of the XmlDocReader class and attempts to load the XML documentation file for the
        /// specified assembly.
        /// </summary>
        /// <remarks>If the XML documentation file cannot be loaded, the LoadException property is set
        /// with the encountered exception, and the Loaded property is set to false. The logger, if provided, is used to
        /// log any errors during initialization.</remarks>
        /// <param name="assemblyName">The name of the assembly whose XML documentation file should be loaded. The file is expected to be located
        /// in the application's base directory and named '{assemblyName}.xml'.</param>
        /// <param name="logger">An optional logger used to record errors encountered during the loading of the XML documentation file. If
        /// null, errors are not logged.</param>
        /// <exception cref="FileNotFoundException">Thrown if the XML documentation file for the specified assembly cannot be found in the application's base
        /// directory.</exception>
        public XmlDocReader(string assemblyName, ILogger? logger)
        {
            try
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{assemblyName}.xml");

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"XML documentation file not found: {filePath}");

                xmlDoc = XDocument.Load(filePath);
                Loaded = true;
            }
            catch (Exception ex)
            {
                LoadException = ex;
                logger?.LogError(ex, "Error loading XML documentation file.");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the resource has been successfully loaded.
        /// </summary>
        public bool Loaded { get; }

        /// <summary>
        /// Gets the exception that occurred during the load operation, if any.
        /// </summary>
        /// <remarks>If no exception was thrown during loading, this property returns <see
        /// langword="null"/>. This property can be used to diagnose failures when loading content or
        /// resources.</remarks>
        public Exception? LoadException { get; }

        /// <summary>
        /// Retrieves a dictionary containing summary comments for each public property of the specified type.
        /// </summary>
        /// <remarks>Only properties of the specified type are included in the result. The method returns
        /// an empty dictionary if the data source is not loaded. Property names are used as dictionary keys, and their
        /// corresponding summary comments as values.</remarks>
        /// <typeparam name="T">The class type whose property summaries are to be retrieved.</typeparam>
        /// <returns>A dictionary mapping property names to their summary comments. If no properties are found or the data is not
        /// loaded, returns an empty dictionary.</returns>
        public Dictionary<string, string?> GetSummary<T>() where T : class
        {
            var summary = new Dictionary<string, string?>();
            var type = typeof(T);

            if (!Loaded)
                return summary;

            foreach (var info in type.GetMembers())
            {
                if (info.MemberType == System.Reflection.MemberTypes.Property)
                {
                    var comments = GetMemberSummary(info);
                    summary.Add(info.Name, comments);
                }
            }

            return summary;
        }

        /// <summary>
        /// Retrieves the summary documentation for the specified member from the XML documentation file.
        /// </summary>
        /// <remarks>The returned summary is extracted from the `summary` element in the XML documentation
        /// file, if present. This method supports types, methods, properties, and fields. If the XML documentation is
        /// not loaded or the member is not documented, the method returns null.</remarks>
        /// <param name="member">The reflection metadata for the member whose summary documentation is to be retrieved. Must represent a
        /// type, method, property, or field.</param>
        /// <returns>The summary text from the XML documentation for the specified member, or null if no summary is available.</returns>
        public string? GetMemberSummary(MemberInfo member)
        {
            if (xmlDoc == null)
                return null;

            // Build the member name in XML doc format
            var memberName = member.MemberType switch
            {
                MemberTypes.TypeInfo => "T:" + FullName(member),
                MemberTypes.Method => "M:" + FullName(member),
                MemberTypes.Property => "P:" + FullName(member),
                MemberTypes.Field => "F:" + FullName(member),
                _ => null
            };

            if (memberName == null) return null;

            var summaryNode = xmlDoc.Descendants("member")
                .FirstOrDefault(m => m.Attribute("name")?.Value == memberName)?
                .Element("summary");

            return summaryNode?.Value.Trim();
        }

        private static string? FullName(MemberInfo member)
        {
            return member is Type t
                ? t.FullName
                : $"{member?.DeclaringType?.FullName}.{member?.Name}";
        }
    }
}
