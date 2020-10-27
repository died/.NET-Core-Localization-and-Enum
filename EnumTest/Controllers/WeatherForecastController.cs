using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EnumTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStringLocalizer _localizer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStringLocalizerFactory factory)
        {
            _logger = logger;
            _localizer = factory.Create("Message", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),

                //get from array
                //Summary = _localizer[Summaries[rng.Next(Summaries.Length)]]

                //get from enum
                Summary = GetSummary(rng.Next(Summaries.Length))
            })
            .ToArray();
        }

        [HttpGet("tw")]
        public dynamic GetZhTw()
        {
            SetCulture("zh-TW");
            return Get();
        }

        [HttpGet("en")]
        public dynamic GetEnUs()
        {
            SetCulture("en-US");
            return Get();
        }

        [HttpGet("jp")]
        public dynamic GetJaJp()
        {
            SetCulture("ja-JP");
            return Get();
        }

        private void SetCulture(string name)
        {
            //usually this setting will put on baseController, get user culture from Claims, but this demo didn't using identity, so put here.
            Thread.CurrentThread.CurrentCulture = new CultureInfo(name);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        private string GetSummary(int value)
        {
            var summary = (WeatherSummary)value;
            return _localizer[summary.ToString()];
        }
    }
}
