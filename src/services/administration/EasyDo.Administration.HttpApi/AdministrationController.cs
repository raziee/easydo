using EasyDo.Administration.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyDo.Administration;

public abstract class AdministrationController : AbpControllerBase
{
    protected AdministrationController()
    {
        LocalizationResource = typeof(AdministrationResource);
    }
}