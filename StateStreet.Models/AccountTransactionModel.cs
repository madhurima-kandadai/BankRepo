namespace StateStreet.Models
{
    public class AccountTransactionModel
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public string TransactionType { get; set; }

        public decimal? Amount { get; set; }
    }
}