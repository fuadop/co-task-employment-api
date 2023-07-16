namespace employment_api.Dto
{
	public class ResponseBase<T>
	{
		public int Status { get; set; }
		public string? Message { get; set; } 
		public T? Data { get; set; }

		public ResponseBase(int status, string? message, T data)
		{
			this.Status = status;
			this.Message = message;
			this.Data = data;
		}
	}
}

