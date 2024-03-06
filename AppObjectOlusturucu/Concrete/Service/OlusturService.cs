using AppObjectOlusturucu.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace AppObjectOlusturucu.Concrete.Service
{
	public static class OlusturService
	{
		public static void OlusturcuServiceCreate(this IServiceCollection service, IOlusturucuCreateHandler createHandler)
		{
			createHandler.HandleAndNext();
			//var a = Olusturucu.olustur;
		}
	}
}
