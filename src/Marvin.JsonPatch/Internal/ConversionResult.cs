namespace Marvin.JsonPatch.Internal
{
    public class ConversionResult
    {
        public ConversionResult(bool canBeConverted, object convertedInstance)
        {
            CanBeConverted = canBeConverted;
            ConvertedInstance = convertedInstance;
        }

        public bool CanBeConverted { get; }
        public object ConvertedInstance { get; }
    }
}
