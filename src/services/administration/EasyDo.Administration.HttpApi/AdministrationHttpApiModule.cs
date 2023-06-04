﻿using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using EasyDo.Administration.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;

namespace EasyDo.Administration;

[DependsOn(
    typeof(AdministrationApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule)
)]
public class AdministrationHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AdministrationHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AdministrationResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}