using NUnit.Framework;
using PactNet.Mocks.MockHttpService.Models;
using ServiceAccess.PactTest.Pacts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ServiceAccess.PactTest
{
    class ServiceAccessPactFixture
    {
        ServiceAccessPact ServicePact;
        HttpClient ServiceClient;

        private const string ValuesRoute = "/api/values";

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            ServicePact = ServiceAccessPact.GetInstance();
            ServiceClient = new HttpClient
            {
                BaseAddress = new Uri($"{ServiceAccessPact.MockProviderServiceBaseUri}")
            };
        }

        [OneTimeTearDown]
        public void FixtureTeardown() => ServicePact.Dispose();

        [SetUp]
        public void TestSetup()
        {
            ServicePact.MockProviderService.ClearInteractions();
        }

        [Test]
        public void GetData_Success()
        {
            ServiceAccess access = new ServiceAccess(ServiceClient);

            #region Pact response
            ServicePact.MockProviderService
                .Given($"A ServiceProvider endpoint for values")
                .UponReceiving("A Get request for 1")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = $"{ValuesRoute}/1"
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, object> { { "Content-Type", "text/plain; charset=utf-8" } },
                    Body = "value"
                });
            #endregion

            var result = access.GetData($"{ValuesRoute}/1").Result;

            Assert.AreEqual(result, "value");
        }

        [Test]
        public void GetData_BadRequest()
        {
            ServiceAccess access = new ServiceAccess(ServiceClient);

            ServicePact.MockProviderService
                .Given($"A ServiceProvider endpoint for values")
                .UponReceiving("A Get request for 0")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = $"{ValuesRoute}/0"
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Headers = new Dictionary<string, object> { { "Content-Type", "text/plain; charset=utf-8" } },
                    Body = "bad request yo"
                });

            Assert.ThrowsAsync<Exception>(() => access.GetData($"{ValuesRoute}/0"));
        }
    }
}
