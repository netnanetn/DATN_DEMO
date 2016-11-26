using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace Competitiveness.Models
{
    public class BranchModel
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
    }
    public class AttributeModel
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public int FactorId { get; set; }
        public int CriteriaId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public Nullable<double> Score { get; set; }
        public Nullable<double> Weight { get; set; }
    }
    public class CriteriaModel
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public int FactorId { get; set; }
        public int CriteriaId { get; set; }
        public string CriteriaName { get; set; }
        public Nullable<double> Score { get; set; }
        public Nullable<double> Weight { get; set; }
    }
    public class FactorModel
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public int FactorId { get; set; }
        public string FactorName { get; set; }
        public Nullable<double> Score { get; set; }
        public Nullable<double> Weight { get; set; }
    }
    public class AllModel
    {
        public List<Factor> factor { get; set; }
        public List<Criteria> criteria { get; set; }
        public List<Attribute> attribute { get; set; }
        public List<Branch> branch { get; set; }
        public AllModel()
        {
            factor = new List<Factor>();
            criteria = new List<Criteria>();
            attribute = new List<Attribute>();
            branch = new List<Branch>();
        }
    }
    public class ListModel
    {
        public List<AllModel> listModel { get; set; }

        public ListModel()
        {
            listModel = new List<AllModel>();
        }
    }

    public class CompetitivenessJson
    {
        [JsonProperty("axis")]
        public string Axis { get; set; }
        [JsonProperty("value")]
        public Nullable<double> Value { get; set; }

    }
    public class CompetitivenessJsons
    {
        public List<CompetitivenessJson> ImportantWeight { get; set; }
        public List<CompetitivenessJson> MeanWeight { get; set; }
        public CompetitivenessJsons()
        {
            ImportantWeight = new List<CompetitivenessJson>();
            MeanWeight = new List<CompetitivenessJson>();
        }
    }
    public class ChartModel
    {
        public string modelChart { get; set; }
        public ChartModel()
        {

        }
    }
    public class AttributesOfCompanyModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int FactorId { get; set; }
        public int CriteriaId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public Nullable<double> Score { get; set; }
        public Nullable<double> Weight { get; set; }
    }
    public class CriteriasOfCompanyModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int FactorId { get; set; }
        public int CriteriaId { get; set; }
        public string CriteriaName { get; set; }
        public Nullable<double> Score { get; set; }
        public Nullable<double> Weight { get; set; }
    }
    public class FactorsOfCompanyModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int FactorId { get; set; }
        public string FactorName { get; set; }
        public Nullable<double> Score { get; set; }
        public Nullable<double> Weight { get; set; }
    }
    public class CompanyModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }
    }
    public class CreateNewCompanyModel
    {
        //  public List<Branch> Branchs { get; set; }
        public List<SelectListItem> branchItem { get; set; }
        //   public CompanyModel CompanyModel { get; set; }
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }
        public CreateNewCompanyModel()
        {
            // branchItem = new IEnumerable<SelectListItem>();
            //   CompanyModel = new CompanyModel();
            branchItem = new List<SelectListItem>();
        }
    }
    public class AllModelCompany
    {
        public List<FactorsOfCompany> factor { get; set; }
        public List<CriteriasOfCompany> criteria { get; set; }
        public List<AttributesOfCompany> attribute { get; set; }
        public AllModelCompany()
        {
            factor = new List<FactorsOfCompany>();
            criteria = new List<CriteriasOfCompany>();
            attribute = new List<AttributesOfCompany>();

        }
    }
    public class ListCriteriasOfCompany
    {
        public List<FactorsOfCompany> factors { get; set; }
        public ListCriteriasOfCompany()
        {
            factors = new List<FactorsOfCompany>();
        }
    }
}