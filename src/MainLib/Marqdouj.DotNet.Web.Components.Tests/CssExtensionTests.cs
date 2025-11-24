using Marqdouj.DotNet.Web.Components.Css;

namespace Marqdouj.DotNet.Web.Components.Tests
{
    [TestClass]
    public sealed class CssExtensionTests
    {
        [TestMethod]
        public void CssExtension_ToCssId_Empty_DefaultPrefix()
        {
            var guid = Guid.Empty;
            var id = guid.ToCssId();
            Assert.StartsWith("g_", id);
            Console.WriteLine(id);
        }

        [TestMethod]
        public void CssExtension_ToCssId_Empty_CustomPrefix()
        {
            var prefix = "g";
            var guid = Guid.Empty;
            var id = guid.ToCssId(prefix);
            Assert.StartsWith(prefix, id);
            Console.WriteLine(id);
        }

        [TestMethod]
        public void CssExtension_ToCssId_InvalidPrefix_EmptyGuid()
        {
            ValidatePrefix(Guid.Empty);
        }

        [TestMethod]
        public void CssExtension_ToCssId_InvalidPrefix()
        {
            ValidatePrefix(Guid.NewGuid());
        }

        private static void ValidatePrefix(Guid guid)
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentException>(() => guid.ToCssId(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentException>(() => guid.ToCssId(""));
            Assert.Throws<ArgumentException>(() => guid.ToCssId(" "));
            Assert.Throws<ArgumentException>(() => guid.ToCssId("1_"));
            Assert.Throws<ArgumentException>(() => guid.ToCssId("1"));
        }
    }
}
