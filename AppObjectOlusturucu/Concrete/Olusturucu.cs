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
				//Default value is null because object create is at down, that do create is in method of getObj   
				_Objects.Add(item.Key, null!);
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
                    return _olusturucu!; // burada null değil  / it is not null here
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

			// if Object value is null, that create with Activator of create instance method 
			_Objects[typeof(TInterface)] ??= Activator.CreateInstance(OlusturCreator._ObjectsTypes[typeof(TInterface)]!)!;


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
