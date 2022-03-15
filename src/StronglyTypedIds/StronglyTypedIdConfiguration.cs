
namespace StronglyTypedIds
{
    internal readonly struct StronglyTypedIdConfiguration
    {
        public StronglyTypedIdBackingType BackingType { get; }

        public StronglyTypedIdConverter Converters { get; }

        public StronglyTypedIdConversionOperator FromOperator { get; }

        public StronglyTypedIdImplementations Implementations { get; }

        public StronglyTypedIdConversionOperator ToOperator { get; }

        public StronglyTypedIdConfiguration(
            StronglyTypedIdBackingType backingType,
            StronglyTypedIdConverter converters,
            StronglyTypedIdImplementations implementations,
            StronglyTypedIdConversionOperator fromOperator,
            StronglyTypedIdConversionOperator toOperator)
        {
            BackingType = backingType;
            Converters = converters;
            FromOperator = fromOperator;
            Implementations = implementations;
            ToOperator = toOperator;
        }

        /// <summary>
        /// Gets the default values for when a default attribute is not used.
        /// Should be kept in sync with the default values referenced in <see cref="StronglyTypedIdDefaultsAttribute"/>
        /// and <see cref="StronglyTypedIdAttribute"/>, but should always be "definite" values (not "Default")
        /// </summary>
        public static readonly StronglyTypedIdConfiguration Defaults = new(
            backingType: StronglyTypedIdBackingType.Guid,
            converters: StronglyTypedIdConverter.TypeConverter | StronglyTypedIdConverter.NewtonsoftJson,
            implementations: StronglyTypedIdImplementations.IEquatable | StronglyTypedIdImplementations.IComparable,
            fromOperator: StronglyTypedIdConversionOperator.None,
            toOperator: StronglyTypedIdConversionOperator.None);

        /// <summary>
        /// Combines multiple <see cref="StronglyTypedIdConfiguration"/> values associated
        /// with a given <see cref="StronglyTypedIdAttribute"/>, returning definite values.
        /// </summary>
        /// <returns></returns>
        public static StronglyTypedIdConfiguration Combine(
            StronglyTypedIdConfiguration attributeValues,
            StronglyTypedIdConfiguration? globalValues)
        {
            var backingType = (attributeValues.BackingType, globalValues?.BackingType) switch
            {
                (StronglyTypedIdBackingType.Default, null) => Defaults.BackingType,
                (StronglyTypedIdBackingType.Default, StronglyTypedIdBackingType.Default) => Defaults.BackingType,
                (StronglyTypedIdBackingType.Default, var globalDefault) => globalDefault.Value,
                (var specificValue, _) => specificValue
            };

            var converter = (attributeValues.Converters, globalValues?.Converters) switch
            {
                (StronglyTypedIdConverter.Default, null) => Defaults.Converters,
                (StronglyTypedIdConverter.Default, StronglyTypedIdConverter.Default) => Defaults.Converters,
                (StronglyTypedIdConverter.Default, var globalDefault) => globalDefault.Value,
                (var specificValue, _) => specificValue
            };

            var implementations = (attributeValues.Implementations, globalValues?.Implementations) switch
            {
                (StronglyTypedIdImplementations.Default, null) => Defaults.Implementations,
                (StronglyTypedIdImplementations.Default, StronglyTypedIdImplementations.Default) => Defaults.Implementations,
                (StronglyTypedIdImplementations.Default, var globalDefault) => globalDefault.Value,
                (var specificValue, _) => specificValue
            };

            var fromOperator = (attributeValues.FromOperator, globalValues?.FromOperator) switch
            {
                (StronglyTypedIdConversionOperator.None, null) => Defaults.FromOperator,
                (StronglyTypedIdConversionOperator.None, StronglyTypedIdConversionOperator.None) => Defaults.FromOperator,
                (StronglyTypedIdConversionOperator.None, var globalDefault) => globalDefault.Value,
                (var specificValue, _) => specificValue
            };

            var toOperator = (attributeValues.ToOperator, globalValues?.ToOperator) switch
            {
                (StronglyTypedIdConversionOperator.None, null) => Defaults.ToOperator,
                (StronglyTypedIdConversionOperator.None, StronglyTypedIdConversionOperator.None) => Defaults.ToOperator,
                (StronglyTypedIdConversionOperator.None, var globalDefault) => globalDefault.Value,
                (var specificValue, _) => specificValue
            };

            return new StronglyTypedIdConfiguration(backingType, converter, implementations, fromOperator, toOperator);
        }
    }
}