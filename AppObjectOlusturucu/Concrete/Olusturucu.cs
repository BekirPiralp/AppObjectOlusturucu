using AppObjectOlusturucu.Concrete.Exceptions;

namespace AppObjectOlusturucu.Concrete
{
    public class Olusturucu : IDisposable
	{
		private static Olusturucu? _olusturucu = null;
		private static readonly object lockObj = new Object();

		private Olusturucu()
		{
			if (OlusturCreator._ObjectsTypes.Count == 0)
				throw new CreateException("count 0");

			foreach (var item in OlusturCreator._ObjectsTypes)
			{
				_Objects.Add(item.Key, Activator.CreateInstance(item.Value)!);
			}
		}

		public static Olusturucu olustur
		{
			get
			{
				try
				{
                    if (_olusturucu == null)
                    {
                        lock (lockObj)
                        {
                            if (_olusturucu == null)
                            {
                                _olusturucu = new Olusturucu();
                            }
                        }
                    }
                    return _olusturucu!; // burada null değil
                }
				catch(CreateException)
				{
					throw;
				}
				catch(Exception ex)
				{
					throw new Exception(ex.Message);
				}
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

        public void Dispose()
        {
            _olusturucu = null;

			_Objects.Clear();
        }
    }
}
