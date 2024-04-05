using System;

namespace sky.recovery.Validation
{
    public interface IValidationContent
    {
        public IValidatorRule Pick(String name);
        public ProcessResult Validate();
    }
}