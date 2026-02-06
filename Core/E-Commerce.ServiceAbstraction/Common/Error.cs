namespace E_Commerce.ServiceAbstraction.Common;

public class Error
{
    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public static Error Failure(string code = "General.Failure", string description = "A failure has occurred") =>
        new(code, description, ErrorType.Failure);
    public static Error Validation(string code = "General.Validation", string description = "A validation error has occurred") =>
        new(code, description, ErrorType.Validation);
    public static Error NotFound(string code = "General.NotFound", string description = "A 'Not Found' error has occurred") =>
        new(code, description, ErrorType.NotFound);
    public static Error Conflect(string code = "General.Conflect", string description = "A conflect error has occurred") =>
        new(code, description, ErrorType.Conflect);
    public static Error Unauthorized(string code = "General.Unauthorized", string description = "A Unauthorized error has occurred") =>
        new(code, description, ErrorType.Unauthorized);


}
