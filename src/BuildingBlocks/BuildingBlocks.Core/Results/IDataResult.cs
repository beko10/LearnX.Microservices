namespace BuildingBlocks.Core.Results;

public interface IDataResult<TData> : IResult
{
    public TData? Data { get; init; }
}
