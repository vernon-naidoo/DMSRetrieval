using System.Collections.Generic;
using System.Linq;


using System.Net.Http;

namespace DSV.Webservice.Central.DMSRetrieval.CommonMethods
{


    /// <summary>
    /// HelperOperations
    /// </summary>
    internal class HelperOperations
    {


        //--------------------------------------------------------------------------------
        //Method : GetSpecificHeaderValueFromHttpHeaders
        //--------------------------------------------------------------------------------
        /// <summary>
        /// Gets Specific Header Value From HttpHeaders
        /// </summary>
        /// <param name="p_request"></param>
        /// <param name="p_headerName"></param>
        /// <returns></returns>
        private static string GetSpecificHeaderValueFromHttpHeaders(HttpRequestMessage p_request,
                                                            string p_headerName)
        {
            string headerValue = string.Empty;
            IEnumerable<string> headerValues;
            if (p_request.Headers.TryGetValues(p_headerName, out headerValues))
            {
                headerValue = headerValues.FirstOrDefault();
            }
            return headerValue;
        }
    }
}