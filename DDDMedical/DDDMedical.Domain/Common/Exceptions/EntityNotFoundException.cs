using System;

namespace DDDMedical.Domain.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() {}
        
        public EntityNotFoundException(string message) : base(message) {}
    }
}