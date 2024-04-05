using CorsoNetCore.Models.DataTypes.Enums;

namespace CorsoNetCore.Models.DataTypes
{
    public class Money
    {
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj != null)
            {
                var moneyToCheck = (Money)obj;
                return Currency == moneyToCheck.Currency && Amount == moneyToCheck.Amount;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Currency} {Amount:#.00}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }
    }
}