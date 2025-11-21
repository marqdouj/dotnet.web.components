using Marqdouj.DotNet.Web.Components.UI;

namespace Marqdouj.DotNet.Web.Components.Tests
{
    [TestClass]
    public sealed class UIModelTests
    {
        #region FormatValue D

        [TestMethod]
        public void UIModel_FormatValue_D()
        {
            var obj = new UIModelTestClass { D = 123.406 };
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D))
            {
                FormatString = "N2",
                Source = obj
            };

            var value = model.FormatValue;

            Assert.AreEqual("123.41", value);
        }

        [TestMethod]
        public void UIModel_FormatValue_DN_WithValue()
        {
            var obj = new UIModelTestClass { DN = 123.406 };
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.DN))
            {
                FormatString = "N2",
                Source = obj
            };

            var value = model.FormatValue;

            Assert.AreEqual("123.41", value);
        }

        [TestMethod]
        public void UIModel_FormatValue_DN_Null()
        {
            var obj = new UIModelTestClass { DN = null };
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.DN))
            {
                FormatString = "N2",
                Source = obj
            };

            var value = model.FormatValue;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UIModel_FormatValue_D_NoSource()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D))
            {
                FormatString = "N2",
            };

            var value = model.FormatValue;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UIModel_FormatValue_DN_NoSource()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.DN))
            {
                FormatString = "N2",
            };

            var value = model.FormatValue;

            Assert.IsNull(value);
        }

        #endregion

        #region BindValue D

        [TestMethod]
        public void UIModel_BindValue_D()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D))
            {
                FormatString = "N2",
                Source = obj,
            };

            model.BindValue = "123.406";
            var value = model.BindValue;

            Assert.AreEqual("123.406", value);
        }

        [TestMethod]
        public void UIModel_BindValue_D_Null()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D))
            {
                FormatString = "N2",
                Source = obj,
                Value = 123.406
            };

            model.BindValue = null;
            var value = model.BindValue;

            Assert.AreEqual("123.406", value);
        }

        [TestMethod]
        public void UIModel_BindValue_D_EmptyString()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D))
            {
                FormatString = "N2",
                Source = obj,
                Value = 123.406
            };

            model.BindValue = "";
            var value = model.BindValue;

            Assert.AreEqual("123.406", value);
        }

        [TestMethod]
        public void UIModel_BindValue_D_Null_Flags_UseDefaultSet()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D))
            {
                FormatString = "N2",
                Source = obj,
                Value = 123.406,
                BindValueFlags = BindValueFlags.UseDefaultSetValueForNull
            };

            model.BindValue = null;
            var value = model.BindValue;

            Assert.AreEqual("0", value);
        }

        [TestMethod]
        public void UIModel_BindValue_D_Null_Flags_EmptyStringAsNull()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D))
            {
                FormatString = "N2",
                Source = obj,
                Value = 123.406,
                BindValueFlags = BindValueFlags.TreatEmptyStringAsNullForValueTypes
            };

            model.BindValue = null;
            var value = model.BindValue;

            Assert.AreEqual("123.406", value);
        }

        [TestMethod]
        public void UIModel_BindValue_D_Null_Flags_UseDefaultSet_EmptyStringAsNull()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D))
            {
                FormatString = "N2",
                Source = obj,
                Value = 123.406,
                BindValueFlags = BindValueFlags.UseDefaultSetValueForNull | BindValueFlags.TreatEmptyStringAsNullForValueTypes
            };

            model.BindValue = null;
            var value = model.BindValue;

            Assert.AreEqual("0", value);
        }

        [TestMethod]
        public void UIModel_BindValue_D_EmptyString_Flags_UseDefaultSet()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D))
            {
                FormatString = "N2",
                Source = obj,
                Value = 123.406,
                BindValueFlags = BindValueFlags.UseDefaultSetValueForNull
            };

            model.BindValue = "";
            var value = model.BindValue;

            Assert.AreEqual("123.406", value);
        }

        [TestMethod]
        public void UIModel_BindValue_D_EmptyString_Flags_EmptyStringAsNull()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D))
            {
                FormatString = "N2",
                Source = obj,
                Value = 123.406,
                BindValueFlags = BindValueFlags.TreatEmptyStringAsNullForValueTypes
            };

            model.BindValue = "";
            var value = model.BindValue;

            Assert.AreEqual("123.406", value);
        }

        [TestMethod]
        public void UIModel_BindValue_D_EmptyString_Flags_UseDefaultSet_EmptyStringAsNull()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D))
            {
                FormatString = "N2",
                Source = obj,
                Value = 123.406,
                BindValueFlags = BindValueFlags.UseDefaultSetValueForNull | BindValueFlags.TreatEmptyStringAsNullForValueTypes
            };

            model.BindValue = "";
            var value = model.BindValue;

            Assert.AreEqual("0", value);
        }

        [TestMethod]
        public void UIModel_BindValue_DN_WithValue()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.DN))
            {
                FormatString = "N2",
                Source = obj,
                BindValue = "123.406"
            };

            var value = model.BindValue;

            Assert.AreEqual("123.406", value);
        }

        [TestMethod]
        public void UIModel_BindValue_DN_Null()
        {
            var obj = new UIModelTestClass { DN = 123.406 };
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.DN))
            {
                FormatString = "N2",
                Source = obj,
                BindValue = null
            };

            var value = model.BindValue;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UIModel_BindValue_D_NoSource()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D))
            {
                FormatString = "N2",
            };

            var value = model.BindValue;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UIModel_BindValue_DN_NoSource()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.DN))
            {
                FormatString = "N2",
            };

            var value = model.BindValue;

            Assert.IsNull(value);
        }

        #endregion

        #region FormatValue I

        [TestMethod]
        public void UIModel_FormatValue_I()
        {
            var obj = new UIModelTestClass { I = 123 };
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.I))
            {
                FormatString = "N2",
                Source = obj
            };

            var value = model.FormatValue;

            Assert.AreEqual("123.00", value);
        }

        [TestMethod]
        public void UIModel_FormatValue_IN_WithValue()
        {
            var obj = new UIModelTestClass { IN = 123 };
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.IN))
            {
                FormatString = "N2",
                Source = obj
            };

            var value = model.FormatValue;

            Assert.AreEqual("123.00", value);
        }

        [TestMethod]
        public void UIModel_FormatValue_IN_Null()
        {
            var obj = new UIModelTestClass { IN = null };
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.IN))
            {
                FormatString = "N2",
                Source = obj
            };

            var value = model.FormatValue;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UIModel_FormatValue_I_NoSource()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.I))
            {
                FormatString = "N2",
            };

            var value = model.FormatValue;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UIModel_FormatValue_IN_NoSource()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.IN))
            {
                FormatString = "N2",
            };

            var value = model.FormatValue;

            Assert.IsNull(value);
        }

        #endregion

        #region BindValue I

        [TestMethod]
        public void UIModel_BindValue_I()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.I))
            {
                FormatString = "N2",
                Source = obj,
                BindValue = "123.406"
            };

            var value = model.BindValue;

            Assert.AreEqual("123", value);
        }

        [TestMethod]
        public void UIModel_BindValue_IN_WithValue()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.IN))
            {
                FormatString = "N2",
                Source = obj,
                BindValue = "123.406"
            };

            var value = model.BindValue;

            Assert.AreEqual("123", value);
        }

        [TestMethod]
        public void UIModel_BindValue_IN_Null()
        {
            var obj = new UIModelTestClass { IN = 123 };
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.IN))
            {
                FormatString = "N2",
                Source = obj,
                BindValue = null
            };

            var value = model.BindValue;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UIModel_BindValue_I_NoSource()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.I))
            {
                FormatString = "N2",
            };

            var value = model.BindValue;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UIModel_BindValue_IN_NoSource()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.IN))
            {
                FormatString = "N2",
            };

            var value = model.BindValue;

            Assert.IsNull(value);
        }

        #endregion

        #region FormatValue S

        [TestMethod]
        public void UIModel_FormatValue_S()
        {
            var obj = new UIModelTestClass { S = "123.45678" };
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.S))
            {
                FormatString = "N2",
                Source = obj
            };

            var value = model.FormatValue;

            Assert.AreEqual("123.45678", value);
        }

        [TestMethod]
        public void UIModel_FormatValue_SN_WithValue()
        {
            var obj = new UIModelTestClass { SN = "123.45678" };
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.SN))
            {
                FormatString = "N2",
                Source = obj
            };

            var value = model.FormatValue;

            Assert.AreEqual("123.45678", value);
        }

        [TestMethod]
        public void UIModel_FormatValue_SN_Null()
        {
            var obj = new UIModelTestClass { SN = null };
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.SN))
            {
                FormatString = "N2",
                Source = obj
            };

            var value = model.FormatValue;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UIModel_FormatValue_S_NoSource()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.S))
            {
                FormatString = "N2",
            };

            var value = model.FormatValue;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UIModel_FormatValue_SN_NoSource()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.SN))
            {
                FormatString = "N2",
            };

            var value = model.FormatValue;

            Assert.IsNull(value);
        }

        #endregion

        #region BindValue S

        [TestMethod]
        public void UIModel_BindValue_S()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.S))
            {
                FormatString = "N2",
                Source = obj,
                BindValue = "123.45678"
            };

            var value = model.BindValue;

            Assert.AreEqual("123.45678", value);
        }

        [TestMethod]
        public void UIModel_BindValue_SN_WithValue()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.SN))
            {
                FormatString = "N2",
                Source = obj,
                BindValue = "123.45678"
            };

            var value = model.BindValue;

            Assert.AreEqual("123.45678", value);
        }

        [TestMethod]
        public void UIModel_BindValue_SN_Null()
        {
            var obj = new UIModelTestClass { SN = "123.45678" };
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.SN))
            {
                FormatString = "N2",
                Source = obj,
                BindValue = null
            };

            var value = model.BindValue;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UIModel_BindValue_S_NoSource()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.S))
            {
                FormatString = "N2",
            };

            var value = model.BindValue;

            Assert.IsNull(value);
        }

        [TestMethod]
        public void UIModel_BindValue_SN_NoSource()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.SN))
            {
                FormatString = "N2",
            };

            var value = model.BindValue;

            Assert.IsNull(value);
        }

        #endregion

        #region BindValue ReadOnly

        [TestMethod]
        public void UIModel_BindValue_ReadOnly()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.S))
            {
                FormatString = "N2",
                Source = obj,
                Value = "123.45678",
                ReadOnly = true
            };

            model.BindValue = "456.789";
            var value = model.BindValue;

            Assert.AreEqual("123.45678", value);
        }

        #endregion

        #region BindValue Enum

        [TestMethod]
        public void UIModel_BindValue_Enum_Nullable()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.EN))
            {
                Source = obj,
            };
            model.BindValue = UIModelTestEnum.A.ToString();
            var valueA = model.BindValue;

            model.BindValue = UIModelTestEnum.B.ToString();
            var valueB = model.BindValue;

            model.BindValue = UIModelTestEnum.C.ToString();
            var valueC = model.BindValue;

            model.BindValue = null;
            var valueNull = model.BindValue;

            model.BindValue = "";
            var valueEmpty = model.BindValue;

            Assert.AreEqual(UIModelTestEnum.A.ToString(), valueA);
            Assert.AreEqual(UIModelTestEnum.B.ToString(), valueB);
            Assert.AreEqual(UIModelTestEnum.C.ToString(), valueC);
            Assert.IsNull(valueNull);
            Assert.IsNull(valueEmpty);
        }

        [TestMethod]
        public void UIModel_BindValue_Enum()
        {
            var obj = new UIModelTestClass();
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.E))
            {
                Source = obj,
            };

            model.BindValue = UIModelTestEnum.A.ToString();
            var valueA = model.BindValue;

            model.BindValue = UIModelTestEnum.B.ToString();
            var valueB = model.BindValue;

            model.BindValue = UIModelTestEnum.C.ToString();
            var valueC = model.BindValue;

            model.BindValue = null;
            var valueNull = model.BindValue;

            model.BindValue = "";
            var valueEmpty = model.BindValue;

            Assert.AreEqual(UIModelTestEnum.A.ToString(), valueA);
            Assert.AreEqual(UIModelTestEnum.B.ToString(), valueB);
            Assert.AreEqual(UIModelTestEnum.C.ToString(), valueC);
            Assert.IsNotNull(valueNull);
            Assert.IsNotNull(valueEmpty);
            Assert.AreEqual(UIModelTestEnum.C.ToString(), valueNull);
            Assert.AreEqual(UIModelTestEnum.C.ToString(), valueEmpty);
        }

        #endregion

        #region IsNullable

        [TestMethod]
        public void UIModel_IsNullable_D()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D));
            Assert.IsFalse(model.IsNullable);
        }

        [TestMethod]
        public void UIModel_IsNullable_DN()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.DN));
            Assert.IsTrue(model.IsNullable);
        }

        [TestMethod]
        public void UIModel_IsNullable_S()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.S));
            Assert.IsFalse(model.IsNullable);
        }

        [TestMethod]
        public void UIModel_IsNullable_SN()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.SN));
            Assert.IsTrue(model.IsNullable);
        }

        [TestMethod]
        public void UIModel_IsNullable_E()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.E));
            Assert.IsFalse(model.IsNullable);
        }

        [TestMethod]
        public void UIModel_IsNullable_EN()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.EN));
            Assert.IsTrue(model.IsNullable);
        }

        [TestMethod]
        public void UIModel_IsNullable_Options()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.Options));
            Assert.IsFalse(model.IsNullable);
        }

        [TestMethod]
        public void UIModel_IsNullable_OptionsN()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.OptionsN));
            Assert.IsTrue(model.IsNullable);
        }

        #endregion

        #region IsNullableValueType

        [TestMethod]
        public void UIModel_IsNullableValueType_D()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.D));
            Assert.IsFalse(model.IsNullableValueType);
        }

        [TestMethod]
        public void UIModel_IsNullableValueType_DN()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.DN));
            Assert.IsTrue(model.IsNullableValueType);
        }

        [TestMethod]
        public void UIModel_IsNullableValueType_S()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.S));
            Assert.IsFalse(model.IsNullableValueType);
        }

        [TestMethod]
        public void UIModel_IsNullableValueType_SN()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.SN));
            Assert.IsFalse(model.IsNullableValueType); // Should return false even though string is nullable.
        }

        [TestMethod]
        public void UIModel_IsNullableValueType_E()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.E));
            Assert.IsFalse(model.IsNullableValueType);
        }

        [TestMethod]
        public void UIModel_IsNullableValueType_EN()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.EN));
            Assert.IsTrue(model.IsNullableValueType);
        }

        [TestMethod]
        public void UIModel_IsNullableValueType_Options()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.Options));
            Assert.IsFalse(model.IsNullableValueType);
        }

        [TestMethod]
        public void UIModel_IsNullableValueType_OptionsN()
        {
            var model = new UIModelValue<UIModelTestClass>(nameof(UIModelTestClass.OptionsN));
            Assert.IsFalse(model.IsNullableValueType);
        }

        #endregion
    }

    internal enum UIModelTestEnum
    {
        A,
        B,
        C
    }

    internal class UIModelTestClass
    {
        public double D { get; set; }
        public double? DN { get; set; }
        public int I { get; set; }
        public int? IN { get; set; }
        public string S { get; set; } = "";
        public string? SN { get; set; }
        public UIModelTestEnum E { get; set; }
        public UIModelTestEnum? EN { get; set; }
        public UIModelTestOptions Options { get; set; } = new();
        public UIModelTestOptions? OptionsN { get; set; } 
    }

    internal class UIModelTestOptions
    {
        public string? Name { get; set; }
    }
}
