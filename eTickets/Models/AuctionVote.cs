using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eTickets.Data.Base;

namespace eTickets.Models
{
    public class AuctionVote : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Something went wrong! Check log in")]
        public string FullName { get; set; }
        [Display(Name = "Price")]
        public double Price { get; set; }
        
        //Reletionships
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
