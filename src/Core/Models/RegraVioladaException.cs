namespace Core.Models
{
    using System;

    public class RegraVioladaException : Exception
    {
        public RegraVioladaException(
            string messageFormat,
            params object[] parameters) : base(string.Format(messageFormat, parameters))
        {
        }
    }
}