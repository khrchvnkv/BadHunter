using System.Transactions;

namespace BadHunterTests.Helpers
{
    public static  class Helper
    {
        public static TransactionScope CreateTransactionScope(int seconds = 1)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                TimeSpan.FromSeconds(seconds),
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}