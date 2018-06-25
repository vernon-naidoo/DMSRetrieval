using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DSV.Webservice.Central.DMSRetrieval.Models;
using DSV.Webservice.Central.DMSRetrieval.log4Net;

namespace DSV.Webservice.Central.DMSRetrieval.Controllers
{


    /// <summary>
    /// DocumentController
    /// </summary>
    public class DocumentController : ApiController
    {


        //-----------------------------------------------------------
        // Method : Get
        //-----------------------------------------------------------
        /// <summary>
        /// Gets document for given input parameters
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="region"></param>
        /// <param name="businessSegment"></param>
        /// <param name="businessUnit"></param>
        /// <param name="client"></param>
        /// <param name="docType"></param>
        /// <param name="refNo"></param>
        /// <returns></returns>   
        public HttpResponseMessage Get(string objectId = "",
                                      string region = "",
                                      string businessSegment = "",
                                      string businessUnit = "",
                                      string client = "",
                                      string docType = "",
                                      string refNo = "")
        {
            return ProcessRequest("GET",
                                  region,
                                  businessSegment,
                                  businessUnit,
                                  client,
                                  docType,
                                  refNo,
                                  objectId);            
        }





        //-----------------------------------------------------------
        // Method : Post
        //-----------------------------------------------------------
        /// <summary>
        /// Gets document for given input parameters
        /// </summary>
        /// <param name="documentRequest"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(DocumentRequest documentRequest)
        {
            string region = null;
            string businessSegment = null;
            string businessUnit = null;
            string client = null;
            string docType = null;
            string refNo = null;
            string objectId = null;


            if (documentRequest != null)
            {
                region = documentRequest.Region;
                businessSegment = documentRequest.BusinessSegment;
                businessUnit = documentRequest.BusinessUnit;
                client = documentRequest.Client;
                docType = documentRequest.DocType;
                refNo = documentRequest.RefNo;
                objectId = documentRequest.ObjectId;
            }// if (documentRequest != null)

            return ProcessRequest("POST",
                                  region,
                                  businessSegment,
                                  businessUnit,
                                  client,
                                  docType,
                                  refNo,
                                  objectId);
        }





        //-----------------------------------------------------------
        // Method : ProcessRequest
        //-----------------------------------------------------------
        /// <summary>
        /// ProcessRequest
        /// </summary>
        /// <param name="p_method"></param>
        /// <param name="p_region"></param>
        /// <param name="p_businessSegment"></param>
        /// <param name="p_businessUnit"></param>
        /// <param name="p_client"></param>
        /// <param name="p_docType"></param>
        /// <param name="p_refNo"></param>
        /// <param name="p_objectId"></param>
        /// <returns></returns>
        private HttpResponseMessage ProcessRequest(string p_method,
                                      string p_region,
                                      string p_businessSegment,
                                      string p_businessUnit,
                                      string p_client,
                                      string p_docType,
                                      string p_refNo,
                                      string p_objectId)
        {
            Logger.Info(string.Format("Method : {0}.Input Parameters supplied region:{1}, businessSegment:{2}, businessUnit:{3}, client:{4}, docType:{5}, refno:{6}, objectId:{7}",
                                                   p_method,
                                                   p_region,
                                                   p_businessSegment,
                                                   p_businessUnit,
                                                   p_client,
                                                   p_docType,
                                                   p_refNo,
                                                   p_objectId));

            Document document = null;
            document = new Document();
            try
            {
                document.Region = p_region;
                document.BusinessSegment = p_businessSegment;
                document.BusinessUnit = p_businessUnit;
                document.Client = p_client;
                document.DocType = p_docType;
                document.RefNo = p_refNo;
                document.ObjectId = p_objectId;
                document.GetDocument();
            }
            catch (Exception ex)
            {
                document.IsSuccessful = false;
                document.ErrorMessage = ex.Message;
                Logger.Error(string.Format("Error occurred : {0}",
                                                   document.ErrorMessage));
            }

            Logger.Info(string.Format("Document IsSuccessful : {0}",
                                                   document.IsSuccessful));

            Logger.Info("======================Request Completed=======================");                                                   
            if (document.IsSuccessful)
            {
                var response = Request.CreateResponse<Document>(HttpStatusCode.OK, document);
                return response;
            }
            else
            {
                var response = Request.CreateResponse<Document>(HttpStatusCode.NotFound, document);
                return response;
            }
        }

       

    }
}
