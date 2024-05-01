namespace BillingSoftware.Models
{
    public class ReportDataModel
    {
        public ReportDataModel(int id, string name, string description, string hSNCode, int quantity, int rate, int gst, int amount)
        {
            Id = id;
            Name = name;
            Description = description;
            HSNCode = hSNCode;
            Quantity = quantity;
            Rate = rate;
            GST = gst;
            Amount = amount;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string HSNCode { get; }
        public int Quantity { get; }
        public int Rate { get; }
        public int GST { get; }
        public int Amount { get; }
    }
}