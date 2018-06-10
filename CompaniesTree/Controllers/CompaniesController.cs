using CompaniesTree.Filters;
using CompaniesTree.DAL;
using System.Web.Mvc;
using CompaniesTree.BusinessLogic;

namespace CompaniesTree.Controllers
{
    [InitializeSimpleCompaniesDB]
    public class CompaniesController : Controller
    {
        private Logic BusinessLogic { get; set; }
        public CompaniesController()

        {
            BusinessLogic = new Logic();
        }
        public ActionResult Index()
        {
            return View(BusinessLogic.GetCompanies());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int? parentId, string title, int? earnings)
        {
            BusinessLogic.AddCompanies(parentId, title, earnings);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int? nodeIdToUpdate, string titleToUpdate, int? earningsToUpdate)
        {
            BusinessLogic.UpdateCompanies(nodeIdToUpdate, titleToUpdate, earningsToUpdate);

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Move(int nodeId, int? newParentId)
        {
            if (nodeId == newParentId)
            {
                return RedirectToAction("Index");
            }

            using (CompaniesDAL dal = new CompaniesDAL())
            {
                if (newParentId.HasValue && BusinessLogic.ContainsChilds(dal, nodeId, newParentId.Value))
                {
                    return RedirectToAction("Index");
                }
            }
                BusinessLogic.MoveCompany(nodeId, newParentId);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (CompaniesDAL context = new CompaniesDAL())
            {
                BusinessLogic.DeleteNodes(context, id);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
