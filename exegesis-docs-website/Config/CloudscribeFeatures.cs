using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using cloudscribe.UserProperties.Models;
using cloudscribe.UserProperties.Services;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CloudscribeFeatures
    {

        public static IServiceCollection SetupDataStorage(
            this IServiceCollection services,
            IConfiguration config,
            IWebHostEnvironment env
            )
        {
            var connectionString = config.GetConnectionString("EntityFrameworkConnection");


            services.AddCloudscribeCorePostgreSqlStorage(connectionString);


            services.AddCloudscribeKvpPostgreSqlStorage(connectionString);
            services.AddCloudscribeLoggingPostgreSqlStorage(connectionString);

            services.AddCloudscribeSimpleContentPostgreSqlStorage(connectionString);


            services.AddFormsStoragePostgreSql(connectionString);

            services.AddDynamicPolicyPostgreSqlStorage(connectionString);


            services.AddEmailTemplatePostgreSqlStorage(connectionString);
            services.AddEmailQueuePostgreSqlStorage(connectionString);

            services.AddEmailListPostgreSqlStorage(connectionString);

            services.AddCommentStoragePostgreSql(connectionString);

            services.AddForumStoragePostgreSql(connectionString);



            return services;
        }

        public static IServiceCollection SetupCloudscribeFeatures(
            this IServiceCollection services,
            IConfiguration config
            )
        {

            services.AddCloudscribeLogging(config);

            services.Configure<ProfilePropertySetContainer>(config.GetSection("ProfilePropertySetContainer"));
            services.AddEmailListKvpIntegration(config);
            services.AddCloudscribeKvpUserProperties();


            services.AddScoped<cloudscribe.Web.Navigation.INavigationNodePermissionResolver, cloudscribe.Web.Navigation.NavigationNodePermissionResolver>();
            services.AddScoped<cloudscribe.Web.Navigation.INavigationNodePermissionResolver, cloudscribe.SimpleContent.Web.Services.PagesNavigationNodePermissionResolver>();
            services.AddCloudscribeCoreMvc(config);
            services.AddCloudscribeCoreIntegrationForSimpleContent(config);
            services.AddSimpleContentMvc(config);
            services.AddContentTemplatesForSimpleContent(config);

            services.AddMetaWeblogForSimpleContent(config.GetSection("MetaWeblogApiOptions"));
            services.AddSimpleContentRssSyndiction();
            services.AddCloudscribeSimpleContactFormCoreIntegration(config);
            services.AddCloudscribeSimpleContactForm(config);

            services.AddFormsCloudscribeCoreIntegration(config);
            services.AddFormsServices(config);
            services.AddFormSurveyContentTemplatesForSimpleContent(config);
            // these are examples to show you how to implement custom form submission handlers.
            // see /Services/SampleFormSubmissionHandlers.cs
            services.AddScoped<cloudscribe.Forms.Models.IHandleFormSubmission, exegesis_docs_website.Services.FakeFormSubmissionHandler1>();
            services.AddScoped<cloudscribe.Forms.Models.IHandleFormSubmission, exegesis_docs_website.Services.FakeFormSubmissionHandler2>();



            services.AddEmailQueueBackgroundTask(config);
            services.AddEmailQueueWithCloudscribeIntegration(config);
            services.AddEmailRazorTemplating(config);

            services.AddEmailListWithCloudscribeIntegration(config);

            services.AddCloudscribeDynamicPolicyIntegration(config);
            services.AddDynamicAuthorizationMvc(config);

            services.AddTalkAboutCloudscribeIntegration(config);


            services.AddTalkAboutForumServices(config)
                .AddTalkAboutForumNotificationServices(config);

            services.AddTalkAboutCommentsCloudscribeIntegration(config);
            services.AddTalkAboutServices(config)
                .AddTalkAboutNotificationServices(config);

            return services;
        }

    }
}
