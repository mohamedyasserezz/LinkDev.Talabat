namespace LinkDev.Talabat.APIs.Controllers.Controllers.Errors
{
    public class ApiValdiationErrorResponse : ApiResponse
    {
        public required IEnumerable<string> Errors { get; set; }
        public ApiValdiationErrorResponse(string? message = null) : base(400, message)
        {
            
        }
    }
}
