namespace AppObjectOlusturucu.Concrete.Exceptions
{
	public class CreateException : Exception
	{
		private static string _message = "Kayıt Esnasında Hata Oluştu.\nDetay: ";
		public CreateException() : base(_message)
		{
			
		}
		public CreateException(string? message) : base(_message + message)
		{

		}
		public CreateException(string? message, Exception? innerException) : base(_message + message, innerException)
		{

		}
	}
}
