﻿using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Reifnir.Manager.Acquisition.Interface;
using Reifnir.Manager.Acquisition.Interface.Model;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Reifnir.Client.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RunTests().Wait();
            }
            catch(Exception ex)
            {
                //FYI: one can only run console applications against SF application:
                //  1 - from a node on the cluster
                //  2 - on applications that do not rely on encrypted commuication (meaning: dev only)
                Console.WriteLine(ex.ToString());
            }
        }

        static async Task RunTests()
        {
            //Using basic Service Factory proxy for now...
            var proxy = CreateManagerProxy<IManageAudiobooks>();

            async Task callService(ReformatAssetRequest x)
            {
                var result = await proxy.ReformatAssetsAsync(x);
                Console.WriteLine($"result == null: {result == null}");
                Console.WriteLine($"result?.Success: {result?.Success}");
                Console.WriteLine($"result?.AssetId: {result?.AssetId}");
                Console.WriteLine($"result?.Message: {result?.Message}");
                Console.WriteLine("");
                return;
            }

            Console.WriteLine("ReformatAssetsAsync: passing null request");
            await callService(null);

            Console.WriteLine("ReformatAssetsAsync: passing object but not setting AssetId");
            await callService(new ReformatAssetRequest());

            Console.WriteLine("ReformatAssetsAsync: passing object with AssetId set");
            await callService(new ReformatAssetRequest() { AssetId = $"some-asset-id-format-{Guid.NewGuid()}" } );

        }

        static T CreateManagerProxy<T>()
            where T : class, IService
        {
            var facetInterface = typeof(T);
            var facetNamespaceParts = facetInterface.Namespace.Split('.');
            Debug.Assert(facetNamespaceParts.Length == 4, "Facets on Managers are expected to be in a format of <Company>.<Concept (i.e. Manager)>.<Volatility>.Interface.");
            var company = facetNamespaceParts[0];
            var concept = facetNamespaceParts[1];
            var volatility = facetNamespaceParts[2];

            //TODO: Provide config for connection string override for application uri root for heterogenous clusters.
            var uri = new Uri($"fabric:/{company}.Microservice.{volatility}/{company}.{concept}.{volatility}.Service");
            Debug.Assert(uri.ToString() == "fabric:/Reifnir.Microservice.Acquisition/Reifnir.Manager.Acquisition.Service");
            return ServiceProxy.Create<T>(uri);
        }
    }
}