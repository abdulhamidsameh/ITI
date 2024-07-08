namespace ITI.PL.ViewModels
{
	public class ApiExceptionResponse : ErrorViewModel
	{
		public ApiExceptionResponse(int statusCode, string? message = null,string? details = null) 
			: base(statusCode)
		{
			Details = details;
		}

		public string? Details { get; set; }
    }
}
