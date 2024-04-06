# <p style="text-align:center;">**Uygulama Nesne Oluþturcu** <br/> &emsp; (App Object Creater)</p>


|Platform|Bilgi|
|:---|---:|
|**Nuget**|[![NuGet Version](https://img.shields.io/nuget/v/AppObjectOlusturucu)](https://www.nuget.org/packages/AppObjectOlusturucu/)&emsp;![NuGet Downloads](https://img.shields.io/nuget/dt/AppObjectOlusturucu)|
|**Github**|[![GitHub language count](https://img.shields.io/github/languages/count/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/AppObjectOlusturucu)&emsp;[![GitHub top language](https://img.shields.io/github/languages/top/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/AppObjectOlusturucu)<br>&emsp;[![GitHub commit activity](https://img.shields.io/github/commit-activity/t/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/AppObjectOlusturucu/commits/master/)&emsp;[![GitHub last commit (branch)](https://img.shields.io/github/last-commit/BekirPiralp/AppObjectOlusturucu/master)](https://github.com/BekirPiralp/AppObjectOlusturucu)<br>&emsp;[![GitHub repo file or directory count](https://img.shields.io/github/directory-file-count/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/AppObjectOlusturucu)&emsp;[![GitHub repo size](https://img.shields.io/github/repo-size/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/AppObjectOlusturucu)<br>&emsp;[![GitHub watchers](https://img.shields.io/github/watchers/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/)&emsp;[![GitHub followers](https://img.shields.io/github/followers/BekirPiralp)](https://github.com/BekirPiralp/)&emsp;[![GitHub Repo stars](https://img.shields.io/github/stars/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/)|



<br>

## Tanýtým
&nbsp;Bu uygulama öncelikli olarak ***RESTfull Api***lerde **katmanlar (*Layers*)** arasýnda iletiþimi ve nesne oluþturmasýný saðlamak amacý ile yazýlmýþ bir uygulamadýr. [<u>*Dependency Ýnjection*</u>](https://www.google.com/search?q=Dependency+Injection) [(*ör. bkz.*)](https://gokhana.medium.com/dependency-injection-nedir-nas%C4%B1l-uygulan%C4%B1r-kod-%C3%B6rne%C4%9Fiyle-44f4b0d576e4) iþlemni saðlamak için genelde **Kurucu Method ([Constructors](https://medium.com/@fatihhizgi1/constructor-method-e82d8286e4f2))** ile enjecte ederek saðlamaktayýz fakat katmalarýn ve katmanlarda bulunan sýnýflarýn bol olmasý bizlere nesneleri **ara yüzleri (interface)** (*[bkz.](https://aysedemirel.medium.com/neden-interface-kullanal%C4%B1m-2852b276bae4)*) kullanarak temellendirip özelleþtirdiðimiz sýnýflara eriþimlerimizin belirli merkezlerde toplamalý ve bu toplamýþ olduðumuz nesnelere **OOP** ([bkz.](https://furkanalaybeg.medium.com/nesne-y%C3%B6nelimli-programlama-oop-nedir-b6f805a9473f) [bkz.](https://feyyazacet.medium.com/object-oriented-programming-oop-a90c040e21b8))'nin temeli olan ***SOLID*** ([bkz.](https://gokhana.medium.com/solid-nedir-solid-yaz%C4%B1l%C4%B1m-prensipleri-nelerdir-40fb9450408e))'in gereði fazla bir baðýmlýlýk olmadan nesneye heryerden eriþmeyi saðlamamýz gerekmektedir.  

&nbsp;Paketimiz yukarýda da bilrtiðimiz **OOP** ve **SOLID** için gerekli olan tek merkezde toplama, kolay yönetim, rahat deðiþiklik ve düþük baðýmlýlýk gerekbinimlerinin hepsini saðlamaktadýr.

## Kullanýmý
&nbsp;Paketizmi kullanabilmek için temel olarak iki iþlem mevcuttur. Ýlk olarak ilgili katman içerisinde gerekli **inheritance (miras alma)** ve **implementions (uygulamalar)** yapýlarak oluþturulacak nesneler belirlenmeli, ardýndan **Program.cs** vb. yerde ise servis kullanýlarak gerekli kayýtlarýn yapýlýp kullanýlýr hale getirilmesi gerekmektedir. Sonrasý ise kullanýlmak istenen yerde kullanma...

### Örnek:

Ýlk olarak ilgili katmanlarda gerekli olan interface ve classlar arasýnda eþlemmeler yapýlýr. 

``` C#
using AppObjectOlusturucu.Abstract;
using AppObjectOlusturucu.Concrete;

namespace MyAppBackend.ExampleLayer {

    public class ExampleLayerCreateHandler : OlusturCreateHandler, IOlusturucuCreateHandler {

        public override void CreateObj() {

            this._nextHandler = null;

            // or

            this._nextHandler = ExampleLayer1CreateHandler;

            ...
            ...


            #region example

            OlusturCreator.CreateObject<IFooClassExampleLayer, FooClassExampleLayer>();

            ...
            ...
              //your create ...
            .
            #endregion
        }
    }
}

```

Gerekli eþlemelerin ardýndan ***Program.cs*** vb. þekilde ilk baþlatýlacak yerden ilgi service çelierek çalýþtýrýlýr.

``` C#
using AppObjectOlusturucu.Concrete.Service;
using MyAppBackend.ExampleLayer;

var builder = WebApplication.CreateBuilder(args);

...
...

builder.Services.OlusturcuServiceCreate(
    new ExampleLayerCreateHandler()
);

// or

builder.Services.OlusturcuServiceCreate(
    new ExampleLayerCreateHandler(),
    new ExampleLayer1CreateHandler(),
    new ExampleLayer2CreateHandler()
);

...
...
```
Kullanýlmasý gereken diðer katmanda ise caðrýlma ve nesne getirilmesi ise :

``` C#
using AppObjectOlusturucu.Concrete

namespace OtherExampleLayer {
    class OtherClassOtherExampleLayer {

        OtherClassOtherExampleLayer(){

            IFooClassExampleLayer foo = Olusturucu.olustur.GetObj<IFooClassExampleLayer>();

        }

        //or

        IFooClassExampleLayer foo = Olusturucu.olustur.GetObj<IFooClassExampleLayer>();


    }
}
```

**ya da**

``` C#
using AppObjectOlusturucu.Concrete

namespace OtherExampleLayer {
    class OtherClassOtherExampleLayer {

        OtherClassOtherExampleLayer(){
            
            Olusturucu.olustur;

            //or

            var a = Olusturucu.olustur;

        }

        IFooClassExampleLayer foo = Olusturucu.GetObj<IFooClassExampleLayer>();

    }
}
```
