namespace StateStreet.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountTransaction")]
    public partial class AccountTransaction
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [StringLength(10)]
        public string TransactionType { get; set; }

        public decimal? Amount { get; set; }
    }
}
