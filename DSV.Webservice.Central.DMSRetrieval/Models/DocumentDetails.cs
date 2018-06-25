using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSV.Webservice.Central.DMSRetrieval.Models
{

    /// <summary>
    /// DocumentDetails
    /// </summary>
    public class DocumentDetails
    {


        /// <summary>
        /// DocumentName
        /// </summary>
        public string DocumentName { get; set; }



        /// <summary>
        /// Actual Document in Byte array
        /// </summary>
        public byte[] DocumentByteData { get; set; }



        /// <summary>
        /// Document type
        /// </summary>
        public string DocumentType { get; set; }



        /// <summary>
        /// ErrorMessage
        /// </summary>
        internal string ErrorMessage { get; set; }

    }
}