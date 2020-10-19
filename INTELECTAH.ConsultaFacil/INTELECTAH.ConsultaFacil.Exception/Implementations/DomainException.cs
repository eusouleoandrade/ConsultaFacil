using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Exception.Implementations
{
    public class DomainException : AppException
    {
        public DomainException(string message) : base(message) { }

        public DomainException(IList<string> list) : base(list) { }

        public DomainException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
