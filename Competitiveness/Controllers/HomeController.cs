using Competitiveness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Competitiveness.Controllers
{
    public class HomeController : Controller
    {
        CompetitivenessIndexEntities db = new CompetitivenessIndexEntities();

        public ActionResult Index()
        {
    
            return View();
        }
        public ActionResult BuildingIndex()
        {
            // var model = new AllModel();
            // foreach(var s in db.Branchs)
            // {
            //     var branchmodel = new BranchModel
            //     {
            //         Id = s.Id,
            //         BranchName = s.BranchName,
            //         BranchId = s.BranchId
            //     };
            //     model.branchmodel.Add(branchmodel);
            // }
            //foreach(var fac in db.Factors)
            // {
            //     var factor = new FactorModel
            //     {
            //         Id = fac.Id,
            //         BranchId = fac.BranchId,
            //         CompanyId = fac.CompanyId,
            //         FactorId = fac.FactorId,
            //         FactorName = fac.FactorName,
            //         Score = fac.Score,
            //         Weight = fac.Weight
            //     };
            //     model.factor.Add(factor);
            // }
            var model = from branch in db.Branchs
                        where 1 == 1
                        select branch;
            return View(model);
        }
      
        public ActionResult PartialViewFactor(int id=0)
        {
            var model = new AllModel();
            var branchId = (from branch in db.Branchs
                           where branch.BranchId == id
                           select branch.BranchId).FirstOrDefault();

            var factors = from factor in db.Factors
                          where factor.BranchId.Equals(id)
                          select factor;
            foreach(var fa in factors)
            {
                model.factor.Add(fa);
            }

            var criterias = from criteria in db.Criterias
                            where criteria.BranchId.Equals(id)
                            select criteria;
            foreach(var criteria in criterias)
            {
                model.criteria.Add(criteria);
            }
            var attributes = from attribute in db.Attributes
                            where attribute.BranchId.Equals(id)
                            select attribute;
            foreach (var attribute in attributes)
            {
                model.attribute.Add(attribute);
            }


            return PartialView("PartialViewFactor",model);
        }
        public ActionResult Chart(int id =1)
        {
           
            var model = new AllModel();
            var branchs = (from branch in db.Branchs
                            where 1==1
                            select branch);
            foreach(var branch in branchs)
            {
                model.branch.Add(branch);
            }

            var factors = from factor in db.Factors
                          where factor.BranchId.Equals(id)
                          select factor;
            foreach (var fa in factors)
            {
                model.factor.Add(fa);
            }

            var criterias = from criteria in db.Criterias
                            where criteria.BranchId.Equals(id)
                            select criteria;
            foreach (var criteria in criterias)
            {
                model.criteria.Add(criteria);
            }
            var attributes = from attribute in db.Attributes
                             where attribute.BranchId.Equals(id)
                             select attribute;
            foreach (var attribute in attributes)
            {
                model.attribute.Add(attribute);
            }
            return View(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}