namespace BlazorProds.Model
{
    public class ResponseDto
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public object Result { get; set; } = default!;
        public bool IsSuccess { get; set; } = true;
    }
}
