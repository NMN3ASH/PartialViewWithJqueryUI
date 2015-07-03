//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PayrollUsingJqueryUIWithValidation.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public Employee()
        {
            this.Payments = new HashSet<Payment>();
            this.Salaries = new HashSet<Salary>();
        }
    
        public int Employee_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Address1 { get; set; }
        public Nullable<int> Department_Id { get; set; }
        public Nullable<System.DateTime> Join_date { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
