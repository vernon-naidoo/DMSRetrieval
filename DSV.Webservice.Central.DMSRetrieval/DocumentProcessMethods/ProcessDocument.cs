using System;
using System.Collections.Generic;
using System.Linq;

using DSV.Webservice.Central.DMSRetrieval.Models;

namespace DSV.Webservice.Central.DMSRetrieval.DocumentProcessMethods
{


    /// <summary>
    /// Defines different DocumentService Types
    /// </summary>
    internal enum DocumentServiceType
    {
        WCF_SERVICE_6P,
        REST_SERVICE_1P,
        NONE
    }


    /// <summary>
    /// ProcessDocument
    /// </summary>
    internal class ProcessDocument
    {

        //-----------------------------------------------------------------------------------------------
        //Method : CreateDocumentumApiService
        //-----------------------------------------------------------------------------------------------
        /// <summary>
        /// CreateDocumentumApiService
        /// </summary>
        /// <param name="p_objectId"></param>
        /// <returns></returns>
        private static DocumentumApiService CreateDocumentumApiService(string p_objectId)
        {
            return new DocumentumApiService(p_objectId);
        }




        //-----------------------------------------------------------------------------------------------
        //Method : CreateDocumentumService
        //-----------------------------------------------------------------------------------------------
        /// <summary>
        /// CreateDocumentumService
        /// </summary>
        /// <param name="p_region"></param>
        /// <param name="p_businessSegment"></param>
        /// <param name="p_businessUnit"></param>
        /// <param name="p_client"></param>
        /// <param name="p_docType"></param>
        /// <param name="p_refNo"></param>
        /// <returns></returns>
        private static DocumentumService CreateDocumentumService(string p_region,
                                                          string p_businessSegment,
                                                          string p_businessUnit,
                                                          string p_client,
                                                          string p_docType,
                                                          string p_refNo)
        {
            return new DocumentumService(p_region,
                                        p_businessSegment,
                                        p_businessUnit,
                                        p_client,
                                        p_docType,
                                        p_refNo);

                                                          
        }




        //-----------------------------------------------------------------------------------------------
        //Method : Start
        //-----------------------------------------------------------------------------------------------
        /// <summary>
        /// Start
        /// </summary>
        /// <param name="p_documentServiceType"></param>
        /// <param name="p_document"></param>
        internal static void Start(DocumentServiceType p_documentServiceType,
                                                    Document p_document)
        {
            DocumentService documentService = null;

            try
            {
                if (p_documentServiceType == DocumentServiceType.REST_SERVICE_1P)
                {
                    DocumentumApiService documentApiService = CreateDocumentumApiService(p_document.ObjectId);                    
                    p_document.IsSuccessful = documentApiService.GetDocumentDetails();
                    documentService = documentApiService;
                    documentApiService = null;
                }
                else if (p_documentServiceType == DocumentServiceType.WCF_SERVICE_6P)
                {
                    DocumentumService documentumService = CreateDocumentumService(p_document.Region,
                                                                p_document.BusinessSegment,
                                                                p_document.BusinessUnit,
                                                                p_document.Client,
                                                                p_document.DocType,
                                                                p_document.RefNo);

                    p_document.IsSuccessful = documentumService.GetDocumentDetails();
                    documentService = documentumService;
                    documentumService = null;
                }

                if (p_document.IsSuccessful)
                {
                    p_document.DocumentDetails = new Models.DocumentDetails();
                    p_document.DocumentDetails.DocumentName = documentService.DocumentName;
                    p_document.DocumentDetails.DocumentByteData = documentService.DocumentByteData;
                    p_document.DocumentDetails.DocumentType = documentService.DocumentType;
                }
                else
                {
                    p_document.ErrorMessage = documentService.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                p_document.ErrorMessage = ex.Message;
            }
            finally
            {
                documentService = null;
            }
        
        }





    }
}