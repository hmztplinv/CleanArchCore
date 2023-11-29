

using FluentValidation.Results;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
        ValidationErrors = new List<string>();
    }

    public BadRequestException(string message,ValidationResult validationResult) : base(message)
    {
        ValidationErrors = new();
        foreach (var validationError in validationResult.Errors)
        {
            ValidationErrors.Add(validationError.ErrorMessage);
        }
        
    }

    public List<string> ValidationErrors { get; set; }
}