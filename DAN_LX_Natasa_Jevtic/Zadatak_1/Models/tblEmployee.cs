//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zadatak_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblEmployee()
        {
            this.tblEmployee1 = new HashSet<tblEmployee>();
        }
    
        public int EmployeeId { get; set; }
        public string NameAndSurname { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string NumberOfIdentityCard { get; set; }
        public string JMBG { get; set; }
        public int Gender { get; set; }
        public string PhoneNumber { get; set; }
        public int Sector { get; set; }
        public int LocationId { get; set; }
        public Nullable<int> Manager { get; set; }
    
        public virtual tblGender tblGender { get; set; }
        public virtual tblLocation tblLocation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblEmployee> tblEmployee1 { get; set; }
        public virtual tblEmployee tblEmployee2 { get; set; }
        public virtual tblSector tblSector { get; set; }
    }
}