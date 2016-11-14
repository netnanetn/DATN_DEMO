using Competitiveness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Competitiveness.Controllers
{
    public class BranchController : Controller
    {
        CompetitivenessIndexEntities db = new CompetitivenessIndexEntities();
        // GET: Branch
        public ActionResult Index()
        {
            var model = from branch in db.Branchs
                        where 1 == 1
                        select branch;
            return View(model);
        }

        // GET: Branch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Branch/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branch/Create
        [HttpPost]
        public ActionResult Create(Branch branch)
        {
            try
            {
                // TODO: Add insert logic here
                var branchSave = new Branch
                {
                    BranchId = branch.BranchId,
                    BranchName = branch.BranchName
                };
                db.Branchs.Add(branchSave);
                db.SaveChanges();

                return RedirectToAction("/Branch/Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Branch/Edit/5
        public ActionResult Edit(int branchId)
        {

            var model = new AllModel();
            var branchs = (from branch in db.Branchs
                           where branch.BranchId == branchId
                           select branch).FirstOrDefault();
            model.branch.Add(branchs);
            var factors = from factor in db.Factors
                          where factor.BranchId.Equals(branchId)
                          select factor;
            foreach (var fa in factors)
            {
                model.factor.Add(fa);
            }

            var criterias = from criteria in db.Criterias
                            where criteria.BranchId.Equals(branchId)
                            select criteria;
            foreach (var criteria in criterias)
            {
                model.criteria.Add(criteria);
            }
            var attributes = from attribute in db.Attributes
                             where attribute.BranchId.Equals(branchId)
                             select attribute;
            foreach (var attribute in attributes)
            {
                model.attribute.Add(attribute);
            }

            return View(model);
        }

        // POST: Branch/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //EditBranch
        public ActionResult EditFactor(int branchId, int factorId)
        {
            var model = new AllModel();
            var factors = from factor in db.Factors
                          where factor.FactorId.Equals(factorId)
                          select factor;
            foreach (var fa in factors)
            {
                model.factor.Add(fa);
            }

            var criterias = from criteria in db.Criterias
                            where criteria.FactorId.Equals(factorId)
                            select criteria;
            foreach (var criteria in criterias)
            {
                model.criteria.Add(criteria);
            }
            var attributes = from attribute in db.Attributes
                             where attribute.FactorId.Equals(factorId)
                             select attribute;
            foreach (var attribute in attributes)
            {
                model.attribute.Add(attribute);
            }
            return View(model);
        }



        public ActionResult CreateFactor(int branchId, int companyId = 0)
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateFactor(int branchId, Factor factor)
        {
            try
            {
                var count = db.Factors.Max(x => x.Id) + 1;
                var factorSave = new Factor
                {
                    BranchId = factor.BranchId,
                    FactorId = count,
                    FactorName = factor.FactorName,
                    Score = factor.Score,
                    Weight = factor.Weight
                };
                db.Factors.Add(factorSave);
                db.SaveChanges();

                return RedirectToAction("/Branch/Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        // GET: Branch/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Branch/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
