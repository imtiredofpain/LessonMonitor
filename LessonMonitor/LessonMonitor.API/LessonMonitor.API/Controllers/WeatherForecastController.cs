using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LessonMonitor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
            })
            .ToArray();
        }

        [HttpGet("model")]
        public WeatherForecast GetWeatherForecastModel()
        {
            var weatherForecastModel = typeof(WeatherForecast);

            var constructors = weatherForecastModel.GetConstructors();

            var defaultConstructor = constructors.FirstOrDefault(x => x.GetParameters().Length == 0);
            
            var obj = defaultConstructor.Invoke(null);

            var properties = weatherForecastModel.GetProperties();

            foreach (var property in properties)
            {
                if (_weatherforecastvalue.TryGetValue(property.Name, out var value))
                {
                //    if (property.PropertyType.Name == "DateTime")
                //    {
                //        var date = DateTime.Parse(value);
                //        property.SetValue(obj, date);
                //    }

                //    if (property.PropertyType.Name == "Int32")
                //    {
                //        var number = Int32.Parse(value);
                //        property.SetValue(obj, number);
                //    }

                //    if (property.PropertyType.Name == "String")
                //    {
                //        property.SetValue(obj, value);
                //    }

                    var specified = Convert.ChangeType(value, property.PropertyType); 
                    property.SetValue(obj, specified);  
                }

            }

            return (WeatherForecast)obj;
        }

        private Dictionary<string, string> _weatherforecastvalue = new Dictionary<string, string>
        {
            {"Date", DateTime.Now.ToString() },
            {"TemperatureC", "232" },
            {"Summary", Guid.NewGuid().ToString() },
        };

     
    }
}
