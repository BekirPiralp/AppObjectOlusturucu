namespace AppObjectOlusturucu.Concrete
{
	public class Olusturucu
	{
		private static Olusturucu? _olusturucu;
		private static readonly object lockObj = new Object();

		private Olusturucu()
		{
			foreach (var item in OlusturCreator._ObjectsTypes)
			{
				_Objects.Add(item.Key, Activator.CreateInstance(item.Value)!);
			}
		}

		public static Olusturucu olustur
		{
			get
			{
				if (olustur == null)
				{
					lock (lockObj)
					{
						if (olustur == null)
						{
							_olusturucu = new Olusturucu();
						}
					}
				}
				return _olusturucu!; // burada null değil
			}
		}

		private static Dictionary<object, object> _Objects = new Dictionary<dynamic, object>();


		public TInterface? GetObj<TInterface>(TInterface resultObject)
			where TInterface : class
		{
			TInterface result;

			result = GetObj<TInterface>()!;

			return result;
		}
		public TInterface? GetObj<TInterface>()
			where TInterface : class
		{
			TInterface result;

			result = (TInterface)_Objects.GetValueOrDefault(typeof(TInterface))!;

			return result;
		}

	}
}
