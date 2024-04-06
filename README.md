# <p style="text-align:center;">**Uygulama Nesne Olu�turcu** <br/> &emsp; (App Object Creater)</p>


|Platform|Bilgi|
|:---|---:|
|**Nuget**|[![NuGet Version](https://img.shields.io/nuget/v/AppObjectOlusturucu)](https://www.nuget.org/packages/AppObjectOlusturucu/)&emsp;![NuGet Downloads](https://img.shields.io/nuget/dt/AppObjectOlusturucu)|
|**Github**|[![GitHub language count](https://img.shields.io/github/languages/count/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/AppObjectOlusturucu)&emsp;[![GitHub top language](https://img.shields.io/github/languages/top/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/AppObjectOlusturucu)<br>&emsp;[![GitHub commit activity](https://img.shields.io/github/commit-activity/t/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/AppObjectOlusturucu/commits/master/)&emsp;[![GitHub last commit (branch)](https://img.shields.io/github/last-commit/BekirPiralp/AppObjectOlusturucu/master)](https://github.com/BekirPiralp/AppObjectOlusturucu)<br>&emsp;[![GitHub repo file or directory count](https://img.shields.io/github/directory-file-count/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/AppObjectOlusturucu)&emsp;[![GitHub repo size](https://img.shields.io/github/repo-size/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/AppObjectOlusturucu)<br>&emsp;[![GitHub watchers](https://img.shields.io/github/watchers/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/)&emsp;[![GitHub followers](https://img.shields.io/github/followers/BekirPiralp)](https://github.com/BekirPiralp/)&emsp;[![GitHub Repo stars](https://img.shields.io/github/stars/BekirPiralp/AppObjectOlusturucu)](https://github.com/BekirPiralp/)|



<br>

## Tan�t�m
&nbsp;Bu uygulama �ncelikli olarak ***RESTfull Api***lerde **katmanlar (*Layers*)** aras�nda ileti�imi ve nesne olu�turmas�n� sa�lamak amac� ile yaz�lm�� bir uygulamad�r. [<u>*Dependency �njection*</u>](https://www.google.com/search?q=Dependency+Injection) [(*�r. bkz.*)](https://gokhana.medium.com/dependency-injection-nedir-nas%C4%B1l-uygulan%C4%B1r-kod-%C3%B6rne%C4%9Fiyle-44f4b0d576e4) i�lemni sa�lamak i�in genelde **Kurucu Method ([Constructors](https://medium.com/@fatihhizgi1/constructor-method-e82d8286e4f2))** ile enjecte ederek sa�lamaktay�z fakat katmalar�n ve katmanlarda bulunan s�n�flar�n bol olmas� bizlere nesneleri **ara y�zleri (interface)** (*[bkz.](https://aysedemirel.medium.com/neden-interface-kullanal%C4%B1m-2852b276bae4)*) kullanarak temellendirip �zelle�tirdi�imiz s�n�flara eri�imlerimizin belirli merkezlerde toplamal� ve bu toplam�� oldu�umuz nesnelere **OOP** ([bkz.](https://furkanalaybeg.medium.com/nesne-y%C3%B6nelimli-programlama-oop-nedir-b6f805a9473f) [bkz.](https://feyyazacet.medium.com/object-oriented-programming-oop-a90c040e21b8))'nin temeli olan ***SOLID*** ([bkz.](https://gokhana.medium.com/solid-nedir-solid-yaz%C4%B1l%C4%B1m-prensipleri-nelerdir-40fb9450408e))'in gere�i fazla bir ba��ml�l�k olmadan nesneye heryerden eri�meyi sa�lamam�z gerekmektedir.  

&nbsp;Paketimiz yukar�da da bilrti�imiz **OOP** ve **SOLID** i�in gerekli olan tek merkezde toplama, kolay y�netim, rahat de�i�iklik ve d���k ba��ml�l�k gerekbinimlerinin hepsini sa�lamaktad�r.

## Kullan�m�
&nbsp;Paketizmi kullanabilmek i�in temel olarak iki i�lem mevcuttur. �lk olarak ilgili katman i�erisinde gerekli **inheritance (miras alma)** ve **implementions (uygulamalar)** yap�larak olu�turulacak nesneler belirlenmeli, ard�ndan **Program.cs** vb. yerde ise servis kullan�larak gerekli kay�tlar�n yap�l�p kullan�l�r hale getirilmesi gerekmektedir. Sonras� ise kullan�lmak istenen yerde kullanma...

### �rnek:

�lk olarak ilgili katmanlarda gerekli olan interface ve classlar aras�nda e�lemmeler yap�l�r. 

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

Gerekli e�lemelerin ard�ndan ***Program.cs*** vb. �ekilde ilk ba�lat�lacak yerden ilgi service �elierek �al��t�r�l�r.

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
Kullan�lmas� gereken di�er katmanda ise ca�r�lma ve nesne getirilmesi ise :

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
