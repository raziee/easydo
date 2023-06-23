// using EasyDo.Shared.Hosting.AspNetCore;
// using Serilog;
// using System;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Builder;

// namespace EasyDo.Web;

// public class Program
// {
//     public static async Task<int> Main(string[] args)
//     {
//         var assemblyName = typeof(Program).Assembly.GetName().Name;

//         SerilogConfigurationHelper.Configure(assemblyName);

//         try
//         {
//             Log.Information($"Starting {assemblyName}.");
//             var app = await ApplicationBuilderHelper
//                 .BuildApplicationAsync<EasyDoWebModule>(args);
//             await app.InitializeApplicationAsync();
//             await app.RunAsync();

//             return 0;
//         }
//         catch (Exception ex)
//         {
//             Log.Fatal(ex, $"{assemblyName} terminated unexpectedly!");
//             return 1;
//         }
//         finally
//         {
//             Log.CloseAndFlush();
//         }
//     }
// }

using Microsoft.EntityFrameworkCore;
using Piranha;
using Piranha.AttributeBuilder;
using Piranha.AspNetCore.Identity.SQLServer;
using Piranha.Data.EF.SQLServer;
using Piranha.Manager.Editor;

var builder = WebApplication.CreateBuilder(args);

builder.AddPiranha(options =>
{
    /**
     * This will enable automatic reload of .cshtml
     * without restarting the application. However since
     * this adds a slight overhead it should not be
     * enabled in production.
     */
    options.AddRazorRuntimeCompilation = true;

    options.UseCms();
    options.UseManager();

    options.UseFileStorage(naming: Piranha.Local.FileStorageNaming.UniqueFolderNames);
    options.UseImageSharp();
    options.UseTinyMCE();
    options.UseMemoryCache();

    var connectionString = builder.Configuration.GetConnectionString("EasyDoPiranha");
    options.UseEF<SQLServerDb>(db => db.UseSqlServer(connectionString));
    // options.UseEF<SQLiteDb>(db => db.UseSqlite(connectionString));
    var identityConnectionString = builder.Configuration.GetConnectionString("EasyDoPiranhaIdentity");
    options.UseIdentityWithSeed<IdentitySQLServerDb>(db => db.UseSqlServer(identityConnectionString));

    /**
     * Here you can configure the different permissions
     * that you want to use for securing content in the
     * application.
    options.UseSecurity(o =>
    {
        o.UsePermission("WebUser", "Web User");
    });
     */

    /**
     * Here you can specify the login url for the front end
     * application. This does not affect the login url of
     * the manager interface.
    options.LoginUrl = "login";
     */
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UsePiranha(options =>
{
    // Initialize Piranha
    App.Init(options.Api);

    // Build content types
    new ContentTypeBuilder(options.Api)
        .AddAssembly(typeof(Program).Assembly)
        .Build()
        .DeleteOrphans();

    // Configure Tiny MCE
    EditorConfig.FromFile("editorconfig.json");

    options.UseManager();
    options.UseTinyMCE();
    options.UseIdentity();
});

app.Run();