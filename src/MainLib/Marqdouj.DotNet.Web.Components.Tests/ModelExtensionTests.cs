using Marqdouj.DotNet.Web.Components.UI;
using System.ComponentModel.DataAnnotations;

namespace Marqdouj.DotNet.Web.Components.Tests
{
    [TestClass]
    public sealed class ModelExtensionTests
    {
        private const string EnumName = "Second Enum";
        private const string PropName = "Prop Name";

        [TestMethod]
        public void DisplayName_Enum_NoAttribute()
        {
            //Arrange
            var value = MyEnum.First;
            var name = nameof(MyEnum.First);

            //Act
            var result = value.GetDisplayName();

            //Assert
            Assert.AreEqual(name, result);
        }

        [TestMethod]
        public void DisplayName_Enum_NoAttribute_NullIfNotFound()
        {
            //Arrange
            var value = MyEnum.First;
            string? name = null;

            //Act
            var result = value.GetDisplayName(false);

            //Assert
            Assert.AreEqual(name, result);
        }

        [TestMethod]
        public void DisplayName_Enum_WithAttribute()
        {
            //Arrange
            var value = MyEnum.Second;
            var name = EnumName;

            //Act
            var result = value.GetDisplayName();

            //Assert
            Assert.AreEqual(name, result);
        }

        [TestMethod]
        public void DisplayName_Enum_WithAttribute_NullIfNotFound()
        {
            //Arrange
            var value = MyEnum.Second;
            var name = EnumName;

            //Act
            var result = value.GetDisplayName(false);

            //Assert
            Assert.AreEqual(name, result);
        }

        [TestMethod]
        public void DisplayName_Prop_NoAttribute()
        {
            //Arrange
            var value = typeof(MyClass).GetProperty(nameof(MyClass.Alias));
            var name = nameof(MyClass.Alias);

            //Act
            var result = value?.GetDisplayName();

            //Assert
            Assert.AreEqual(name, result);
        }

        [TestMethod]
        public void DisplayName_Prop_NoAttribute_NullIfNotFound()
        {
            //Arrange
            var value = typeof(MyClass).GetProperty(nameof(MyClass.Alias));
            string? name = null;

            //Act
            var result = value?.GetDisplayName(false);

            //Assert
            Assert.AreEqual(name, result);
        }

        [TestMethod]
        public void DisplayName_Prop_WithAttribute()
        {
            //Arrange
            var value = typeof(MyClass).GetProperty(nameof(MyClass.Name));
            var name = PropName;

            //Act
            var result = value?.GetDisplayName();

            //Assert
            Assert.AreEqual(name, result);
        }

        [TestMethod]
        public void DisplayName_Prop_WithAttribute_NullIfNotFound()
        {
            //Arrange
            var value = typeof(MyClass).GetProperty(nameof(MyClass.Name));
            var name = PropName;

            //Act
            var result = value?.GetDisplayName(false);

            //Assert
            Assert.AreEqual(name, result);
        }

        private enum MyEnum
        {
            First,
            [Display(Name = EnumName)]
            Second,
        }

        private class MyClass
        {
            [Display(Name = PropName)]
            public string? Name { get; set; }

            public string? Alias { get; set; }
        }
    }
}
