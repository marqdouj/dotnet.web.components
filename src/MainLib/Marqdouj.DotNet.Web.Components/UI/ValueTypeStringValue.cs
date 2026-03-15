using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Marqdouj.DotNet.Web.Components.UI
{
    /// <summary>
    /// Handles conversion between value types and their string representations, supporting nullable value types.
    /// </summary>
    /// <remarks>This class is useful for scenarios where value types need to be represented as strings, such
    /// as in serialization or user input processing. It supports both nullable and non-nullable value types, and logs
    /// conversion errors if a logger is provided.</remarks>
    /// <typeparam name="T">The value type to be converted. Must be a struct.</typeparam>
    public class ValueTypeStringValue<T> where T : struct
    {
        private readonly Type targetType;

        /// <summary>
        /// Initializes a new instance of the ValueTypeStringConverter class with the specified nullability, value, and
        /// optional logger.
        /// </summary>
        /// <param name="isNullable">A optional value indicating whether the target type is nullable. Set to <see langword="true"/> if the type supports
        /// null values; otherwise, <see langword="false"/>.</param>
        /// <param name="value">An optional initial value. If <see langword="null"/> then the default will be assigned
        /// NOTE: If an invalid value is provided it will fail conversion, and if <see cref="IsNullable"/> is <see langword="true"/>
        /// an exception will be thrown based on <see cref="ThrowExceptionOnConversionFailure"/>.</param>
        /// <param name="throwExceptionOnConversionFailure"><see cref="ThrowExceptionOnConversionFailure"/></param>
        /// <param name="logger">An optional logger used to record conversion events or errors. If null, no logging is performed.</param>
        public ValueTypeStringValue(bool isNullable = default, string? value = null, bool throwExceptionOnConversionFailure = false, ILogger? logger = null)
        {
            Logger = logger;
            targetType = typeof(T);
            IsNullable = isNullable;
            ThrowExceptionOnConversionFailure = throwExceptionOnConversionFailure;

            //Intialize to default value
            if (isNullable)
            {
                Nullable<T> nullable = null;
                Value = nullable.ToString();
            }
            else
            {
                Value = default(T).ToString();
            }

            //Assign value if provided.
            if (value != null)
                Value = value;
        }

        /// <summary>
        /// Gets or sets the logger used to record diagnostic and operational messages.
        /// </summary>
        /// <remarks>Assigning a value to this property enables logging for the associated component. If
        /// set to null, logging is disabled.</remarks>
        public ILogger? Logger { get; set; }

        /// <summary>
        /// Gets a value indicating whether the associated type or member allows null values.
        /// </summary>
        public bool IsNullable { get; }

        /// <summary>
        /// Gets the most recent exception encountered during the execution of a conversion operation.
        /// </summary>
        public Exception? LastKnowException { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether an exception is thrown when a conversion operation fails.
        /// </summary>
        /// <remarks>Set this property to <see langword="true"/> to throw an exception if a conversion
        /// cannot be completed. If set to <see langword="false"/>, the conversion will fail silently or return a
        /// default value, depending on the implementation.</remarks>
        public bool ThrowExceptionOnConversionFailure { get; set; }

        /// <summary>
        /// Gets or sets the string value represented by this property.
        /// </summary>
        /// <remarks>When setting this property, the input is converted to the target type and then stored
        /// as a string. If the property is nullable and the input is null or whitespace, the value is set to null. If
        /// conversion fails, an error is logged and the value is not updated.</remarks>
        public virtual string? Value
        { 
            get; 
            set 
            {
                LastKnowException = null;

                try
                {
                    if (IsNullable && string.IsNullOrWhiteSpace(value))
                    {
                        field = null;
                        return;
                    }

                    var convertedValue = Convert.ChangeType(value, targetType);
                    field = convertedValue?.ToString();
                }
                catch (Exception ex)
                {
                    LastKnowException = ex;   
                }

                if (LastKnowException != null)
                {
                    var msg = $"Error converting value '{value}' to {targetType.Name}.";
                    Logger?.LogError(LastKnowException, msg);
                    if (Debugger.IsAttached)
                        Console.WriteLine($"{msg}. {LastKnowException.Message}");

                    if (ThrowExceptionOnConversionFailure)
                        throw LastKnowException;
                }
            }
        }
    }
}
