using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinePlan.DineConnect.WebApi.Models.Nois
{
    /// <summary>
    /// Get PowerBI Token Request
    /// </summary>
    public class GetPBITokenRequest
    {
        /// <summary>
        /// UserName
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        [Required]
        public string TetantId { get; set; }
    }
}
