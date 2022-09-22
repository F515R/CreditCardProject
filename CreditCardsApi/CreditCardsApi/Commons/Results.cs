namespace CreditCardsApi.Commons
{
    public enum ResultEnum {
        OK,
        NOT_OK,
        ERROR
    }
    public class Result<T>
    {
        public ResultEnum status { get; set; }
        public T? content { get; set; }
    }
}
