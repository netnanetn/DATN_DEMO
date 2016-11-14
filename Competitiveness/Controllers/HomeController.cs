﻿using Competitiveness.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
        public JsonResult GetDataJson(int id=1)
        {
            var model = new AllModel();
            var branchs = (from branch in db.Branchs
                           where 1 == 1
                           select branch);
            foreach (var branch in branchs)
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

            CompetitivenessJsons jsonObject = new CompetitivenessJsons();
            foreach (var fa in factors)
            {
                var JsonImportantWeight = new CompetitivenessJson
                {
                    Axis = fa.FactorName,
                    Value = (Double)fa.Score
                };
                jsonObject.ImportantWeight.Add(JsonImportantWeight);
            }

            var MeanWeightFactor = 0.00;
            var countFactor = factors.Count();
                 foreach (var fa in factors)
            {
                MeanWeightFactor += (Double)fa.Score;
            }
            MeanWeightFactor = MeanWeightFactor / countFactor;
            foreach (var fa in factors)
            {
                var JsonImportantWeight = new CompetitivenessJson
                {
                    Axis = fa.FactorName,
                    Value = MeanWeightFactor
                };
                jsonObject.MeanWeight.Add(JsonImportantWeight);
            }
            string json = new JavaScriptSerializer().Serialize(jsonObject);
            return Json(json);
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