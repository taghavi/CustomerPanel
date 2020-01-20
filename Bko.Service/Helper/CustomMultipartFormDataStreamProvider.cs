using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Bko.Service.Helper
{
    public class CustomMultipartFormDataStreamProvider: MultipartFormDataStreamProvider
    {  
            public CustomMultipartFormDataStreamProvider(string path) : base(path)
            { }

       
        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
            {
                // override the filename which is stored by the provider (by default is bodypart_x)
                string originalFileName = headers.ContentDisposition.FileName.Trim('\"');

                return originalFileName;
            }
    }
}