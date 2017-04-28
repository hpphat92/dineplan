using Abp.Application.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DinePlan.DineConnect.Nois
{
    public interface INoisService : IApplicationService
    {

        string SignInPowerBI();

        Task<string> GetTokenFromCode(string code);

        GetPBITokenResponse GetAccessToken(GetPBITokenRequest model);

        GetDatasetsResponse GetListDataset(GetDatasetsRequest model);

        CreateDatasetReposne CreateDataset(CreateDatasetRequest model);

        bool PushRows(PushRowsRequest model);

        bool ClearTable(ClearTableRequest model);
    }


    #region dto

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

    /// <summary>
    /// Get PowerBI TokenResponse
    /// </summary>
    public class GetPBITokenResponse
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken { get; set; }
    }

    /// <summary>
    /// Get Datasets Response From PowerBi Service
    /// </summary>
    public class GetDatasetsResponse
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public GetDatasetsResponse()
        {
            Value = new List<DataSetObject>();
        }

        /// <summary>
        /// List Dataset Id and Name
        /// </summary>
        public List<DataSetObject> Value { get; set; }
    }

    /// <summary>
    /// DataSetObject
    /// </summary>
    public class DataSetObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// AddRowsAPIEnabled
        /// </summary>
        public bool AddRowsAPIEnabled { get; set; }
    }

    /// <summary>
    /// Create dataset request
    /// </summary>
    public class CreateDatasetRequest
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CreateDatasetRequest()
        {
            Tables = new List<TableSchemeObject>();
        }

        /// <summary>
        /// Power Bi AccessToken
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Dataset Name
        /// </summary>
        public string Datasetname { get; set; }

        /// <summary>
        /// List of table
        /// </summary>
        public List<TableSchemeObject> Tables { get; set; }
    }

    /// <summary>
    /// Table Scheme Object
    /// </summary>
    public class TableSchemeObject
    {
        /// <summary>
        /// ctor
        /// </summary>
        public TableSchemeObject()
        {
            Columns = new List<ColumnObj>();
        }

        /// <summary>
        /// Table Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Columns name with format
        /// </summary>
        public List<ColumnObj> Columns { get; set; }
    }

    /// <summary>
    /// Column Obj
    /// </summary>
    public class ColumnObj
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Format
        /// </summary>
        public string Format { get; set; }
    }

    public class PushRowsRequest
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public PushRowsRequest()
        {
            Rows = new List<object>();
        }

        /// <summary>
        /// Dataset Id
        /// </summary>
        public string DatasetId { get; set; }

        /// <summary>
        /// Table Name
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Power Bi AccessToken
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Rows, with json format
        /// </summary>
        public List<object> Rows { get; set; }
    }

    /// <summary>
    /// Clear Table Request
    /// </summary>
    public class ClearTableRequest
    {
        /// <summary>
        /// Power Bi Access Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Dataset Id
        /// </summary>
        public string DatasetId { get; set; }

        /// <summary>
        /// Table Name
        /// </summary>
        public string TableName { get; set; }
    }

    /// <summary>
    /// Create Dataset Reposne
    /// </summary>
    public class CreateDatasetReposne
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Default Retention Policy
        /// </summary>
        public string DefaultRetentionPolicy { get; set; }

        /// <summary>
        /// Add Rows API Enabled
        /// </summary>
        public bool AddRowsAPIEnabled { get; set; }
    }
    #endregion


}
