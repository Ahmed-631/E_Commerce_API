using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce.ServiceAbstraction.Common;

public class Result
{
    protected readonly List<Error> _errors = [];

    public IReadOnlyList<Error> Errors => _errors;

    public bool IsSuccess => _errors.Count == 0;

    public bool IsFailure => !IsSuccess;

    protected Result() { }

    protected Result(Error error) 
    {
        _errors.Add(error);
    }

    protected Result(List<Error> error) 
    {
        _errors.AddRange(error);
    }

    public static Result Ok() => new();
    public static Result Failure(Error error) => new(error);
    public static Result Failure(List<Error> error) => new(error);
}

public class Result<TValue> : Result
{
    private readonly TValue _value;

    public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("Cannot access the value of a failed result!");

    private Result(TValue value) : base()
    {
        _value = value;
    }
    private Result(Error error) : base(error)
    {
        _value = default!;
    }
    private Result(List<Error> errors) : base(errors)
    {
        _value = default!;
    } 

    public static Result<TValue> Ok(TValue value) => new(value);
    public static Result<TValue> Fail(Error error) => new(error);
    public static Result<TValue> Fail(List<Error> error) => new(error);

    public static implicit operator Result<TValue>(TValue value) => Ok(value);

    public static implicit operator Result<TValue>(Error error) => Fail(error);

    public static implicit operator Result<TValue>(List<Error> errors) => Fail(errors);
}
