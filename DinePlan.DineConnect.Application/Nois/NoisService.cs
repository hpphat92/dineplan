using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Linq;
using RestSharp.Extensions.MonoHttp;

namespace DinePlan.DineConnect.Nois
{
    //[AbpAuthorize(AppPermissions.Pages_Administration_Host_Settings)]
    public class NoisService : DineConnectAppServiceBase, INoisService
    {
        //private readonly IEmailSender _emailSender;
        //private readonly EditionManager _editionManager;
        //private readonly ITimeZoneService _timeZoneService;
        //readonly ISettingDefinitionManager _settingDefinitionManager;

        //public NoisService(
        //    IEmailSender emailSender,
        //    EditionManager editionManager,
        //    ITimeZoneService timeZoneService,
        //    ISettingDefinitionManager settingDefinitionManager)
        //{
        //    _emailSender = emailSender;
        //    _editionManager = editionManager;
        //    _timeZoneService = timeZoneService;
        //    _settingDefinitionManager = settingDefinitionManager;
        //}


        #region fields
        //private readonly DineConnectDbContext _context;
        #endregion

        #region webconfig
        private readonly string _clientId = "19679192-afd1-4be4-b28a-94b565fe349f";
        //private readonly string _clientSecrect = "GWumjvLxN2ePmkinK1qoMPSCzlkFy+vwnTJ9sz8SL1Q=";
        private readonly string _clientSecrect = "XWgwx9jEtsipSfdab0m/D5Je5gUccbEhefurfcFHBvo=";
        private readonly string _powerBIApi = "https://analysis.windows.net/powerbi/api";
        private readonly string _powerBiDataset = "https://api.powerbi.com/v1.0/myorg/datasets";
        private readonly string _authUrl = "https://login.windows.net/common/oauth2/authorize/";
        private readonly string _redirectUrl = "http://localhost:6240";
        #endregion

        #region private methods
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
        public NoisService(/*DineConnectDbContext context*/)
        {
            //_context = context;
        }
        #endregion
        //private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        //{
        //    if (_context.Settings.Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
        //    {
        //        return;
        //    }

        //    _context.Settings.Add(new Setting(tenantId, null, name, value));
        //    _context.SaveChanges();
        //}




        #region methods
        /// <summary>
        /// Return the URL to Microsoft Auth Page to get Auth Code
        /// </summary>
        /// <returns></returns>
        public string SignInPowerBI()
        {
            //var a = await SettingManager.GetSettingValueForApplicationAsync("Abp.Net.Mail.DefaultFromDisplayName");
            //try
            //{
            //    //var b = await SettingManager.GetSettingValueAsync("test");
            //    //await SettingManager.ChangeSettingForApplicationAsync("test", "val_1");
            //    //b = await SettingManager.GetSettingValueForApplicationAsync("test");

            //    var setting = _context.Settings.FirstOrDefault(x => x.Name == "powerbi.accesstoken");
            //    if (setting == null)
            //    {
            //        _context.Settings.Add(new Setting(null, null, "powerbi.accesstoken", "test"));
            //        _context.SaveChanges();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
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

            var utl = $"{_authUrl}?response_type=code&client_id={_clientId}&resource={_powerBIApi}&redirect_uri={_redirectUrl}";

            //Redirect authority
            //Authority Uri is an Azure resource that takes a client id to get an Access token
            //return string.Format("{0}?{1}", _authUrl, queryString.ToString());
            return utl;
        }

        /// <summary>
        /// Get AccessToken From Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<string> GetTokenFromCode(string code)
        {
            //Redirect uri must match the redirect_uri used when requesting Authorization code.
            string redirectUri = _redirectUrl;
            string authorityUri = _authUrl;

            try
            {
                // Install-Package Microsoft.IdentityModel.Clients.ActiveDirectory

                // Get auth token from auth code       
                TokenCache TC = new TokenCache();

                AuthenticationContext AC = new AuthenticationContext(authorityUri, TC);

                ClientCredential cc = new ClientCredential(_clientId, _clientSecrect);

                AuthenticationResult AR = await AC.AcquireTokenByAuthorizationCodeAsync(code, new Uri(redirectUri), cc);

                // Get Accesstoken
                var accessToken = AR.AccessToken;

                return accessToken;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Get Access Token for PowerBi
        /// </summary>
        /// <param name="model">GetPBITokenRequest</param>
        /// <returns></returns>
        public GetPBITokenResponse GetAccessToken(GetPBITokenRequest model)
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

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get list dataset Id
        /// </summary>
        /// <param name="model">GetDatasetsRequest</param>
        /// <returns></returns>
        public GetDatasetsResponse GetListDataset(GetDatasetsRequest model)
        {
            try
            {
                var dataSetIds = GetDatasetIds(model.Token);

                return dataSetIds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Create Dataset
        /// NOTE : table can only be create with dataset, currently there are not exist feature to create table to an existing dataset
        /// </summary>
        /// <param name="model">CreateDatasetRequest</param>
        /// <returns></returns>
        public CreateDatasetReposne CreateDataset(CreateDatasetRequest model)
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

            return deSerializedResponse;
        }

        /// <summary>
        /// Push row to table
        /// </summary>
        /// <param name="model">PushRowsRequest</param>
        /// <returns></returns>
        public bool PushRows(PushRowsRequest model)
        {
            // Validate model
            if (!model.Rows.Any())
                return false;

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

            return true;

        }

        /// <summary>
        /// Clear table
        /// </summary>
        /// <param name="model">ClearTableRequest</param>
        /// <returns></returns>
        public bool ClearTable(ClearTableRequest model)
        {
            // exec api clear table
            var client = new RestClient(_powerBiDataset);
            var request = new RestRequest($"{model.DatasetId}/tables/{model.TableName}/rows", Method.DELETE);
            request.AddHeader("Authorization", string.Format("Bearer {0}", model.Token));

            var response = client.Execute(request);

            return true;

        }
        #endregion


    }
}