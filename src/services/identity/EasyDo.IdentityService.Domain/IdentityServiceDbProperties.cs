
namespace EasyDo.IdentityService;
    public static class IdentityServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "IdentityService";

    public const string DefaultAdminEmailAddress = "admin@local.ir";

    public const string DefaultAdminPassword = "1q2w3E*";
}
