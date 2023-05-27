using PaymentAPI.Data.Entities;
using PaymentAPI.Data.Repositories.Interfaces;

namespace PaymentAPI.Data.Repositories;

public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
{
    public SubscriptionRepository(AppDbContext context) : base(context)
    {
    }
}
