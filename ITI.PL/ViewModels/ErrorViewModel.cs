namespace ITI.PL.ViewModels
{
	public class ErrorViewModel
	{
		public ErrorViewModel(int statusCode, string? message = null)
		{
			StatusCode = statusCode;
			Message = message ?? GetDefaultMessageForCode(statusCode);
		}

		private string? GetDefaultMessageForCode(int statusCode)
			=> statusCode switch
			{
				400 => "A Bad Request, You Have Made",
				401 => "Unauthorized, You Are Not",
				404 => "Resource Was Not Found",
				500 => "Server Error",
				_ => null,
			};

		public int StatusCode { get; set; }
		public string? Message { get; set; }
	}
}
