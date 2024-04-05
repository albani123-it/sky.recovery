using System;

namespace sky.recovery.Validation
{
    public interface IValidator
    {
        public IValidationContent WithPoCo(Object obj);
    }
}