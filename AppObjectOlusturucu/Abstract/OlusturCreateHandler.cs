namespace AppObjectOlusturucu.Abstract
{
    public abstract class OlusturCreateHandler : IOlusturucuCreateHandler
    {
        protected IOlusturucuCreateHandler? _nextHandler;
        public void HandleAndNext()
        {
            try
            {
                this.CreateObj();
                if (this._nextHandler != null)
                {
                    this._nextHandler.HandleAndNext();
                }
            }
            catch 
            {
                throw new MethodAccessException(nameof(this.CreateObj));
            }
        }

        /// <summary>
        /// Writing your code and add next handler
        /// Sample: this._nextHandler = Handler   ->(OlusturCreateHandle to Layer)
        /// </summary>
        public abstract void CreateObj();//next handler ataması ve obje kayıt yapılacak.
    }
}
