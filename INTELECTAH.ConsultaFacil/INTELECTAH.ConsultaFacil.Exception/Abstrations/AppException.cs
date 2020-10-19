using System.Collections.Generic;

namespace INTELECTAH.ConsultaFacil.Exception
{
    public abstract class AppException : System.Exception
    {
        public AppException(string message) : base(message) { }

        public AppException(IList<string> list): base(string.Join(", ", list)) { }

        public AppException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
