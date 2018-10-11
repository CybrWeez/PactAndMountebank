using PactNet;
using PactNet.Mocks.MockHttpService;
using System;

namespace ServiceAccess.PactTest.Pacts
{
    class ServiceAccessPact : IDisposable
    {
        private static readonly string MockServerName = "localhost";
        private static readonly int MockServerPort = 8888;

        public static String MockProviderServiceBaseUri => $"http://{MockServerName}:{MockServerPort}";
        private static ServiceAccessPact PactInstance;

        public static readonly PactConfig PactConfig = new PactConfig
        {
            PactDir = AppDomain.CurrentDomain.BaseDirectory + @"\pacts",
            LogDir = AppDomain.CurrentDomain.BaseDirectory + @"\pacts_log",
            SpecificationVersion = "2.0.0"
        };

        public IPactBuilder PactBuilder = new PactBuilder(PactConfig)
                .ServiceConsumer("MyCoolWebApp")
                .HasPactWith("ServiceProvider");

        public IMockProviderService MockProviderService;

        private ServiceAccessPact()
        {
            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        public static ServiceAccessPact GetInstance()
        {
            return PactInstance ?? (PactInstance = new ServiceAccessPact());
        }

        public void Dispose()
        {
            PactBuilder.Build(); //NOTE: Will save the pact file once finished
        }
    }
}
