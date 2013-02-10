﻿using System;
using System.Net;
using System.Runtime.Serialization;
using Simple.NExtLib.IO;

namespace Simple.OData.Client
{
    [Serializable]
    public class WebRequestException : Exception
    {
        private readonly string _code;
        private readonly string _response;

        public static WebRequestException CreateFromWebException(WebException ex)
        {
            var response = ex.Response as HttpWebResponse;
            return response == null ?
                new WebRequestException(ex) :
                new WebRequestException(ex.Message, response, ex);
        }

        public WebRequestException(string message)
            : base(message)
        {
        }

        public WebRequestException(string message, string code)
            : base(message)
        {
            _code = code;
        }

        public WebRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public WebRequestException(string message, HttpWebResponse response, Exception inner)
            : base(message, inner)
        {
            _code = response.StatusCode.ToString();
            _response = QuickIO.StreamToString(response.GetResponseStream());
        }

        public WebRequestException(string message, string code, Exception inner)
            : base(message, inner)
        {
            _code = code;
        }

        public WebRequestException(WebException inner)
            : base("Unexpected WebException encountered", inner)
        {
        }

        protected WebRequestException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public string Code
        {
            get { return _code; }
        }

        public string Response
        {
            get { return _response; }
        }
    }
}
