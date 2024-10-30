using authservice.Interfaces;
using authservice.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace authservice.test.Framework
{
    public class ServiceCollectionTests
    {
        [Fact]
        void test()
        {
            var serviceColletion = new ServiceCollection();
            serviceColletion.AddSingleton<ISettingsService, SettingsService>();
            serviceColletion.AddTransient<IAllegroService, AllegroService>();
 
            var serviceProvider = serviceColletion.BuildServiceProvider();
          
        }
    }
}
