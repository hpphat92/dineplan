using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinePlan.DineConnect.WebApi.Models.Nois
{
    /// <summary>
    /// Get Datasets Request
    /// </summary>
    public class GetDatasetsRequest
    {
        /// <summary>
        /// Power Bi AccessToken
        /// </summary>
        public string Token { get; set; }
    }
}
