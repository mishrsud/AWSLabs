using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.SQSEvents;
using FluentAssertions;
using MessageProcessor.Features;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MessageProcessor.Tests
{
    public class BootstrapTests
    {
        [Fact]
        public async Task ItJustWorks()
        {
//            using (var mockLambdaHost = new MockLambdaHost())
//            {
//                await mockLambdaHost.Execute(new SQSEvent
//                {
//                    Records = new List<SQSEvent.SQSMessage>
//                    {
//                        new SQSEvent.SQSMessage
//                        {
//                            Body = "Test message"
//                        }
//                    }
//                });
//            }

            var sc = new ServiceCollection();
            var serviceProvider = Startup.Initialise(sc);
            serviceProvider.Should().NotBeNull();
        }
    }
}