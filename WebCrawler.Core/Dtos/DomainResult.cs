using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.Core.Dtos
{
    public class DomainResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Object Result { get; set; }
    }
}
