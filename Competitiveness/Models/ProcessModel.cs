using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    public  class CriteriaModel
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
}