using Competitiveness.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Competitiveness.Controllers
{
    public class ChartController : Controller
    {
        CompetitivenessIndexEntities db = new CompetitivenessIndexEntities();
        #region
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        // GET: Chart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Chart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chart/Edit/5
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

        // GET: Chart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chart/Delete/5
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
        #endregion

        [HttpGet]
        public string GetDataJson(int id)
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

            JObject jo = new JObject();

            // Parse json *OBJECT*
            jo = JObject.Parse(json);
            JToken tokenImportantWeight = jo["ImportantWeight"];
            JToken tokenMeanWeight = jo["MeanWeight"];
            var ImportantWeightString = tokenImportantWeight.ToString();
            var MeanWeightString = tokenMeanWeight.ToString();
            var result = ("[" + ImportantWeightString + "," + MeanWeightString + "]").Replace("Axis", "axis").Replace("Value", "value").Replace("\n","").Replace("\r","").Replace('\"','"');
            ChartModel modelChart = new ChartModel();
            modelChart.modelChart = result;
            return result;
        }
    }
}
