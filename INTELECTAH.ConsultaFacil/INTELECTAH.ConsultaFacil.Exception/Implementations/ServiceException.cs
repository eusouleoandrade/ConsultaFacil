using System;
using System.Collections.Generic;
using System.Text;

namespace INTELECTAH.ConsultaFacil.Exception.Implementations
{
    public class ServiceException : AppException
    {
        private const string _message = "Service processing failed.";

        public ServiceException(string message) : base(message) { }

        public ServiceException(IList<string> list) : base(list) { }

        public ServiceException(string message, System.Exception innerException) : base(message, innerException) { }

        public ServiceException(System.Exception innerException) : base(_message, innerException) { }
    }
}
