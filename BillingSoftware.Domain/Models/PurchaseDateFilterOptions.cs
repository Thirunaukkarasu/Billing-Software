namespace BillingSoftware.Domain.Models
{
    public class PurchaseDateFilterOptions
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public PurchaseDateFilterOptions() { }

        public PurchaseDateFilterOptions(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
        public override string ToString()
        {
            return $"{Value}";
        }

        public override bool Equals(object obj)
        {
            if (obj is PurchaseDateFilterOptions category)
            {
                return Key == category.Key;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}
