using BlazorSecureWithIdentity.BlazorIndividual.Security;
using BlazorSecureWithIdentity.gRPC;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSecureWithIdentity.BlazorIndividual
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("companyApi", config=> {
                config.BaseAddress = new Uri("https://localhost:44399/");
            })
                .AddHttpMessageHandler(sp =>
                {
                    var handler = sp.GetService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { "https://localhost:44399" },
                            scopes: new[] { "companyApi" });

                    return handler;
                });

            //builder.Services.AddSingleton(services =>
            //{
            //    // Now we can instantiate gRPC clients for this channel
            //    var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
            //    var channel = GrpcChannel.ForAddress("https://localhost:5011", new GrpcChannelOptions { HttpClient = httpClient });
            //    return new Greeter.GreeterClient(channel);
            //});
            builder.Services.AddScoped(services =>
            {
                var baseAddressMessageHandler =
                            services.GetRequiredService<BaseAddressAuthorizationMessageHandler>();
                baseAddressMessageHandler.InnerHandler = new HttpClientHandler();
                var grpcWebHandler =
                            new GrpcWebHandler(GrpcWebMode.GrpcWeb, baseAddressMessageHandler);


                //var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                var channel = GrpcChannel.ForAddress("https://localhost:5011", new GrpcChannelOptions { HttpHandler = grpcWebHandler });
                return new Greeter.GreeterClient(channel);
            });



            //builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("companyApi"));

            builder.Services.AddOidcAuthentication(options =>
            {

                builder.Configuration.Bind("oidc", options.ProviderOptions);

            });
            await builder.Build().RunAsync();


            //.AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

            //builder.Services.AddApiAuthorization()
            //    .AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();
            //builder.Services.AddHttpClient("ServerAPI",
            //    client => client.BaseAddress = new Uri("https://localhost:44399"))
            //.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            //        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
            //            .CreateClient("ServerAPI"));

            //builder.Services.AddOidcAuthentication(options =>
            //{
            //    // Configure your authentication provider options here.
            //    // For more information, see https://aka.ms/blazor-standalone-auth
            //    builder.Configuration.Bind("Local", options.ProviderOptions);
            //});


        }
    }
}
