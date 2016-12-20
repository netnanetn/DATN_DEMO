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
    public class CompanyController : Controller
    {
        CompetitivenessIndexEntities db = new CompetitivenessIndexEntities();
        // GET: Company
        public ActionResult Index()
        {
            var companies = db.Companies.ToList();
            return View(companies);
        }

        [HttpGet]
        public ActionResult PartialViewCreate()
        {
            CreateNewCompanyModel company = new CreateNewCompanyModel();

            var items = (from m in db.Branchs
                         select new SelectListItem
                         {
                             Value = m.Id.ToString(),
                             Text = m.BranchName
                         }); ;
            foreach (var item in items)
            {
                company.branchItem.Add(item);
            }

            // YourViewModel.Model1Items = new SelectList(items.ToList(), "Value", "Text");
            return PartialView("PartialViewCreate", company);
        }
        [HttpPost]
        public ActionResult PartialViewCreate(CreateNewCompanyModel newCompany)
        {
            try
            {
                //Math.Round((double)(attributeScore), 4, MidpointRounding.AwayFromZero);
                Random randNum = new Random();
                if (String.IsNullOrEmpty(newCompany.Name))
                {
                    return RedirectToAction("Index");
                }
                var companyId = 0;
                if (db.Companies.Any())
                {
                    companyId = db.Companies.Max(x => x.Id) + 1;
                }


                var branchId = newCompany.BranchId;
                Company company = new Company()
                {
                    BranchId = branchId,
                    Name = newCompany.Name,
                    CompanyId = companyId
                };
                db.Companies.Add(company);
                var factors = db.Factors.Where(x => x.BranchId == branchId);
                foreach (var factor in factors)
                {
                    FactorsOfCompany factorOfCompany = new FactorsOfCompany
                    {
                        CompanyId = companyId,
                        FactorId = factor.FactorId,
                        FactorName = factor.FactorName,
                        Score = factor.Score,
                        Weight = Math.Round((double)(factor.Weight), 4, MidpointRounding.AwayFromZero)
                 
                    };
                    db.FactorsOfCompanies.Add(factorOfCompany);
                }
                var criterias = db.Criterias.Where(x => x.BranchId == branchId);
                foreach (var criteria in criterias)
                {
                    CriteriasOfCompany criteriaOfCompany = new CriteriasOfCompany
                    {
                        CompanyId = companyId,
                        FactorId = criteria.FactorId,
                        CriteriaId = criteria.CriteriaId,
                        CriteriaName = criteria.CriteriaName,
                        Score = criteria.Score,
                        Weight = Math.Round((double)(criteria.Weight), 4, MidpointRounding.AwayFromZero)
                 
                    };
                    db.CriteriasOfCompanies.Add(criteriaOfCompany);
                }
                var attributes = db.Attributes.Where(x => x.BranchId == branchId);
                foreach (var attribute in attributes)
                {
                    AttributesOfCompany attributesOfCompany = new AttributesOfCompany
                    {
                        CompanyId = companyId,
                        FactorId = attribute.FactorId,
                        CriteriaId = attribute.CriteriaId,
                        AttributeId = attribute.AttributeId,
                        AttributeName = attribute.AttributeName,
                        Score = attribute.Score,
                        Weight = Math.Round((double)(attribute.Weight * (0.5 + randNum.NextDouble() * 0.8)), 4, MidpointRounding.AwayFromZero)
                   
                    };
                    db.AttributesOfCompanies.Add(attributesOfCompany);
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("PartialViewCreate", newCompany);
            }
        }

        [HttpGet]
        public ActionResult DetailsOfCompany(int companyId)
        {
            var model = new AllModelCompany();
            var factors = db.FactorsOfCompanies.Where(x => x.CompanyId == companyId);
            model.factor.AddRange(factors);

            var criterias = db.CriteriasOfCompanies.Where(x => x.CompanyId == companyId);
            model.criteria.AddRange(criterias);

            var attributes = db.AttributesOfCompanies.Where(x => x.CompanyId == companyId);
            model.attribute.AddRange(attributes);

            return View(model);
        }

        [HttpGet]
        public ActionResult EditManyAttribute(int companyId)
        {
            var model = new AllModelCompany();
            var factors = db.FactorsOfCompanies.Where(x => x.CompanyId == companyId);
            model.factor.AddRange(factors);

            var criterias = db.CriteriasOfCompanies.Where(x => x.CompanyId == companyId);
            model.criteria.AddRange(criterias);

            var attributes = db.AttributesOfCompanies.Where(x => x.CompanyId == companyId);
            model.attribute.AddRange(attributes);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditManyAttribute(AllModelCompany allModel)
        {
            var CompanyId = 0;
            try
            {
                CompanyId = allModel.factor[0].CompanyId;
                foreach (var factor in allModel.factor)
                {
                    var fc = db.FactorsOfCompanies.Find(factor.Id);
                    fc.FactorName = factor.FactorName;
                    fc.Weight = factor.Weight;
                }
                foreach (var criteria in allModel.criteria)
                {
                    var cr = db.CriteriasOfCompanies.Find(criteria.Id);
                    cr.CriteriaName = criteria.CriteriaName;
                    cr.Weight = criteria.Weight;
                }
                foreach (var attribute in allModel.attribute)
                {
                    var at = db.AttributesOfCompanies.Find(attribute.Id);
                    at.AttributeName = attribute.AttributeName;
                    at.Weight = attribute.Weight;
                }
                db.SaveChanges();
                return RedirectToAction("DetailsOfCompany", "Company", new { CompanyId = CompanyId });
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult SyncData(int companyId = 0)
        {

            var factors = db.FactorsOfCompanies.Where(x => x.CompanyId == companyId);
            var totalWeightFactor = factors.Sum(x => x.Weight);

            foreach (var factor in factors)
            {
                var weightOfFactor = (double)(factor.Weight / totalWeightFactor);//trọng lượng của từng yếu tố theo %
                var criterias = db.CriteriasOfCompanies.Where(x => x.CompanyId == companyId && x.FactorId == factor.FactorId);
                var totalWeightCriteria = criterias.Sum(x => x.Weight);


                foreach (var criteria in criterias)
                {
                    var weightOfCriteria = (double)(criteria.Weight / totalWeightCriteria);//trọng lượng của từng tiêu chí theo %
                    var attributes = db.AttributesOfCompanies.Where(x => x.CompanyId == companyId && x.CriteriaId == criteria.CriteriaId && x.FactorId == factor.FactorId);
                    var totalWeightAttribute = attributes.Sum(x => x.Weight);

                    foreach (var attribute in attributes)
                    {
                        var weightOfAttribute = (double)(attribute.Weight / totalWeightAttribute);
                        var attributeScore = attribute.Weight * 5 * weightOfAttribute * weightOfCriteria * weightOfFactor;
                        attribute.Score = Math.Round((double)(attributeScore), 4, MidpointRounding.AwayFromZero);
                    }
                    criteria.Score = attributes.Sum(x => x.Score);
                }
                factor.Score = criterias.Sum(x => x.Score);

            }
            
            db.SaveChanges();

            return RedirectToAction("DetailsOfCompany", "Company", new { companyId = companyId });
        }
        [HttpGet]
        public ActionResult DeleteCompany(int companyId)
        {
            try
            {
                Company cp = db.Companies.Where(x=>x.CompanyId == companyId).SingleOrDefault();
                var factors = db.FactorsOfCompanies.Where(x => x.CompanyId == cp.CompanyId);
                var criterias = db.CriteriasOfCompanies.Where(x => x.CompanyId == cp.CompanyId);
                var attributes = db.AttributesOfCompanies.Where(x => x.CompanyId == cp.CompanyId);
                // đoạn này mở sau khi làm phần xác nhận đã
                db.AttributesOfCompanies.RemoveRange(attributes);
                db.CriteriasOfCompanies.RemoveRange(criterias);
                db.FactorsOfCompanies.RemoveRange(factors);
                db.Companies.Remove(cp);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditCompanyPartial(int companyId)
        {
            Company company = new Company();
            company = db.Companies.Where(x=>x.CompanyId == companyId).SingleOrDefault();
            return PartialView("EditCompanyPartial", company);
        }
        [HttpPost]
        public ActionResult EditCompanyPartial(Company company)
        {
            try
            {
                Company cp = db.Companies.Where(x=>x.CompanyId == company.CompanyId).SingleOrDefault();
                cp.Name = company.Name;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return PartialView("EditBranchPartial", company);
            }
        }
        
        [HttpGet]
        public ActionResult CompareChart()
        {
            var companies = db.Companies.ToList();
            return View(companies);
        }
        [HttpGet]
        public ActionResult CompareChartCompany(int companyId)
        {
            ListCriteriasOfCompany listModel = new ListCriteriasOfCompany();
            var factors = db.FactorsOfCompanies.Where(x => x.CompanyId == companyId);
            listModel.factors.AddRange(factors);
            return View(listModel);
        }
        [HttpGet]
        public string GetDataOfCriteriaCompareJson(int companyId, int factorId)
        {
            CompetitivenessJsons jsonObject = new CompetitivenessJsons();
            var company = db.Companies.Where(x => x.CompanyId == companyId).SingleOrDefault();
            var criteriaNormals = db.Criterias.Where(x=>x.FactorId == factorId && x.BranchId == company.BranchId);

            foreach(var criteriaNomal in criteriaNormals)
            {
                var JsonScoreNormal = new CompetitivenessJson
                {
                    Axis = criteriaNomal.CriteriaName,
                    Value = (Double)criteriaNomal.Score
                };
                jsonObject.MeanWeight.Add(JsonScoreNormal);
            }

            var criteriaCompanys = db.CriteriasOfCompanies.Where(x=>x.CompanyId == companyId && x.FactorId == factorId);
             foreach(var criteriaCompany in criteriaCompanys)
            {
                var JsonScoreCompany = new CompetitivenessJson
                {
                    Axis = criteriaCompany.CriteriaName,
                    Value = (Double)criteriaCompany.Score
                };
                jsonObject.ImportantWeight.Add(JsonScoreCompany);
            }

            string json = new JavaScriptSerializer().Serialize(jsonObject);

            JObject jo = new JObject();

            // Parse json *OBJECT*
            jo = JObject.Parse(json);
            JToken tokenImportantWeight = jo["ImportantWeight"];
            JToken tokenMeanWeight = jo["MeanWeight"];
            var ImportantWeightString = tokenImportantWeight.ToString();
            var MeanWeightString = tokenMeanWeight.ToString();
            var result = ("[" + ImportantWeightString + "," + MeanWeightString + "]").Replace("Axis", "axis").Replace("Value", "value").Replace("\n", "").Replace("\r", "").Replace('\"', '"');
            ChartModel modelChart = new ChartModel();
            modelChart.modelChart = result;
            return result;
        }
        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
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

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Company/Edit/5
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

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Company/Delete/5
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
