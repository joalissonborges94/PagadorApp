

namespace PagadorApp.Models.Enum
{
    public enum StatusTransacaoEnum
    {
        Active = 1,
        Finished = 2,
        DisabledByMerchant = 3,
        DisabledMaxAttempts = 4,
        DisabledExpiredCreditCard = 5,
    }
}
