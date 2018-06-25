using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using DSV.Webservice.Central.DMSRetrieval.Controllers;
using DSV.Webservice.Central.DMSRetrieval.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSV.Webservice.Central.DMSRetrieval.Tests.Controllers
{
    [TestClass]
    public class DocumentControllerTest
    {
        [TestMethod]
        public void Get_ShouldReturnDocument()
        {
            // Arrange
            DocumentController docController = new DocumentController();
            docController.Request =  new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            HttpResponseMessage response = docController.Get(null,
                                                             "AF",
                                                              "D",
                                                              "M",
                                                              "Nedbank",
                                                              "B0004",
                                                              "0520160713095416112");

            Document doc = (Document)((System.Net.Http.ObjectContent)(response.Content)).Value;

            if (doc.IsSuccessful)
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            else
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }           
        }




        [TestMethod]
        public void Post_ShouldReturnDocument()
        {
            // Arrange
            DocumentController docController = new DocumentController();
            docController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            DocumentRequest docRequest = new DocumentRequest();
            docRequest.Region= "AF";
            docRequest.BusinessSegment = "D";
            docRequest.BusinessUnit = "M";
            docRequest.Client = "Nedbank";
            docRequest.DocType = "B0004";
            docRequest.RefNo = "0520160713095416112";



            HttpResponseMessage response = docController.Post(docRequest);

            Document doc = (Document)((System.Net.Http.ObjectContent)(response.Content)).Value;

            if (doc.IsSuccessful)
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            else
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }




        [TestMethod]
        public void Get_ObjectId_ShouldReturnDocument()
        {
            // Arrange
            DocumentController docController = new DocumentController();
            docController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            HttpResponseMessage response = docController.Get("09025ffd80271ec1");

            Document doc = (Document)((System.Net.Http.ObjectContent)(response.Content)).Value;

            if (doc.IsSuccessful)
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            else
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }           
        }




        [TestMethod]
        public void Post_ObjectId_ShouldReturnDocument()
        {
            // Arrange
            DocumentController docController = new DocumentController();
            docController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            DocumentRequest docRequest = new DocumentRequest();
            docRequest.ObjectId = "09025ffd80271ec1";     

            HttpResponseMessage response = docController.Post(docRequest);

            Document doc = (Document)((System.Net.Http.ObjectContent)(response.Content)).Value;

            if (doc.IsSuccessful)
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            else
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

       
    }
}
