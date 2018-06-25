using System;
namespace DSV.Webservice.Central.DMSRetrieval.DocumentProcessMethods
{
    internal abstract class DocumentService
    {
        /// <summary>
        /// DocumentName
        /// </summary>
        internal string DocumentName { get; set; }



        /// <summary>
        /// Actual Document in Byte array
        /// </summary>
        internal byte[] DocumentByteData { get; set; }



        /// <summary>
        /// Document type
        /// </summary>
        internal string DocumentType { get; set; }



        /// <summary>
        /// ErrorMessage
        /// </summary>
        internal string ErrorMessage { get; set; }
        //internal abstract bool FindDocuments();


        /// <summary>
        /// GetDocumentDetails
        /// </summary>
        /// <returns></returns>
        internal abstract bool GetDocumentDetails();
    }

}
