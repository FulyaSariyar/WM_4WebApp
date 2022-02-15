using ItServiceApp.Business.Repository.Abstracts;
using ItServiceApp.Core.Entities;
using ItServiceApp.Data;
using System;

namespace ItServiceApp.Business.Repository
{
    public class SubscriptionsTypeRepo : BaseRepository<SubscriptionType, Guid>
    {
        public SubscriptionsTypeRepo(MyContext context) : base(context)
        {

        }
    }
 
}
