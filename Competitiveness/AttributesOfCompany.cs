//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Competitiveness
{
    using System;
    using System.Collections.Generic;
    
    public partial class AttributesOfCompany
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
}
