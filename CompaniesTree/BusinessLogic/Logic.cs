using System.Linq;
using CompaniesTree.DAL;
using CompaniesTree.Models;

namespace CompaniesTree.BusinessLogic
{
    public class Logic
    {
        public void DeleteNodes(CompaniesDAL dal, int id)
        {
            Companies[] companiesList = dal.Companies.Where(x => x.ParentId == id && !x.IsDeleted).ToArray();

            foreach (Companies node in companiesList)
            {
                node.IsDeleted = true;
                DeleteNodes(dal, node.Id);
            }
            var deleted = dal.Companies.Where(x => x.Id == id && !x.IsDeleted).Single();
            deleted.IsDeleted = true;
        }


        public bool ContainsChilds(CompaniesDAL dal, int parentId, int id)
        {
            bool presenceOfChild = false;

            Companies[] companiesList = dal.Companies.Where(x => x.ParentId == parentId && !x.IsDeleted).ToArray();

            foreach (var node in companiesList)
            {
                if (node.Id == id && node.ParentId == parentId)
                {
                    return true;
                }
                presenceOfChild = ContainsChilds(dal, node.Id, id);
            }

            return presenceOfChild;
        }

        public CompaniesListModel GetCompanies()
        {
            using (CompaniesDAL dal = new CompaniesDAL())
            {
                Companies[] Companies = dal.Companies.Where(x => !x.IsDeleted).ToArray();
                foreach (Companies company in Companies)
                {
                    int?[] totalChildEarnings = dal.Companies.Where(x => x.ParentId == company.Id).Select(x => x.Earnings).ToArray();
                    company.TotalEarnings = totalChildEarnings.Sum() + company.Earnings;
                }

                CompaniesListModel model = new CompaniesListModel()
                {
                    Companies = Companies.Select(x => new CompaniesModel(x))
                };

                return model;
            }
        }
        public void AddCompanies(int? parentId, string title, int? earnings)
        {
            using (CompaniesDAL dal = new CompaniesDAL())
            {
                Companies company = new Companies()
                {
                    ParentId = parentId,
                    Title = title,
                    Earnings = earnings
                };
                dal.Companies.Add(company);
                dal.SaveChanges();
            }
        }
        public void UpdateCompanies(int? nodeId, string title, int? earnings)
        {
            using (CompaniesDAL dal = new CompaniesDAL())
            {
                Companies company = dal.Companies.Where(x => x.Id == nodeId).Single();
                company.Title = title;
                company.Earnings = earnings;
                dal.SaveChanges();
            }
        }
        public void MoveCompany(int nodeId, int? newParentId)
        {
            using (CompaniesDAL dal = new CompaniesDAL())
            {
                Companies company = dal.Companies.Where(x => x.Id == nodeId).Single();
                company.ParentId = newParentId;
                dal.SaveChanges();
            }
        }
    }
}