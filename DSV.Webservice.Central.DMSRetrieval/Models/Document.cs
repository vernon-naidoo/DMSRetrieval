using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DSV.Webservice.Central.DMSRetrieval.DocumentProcessMethods;
using DSV.Webservice.Central.DMSRetrieval.log4Net;

namespace DSV.Webservice.Central.DMSRetrieval.Models
{
    /// <summary>
    /// Document
    /// </summary>
    public class Document : DocumentRequest
    {
        
        /// <summary>
        /// Is Processing successful
        /// </summary>
        public bool IsSuccessful { get; set; }



        /// <summary>
        /// Error message if process is not successful
        /// </summary>
        public string ErrorMessage { get; set; }



        /// <summary>
        /// DocumentDetails
        /// </summary>
        public DocumentDetails DocumentDetails { get; set; }



        /// <summary>
        /// DocumentServiceType
        /// </summary>
        private DocumentServiceType DocumentServiceType { get; set; }



       
        //-------------------------------------------
        //Method : GetDocument
        //-------------------------------------------
        /// <summary>
        /// GetDocument
        /// </summary>
        internal void GetDocument()
        {
            DocumentDetails = null;
            IsSuccessful = false;
            DocumentServiceType = DocumentServiceType.NONE;


            //check for Input
            if (!ValidateInput())
            {
                IsSuccessful = false;
                return;
            }


            try
            {
                if (DocumentServiceType == DocumentServiceType.NONE)
                {
                    ErrorMessage = "Invalid Document Service Type";
                }
                else
                {
                    //process document request
                    ProcessDocument.Start(DocumentServiceType, this);
                }
            }
            catch (Exception ex)
            {                
                ErrorMessage = ex.Message;
            }
        }




        //-----------------------------------------------------------------
        // METHOD : ValidateInput
        //-----------------------------------------------------------------
        /// <summary>
        /// ValidateInput
        /// </summary>
        /// <returns></returns>
        private bool ValidateInput()
        {
            bool isValid = true;

            StringBuilder sbError = new StringBuilder();
            if (!string.IsNullOrEmpty(ObjectId))
            {
                DocumentServiceType = DocumentServiceType.REST_SERVICE_1P;
                return isValid;
            }// if (!string.IsNullOrEmpty(ObjectId))
            else
            {               

                if (string.IsNullOrEmpty(Region))
                {
                    isValid = false;
                    //sbError.AppendLine("Region parameter is required");
                }
                if (string.IsNullOrEmpty(BusinessSegment))
                {
                    isValid = false;
                    //sbError.AppendLine("BusinessSegment parameter is required");
                }
                if (string.IsNullOrEmpty(BusinessUnit))
                {
                    isValid = false;
                    //sbError.AppendLine("BusinessUnit parameter is required");
                }
                if (string.IsNullOrEmpty(Client))
                {
                    isValid = false;
                    //sbError.AppendLine("Client parameter is required");
                }
                if (string.IsNullOrEmpty(DocType))
                {
                    isValid = false;
                    //sbError.AppendLine("DocType parameter is required");
                }
                if (string.IsNullOrEmpty(RefNo))
                {
                    isValid = false;
                    //sbError.AppendLine("RefNo parameter is required");
                }


                if (!isValid)
                {
                    sbError.AppendLine("Either Region,BusinessSegment,BusinessUnit,Client,DocType,RefNo parameters are required");
                    sbError.AppendLine("or ObjectId parameter is required");
                }
                else
                {
                    DocumentServiceType = DocumentServiceType.WCF_SERVICE_6P;
                }


            }//else  if (!string.IsNullOrEmpty(ObjectId))

            if (!isValid)
            {
                ErrorMessage = sbError.ToString();
                Logger.Error(ErrorMessage);
            }


            return isValid;
        }










    }
}