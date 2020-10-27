# .NET Core Localization and Enum
 This is a demo project base on .NET core WebAPI template WeatherForecast, implement Localization on Text and Enum, read i18n text from resources.  

---  
1. Add resource file(.resx)  
   Usually I will put my resource file in [Resources] folder, remember put cultureInfo name on resx file like this `{name}.{culture}.resx`  
   ![img](https://i.imgur.com/XRQEbNa.png)
2. Setup Startup.cs  
   In `public void ConfigureServices(IServiceCollection services)` add following code  
   ```csharp
   //set Support Cultures and default
   services.Configure<RequestLocalizationOptions>(options =>
   {
       var supportedCultures = new[]{
           new CultureInfo("en-US"),
           new CultureInfo("zh-TW"),
           new CultureInfo("ja-JP")
       };
       options.DefaultRequestCulture = new RequestCulture("en-US");
       options.SupportedCultures = supportedCultures;
       options.SupportedUICultures = supportedCultures;
   });
   //set resource path
   services.AddLocalization(options => options.ResourcesPath = "Resources");
   ```
3. Setup controller  
   * DI
      ``` csharp
     private readonly IStringLocalizer _localizer;
     public YourController(IStringLocalizerFactory factory)
     {
         _localizer = factory.Create("Message", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
     }
      ```  
   * Get Value  
      using `_localizer[{name}]`  to get the value from resource file, for example, `_localizer["cold"]` can get key name [cold]'s culture string, if can't find the key in resource, it will just output the key name "cold" as string.  
      You can check [WeatherForecastController.cs](https://github.com/died/.NET-Core-Localization-and-Enum/blob/main/EnumTest/Controllers/WeatherForecastController.cs) for more usage detail.  
  
  * Enum with i18N  
   It was easy, just use `_localizer[{enumName}.ToString()]` to get culture string from resource, or you can check [here](https://github.com/died/.NET-Core-Localization-and-Enum/blob/33619f0d50671a9b8f8235c49fec4f0260a11706/EnumTest/Controllers/WeatherForecastController.cs#L77-L78) for the usage.  
  
  
---  
##### That's all.