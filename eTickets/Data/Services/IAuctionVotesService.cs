using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Models;
using System.Linq;

namespace eTickets.Data.Services
{
    public interface IAuctionVotesService : IEntityBaseRepository<AuctionVote>
    {
        IQueryable<AuctionVote> GetAllVotes(Expression<Func<AuctionVote, bool>> expression);
    }
}
