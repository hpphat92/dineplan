using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DinePlan.DineConnect.WebApi.Controllers
{
    public class NoisController : DineConnectApiControllerBase
    {
        #region webconfig
        private readonly string _clientId = "19679192-afd1-4be4-b28a-94b565fe349f";
        //private readonly string _clientSecrect = "GWumjvLxN2ePmkinK1qoMPSCzlkFy+vwnTJ9sz8SL1Q=";
        private readonly string _clientSecrect = "XWgwx9jEtsipSfdab0m/D5Je5gUccbEhefurfcFHBvo=";
        private readonly string _powerBIApi = "https://analysis.windows.net/powerbi/api";
        private readonly string _powerBiDataset = "https://api.powerbi.com/v1.0/myorg/datasets";
        private readonly string _authUrl = "https://login.windows.net/common/oauth2/authorize/";
        private readonly string _redirectUrl = "http://localhost:6240";
        #endregion

        #region
        #endregion

        #region services
        private GetDatasetsResponse GetDatasetIds(string token)
        {
            // execute api
            var client = new RestClient(_powerBiDataset);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", token));
            // deserialize reponse
            var response = client.Execute(request);
            var resContent = new GetDatasetsResponse();
            if (!string.IsNullOrEmpty(response.Content))
                resContent = JsonConvert.DeserializeObject<GetDatasetsResponse>(response.Content);

            return resContent;
        }

        /// <summary>
        /// Get token
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="userName">UserName</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        private string GetToken(string tenantId, string userName, string password)
        {
            // url to get access token via password
            string url = $"https://login.windows.net/{tenantId}/oauth2/token";
            var paramStr =
                "client_id=" + HttpUtility.UrlEncode(_clientId) +
                "&client_secret=" + HttpUtility.UrlEncode(_clientSecrect) +
                "&username=" + userName +
                "&password=" + password +
                "&grant_type=password" +
                "&resource=https://analysis.windows.net/powerbi/api";

            // request access token
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            // attach body
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", paramStr, ParameterType.RequestBody);
            // execute request
            var response = client.Execute(request);
            var resContent = new JObject();
            if (!string.IsNullOrEmpty(response.Content) && response.StatusCode == System.Net.HttpStatusCode.OK)
                resContent = JObject.Parse(response.Content);
            // get access token
            var accessToken = "";
            if (resContent["access_token"] != null)
                accessToken = resContent["access_token"].ToString();

            return accessToken;
        }
        #endregion

        #region ctor
        public NoisController()
        {

        }
        #endregion

        #region models
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

        #region methods
        /// <summary>
        /// Return the URL to Microsoft Auth Page to get Auth Code
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult SignInPowerBI()
        {
            //Create a query string
            //Create a sign-in NameValueCollection for query string
            var @params = new NameValueCollection
            {
                //Azure AD will return an authorization code. 
                //See the Redirect class to see how "code" is used to AcquireTokenByAuthorizationCode
                {"response_type", "code"},

                //Client ID is used by the application to identify themselves to the users that they are requesting permissions from. 
                //You get the client id when you register your Azure app.
                {"client_id", _clientId},

                //Resource uri to the Power BI resource to be authorized
                {"resource", _powerBIApi},

                //After user authenticates, Azure AD will redirect back to the web app
                {"redirect_uri", _redirectUrl}
            };

            //Create sign-in query string
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add(@params);

            //Redirect authority
            //Authority Uri is an Azure resource that takes a client id to get an Access token
            return Ok(string.Format("{0}?{1}", _authUrl, queryString));
        }

        /// <summary>
        /// Get AccessToken From Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetTokenFromCode(string code)
        {
            //Redirect uri must match the redirect_uri used when requesting Authorization code.
            string redirectUri = _redirectUrl;
            string authorityUri = _authUrl;

            // Get auth token from auth code       
            TokenCache TC = new TokenCache();

            AuthenticationContext AC = new AuthenticationContext(authorityUri, TC);

            ClientCredential cc = new ClientCredential(_clientId, _clientSecrect);

            AuthenticationResult AR = await AC.AcquireTokenByAuthorizationCodeAsync(code, new Uri(redirectUri), cc);
            
            // Get Accesstoken
            var accessToken = AR.AccessToken;

            return Ok();
        }


        /// <summary>
        /// Get Access Token for PowerBi
        /// </summary>
        /// <param name="model">GetPBITokenRequest</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetAccessToken([FromBody]GetPBITokenRequest model)
        {
            try
            {
                // Get PowerBI Access Token
                var token = GetToken(model.TetantId, model.UserName, model.Password);

                // Prepair response object
                var res = new GetPBITokenResponse()
                {
                    AccessToken = token
                };

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Get list dataset Id
        /// </summary>
        /// <param name="model">GetDatasetsRequest</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetListDataset(GetDatasetsRequest model)
        {
            try
            {
                var dataSetIds = GetDatasetIds(model.Token);

                return Ok(dataSetIds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Create Dataset
        /// NOTE : table can only be create with dataset, currently there are not exist feature to create table to an existing dataset
        /// </summary>
        /// <param name="model">CreateDatasetRequest</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateDataset(CreateDatasetRequest model)
        {
            // Create Power Request String for Create Dataset
            var listTable = new List<string>();
            foreach (var tableObj in model.Tables)
            {
                var colNameWithFormat = new List<string>();
                foreach (var col in tableObj.Columns)
                {
                    colNameWithFormat.Add("{ \"name\": \"" + col.Name + "\", \"dataType\": \"" + col.Format + "\"}");
                }
                var colNameWithFormatStr = string.Join(",", colNameWithFormat);

                var tableStr = "{" +
                                    "\"name\": \"" + tableObj.Name + "\", " +
                                    "\"columns\":" +
                                        "[" +
                                            colNameWithFormatStr +
                                        "]" +
                                    "}";

                listTable.Add(tableStr);

            }

            #region ref
            //var datasetScheme = "{" +
            //                        $"\"name\": \"{model.Datasetname}\"," +
            //                        "\"tables\":" +
            //                            "[" +
            //                                "{" +
            //                                    "\"name\": \"AuditContentDetails\", " +
            //                                    "\"columns\":" +
            //                                        "[" +
            //                                            "{ \"name\": \"Id\", \"dataType\": \"string\"},  " +
            //                                            "{ \"name\": \"ResultStatus\", \"dataType\": \"string\"},  " +
            //                                            "{ \"name\": \"ObjectId\", \"dataType\": \"string\"},  " +
            //                                            "{ \"name\": \"Operation\", \"dataType\": \"string\"}," +
            //                                            "{ \"name\": \"ClientIP\", \"dataType\": \"string\"},  " +
            //                                            "{ \"name\": \"UserId\", \"dataType\": \"string\"},  " +
            //                                            "{ \"name\": \"ContentType\", \"dataType\": \"string\"},  " +
            //                                            "{ \"name\": \"Detail\", \"dataType\": \"string\"},  " +
            //                                            "{ \"name\": \"TenantId\", \"dataType\": \"string\"},  " +
            //                                            "{ \"name\": \"CreationTime\", \"dataType\": \"DateTime\"},  " +
            //                                            "{ \"name\": \"ExpirationTime\", \"dataType\": \"DateTime\"}  " +
            //                                        "]" +
            //                                    "}" +
            //                                "]" +
            //                            "}";
            #endregion

            var datasetScheme = "{" +
                                    "\"name\": \"" + model.Datasetname + "\"," +
                                    "\"tables\":" +
                                        "[" +
                                            string.Join(",", listTable) +
                                        "]" +
                                    "}";

            // todo check if Dataset/Table already exist
            // Execute api
            var client = new RestClient(_powerBiDataset);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", string.Format("Bearer {0}", model.Token));
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", datasetScheme, ParameterType.RequestBody);
            var response = client.Execute(request);

            var deSerializedResponse = JsonConvert.DeserializeObject<CreateDatasetReposne>(response.Content);

            return Ok(deSerializedResponse);
        }

        /// <summary>
        /// Push row to table
        /// </summary>
        /// <param name="model">PushRowsRequest</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PushRows(PushRowsRequest model)
        {
            // Validate model
            if (!model.Rows.Any())
                return BadRequest();

            // Joins all element to create body request
            var rowsStr = "";
            if (model.Rows.Count > 1)
                rowsStr = string.Join(",", model.Rows);
            else
                rowsStr = model.Rows[0].ToString();

            var requestBody = "{\"rows\":[" + rowsStr + "]}";
            // exec api to push data rows
            var client = new RestClient(_powerBiDataset);
            var request = new RestRequest($"{model.DatasetId}/tables/{model.TableName}/rows", Method.POST);
            request.AddHeader("Authorization", string.Format("Bearer {0}", model.Token));
            request.AddParameter("application/json", requestBody, ParameterType.RequestBody);

            var response = client.Execute(request);

            return Ok();

        }

        /// <summary>
        /// Clear table
        /// </summary>
        /// <param name="model">ClearTableRequest</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ClearTable(ClearTableRequest model)
        {
            // exec api clear table
            var client = new RestClient(_powerBiDataset);
            var request = new RestRequest($"{model.DatasetId}/tables/{model.TableName}/rows", Method.DELETE);
            request.AddHeader("Authorization", string.Format("Bearer {0}", model.Token));

            var response = client.Execute(request);

            return Ok();

        }
        #endregion
    }
}
