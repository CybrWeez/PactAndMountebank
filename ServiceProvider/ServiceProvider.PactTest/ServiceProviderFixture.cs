using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using PactNet;
using System;

namespace ServiceProvider.PactTest
{
    public class ServiceProviderFixture : IDisposable
    {
        private IWebHost TestServer;

        [Test]
        public void ProviderTest()
        {
            var url = "http://localhost:9222";

            var config = new PactVerifierConfig
            {
                Verbose = true
            };

            TestServer = WebHost.CreateDefaultBuilder()
                .UseUrls(url)
                .UseStartup<Startup>()
                .Build();

            TestServer.Start();

            var pactVerifier = new PactVerifier(config);
            pactVerifier
                .ServiceProvider("serviceprovider", url)
                .HonoursPactWith("mycoolwebapp")
                .PactUri(@"C:\Tools\PactAndMountebank\ServiceAccess\ServiceAccess.PactTest\bin\Debug\netcoreapp2.1\pacts\mycoolwebapp-serviceprovider.json")
                .Verify();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    TestServer.StopAsync().GetAwaiter().GetResult();
                    TestServer.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
