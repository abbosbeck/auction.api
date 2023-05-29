using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class AuctionVotesService : EntityBaseRepository<AuctionVote>, IAuctionVotesService
    {
        private readonly AppDbContext _context;
        public AuctionVotesService(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public IQueryable<AuctionVote> GetAllVotes(Expression<Func<AuctionVote, bool>> expression)
        {
            var allVotes = _context.AuctionVotes.Where(expression);
            return allVotes;
        }
    }
}
