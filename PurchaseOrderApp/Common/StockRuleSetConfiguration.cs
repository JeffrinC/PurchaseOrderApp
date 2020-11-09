namespace PurchaseOrderApp.Common
{
    public sealed class StockRuleSetConfiguration
    {
        public const string StockRuleSet = "StockRuleSet";

        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public decimal ChangeRate { get; set; }
    }
}
