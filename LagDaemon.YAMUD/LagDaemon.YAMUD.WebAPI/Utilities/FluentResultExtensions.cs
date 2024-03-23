using FluentResults;
using System;

public static class FluentResultExtensions
{
    public static Result<TSuccess> OnSuccess<TSuccess>(
        this Result<TSuccess> result,
        Action<TSuccess> onSuccessAction)
    {
        if (result.IsSuccess)
            onSuccessAction(result.Value);

        return result;
    }

    public static Result<TFailure> OnFailure<TFailure>(
    this Result<TFailure> result,
    Action<IEnumerable<IError>> onFailureAction)
    {
        if (result.IsFailed)
            onFailureAction(result.Errors);

        return result;
    }
}
