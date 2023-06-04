﻿using EasyDo.IdentityService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyDo.IdentityService;

public abstract class IdentityServiceController : AbpControllerBase
{
    protected IdentityServiceController()
    {
        LocalizationResource = typeof(IdentityServiceResource);
    }
}