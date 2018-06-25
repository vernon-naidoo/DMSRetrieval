using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DSV.Webservice.Central.DMSRetrieval.CommonMethods;
using DSV.Webservice.Central.DMSRetrieval.log4Net;

namespace DSV.Webservice.Central.DMSRetrieval.Models
{
    /// <summary>
    /// DocumentRequest
    /// </summary>
    public class DocumentRequest
    {
        /// <summary>
        /// Region supplied as input
        /// </summary>           
        public string Region { get; set; }


        /// <summary>
        /// BusinessSegment supplied as input
        /// </summary>  
        public string BusinessSegment { get; set; }



        /// <summary>
        /// BusinessUnit supplied as input
        /// </summary>
        public string BusinessUnit { get; set; }



        /// <summary>
        /// Client supplied as input
        /// </summary> 
        public string Client { get; set; }



        /// <summary>
        /// DocType supplied as input
        /// </summary> 
        public string DocType { get; set; }



        /// <summary>
        /// Captiva ReferenceNumber supplied as input
        /// </summary>
        public string RefNo { get; set; }



        /// <summary>
        /// ObjectId supplied as input
        /// </summary>
        public string ObjectId { get; set; }
    }
}