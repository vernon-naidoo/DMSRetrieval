using System;
using System.Configuration;
using System.Net;
using System.IO;


namespace DSV.Webservice.Central.DMSRetrieval.DocumentProcessMethods
{


    /// <summary>
    /// DocumentumApiService
    /// </summary>
    internal class DocumentumApiService : DocumentService 
    {
    
        /// <summary>
        /// AuthenticationHeaderValue
        /// </summary>
        private string AuthenticationHeaderValue
        {
            get
            {
                // Returns the password string.
                return ConfigurationManager.AppSettings["documentumapiauthheadervalue"];
            }
        }
        

        /// <summary>
        /// ApiUri
        /// </summary>
        private string ApiUri
        {
            get
            {
                // Returns the password string.
                return ConfigurationManager.AppSettings["documentumapiuri"];
            }
        }

        

        #region "Web Service Input parameters"



        private string objectId;
        /// <summary>
        /// ObjectId
        /// </summary>
        internal string ObjectId
        {
            get { return objectId; }
            //set { objectId = value; }
        }



        #endregion



        /// <summary>
        /// gets Document's Name
        /// </summary>
        private string GetDocumentName
        {
            get
            {
                return string.Format("{0}", objectId);
            }

        }




        internal DocumentumApiService(string p_objectId)
        {
            objectId = p_objectId;
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
            HttpStatusCode responseStatus = HttpStatusCode.BadRequest;
            try
            {
                HttpWebRequest httpWebRequest = null;
                
                try
                {
                    #region "Create and Send web request data"

                    string apiUriParams = string.Format("{0}/{1}/",
                                                ApiUri,
                                                objectId);

                    httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(apiUriParams));
                    //httpWebRequest.Headers.Add("authorization", "u0YrfLbqHJmSTcikVONokVLbN/ma5wa/EitczjejZS4qwM+6g=");
                    httpWebRequest.Headers.Add("authorization", AuthenticationHeaderValue);
                    //set the method type
                    httpWebRequest.Method = "GET";

                    #endregion


                    #region "Get Web response"


                    HttpWebResponse httpWebResponse = null;
                    try
                    {
                        httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (MemoryStream output = new MemoryStream())
                        using (Stream input = httpWebResponse.GetResponseStream())
                        {
                            input.CopyTo(output);
                            DocumentByteData = output.ToArray();
                        }

                        DocumentType = httpWebResponse.ContentType;
                        responseStatus = httpWebResponse.StatusCode;
                        httpWebResponse.Close();
                        DocumentName = GetDocumentName;
                        hasFileBeenRetrieved = true;
                    }
                    catch (WebException webEx)
                    {
                        if (webEx.Response == null)
                        {
                            ErrorMessage = webEx.InnerException.Message;
                        }
                        else
                        {
                            httpWebResponse = ((HttpWebResponse)webEx.Response);

                            using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpWebResponse.GetResponseStream()))
                            {
                                ErrorMessage = streamReader.ReadToEnd();
                            }
                            responseStatus = httpWebResponse.StatusCode;
                            httpWebResponse.Close();
                        }
                    }
                    catch (Exception ex)
                    {

                        ErrorMessage = ex.Message;
                        responseStatus = HttpStatusCode.InternalServerError;
                    }
                    finally
                    {
                        if (httpWebResponse != null)
                        {
                            httpWebResponse.Close();
                        }
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    responseStatus = HttpStatusCode.InternalServerError;
                } 
                
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                responseStatus = HttpStatusCode.InternalServerError;
            }
            return hasFileBeenRetrieved;
        }



      
    }
}