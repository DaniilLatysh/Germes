namespace Trade.Helpers
{
    public static class CurrencyMaker
    {
        public static double MakePriceSale(double priceIn, double markup, double currency)
        {
            if (priceIn > 0 && markup > 0)
            {
                return (priceIn + ((priceIn * markup) / 100)) * currency;
            }
            return 0;
        }
    }
}