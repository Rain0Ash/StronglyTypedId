using System;

namespace StronglyTypedIds
{
    /// <summary>
    /// Used to control the default Place on partial structs to make the type a strongly-typed ID
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    [System.Diagnostics.Conditional("STRONGLY_TYPED_ID_USAGES")]
    public sealed class StronglyTypedIdDefaultsAttribute : Attribute
    {
        /// <summary>
        /// Set the default values used for strongly typed ids
        /// </summary>
        /// <param name="backingType">The <see cref="Type"/> to use to store the strongly-typed ID value.
        /// Defaults to <see cref="StronglyTypedIdBackingType.Guid"/></param>
        /// <param name="converters">JSON library used to serialize/deserialize strongly-typed ID value.
        /// Defaults to <see cref="StronglyTypedIdConverter.NewtonsoftJson"/> and <see cref="StronglyTypedIdConverter.TypeConverter"/></param>
        /// <param name="implementations">Interfaces and patterns the strongly typed id should implement
        /// Defaults to <see cref="StronglyTypedIdImplementations.IEquatable"/> and <see cref="StronglyTypedIdImplementations.IComparable"/></param>
        /// <param name="fromOperator">The type of conversion operator to use to cast from the base type to the strongly-typed ID.
        /// If not set, no conversion operator from the base type to the strongly-typed ID will be generated.</param>
        /// <param name="toOperator">The type of conversion operator to use to cast from the strongly-typed ID to the base type.
        /// If not set, no conversion operator from the strongly-typed ID to the base type will be generated.</param>
        public StronglyTypedIdDefaultsAttribute(
            StronglyTypedIdBackingType backingType = StronglyTypedIdBackingType.Default,
            StronglyTypedIdConverter converters = StronglyTypedIdConverter.Default,
            StronglyTypedIdImplementations implementations = StronglyTypedIdImplementations.Default,
            StronglyTypedIdConversionOperator fromOperator = StronglyTypedIdConversionOperator.None,
            StronglyTypedIdConversionOperator toOperator = StronglyTypedIdConversionOperator.None)
        {
            BackingType = backingType;
            Converters = converters;
            FromOperator = fromOperator;
            Implementations = implementations;
            ToOperator = toOperator;
        }

        /// <summary>
        /// The default <see cref="Type"/> to use to store the strongly-typed ID values.
        /// </summary>
        public StronglyTypedIdBackingType BackingType { get; }

        /// <summary>
        /// The default converters to create for serializing/deserializing strongly-typed ID values.
        /// </summary>
        public StronglyTypedIdConverter Converters { get; }

        /// <summary>
        /// The type of conversion operator to use to cast from the base type to the strongly-typed ID
        /// </summary>
        public StronglyTypedIdConversionOperator FromOperator { get; }

        /// <summary>
        /// Interfaces and patterns the strongly typed id should implement
        /// </summary>
        public StronglyTypedIdImplementations Implementations { get; }

        /// <summary>
        /// The type of conversion operator to use to cast from the strongly-typed ID to the base type
        /// </summary>
        public StronglyTypedIdConversionOperator ToOperator { get; }
    }
}