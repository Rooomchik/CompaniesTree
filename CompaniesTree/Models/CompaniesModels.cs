using System.Collections.Generic;
using CompaniesTree.DAL;

namespace CompaniesTree.Models
{
    public class CompaniesModel
    {
        internal CompaniesModel(Companies companies)
        {
            this.Id = companies.Id;
            this.ParentId = companies.ParentId;
            this.Title = companies.Title;
            this.Earnings = companies.Earnings;
            this.TotalEarnings = companies.TotalEarnings;
        }

        public int Id {get; set; }
        public int? ParentId {get; set; }
        public int? Earnings { get; set; }
        public int? TotalEarnings { get; set; }
        public string Title {get;set;}
    }

    public class CompaniesListModel
    {
        public int? Seed { get; set; }
        public IEnumerable<CompaniesModel> Companies { get; set; }
    }

    
}
