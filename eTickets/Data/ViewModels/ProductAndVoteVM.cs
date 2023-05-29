using System.Collections.Generic;
using System.Linq;
using eTickets.Models;

namespace eTickets.Data.ViewModels
{
    public class ProductAndVoteVM
    {
        public Product Product { get; set; }
        public IQueryable<AuctionVote> AuctionVote { get; set; }
        public AuctionVote AuctionVoteRegister { get; set; }
        public ApplicationUser User { get; set; }
    }
}
