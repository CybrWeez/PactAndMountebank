using NUnit.Framework;
using System;
using System.Net.Http;

namespace ServiceAccess.PactTest
{
    public class ServiceAccessIntegrationFixture
    {
        [Test]
        public void GetData_Success()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(@"http://localhost:51030/api/")
            };
            ServiceAccess access = new ServiceAccess(httpClient);

            var result = access.GetData("values/1").Result;

            Assert.AreEqual("value", result);
        }

        [Test]
        public void GetData_BadRequest()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(@"http://localhost:51030/api/")
            };
            ServiceAccess access = new ServiceAccess(httpClient);

            Assert.ThrowsAsync<Exception>(() => access.GetData("values/0"));
        }

        #region MB Tests
        [Test]
        public void GetData_MB_Success()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(@"http://localhost:4545")
            };
            ServiceAccess access = new ServiceAccess(httpClient);

            var result = access.GetData("/api/values/1").Result;

            Assert.AreEqual("value", result);
        }

        [Test]
        public void GetData_MB_BadRequest()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(@"http://localhost:4545")
            };
            ServiceAccess access = new ServiceAccess(httpClient);

            Assert.ThrowsAsync<Exception>(() => access.GetData("/api/values/0"));
        }

        #endregion

    }
}
