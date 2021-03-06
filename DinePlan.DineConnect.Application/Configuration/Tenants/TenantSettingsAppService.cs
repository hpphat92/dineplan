﻿using System.Globalization;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Extensions;
using Abp.Json;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.Zero.Configuration;
using Abp.Zero.Ldap.Configuration;
using DinePlan.DineConnect.Authorization;
using DinePlan.DineConnect.Configuration.Host.Dto;
using DinePlan.DineConnect.Configuration.Tenants.Dto;
using DinePlan.DineConnect.Security;
using DinePlan.DineConnect.Timing;
using Newtonsoft.Json;

namespace DinePlan.DineConnect.Configuration.Tenants
{
    [AbpAuthorize(AppPermissions.Pages_Administration_Tenant_Settings)]
    public class TenantSettingsAppService : DineConnectAppServiceBase, ITenantSettingsAppService
    {
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IAbpZeroLdapModuleConfig _ldapModuleConfig;
        private readonly ITimeZoneService _timeZoneService;

        public TenantSettingsAppService(
            IMultiTenancyConfig multiTenancyConfig,
            IAbpZeroLdapModuleConfig ldapModuleConfig,
            ITimeZoneService timeZoneService)
        {
            _multiTenancyConfig = multiTenancyConfig;
            _ldapModuleConfig = ldapModuleConfig;
            _timeZoneService = timeZoneService;
        }

        public async Task<TenantSettingsEditDto> GetAllSettings()
        {
            var settings = new TenantSettingsEditDto
            {
                UserManagement = new TenantUserManagementSettingsEditDto
                {
                    AllowSelfRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.AllowSelfRegistration),
                    IsNewRegisteredUserActiveByDefault = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault),
                    IsEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin),
                    UseCaptchaOnRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettings.UserManagement.UseCaptchaOnRegistration)
                }
            };

            var passwordComplexitySetting = await SettingManager.GetSettingValueAsync(AppSettings.Security.PasswordComplexity);
            var defaultPasswordComplexitySetting = await SettingManager.GetSettingValueForApplicationAsync(AppSettings.Security.PasswordComplexity);

            settings.Security = new SecuritySettingsEditDto
            {
                UseDefaultPasswordComplexitySettings = passwordComplexitySetting == defaultPasswordComplexitySetting,
                PasswordComplexity = JsonConvert.DeserializeObject<PasswordComplexitySetting>(passwordComplexitySetting),
                DefaultPasswordComplexity = JsonConvert.DeserializeObject<PasswordComplexitySetting>(defaultPasswordComplexitySetting)
            };

            if (!_multiTenancyConfig.IsEnabled || Clock.SupportsMultipleTimezone)
            {
                //General
                settings.General = new GeneralSettingsEditDto();
                if (!_multiTenancyConfig.IsEnabled)
                {
                    settings.General.WebSiteRootAddress = await SettingManager.GetSettingValueAsync(AppSettings.General.WebSiteRootAddress);
                }

                if (Clock.SupportsMultipleTimezone)
                {
                    var timezone = await SettingManager.GetSettingValueForTenantAsync(TimingSettingNames.TimeZone, AbpSession.GetTenantId());

                    settings.General.Timezone = timezone;
                    settings.General.TimezoneForComparison = timezone;
                }

                var defaultTimeZoneId = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Tenant, AbpSession.TenantId);
                if (settings.General.Timezone == defaultTimeZoneId)
                {
                    settings.General.Timezone = string.Empty;
                }
            }


            if (!_multiTenancyConfig.IsEnabled)
            {
                //Email
                settings.Email = new EmailSettingsEditDto
                {
                    DefaultFromAddress = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromAddress),
                    DefaultFromDisplayName = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromDisplayName),
                    SmtpHost = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Host),
                    SmtpPort = await SettingManager.GetSettingValueAsync<int>(EmailSettingNames.Smtp.Port),
                    SmtpUserName = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.UserName),
                    SmtpPassword = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Password),
                    SmtpDomain = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Domain),
                    SmtpEnableSsl = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.EnableSsl),
                    SmtpUseDefaultCredentials = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.UseDefaultCredentials)
                };

                //Ldap
                if (_ldapModuleConfig.IsEnabled)
                {
                    settings.Ldap = new LdapSettingsEditDto
                    {
                        IsModuleEnabled = true,
                        IsEnabled = await SettingManager.GetSettingValueAsync<bool>(LdapSettingNames.IsEnabled),
                        Domain = await SettingManager.GetSettingValueAsync(LdapSettingNames.Domain),
                        UserName = await SettingManager.GetSettingValueAsync(LdapSettingNames.UserName),
                        Password = await SettingManager.GetSettingValueAsync(LdapSettingNames.Password),
                    };
                }
                else
                {
                    settings.Ldap = new LdapSettingsEditDto
                    {
                        IsModuleEnabled = false
                    };
                }
            }

            return settings;
        }

        public async Task UpdateAllSettings(TenantSettingsEditDto input)
        {
            //User management
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.AllowSelfRegistration, input.UserManagement.AllowSelfRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault, input.UserManagement.IsNewRegisteredUserActiveByDefault.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin, input.UserManagement.IsEmailConfirmationRequiredForLogin.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.UserManagement.UseCaptchaOnRegistration, input.UserManagement.UseCaptchaOnRegistration.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));

            //Security
            if (input.Security.UseDefaultPasswordComplexitySettings)
            {
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.Security.PasswordComplexity, input.Security.DefaultPasswordComplexity.ToJsonString());
            }
            else
            {
                await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.Security.PasswordComplexity, input.Security.PasswordComplexity.ToJsonString());
            }

            //Time Zone
            if (Clock.SupportsMultipleTimezone)
            {
                if (input.General.Timezone.IsNullOrEmpty())
                {
                    var defaultValue = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Tenant, AbpSession.TenantId);
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), TimingSettingNames.TimeZone, defaultValue);
                }
                else
                {
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), TimingSettingNames.TimeZone, input.General.Timezone);
                }
            }

            if (!_multiTenancyConfig.IsEnabled)
            {
                input.ValidateHostSettings();

                //General
                await SettingManager.ChangeSettingForApplicationAsync(AppSettings.General.WebSiteRootAddress, input.General.WebSiteRootAddress.EnsureEndsWith('/'));

                //Email
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromAddress, input.Email.DefaultFromAddress);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromDisplayName, input.Email.DefaultFromDisplayName);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Host, input.Email.SmtpHost);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Port, input.Email.SmtpPort.ToString(CultureInfo.InvariantCulture));
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.UserName, input.Email.SmtpUserName);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Password, input.Email.SmtpPassword);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Domain, input.Email.SmtpDomain);
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.EnableSsl, input.Email.SmtpEnableSsl.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
                await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.UseDefaultCredentials, input.Email.SmtpUseDefaultCredentials.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));

                //Ldap
                if (_ldapModuleConfig.IsEnabled)
                {
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.IsEnabled, input.Ldap.IsEnabled.ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.Domain, input.Ldap.Domain.IsNullOrWhiteSpace() ? null : input.Ldap.Domain);
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.UserName, input.Ldap.UserName.IsNullOrWhiteSpace() ? null : input.Ldap.UserName);
                    await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), LdapSettingNames.Password, input.Ldap.Password.IsNullOrWhiteSpace() ? null : input.Ldap.Password);
                }
            }
        }
    }
}