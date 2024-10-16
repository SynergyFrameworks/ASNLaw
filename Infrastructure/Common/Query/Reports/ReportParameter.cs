using System;

namespace Infrastructure.Query.Reports
{
    public class ReportParameter
    {
        public Guid ReportParameterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public bool BooleanValue { get; set; }
        public DateTime DateValue { get; set; }
        public string StringValue { get; set; }
        public double? DecimalValue { get; set; }
        public int? IntegerValue { get; set; }
        public int DisplayOrder { get; set; }
    }
}
