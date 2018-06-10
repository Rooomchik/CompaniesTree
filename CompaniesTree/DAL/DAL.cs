using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CompaniesTree.DAL
{
    public class CompaniesDAL : DbContext
    {
        public CompaniesDAL()
            : base("DefaultConnection")
        {
        }

        public DbSet<Companies> Companies { get; set; }
    }

    [Table("Companies")]
    public class Companies
    {
        public Companies()
        {
            IsDeleted = false;
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public int? Earnings { get; set; }
        public int? TotalEarnings { get; set; }
        public bool IsDeleted { get; set; }
    }
}