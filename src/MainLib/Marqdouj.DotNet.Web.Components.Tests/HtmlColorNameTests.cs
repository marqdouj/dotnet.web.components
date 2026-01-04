using Marqdouj.DotNet.Web.Components.Css;

namespace Marqdouj.DotNet.Web.Components.Tests
{
    [TestClass]
    public sealed class HtmlColorNameTests
    {
        [TestMethod]
        public void HtmlColorName_ToHex_AllNamesCovered()
        {
            // Arrange
            var names = Enum.GetValues<HtmlColorName>().Cast<HtmlColorName>().ToList();

            foreach (var name in names)
            {
                // Act
                var hex = name.ToHex(); //Will throw exception if the name is not supported in ToHex

                // Assert
                Assert.StartsWith("#", hex, $"Hex for {name} should start with # but was {hex}"); // Fixed CA1866
                Assert.AreEqual(7, hex.Length, $"Hex for {name} should be 7 characters long but was {hex.Length} characters");
            }
        }

        [TestMethod]
        public void HtmlColorName_Collection_NullItem_True()
        {
            //Arrange
            var names = new HtmlColorNameCollection(true);
            var count = Enum.GetValues<HtmlColorName>().Length + 1;

            //Assert
            Assert.IsFalse(names.Items.First().Value.HasValue);
            Assert.HasCount(count, names.Items, $"Collection should contain {count} items but was {names.Items.Count} items");
            Assert.IsNull(names.Items.First().NameAlias);
            Assert.AreEqual("", names.Items.First().NameDisplay);
        }

        [TestMethod]
        public void HtmlColorName_Collection_NullItem_True_Alias()
        {
            //Arrange
            var names = new HtmlColorNameCollection(true, "My Alias");
            var count = Enum.GetValues<HtmlColorName>().Length + 1;

            //Assert
            Assert.IsFalse(names.Items.First().Value.HasValue);
            Assert.HasCount(count, names.Items, $"Collection should contain {count} items but was {names.Items.Count} items");
            Assert.AreEqual("My Alias", names.Items.First().NameAlias);
            Assert.AreEqual("My Alias", names.Items.First().NameDisplay);
        }

        [TestMethod]
        public void HtmlColorName_Collection_NullItem_False()
        {
            //Arrange
            var names = new HtmlColorNameCollection(false);
            var count = Enum.GetValues<HtmlColorName>().Length;

            //Assert
            Assert.IsTrue(names.Items.First().Value.HasValue);
            Assert.HasCount(count, names.Items, $"Collection should contain {count} items but was {names.Items.Count} items");
        }

        [TestMethod]
        public void HtmlColorName_Collection_AllItems()
        {
            //Arrange
            var names = new HtmlColorNameCollection();

            //Act
            foreach (var item in names.Items)
            {
                var actualValue = (HtmlColorName)item.Value!;

                Assert.IsTrue(item.Value.HasValue);
                Assert.AreEqual(item.Name, item.ToString());
                Assert.AreEqual(item.Name, actualValue.ToString());
                Assert.AreEqual(item.Hex, actualValue.ToHex());
            }
        }

        [TestMethod]
        public void HtmlColorNameListItem_NullItem_True()
        {
            //Arrange
            var item = new HtmlColorNameListItem(null);

            //Assert
            Assert.IsFalse(item.Value.HasValue);
            Assert.AreEqual("", item.Name);
            Assert.AreEqual("", item.Hex);
            Assert.AreEqual("", item.ToString());
        }

        [TestMethod]
        public void HtmlColorNameListItem_NullItem_False()
        {
            //Arrange
            var item = new HtmlColorNameListItem(HtmlColorName.Aqua);
            var actualValue = (HtmlColorName)item.Value!;

            //Assert
            Assert.IsTrue(item.Value.HasValue);
            Assert.AreEqual(actualValue.ToString(), item.Name);
            Assert.AreEqual(actualValue.ToHex(), item.Hex);
            Assert.AreEqual(actualValue.ToString(), item.ToString());
        }
    }
}
