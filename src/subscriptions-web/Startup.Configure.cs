﻿using Esfa.Recruit.Subscriptions.Web.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;

namespace Esfa.Recruit.Subscriptions.Web
{
    public partial class Startup
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<ExternalLinksConfiguration> externalLinks, IApplicationLifetime applicationLifetime, ILogger<Startup> logger)
        {
            var cultureInfo = new CultureInfo("en-GB");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            applicationLifetime.ApplicationStarted.Register(() => logger.LogInformation("Host fully started"));
            applicationLifetime.ApplicationStopping.Register(() => logger.LogInformation("Host shutting down...waiting to complete requests."));
            applicationLifetime.ApplicationStopped.Register(() => logger.LogInformation("Host fully stopped. All requests processed."));

            app.UseStatusCodePagesWithReExecute("/error/{0}");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error/handle");
                app.UseHsts(hsts => hsts.MaxAge(365));
            }
            
            // Add Content Security Policy
            app.UseCsp(options => options
                .DefaultSources(s => s.Self())
                .StyleSources(s => 
                    s.Self()
                )
                .ScriptSources(s => 
                    s.Self()
                    .CustomSources("https://az416426.vo.msecnd.net", 
                                    "https://www.google-analytics.com/analytics.js", 
                                    "https://www.googletagmanager.com/",
                                    "https://www.tagmanager.google.com/", 
                                    "https://services.postcodeanywhere.co.uk/")
                    .UnsafeInline()
                ) // TODO: Look at moving AppInsights inline js code.
                .FontSources(s => 
                    s.Self()
                    .CustomSources("data:")
                )
                .ConnectSources(s => 
                    s.Self()
                    .CustomSources("https://dc.services.visualstudio.com")
                )
                .ImageSources(s => 
                    s.Self()
                    .CustomSources("https://maps.googleapis.com", 
                                    "https://www.google-analytics.com", 
                                    "data:")
                 )
                .ReportUris(r => r.Uris("/ContentPolicyReport/Report")));

            //Registered before static files to always set header
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opts => opts.NoReferrer());
            app.UseXXssProtection(opts => opts.EnabledWithBlockMode());

            app.UseStaticFiles();

            //Registered after static files, to set headers for dynamic content.
            app.UseXfo(xfo => xfo.Deny());
            app.UseRedirectValidation(opts => {
                opts.AllowSameHostRedirectsToHttps();
                // opts.AllowedDestinations(GetAllowableDestinations(externalLinks.Value)); // TODO: Re-enabled if have redirect url
            }); //Register this earlier if there's middleware that might redirect.
            
            app.UseXDownloadOptions();
            app.UseXRobotsTag(options => options.NoIndex().NoFollow());

            app.UseNoCacheHttpHeaders(); // Effectively forces the browser to always request dynamic pages

            app.UseMvc();
        }

        private static string[] GetAllowableDestinations(ExternalLinksConfiguration linksConfig)
        {
            var destinations = new List<string>();
            
            // if (!string.IsNullOrWhiteSpace(linksConfig?.ManageApprenticeshipSiteUrl))
            //     destinations.Add(linksConfig?.ManageApprenticeshipSiteUrl);

            return destinations.ToArray();
        }
    }
}
