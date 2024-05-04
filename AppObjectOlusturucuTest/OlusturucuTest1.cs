using AppObjectOlusturucu.Abstract;
using AppObjectOlusturucu.Concrete;
using AppObjectOlusturucu.Concrete.Exceptions;
using AppObjectOlusturucu.Concrete.Service;
using Microsoft.Extensions.DependencyInjection;

namespace AppObjectOlusturucuTest
{
    [TestClass]
    public class OlusturucuTest1
    {
        [TestMethod]
        public void Count_Error()
        {
            Olusturucu _olusturucu;
            try
            {
                _olusturucu = Olusturucu.olustur;
            }
            catch (CreateException ex)
            {
                Assert.AreEqual(ex.Message, "Kayýt Esnasýnda Hata Oluþtu.\nDetay: count 0", false, "Error");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + " \n source: " + ex.Source + "\n stackTrace: " + ex.StackTrace);
            }
        }

        [TestMethod]
        public void Object_Refrence()
        {
            ServiceCollection services = new ServiceCollection();

            services.OlusturcuServiceCreate<TestCreator>();

            Olusturucu? _olusturucu = null;
            try
            {
                _olusturucu = Olusturucu.olustur;
            }
            catch (Exception ex)
            {
                Assert.Fail("Hata ile karþilaþýldý.\nDetay: " + ex.Message);
            }


            Assert.IsNotNull(_olusturucu, "Nesne Oluþturulamadý");

        }

        [TestMethod]
        public void Object_Refrence_Two_Create()
        {
            ServiceCollection services = new ServiceCollection();

            try
            {
                services.OlusturcuServiceCreate<TestCreator>();
            }
            catch (Exception ex)
            {

                Assert.Fail("Hata ile karþilaþýldý.\nDetay: " + ex.Message + "\nStacktTrace: " + ex.StackTrace);
            }

            Olusturucu? _olusturucu = null;
            try
            {
                _olusturucu = Olusturucu.olustur;
            }
            catch (Exception ex)
            {
                Assert.Fail("Hata ile karþilaþýldý.\nDetay: " + ex.Message);
            }


            Assert.IsNotNull(_olusturucu, "Nesne Oluþturulamadý");

            _olusturucu.Dispose();
            _olusturucu = null;


            try
            {
                _olusturucu = Olusturucu.olustur;
            }
            catch (Exception ex)
            {
                Assert.Fail("Hata ile karþilaþýldý.\nDetay: " + ex.Message);
            }


            Assert.IsNotNull(_olusturucu, "Nesne Oluþturulamadý");

        }

        [TestMethod]
        public void Get_Object()
        {
            Object_Refrence();

            var a = Olusturucu.olustur.GetObj<ITest>();
            var b = Olusturucu.olustur.GetObj<ITest2>();

            Assert.IsNotNull(a, "Nesne getirilemedi. a");

            Assert.AreEqual(a.message, "testClass");
            a.print();

            Assert.IsNotNull(b, "Nesne getirilemedi. b");

            Assert.AreEqual(b.message, "hellllloooo");
            b.print2();

            Olusturucu.olustur.Dispose();
        }
        [TestMethod]
        public void Get_Object_2()
        {

            ServiceCollection services = new ServiceCollection();
            services.OlusturcuServiceCreate<TestCreator2>();

            Olusturucu? _olusturucu = null;

            _olusturucu = Olusturucu.olustur;


            var a = Olusturucu.olustur.GetObj<ITest3>();
            var b = Olusturucu.olustur.GetObj<ITest2>();

            Assert.IsNotNull(a, "Nesne getirilemedi. a");
            Assert.AreEqual(a.message, "testClass3");
            a.print();

            Assert.IsNotNull(b, "Nesne getirilemedi. b");
            Assert.AreEqual(b.message, "hellllloooo");
            b.print2();

            Olusturucu.olustur.Dispose();
        }

        class TestCreator : OlusturCreateHandler
        {
            public override void CreateObj()
            {
                OlusturCreator.CreateObject<ITest, TestClass>();
                OlusturCreator.CreateObject<ITest2, TestClass2>();
            }
        }

        class TestCreator2 : OlusturCreateHandler
        {
            public override void CreateObj()
            {
                OlusturCreator.CreateObject<ITest, TestClass>();
                OlusturCreator.CreateObject<ITest2, TestClass2>();
                OlusturCreator.CreateObject<ITest3, TestClass3_1>();
                OlusturCreator.CreateObject<ITest3, TestClass3>();
            }
        }

        interface IT
        {
            string message { get; }
        }

        interface ITest : IT
        {
            void print();
        }
        interface ITest3 : IT
        {
            void print();
        }
        class TestClass : ITest
        {
            public string message { get => "testClass"; }

            public void print()
            {
                Console.WriteLine(message);
            }
        }
        class TestClass3_1 : ITest3
        {
            public string message => "testClass3_1";

            public void print()
            {
                Console.WriteLine(message);
            }
        }
        class TestClass3 : ITest3
        {
            public string message => "testClass3";

            public void print()
            {
                Console.WriteLine(message);
            }
        }
        interface ITest2 : IT
        {
            void print2();
        }

        class TestClass2 : ITest2
        {
            public string message => "hellllloooo";

            public void print2()
            {
                Console.WriteLine(message);
            }

        }

        [TestMethod]
        public void Get_Object_in_object()
        {
            ServiceCollection services = new();
            services.OlusturcuServiceCreate(new Get_object_Creta_Handler());

            IIT? a = null;
            IIT2? b = default;
            try
            {
                a = Olusturucu.olustur.GetObj<IIT>();
                b = Olusturucu.olustur.GetObj<IIT2>();
            }
            catch (Exception ex)
            {
                Assert.Fail("Message of: " + ex.Message + "\n stacktrace of: " + ex.StackTrace);
            }

            Assert.IsNotNull(a);
            Assert.IsInstanceOfType<TClass>(a);
            Assert.AreEqual(a.message, nameof(TClass));

            a.print();

            Assert.IsNotNull(b);
            Assert.IsInstanceOfType<TClass2>(b);
            Assert.AreEqual(b.message, nameof(TClass2));

            b.print();

            Olusturucu.olustur.Dispose();
        }


        class Get_object_Creta_Handler : OlusturCreateHandler
        {
            public override void CreateObj()
            {
                OlusturCreator.CreateObject<IIT, TClass>();
                OlusturCreator.CreateObject<IIT2, TClass2>();
            }
        }

        interface IIT : IT
        {
            void print();
        }
        class TClass : IIT
        {
            public TClass()
            {
                var a = Olusturucu.olustur.GetObj<IIT2>();
                if (a == null)
                    throw new NullReferenceException();
            }

            public string message => nameof(TClass);

            public void print()
            {
                Console.WriteLine(message);
            }
        }

        interface IIT2 : IT
        {
            void print();

        }
        class TClass2 : IIT2
        {
            public string message => nameof(TClass2);

            public void print()
            {
                Console.WriteLine(message);
            }



        }

    }
}