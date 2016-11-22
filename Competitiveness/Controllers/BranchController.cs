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
        public ActionResult Edit(AllModel allModel)
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
        [HttpGet]
        public ActionResult EditFactorPartial(int Id)
        {
            Factor fc = new Factor();
            fc = db.Factors.Find(Id);
            return PartialView("EditFactorPartial", fc);
        }
        [HttpPost]
        public ActionResult EditFactorPartial(Factor factor)
        {
            try
            {
                Factor fc = db.Factors.Find(factor.Id);
                fc.FactorName = factor.FactorName;
                fc.Score = factor.Score;
                fc.Weight = factor.Weight;
                db.SaveChanges();
              return  RedirectToAction("Edit", new { branchId = factor.BranchId});
            }catch(Exception ex)
            {
                return PartialView("EditFactorPartial", factor);
            }
        }

        [HttpGet]
        public ActionResult EditCriteriaPartial(int Id)
        {
            Criteria cr = new Criteria();
            cr = db.Criterias.Find(Id);
            return PartialView("EditCriteriaPartial",cr);
        }
        [HttpPost]
        public ActionResult EditCriteriaPartial(Criteria criteria)
        {
            try
            {
                Criteria cr = db.Criterias.Find(criteria.Id);
                cr.CriteriaName = criteria.CriteriaName;
                cr.Score = criteria.Score;
                cr.Weight = criteria.Weight;
                db.SaveChanges();
                return RedirectToAction("Edit", new { branchId = criteria.BranchId });
            }
            catch (Exception ex)
            {
                return PartialView("EditCriteriaPartial", criteria);
            }
        }


        [HttpGet]
        public ActionResult EditAttributePartial(int Id)
        {
            Attribute at = new Attribute();
            at = db.Attributes.Find(Id);
            return PartialView("EditAttributePartial", at);
        }
        [HttpPost]
        public ActionResult EditAttributePartial(Attribute attribute)
        {
            try
            {
                Attribute at = db.Attributes.Find(attribute.Id);
                at.AttributeName = attribute.AttributeName;
                at.Score = attribute.Score;
                at.Weight = attribute.Weight;
                db.SaveChanges();
                return RedirectToAction("Edit", new { branchId = attribute.BranchId});
            }
            catch (Exception ex)
            {
                return PartialView("EditAttributePartial", attribute);
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

        public ActionResult EditCriteria(int branchId, int factorId, int criteriaId)
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
                            where criteria.CriteriaId.Equals(criteriaId)
                            select criteria;
            foreach (var criteria in criterias)
            {
                model.criteria.Add(criteria);
            }
            var attributes = from attribute in db.Attributes
                             where attribute.CriteriaId.Equals(criteriaId)
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

        public ActionResult CreateCriteria(int branchId, int factorId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCriteria(Criteria criteria)
        {
            try
            {
                var count = db.Criterias.Max(x => x.Id) + 1;
                var criteriaSave = new Criteria
                {
                    BranchId = criteria.BranchId,
                    FactorId = criteria.FactorId,
                    CriteriaId = count,
                    CriteriaName = criteria.CriteriaName,
                    Score = criteria.Score,
                    Weight = criteria.Weight
                };
                db.Criterias.Add(criteriaSave);
                db.SaveChanges();
                return RedirectToAction("EditFactor", "Branch", new { branchId = criteria.BranchId, factorId = criteria.FactorId });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult CreateAttribute(int branchId, int factorId, int criteriaId )
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAttribute(Attribute attribute)
        {
            try
            {
                var count = db.Attributes.Max(x => x.Id) + 1;
                var attributeSave = new Attribute
                {
                    BranchId = attribute.BranchId,
                    FactorId = attribute.FactorId,
                    CriteriaId = attribute.CriteriaId,
                    AttributeId = count,
                    AttributeName = attribute.AttributeName,
                    Score = attribute.Score,
                    Weight = attribute.Weight
                };
                db.Attributes.Add(attributeSave);
                db.SaveChanges();
                return RedirectToAction("EditCriteria", "Branch", new { branchId = attribute.BranchId, factorId = attribute.FactorId, criteriaId = attribute.CriteriaId });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Branch/Delete/5
        public ActionResult DeleteFactor(int id)
        {
            return View();
        }

        // POST: Branch/Delete/5
        [HttpPost]
        public ActionResult DeleteFactor(int id, FormCollection collection)
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
