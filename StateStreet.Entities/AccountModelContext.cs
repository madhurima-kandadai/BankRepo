namespace StateStreet.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AccountModelContext : DbContext
    {
        public AccountModelContext()
            : base("name=AccountModel")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountTransaction> AccountTransactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountTransaction>()
                .Property(e => e.TransactionType)
                .IsFixedLength();
        }
    }
}
