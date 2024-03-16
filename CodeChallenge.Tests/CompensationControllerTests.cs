using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;
using CodeChallenge.Repositories;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_OK()
        {
            // Arrange
            var id = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var salary = 100.00;
            var effectiveDate = DateTime.Today;

            var content = new Dictionary<string, string>
            {
                {"id", id },
                {"salary", salary.ToString() },
                {"effectiveDate", effectiveDate.ToString() },
            };

            var requestContent = new FormUrlEncodedContent(content);
            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation", requestContent);
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.Employee);
            Assert.AreEqual(salary, newCompensation.Salary);
            Assert.AreEqual(effectiveDate, newCompensation.EffectiveDate);
            
        }
    }
}
