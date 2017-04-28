using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;


namespace DinePlan.DineConnect.House
{
    [Table("Suppliers")]
    public class Supplier : FullAuditedEntity, IMustHaveTenant
    {
        [Required]
        [MaxLength(60)]
        public virtual string SupplierName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual string Country { get; set; }

        public string ZipCode { get; set; }

        public string PhoneNumber1 { get; set; }

        public int DefaultCreditDays { get; set; }

        public string OrderPlacedThrough { get; set; }

        public string Email { get; set; }

        public string FaxNumber { get; set; }

        public string Website { get; set; }

        public virtual string TaxRegistrationNumber { get; set; }
        public virtual int TenantId { get; set; }
    }
}
