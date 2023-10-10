using FluentValidation;
using System.Runtime.CompilerServices;
using Throw;

namespace N5.User.Application.Exceptions.ValidatableExtension;

/// <summary>
/// Extension methods for FluentValidation.
/// </summary>
public static partial class ValidatableExtensions
{
    /// <summary>
    /// Throws an exception if the validator return false in Validate method.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Validatable<TValue> IfNotValid<TValue>(this in Validatable<TValue> validatable, IEnumerable<IValidator<TValue>> validators, string message = "")
         where TValue : notnull
    {
        var context = new ValidationContext<TValue>(validatable.Value);
        var validationResults = Task.WhenAll(validators.Select(v => v.ValidateAsync(context))).Result;
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
        var _message = $"{(string.IsNullOrWhiteSpace(message) ? string.Empty : message + "; " + Environment.NewLine)}{string.Join("; " + Environment.NewLine, failures)}";

        if (failures.Count != 0)
            ExceptionThrower.Throw(
                _message,
                validatable.ExceptionCustomizations,
                _message);

        return ref validatable;
    }
}