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
    
    public partial class Factors
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public int FactorId { get; set; }
        public string FactorName { get; set; }
        public Nullable<double> Score { get; set; }
        public Nullable<double> Weight { get; set; }
    }
}
