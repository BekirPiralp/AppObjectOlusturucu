using AppObjectOlusturucu.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace AppObjectOlusturucu.Concrete.Service
{
	public static class OlusturService
	{
		public static void OlusturcuServiceCreate(this IServiceCollection service, IOlusturucuCreateHandler createHandler)
		{
			createHandler.HandleAndNext();
		}

		public static void OlusturcuServiceCreate(this IServiceCollection service, params IOlusturucuCreateHandler[] createHandlers )
		{
			if (createHandlers == null || createHandlers.Length <= 0)
				throw new ArgumentNullException(nameof(createHandlers));

			foreach ( var createHandler in createHandlers )
			{
				createHandler.HandleAndNext();
			}
		}

		public static void OlusturcuServiceCreate(this IServiceCollection service, IList<IOlusturucuCreateHandler> createHandlers)
		{
			if( createHandlers == null || createHandlers.Count <= 0 )
				throw new ArgumentNullException(nameof(createHandlers));

			foreach (var createHandler in createHandlers)
			{
				createHandler.HandleAndNext();
			}
		}

		public static void OlusturcuServiceCreate<T1>(this IServiceCollection service)
			where T1: class, IOlusturucuCreateHandler, new()
		{
			T1 createHandler = new();
			createHandler.HandleAndNext();
		}

		public static void OlusturcuServiceCreate<T1,T2>(this IServiceCollection service)
			where T1 : class, IOlusturucuCreateHandler, new()
			where T2 : class, IOlusturucuCreateHandler, new()
		{
			IList<IOlusturucuCreateHandler> createHandlers =
			[
				new T1(),
				new T2()
			];
			
			foreach( var createHandler in createHandlers )
				createHandler.HandleAndNext();
		}

		public static void OlusturcuServiceCreate<T1, T2, T3>(this IServiceCollection service)
			where T1 : class, IOlusturucuCreateHandler, new()
			where T2 : class, IOlusturucuCreateHandler, new()
			where T3 : class, IOlusturucuCreateHandler, new()
		{
			IList<IOlusturucuCreateHandler> createHandlers =
			[
				new T1(), //Data Access Layer
				new T2(), //Business Layer
				new T3()  //Other Layer
			];

			foreach (var createHandler in createHandlers)
				createHandler.HandleAndNext();
		}
	}
}
