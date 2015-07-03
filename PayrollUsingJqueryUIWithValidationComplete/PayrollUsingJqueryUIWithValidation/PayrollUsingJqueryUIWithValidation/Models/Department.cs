//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PayrollUsingJqueryUIWithValidation.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Department
    {
        public Department()
        {
            this.Employees = new HashSet<Employee>();
        }
    
        public int Department_Id { get; set; }
        [Required(ErrorMessage = "Code is required.")]
        [StringLength(10, MinimumLength = 2)]
        [Remote("IsCodeExist", "Department", ErrorMessage = "This code already Exits")]
        [Display(Name = "Department Code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(40, ErrorMessage = "Maximum 40 characters.")]
        [Remote("IsNameExist", "Department", ErrorMessage = "This name already Exits")]
       
        public string Name { get; set; }
    
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
