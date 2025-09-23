using System.ComponentModel.DataAnnotations;
using Common.Common.Enums;
using Common.Common.Exceptions;
using Common.Common.Models;

namespace Common.Common.Halpers;

public static class DtoHelpers
{
    public static ErrorModelResult Validate(this IRequestDtoBase data)
    {
        var context = new ValidationContext(data);
        var validationResults = new List<ValidationResult>();

        if (Validator.TryValidateObject(data, context, validationResults, true))
            return null;

        var errorModelResult = new ErrorModelResult
        {
            Errors = []
        };

        foreach (var validationResult in validationResults)
            errorModelResult.Errors.Add(new ErrorModelResultEntry(ErrorType.ModelState, validationResult.ErrorMessage));

        return errorModelResult;
    }

    public static T ValidateInline<T>(this T data) where T : IRequestDtoBase
    {
        if (data.Validate() is { } validationResult)
            throw new ValidationFailedException(validationResult.Errors.Aggregate(string.Empty,
                (_, __) => $"{_}{Environment.NewLine}{__.Message}"));

        return data;
    }
}