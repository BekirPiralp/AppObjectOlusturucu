using AppObjectOlusturucu.Concrete.Exceptions;

namespace AppObjectOlusturucu.Concrete
{
	public static class OlusturCreator
	{
		internal static Dictionary<Type, Type> _ObjectsTypes = new Dictionary<Type, Type>();
		public static void CreateObject<TInterface, TObject>(TInterface @interface, TObject @object)
			where TInterface : class
			where TObject : class, TInterface, new()
		{
			CreateObject<TInterface, TObject>();
		}

		public static void CreateObject<TInterface, TObject>()
			where TInterface : class
			where TObject : class, TInterface, new()
		{
			try
			{
				if (_ObjectsTypes.Count != 0)
				{
					var result = _ObjectsTypes.Where(p => p.Key.Equals(typeof(TInterface)));//TODO
					if (result != null && result.Count() > 0)
					{
						var response = result.Where(p => p.Value.Equals(typeof(TObject)));
						if (response != null && response.Count() > 0)
						{
							_ObjectsTypes.Remove(typeof(TInterface));
						}
						_ObjectsTypes.Add(typeof(TInterface), typeof(TObject));
					}

				}
				else
				{
					_ObjectsTypes.Add(typeof(TInterface), typeof(TObject));
				}
			}
			catch
			{

				throw new CreateException();
			}
		}
	}
}
