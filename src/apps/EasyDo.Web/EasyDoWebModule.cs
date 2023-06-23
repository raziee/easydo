// using EasyDo.Shared.Hosting.AspNetCore;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Cors;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Volo.Abp;
// using Volo.Abp.Modularity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Routing;
// using Microsoft.AspNetCore.Diagnostics;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Http.Abstractions;
// using Microsoft.AspNetCore.Http.Extensions;
// using Microsoft.AspNetCore.Http.Features;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Server.Kestrel.Core;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Hosting;
// using Microsoft.EntityFrameworkCore;
// using Piranha;
// using Piranha.AttributeBuilder;
// using Piranha.AspNetCore.Identity.SQLServer;
// using Piranha.Data.EF.SQLServer;
// using Piranha.Manager.Editor;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;

// namespace EasyDo.Web;

// [DependsOn(
//     typeof(EasyDoSharedHostingAspNetCoreModule)
// )]
// public class EasyDoWebModule : AbpModule
// {

//     public override void ConfigureServices(ServiceConfigurationContext context)
//     {
//         var configuration = context.Services.GetConfiguration();

//         context.Services.AddPiranha(options =>
//         {
//             /**
//              * This will enable automatic reload of .cshtml
//              * without restarting the application. However since
//              * this adds a slight overhead it should not be
//              * enabled in production.
//              */
//             // options.AddRazorRuntimeCompilation = true;

//             options.UseCms();
//             options.UseManager();

//             options.UseFileStorage(naming: Piranha.Local.FileStorageNaming.UniqueFolderNames);
//             options.UseImageSharp();
//             options.UseTinyMCE();
//             options.UseMemoryCache();

//             var connectionString = configuration.GetConnectionString("EasyDoPiranha");
//             options.UseEF<SQLServerDb>(db => db.UseSqlServer(connectionString));
//             // options.UseEF<SQLiteDb>(db => db.UseSqlite(connectionString));
//             var identityConnectionString = configuration.GetConnectionString("EasyDoPiranhaIdentity");
//             options.UseIdentityWithSeed<IdentitySQLServerDb>(db => db.UseSqlServer(identityConnectionString));

//             /**
//              * Here you can configure the different permissions
//              * that you want to use for securing content in the
//              * application.
//             options.UseSecurity(o =>
//             {
//                 o.UsePermission("WebUser", "Web User");
//             });
//              */

//             /**
//              * Here you can specify the login url for the front end
//              * application. This does not affect the login url of
//              * the manager interface.
//             options.LoginUrl = "login";
//              */
//         });

//     }

//     public override void OnApplicationInitialization(ApplicationInitializationContext context)
//     {

//             var api = context.ServiceProvider
//                 .GetRequiredService<IApi>();

//         var app = context.GetApplicationBuilder();
//         var env = context.GetEnvironment();

//         if (env.IsDevelopment())
//         {
//             app.UseDeveloperExceptionPage();
//         }

//         // app.UseStatusCodePages(context =>
//         // {
//         //     context.HttpContext.Response.ContentType = "text/plain";

//         //     return context.HttpContext.Response.WriteAsync(
//         //         "Status code page, status code: " +
//         //         context.HttpContext.Response.StatusCode);
//         // });

        
//         app.UsePiranha(options =>
//         {
//             // Initialize Piranha
//             App.Init(api);

//             // Build content types
//             new ContentTypeBuilder(api)
//                 .AddAssembly(typeof(Program).Assembly)
//                 .Build()
//                 .DeleteOrphans();

//             // Configure Tiny MCE
//             EditorConfig.FromFile("editorconfig.json");

//             options.UseManager();
//             options.UseTinyMCE();
//             options.UseIdentity();
//         });

//         // app.UseRouting();

//         // app.UseEndpoints(endpoints =>
//         // {
//         //     endpoints.MapGet("/", () => "Hello World!");
//         // });

//         // app.useMapGet("/", () => "Hello World!");
//     }
// }