using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using DinePlan.DineConnect.Dto;
namespace DinePlan.DineConnect.House.Master.Dtos
{
    [AutoMapFrom(typeof(Supplier))]
    public class SupplierListDto : FullAuditedEntityDto
    {
        //TODO: DTO Supplier Properties Missing
        //YOU CAN REFER ATTRIBUTES IN 
        public virtual string SupplierName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber1 { get; set; }
        public int DefaultCreditDays { get; set; }
        public string OrderPlacedThrough { get; set; }
        public string Email { get; set; }
        public string FaxNumber { get; set; }
        public string Website { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string TallySupplierName { get; set; }
        public virtual string TaxRegistrationNumber { get; set; }
    }
    [AutoMapTo(typeof(Supplier))]
    public class SupplierEditDto
    {
        public int? Id { get; set; }
        //TODO: DTO Supplier Properties Missing
        public virtual string SupplierName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber1 { get; set; }
        public int DefaultCreditDays { get; set; }
        public string OrderPlacedThrough { get; set; }
        public string Email { get; set; }
        public string FaxNumber { get; set; }
        public string Website { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string TallySupplierName { get; set; }
        public virtual string TaxRegistrationNumber { get; set; }
    }

    public class GetSupplierInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }

        public string Operation { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
    public class GetSupplierForEditOutput : IOutputDto
    {
        public SupplierEditDto Supplier { get; set; }
    }
    public class CreateOrUpdateSupplierInput : IInputDto
    {
        
        public SupplierEditDto Supplier { get; set; }
    }

    public enum DocumentType
    {
        PO,
        DC,
        INVOICE
    };

}

