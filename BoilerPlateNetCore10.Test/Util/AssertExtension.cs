using BoilerPlateNetCore10.Domain.Validation;

namespace BoilerPlateNetCore10.Test.Util
{
    public static class AssertExtension
    {

        public static void WithMessage(this DomainExceptionValidation exception, string message)
        {
            if (exception.Message.Contains(message)) 
                Assert.True(true); 
            else
                Assert.Fail($"Expected message: {exception.Message}"); 
        }
        
    }
}
