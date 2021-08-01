using ExcelFunctionCalculateTest.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace ExcelFunctionCalculateTest.Test
{
    public class UnitTest1 : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;
        private readonly ITestOutputHelper output;

        public UnitTest1(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            this.factory = factory;
            this.output = output;
        }


        [Fact]
        public void Test1()
        {
            var allResponseTimes = new List<(DateTime Start, DateTime End)>();

            using (var scope = factory.Services.CreateScope())
            {
                var vmparam = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                var a = new DefaultViewModel(vmparam);

                var random = new Random();

                for (int i = 0; i < 1000; i++)
                {
                    var start = DateTime.Now;
                    a.ReadAndReturnValueFromExcel();
                    var end = DateTime.Now;

                    //Assert.Equal(a.Result, result.ToString());
                    allResponseTimes.Add((start, end));

                    output.WriteLine($"Executed in {(end - start).TotalMilliseconds} milliseconds");
                }

            }

            var expected = 100;
            var actual = (int)allResponseTimes.Select(r => (r.End - r.Start).TotalMilliseconds).Average();
            Assert.True(actual <= expected, $"Expected average response time of less than or equal to {expected} ms but was {actual} ms.");
        }
    }
}
