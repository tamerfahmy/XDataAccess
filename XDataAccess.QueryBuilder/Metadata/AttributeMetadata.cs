namespace XDataAccess.QueryBuilder.Metadata
{
    public sealed class AttributeMetadata
    {
        public string Name { get; set; }

        public string PropertyName { get; set; }

        public bool Ignore { get; set; }

        public bool Identity { get; set; }

        public object Value { get; set; }
    }
}