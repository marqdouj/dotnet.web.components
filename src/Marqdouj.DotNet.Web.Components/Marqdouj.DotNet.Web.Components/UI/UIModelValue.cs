using System.Reflection;

namespace Marqdouj.DotNet.Web.Components.UI
{
    public interface IUIModelValue : IUIValueDef
    {
        double? BindMin { get; }
        double? BindMax { get; }
        string? BindValue { get; set; }
        string? FormatString { get; set; }
        string? FormatValue { get; }
        bool IsNullableValueType { get; }
        bool IsNumerical { get; }
        PropertyInfo Property { get; }
        void SetBindMinMax(double? min, double? max);
        object? Value { get; set; }
    }

    public interface IUIModelValue<TSource> : IUIModelValue where TSource : class
    {
        TSource? Source { get; set; }
    }

    public class UIModelValue<TSource> : UIValueDef, IUIModelValue<TSource> where TSource : class
    {
        private readonly Type pType;
        private readonly Type? pTypeN;

        public UIModelValue(string propertyName) : base(propertyName)
        {
            var type = typeof(TSource);
            Property = type.GetProperty(propertyName)
                ?? throw new ArgumentException($"PropertyInfo not found for '{type.Name}.{propertyName}'", nameof(propertyName));
            IsNumerical = Property.PropertyType.IsNumerical();

            pType = Property.PropertyType;
            pTypeN = Nullable.GetUnderlyingType(pType);
            IsNullableValueType = pTypeN != null;
        }

        public UIModelValue(PropertyInfo property) : base(property.Name)
        {
            var type = typeof(TSource);
            if (property.ReflectedType != type)
                throw new ArgumentException($"PropertyInfo ReflectedType [{property.Name}] does not match source type [{type.Name}].", nameof(property));

            Property = property;
            IsNumerical = Property.PropertyType.IsNumerical();

            pType = Property.PropertyType;
            pTypeN = Nullable.GetUnderlyingType(pType);
            IsNullableValueType = pTypeN != null;
        }

        /// <summary>
        /// Indicates if the underlying Property Type is a nullable value type.
        /// </summary>
        public bool IsNullableValueType { get; }

        /// <summary>
        /// Indicates if the Property Type or Underlying Type is numerical.
        /// </summary>
        public bool IsNumerical { get; }

        public PropertyInfo Property { get; }

        public TSource? Source { get; set; }

        /// <summary>
        /// Typically used for non-string values where @bind-Value requires a string.
        /// </summary>
        public string? BindValue 
        { 
            get => Source != null ? Property.GetValue(Source)?.ToString() : null; 
            set
            {
                if (Source == null || !Property.CanWrite || ReadOnly)
                    return;

                if (pType == typeof(string) || pTypeN == typeof(string))
                {
                    Property.SetValue(Source, value);
                    return;
                }

                if (value == null)
                {
                    if (IsNullableValueType)
                        Property.SetValue(Source, null);
                    return;
                }

                if (pType.IsClass || pType.IsArray)
                    return;

                value = value.Trim();

                if (pType == typeof(bool))
                {
                    if (bool.TryParse(value, out var result))
                        Property.SetValue(Source, result);

                    return;
                }

                if (pTypeN == typeof(bool))
                {
                    if (bool.TryParse(value, out var result))
                        Property.SetValue(Source, result);
                    else
                        Property.SetValue(Source, null);
                    return;
                }

                if (pType.IsEnum)
                {
                    if (Enum.TryParse(pType, value, true, out var result))
                    {
                        Property.SetValue(Source, result);
                    }

                    return;
                }

                if (pTypeN?.IsEnum ?? false)
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        Property.SetValue(Source, null);
                        return;
                    }

                    if (Enum.TryParse(pTypeN, value, true, out var result))
                    {
                        Property.SetValue(Source, result);
                    }

                    return;
                }

                if (pType.IsNumericType() || pTypeN.IsNumericType())
                {
                    if (IsNullableValueType && string.IsNullOrWhiteSpace(value))
                    {
                        Property.SetValue(Source, null);
                        return;
                    }

                    if (double.TryParse(value, null, out var result))
                    {
                        var okMin = BindMin == null || result >= BindMin.Value;
                        var okMax = BindMax == null || result <= BindMax.Value;

                        if (okMin && okMax)
                        {
                            if (pType == typeof(double) || pTypeN == typeof(double))
                            {
                                Property.SetValue(Source, result);
                            }
                            else if (pType == typeof(int) || pTypeN == typeof(int))
                            {
                                Property.SetValue(Source, Convert.ToInt32(result));
                            }
                            else if (pType == typeof(long) || pTypeN == typeof(long))
                            {
                                Property.SetValue(Source, Convert.ToInt64(result));
                            }
                            else
                            {
                                Property.SetValue(Source, result);
                            }
                        }
                        else if (BindMin.HasValue && result <= BindMin.Value)
                        {
                            if (pType == typeof(double) || pTypeN == typeof(double))
                            {
                                Property.SetValue(Source, BindMin.Value);
                            }
                            else if (pType == typeof(int) || pTypeN == typeof(int))
                            {
                                Property.SetValue(Source, Convert.ToInt32(BindMin.Value));
                            }
                            else if (pType == typeof(long) || pTypeN == typeof(long))
                            {
                                Property.SetValue(Source, Convert.ToInt64(BindMin.Value));
                            }
                            else
                            {
                                Property.SetValue(Source, BindMin.Value);
                            }
                        }
                        else if (BindMax.HasValue && result >= BindMax.Value)
                        {
                            if (pType == typeof(double) || pTypeN == typeof(double))
                            {
                                Property.SetValue(Source, BindMax.Value);
                            }
                            else if (pType == typeof(int) || pTypeN == typeof(int))
                            {
                                Property.SetValue(Source, Convert.ToInt32(BindMax.Value));
                            }
                            else if (pType == typeof(long) || pTypeN == typeof(long))
                            {
                                Property.SetValue(Source, Convert.ToInt64(BindMax.Value));
                            }
                            else
                            {
                                Property.SetValue(Source, BindMax.Value);
                            }
                        }
                    }

                    return;
                }
            } 
        }

        /// <summary>
        /// Format used when converting the Value to a string (if applicable).
        /// </summary>
        public string? FormatString { get; set; }

        /// <summary>
        /// Converts the Value to a string using the FormatString.
        /// </summary>
        public string? FormatValue => Value.ToFormatValue(FormatString);

        /// <summary>
        /// Wrapper to Property GetValue/SetValue.
        /// </summary>
        public object? Value 
        {  
            get => Source != null ? Property.GetValue(Source) : null;
            set
            {
                if (Source == null) return;
                Property.SetValue(Source, value);
            }
        }

        /// <summary>
        /// If the underlying model value is numeric and BindMin is set, apply min restriction in BindValue.Set.
        /// If underlying model value is nullable and the new BindValue is null, then min is ignored and the value is set to null.
        /// </summary>
        public double? BindMin { get; private set; }

        /// <summary>
        /// If the underlying model value is numeric and BindMax is set, apply max restriction in BindValue.Set.
        /// If underlying model value is nullable and the new BindValue is null, then max is ignored and the value is set to null.
        /// </summary>
        public double? BindMax { get; private set; }

        public void SetBindMinMax(double? min, double? max)
        {
            if (min.HasValue && max.HasValue)
            {
                if (min.Value > max.Value)
                    throw new Exception($"{nameof(SetBindMinMax)}: min [{min.Value}] can not be greater than max [{max.Value}].");

                BindMin = min.Value;
                BindMax = max.Value;
            }
            else
            {
                BindMin = min;
                BindMax = max;
            }
        }
    }
}
