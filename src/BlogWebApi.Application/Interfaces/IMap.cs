namespace BlogWebApi.Application.Interfaces
{
    public interface IMapper<in TInput, out TOutput>
    {
        TOutput Map(TInput from);
    }
}