﻿using Http.Client.Factory.Application.Interfaces;
using Http.Client.Factory.Infra.Services;
using System.Net;
using System.Net.Http.Headers;

namespace http_client_factory.Configurations
{
    public static class HttpClientFactorySettings
    {            
        public static void HttpClientFactory(this WebApplicationBuilder builder)
        {
            /// <summary>
            /// HttpClientFactory Typed Client Example
            /// </summary>  
            builder.Services.AddHttpClient<IHttpClientFactoryTypedClientService, HttpClientFactoryTypedClientService>(client =>
            {
                client.DefaultRequestVersion = HttpVersion.Version20;
                client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact;
                client.BaseAddress = new Uri("https://localhost:44310/");                

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", builder.Configuration.GetSection("ConfigurationApi").GetSection("AccessToken").Value);  --> For add Authentication in header
                //client.DefaultRequestHeaders.Add("varParameter", "dataParameter");  --> For add headers
                //client.DefaultRequestHeaders.Add(HeaderNames.Accept, "*/*");  --> For add headers

            }).SetHandlerLifetime(TimeSpan.FromMilliseconds(double.Parse(builder.Configuration.GetSection("http-client-factory:Timeout").Value)));  // --> Set lifetime in seconds


            /// <summary>
            /// //HttpClientFactory Named Clients Example
            /// </summary>  
            builder.Services.AddHttpClient("NamedClient", client =>
            {
                client.DefaultRequestVersion = HttpVersion.Version20;
                client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact;
                client.BaseAddress = new Uri("https://localhost:44310/");
                client.Timeout = TimeSpan.FromMilliseconds(double.Parse(builder.Configuration.GetSection("http-client-factory:Timeout").Value));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", builder.Configuration.GetSection("ConfigurationApi").GetSection("AccessToken").Value);
            });
        }
    }
}
