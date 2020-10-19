using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Exception
{
    public class RepositoryException : AppException
    {
        private const string _message = "Database processing failed.";

        public RepositoryException() : this(_message) { }

        public RepositoryException(string message) : base(message) { }

        public RepositoryException(string message, System.Exception innerException) : base(message, innerException) { }

        public RepositoryException(System.Exception innerException) : base(_message, innerException) { }

        public RepositoryException(IList<string> exceptions) : base(exceptions) { }
    }
}
