using InoveFk.Core.Error;
using FluentValidation;

namespace InoveFk.Domain.Base;

public class DtoValidate
{
    public static ErrorValidation Validate<T>(T model, AbstractValidator<T> validator)
    {
        var listError = new List<ErrorResponse>();
        var validationResult = validator.Validate(model);

        validationResult.Errors.ForEach(e => listError.Add(
            new ErrorResponse(key: e.PropertyName, message: e.ErrorMessage, statusCode: e.ErrorCode)));

        var validate = new ErrorValidation()
        {
            Invalid = !validationResult.IsValid,
            Errors = listError
        };

        return validate;
    }
}
