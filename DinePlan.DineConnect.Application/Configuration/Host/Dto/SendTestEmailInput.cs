using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using DinePlan.DineConnect.Authorization.Users;

namespace DinePlan.DineConnect.Configuration.Host.Dto
{
    public class SendTestEmailInput
    {
        [Required]
        [MaxLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}