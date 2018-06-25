using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.ServiceModel;


namespace DSV.Webservice.Central.DMSRetrieval.DocumentProcessMethods
{


    /// <summary>
    /// DocumentumService
    /// </summary>
    internal class DocumentumService : DocumentService
    {


        #region "WebService user credentials"


        /// <summary>
        /// DMS Service User Id
        /// </summary>
        private string UserId
        {
            get
            {
                // Returns the userId string.
                return ConfigurationManager.AppSettings["userId"];
            }
        }



        /// <summary>
        /// DMS Service Password
        /// </summary>
        private string Password
        {
            get
            {
                // Returns the password string.
                return ConfigurationManager.AppSettings["password"];
            }
        }


        #endregion



        #region "Web Service Input parameters"

        private string region;
        /// <summary>
        /// Region
        /// </summary>
        internal string Region 
        { 
            get{ return region; }
            //set{ region = value; }
        }



        private string businessSegment;
        /// <summary>
        /// BusinessSegment
        /// </summary>
        internal string BusinessSegment 
        { 
            get{ return businessSegment; }
            //set{ businessSegment = value; }
        }



        private string businessUnit;
        /// <summary>
        /// BusinessUnit
        /// </summary>
        internal string BusinessUnit 
        { 
            get{ return businessUnit; }
            //set{ businessUnit = value; }
        }

        
        
        private string client;
        /// <summary>
        /// Client
        /// </summary>
        internal string Client 
        { 
            get{ return client; }
            //set{ client = value; }
        }



        private string docType;
        /// <summary>
        /// DocType
        /// </summary>
        internal string DocType 
        { 
            get{ return docType; }
            //set{ docType = value; }
        }



        private string captivaRef;
        /// <summary>
        /// CaptivaRef
        /// </summary>
        internal string CaptivaRef 
        { 
            get{ return captivaRef; }
            //set{ captivaRef = value; }
        }

                                                   
        #endregion


        /// <summary>
        /// gets Document's Name
        /// </summary>
        private string GetDocumentName
        {
            get
            {
                return string.Format("{0}{1}{2}-{3}-{4}-{5}",
                                                     region,
                                                     businessSegment,
                                                     businessUnit,
                                                     client,
                                                     docType,
                                                     captivaRef);
            }

        }



        private static void IgnoreBadCertificates()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

        }

        private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }



        //----------------------------------------------------------
        //Constructor: DocumentumService
        //----------------------------------------------------------
        /// <summary>
        /// DocumentumService
        /// </summary>
        /// <param name="p_region"></param>
        /// <param name="p_businessSegment"></param>
        /// <param name="p_businessUnit"></param>
        /// <param name="p_client"></param>
        /// <param name="p_docType"></param>
        /// <param name="p_refNo"></param>
        internal DocumentumService(string p_region,
                                    string p_businessSegment,
                                    string p_businessUnit,
                                    string p_client,
                                    string p_docType,
                                    string p_refNo)
        {
            region = p_region;
            businessSegment = p_businessSegment;
            businessUnit = p_businessUnit;
            client = p_client;
            docType = p_docType;
            captivaRef = p_refNo;
        }





        //----------------------------------------------------------
        //Method: GetDocumentData
        //----------------------------------------------------------
        /// <summary>
        /// GetDocumentData
        /// </summary>
        /// <param name="p_url"></param>
        /// <returns></returns>
        private bool GetDocumentData(string p_url)
        {
            bool hasFileBeenretrieved = false;            
            try
            {
                 var request = (HttpWebRequest)WebRequest.Create(p_url);
                 var response = (HttpWebResponse)request.GetResponse();
                 //if (response.StatusCode == HttpStatusCode.OK && response.ContentType == "application/pdf")
                 if (response.StatusCode == HttpStatusCode.OK)
                 {
                     using (MemoryStream output = new MemoryStream())
                     using (Stream input = response.GetResponseStream())
                     {
                         input.CopyTo(output);
                         DocumentByteData = output.ToArray();
                         DocumentType = response.ContentType;
                         hasFileBeenretrieved = true;
                         DocumentName = GetDocumentName;
                     }
                 }
                 response.Close();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                hasFileBeenretrieved = false;                
            }

            return hasFileBeenretrieved;
        }



        //------------------------------------------------------------
        // METHOD : GetDocumentDetails
        //------------------------------------------------------------
        /// <summary>
        /// GetDocumentDetails
        /// </summary>
        /// <returns></returns>
        internal override bool GetDocumentDetails()           
        {
            bool hasFileBeenRetrieved = false;
            DMSService.DMSSearchServicePortClient pc = new DMSService.DMSSearchServicePortClient("DMSSearchServicePort");

            try
            {

                pc.ClientCredentials.UserName.UserName = UserId;// cred[0];
                pc.ClientCredentials.UserName.Password = Password;// cred[1];

                //Global.log.Write(" Input values for reg - " + p_region + " bs - " + p_businessSegment + " bu - " + p_businessUnit + " cl - " + p_client + " dt - " + p_docType + " bol - " + p_captivaRef);

                using (OperationContextScope scope = new OperationContextScope((IContextChannel)pc.InnerChannel))
                {
                    MessageHeader<string> header = new MessageHeader<string>("secret message");
                    var untyped = header.GetUntypedHeader("Identity", "http://context.core.datamodel.fs.documentum.emc.com/");
                    OperationContext.Current.OutgoingMessageHeaders.Add(untyped);
                    // now make the WCF call within this using block
                    IgnoreBadCertificates();

                    DMSService.DocURLResponse urlRes = pc.searchContractLogisticsDoc(region,
                                                        businessSegment,
                                                        businessUnit,
                                                        client,
                                                        docType,
                                                        captivaRef);

                    if (urlRes.replyStatus == true)//"true")
                    {
                        hasFileBeenRetrieved = GetDocumentData(urlRes.documentURL);
                    }//if (urlRes.replyStatus == true)//"true")
                    else
                    {
                        ErrorMessage = "No Document Found";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return hasFileBeenRetrieved;
        }
        


        
    }
}