using Marqdouj.DotNet.Web.Components.UI;

namespace Marqdouj.DotNet.Web.Components.Tests
{
    [TestClass]
    public sealed class ValueTypeStringValueTests
    {
        #region Bool

        [TestMethod]
        public void VTSC_Constructor_Bool()
        {
            //Arrange
            var value = default(bool).ToString();
            var vtsv = new ValueTypeStringValue<bool>();

            //Assert
            Assert.AreEqual(value, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_Constructor_Bool_WithValue()
        {
            //Arrange
            var value = default(bool).ToString();
            var initialValue = true.ToString();
            var vtsv = new ValueTypeStringValue<bool>(value: initialValue);

            //Assert
            Assert.AreNotEqual(value, vtsv.Value);
            Assert.AreEqual(initialValue, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_Constructor_Bool_WithValue_Invalid()
        {
            //Arrange
            ValueTypeStringValue<bool>? vtsv = null;
            var value = default(bool).ToString();

            //Act
            vtsv = new ValueTypeStringValue<bool>(value: "my bad value", throwExceptionOnConversionFailure: false);

            //Assert
            Assert.AreEqual(value, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_Constructor_Bool_WithValue_Invalid_Throws()
        {
            //Arrange
            ValueTypeStringValue<bool>? vtsv = null;

            //Act//Assert
            Assert.Throws<Exception>(() => vtsv = new ValueTypeStringValue<bool>(value: "my bad value", throwExceptionOnConversionFailure:true));
        }

        [TestMethod]
        public void VTSC_NotNull_Bool()
        {
            //Arrange
            var vtsv = new ValueTypeStringValue<bool>(false);
            var value = default(bool).ToString();
            var valueFalse = false.ToString();
            var valueTrue = true.ToString();

            //Act//Assert
            Assert.AreEqual(value, vtsv.Value);//From constructor
            vtsv.Value = null;
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = "";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = " ";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = valueFalse;
            Assert.AreEqual(valueFalse, vtsv.Value);
            vtsv.Value = valueTrue;
            Assert.AreEqual(valueTrue, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_Null_Bool()
        {
            //Arrange
            var vtsv = new ValueTypeStringValue<bool>(true);
            const string? value = null;
            var valueFalse = false.ToString();
            var valueTrue = true.ToString();

            //Act//Assert
            Assert.AreEqual(value, vtsv.Value);//From constructor
            vtsv.Value = null;
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = "";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = " ";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = valueFalse;
            Assert.AreEqual(valueFalse, vtsv.Value);
            vtsv.Value = valueTrue;
            Assert.AreEqual(valueTrue, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_NotNull_Bool_Throws()
        {
            //Arrange
            var vtsv = new ValueTypeStringValue<bool>(false) { ThrowExceptionOnConversionFailure = true };
            var valueFalse = false.ToString();
            var valueTrue = true.ToString();

            //Act/Assert
            Assert.Throws<Exception>(() => vtsv.Value = null);
            Assert.Throws<Exception>(() => vtsv.Value = "");
            Assert.Throws<Exception>(() => vtsv.Value = " ");
            vtsv.Value = valueFalse;
            Assert.AreEqual(valueFalse, vtsv.Value);
            vtsv.Value = valueTrue;
            Assert.AreEqual(valueTrue, vtsv.Value);
            vtsv.Value = $"   {valueFalse}   ";
            Assert.AreEqual(valueFalse, vtsv.Value);
            vtsv.Value = $"   {valueTrue}   ";
            Assert.AreEqual(valueTrue, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_Null_Bool_Throws()
        {
            //Arrange
            var vtsv = new ValueTypeStringValue<bool>(true) { ThrowExceptionOnConversionFailure = true };
            const string? value = null;
            var valueFalse = false.ToString();
            var valueTrue = true.ToString();

            //Act//Assert
            Assert.AreEqual(value, vtsv.Value);//From constructor
            vtsv.Value = null;
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = "";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = " ";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = valueFalse;
            Assert.AreEqual(valueFalse, vtsv.Value);
            vtsv.Value = valueTrue;
            Assert.AreEqual(valueTrue, vtsv.Value);
        }

        #endregion

        #region Double

        [TestMethod]
        public void VTSC_Constructor_Double()
        {
            //Arrange
            var value = default(double).ToString();
            var vtsv = new ValueTypeStringValue<double>();

            //Assert
            Assert.AreEqual(value, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_NotNull_Double()
        {
            //Arrange
            var vtsv = new ValueTypeStringValue<double>(false);
            var value = default(double).ToString();
            var valueNumber = "1.234";

            //Act//Assert
            Assert.AreEqual(value, vtsv.Value);//From constructor
            vtsv.Value = null;
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = "";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = " ";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = valueNumber;
            Assert.AreEqual(valueNumber, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_Null_Double()
        {
            //Arrange
            var vtsv = new ValueTypeStringValue<double>(true);
            const string? value = null;
            var valueNumber = "1.234";

            //Act//Assert
            Assert.AreEqual(value, vtsv.Value);//From constructor
            vtsv.Value = null;
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = "";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = " ";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = valueNumber;
            Assert.AreEqual(valueNumber, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_NotNull_Double_Throws()
        {
            //Arrange
            var vtsv = new ValueTypeStringValue<double>(false) { ThrowExceptionOnConversionFailure = true };
            var valueNumber = "1.234";

            //Act/Assert
            Assert.Throws<Exception>(() => vtsv.Value = null);
            Assert.Throws<Exception>(() => vtsv.Value = "");
            Assert.Throws<Exception>(() => vtsv.Value = " ");
            vtsv.Value = valueNumber;
            Assert.AreEqual(valueNumber, vtsv.Value);
            vtsv.Value = $"   {valueNumber}   ";
            Assert.AreEqual(valueNumber, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_Null_Double_Throws()
        {
            //Arrange
            var vtsv = new ValueTypeStringValue<double>(true) { ThrowExceptionOnConversionFailure = true };
            const string? value = null;
            var valueNumber = "1.234";

            //Act//Assert
            Assert.AreEqual(value, vtsv.Value);//From constructor
            vtsv.Value = null;
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = "";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = " ";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = valueNumber;
            Assert.AreEqual(valueNumber, vtsv.Value);
            vtsv.Value = $"   {valueNumber}   ";
            Assert.AreEqual(valueNumber, vtsv.Value);
        }

        #endregion

        #region Int

        [TestMethod]
        public void VTSC_Constructor_Int()
        {
            //Arrange
            var value = default(int).ToString();
            var vtsv = new ValueTypeStringValue<int>();

            //Assert
            Assert.AreEqual(value, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_NotNull_Int()
        {
            //Arrange
            var vtsv = new ValueTypeStringValue<int>(false);
            var value = default(int).ToString();
            var valueNumber = "1";

            //Act//Assert
            Assert.AreEqual(value, vtsv.Value);//From constructor
            vtsv.Value = null;
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = "";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = " ";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = valueNumber;
            Assert.AreEqual(valueNumber, vtsv.Value);
            vtsv.Value = "1.23";
            Assert.AreEqual(valueNumber, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_Null_Int()
        {
            //Arrange
            var vtsv = new ValueTypeStringValue<int>(true);
            const string? value = null;
            var valueNumber = "1";

            //Act//Assert
            Assert.AreEqual(value, vtsv.Value);//From constructor
            vtsv.Value = null;
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = "";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = " ";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = valueNumber;
            Assert.AreEqual(valueNumber, vtsv.Value);
            vtsv.Value = "1.23";
            Assert.AreEqual(valueNumber, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_NotNull_Int_Throws()
        {
            //Arrange
            var vtsv = new ValueTypeStringValue<int>(false) { ThrowExceptionOnConversionFailure = true };
            var valueNumber = "1";

            //Act/Assert
            Assert.Throws<Exception>(() => vtsv.Value = null);
            Assert.Throws<Exception>(() => vtsv.Value = "");
            Assert.Throws<Exception>(() => vtsv.Value = " ");
            Assert.Throws<Exception>(() => vtsv.Value = "1.23");
            vtsv.Value = valueNumber;
            Assert.AreEqual(valueNumber, vtsv.Value);
            vtsv.Value = $"   {valueNumber}   ";
            Assert.AreEqual(valueNumber, vtsv.Value);
        }

        [TestMethod]
        public void VTSC_Null_Int_Throws()
        {
            //Arrange
            var vtsv = new ValueTypeStringValue<int>(true) { ThrowExceptionOnConversionFailure = true };
            const string? value = null;
            var valueNumber = "1";

            //Act//Assert
            Assert.AreEqual(value, vtsv.Value);//From constructor
            vtsv.Value = null;
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = "";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = " ";
            Assert.AreEqual(value, vtsv.Value);
            vtsv.Value = valueNumber;
            Assert.AreEqual(valueNumber, vtsv.Value);
            vtsv.Value = $"   {valueNumber}   ";
            Assert.AreEqual(valueNumber, vtsv.Value);
            Assert.Throws<Exception>(() => vtsv.Value = "1.23");
            Assert.AreEqual(valueNumber, vtsv.Value);
        }

        #endregion
    }
}
