using AirLiquide.Api.v1.Middleware;
using AirLiquide.Application.v1.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace AirLiquide.Api.Test
{
    public class CustomExceptionMiddlewareTest
    {
        private Mock<ILogger<CustomExceptionMiddleware>> _logger;
        public CustomExceptionMiddlewareTest()
        {
            _logger = new Mock<ILogger<CustomExceptionMiddleware>>();
        }

        [Fact]
        public async Task ShouldReturnStatus200IfNoExceptionHappens()
        {
            const string responseOutput = "NoErrors";

            // Arrange
            var defaultContext = new DefaultHttpContext();
            defaultContext.Response.Body = new MemoryStream();
            defaultContext.Request.Path = "/";

            // Act
            var middlewareInstance = new CustomExceptionMiddleware(next: (innerHttpContext) =>
            {
                innerHttpContext.Response.WriteAsync(responseOutput);
                return Task.CompletedTask;
            }, _logger.Object);

            await middlewareInstance.InvokeAsync(defaultContext);

            Assert.Equal(200, defaultContext.Response.StatusCode);



        }

        [Fact]
        public async Task ShouldReturnstatus500IfExceptionHappens()
        {

            // Arrange
            var defaultContext = new DefaultHttpContext();
            defaultContext.Response.Body = new MemoryStream();
            defaultContext.Request.Path = "/";

            // Act
            var middlewareInstance = new CustomExceptionMiddleware(next: (innerHttpContext) =>
            {
                throw new Exception();
            }, _logger.Object);

            await middlewareInstance.InvokeAsync(defaultContext);

            Assert.Equal(500, defaultContext.Response.StatusCode);

        }


        [Fact]
        public async Task ShouldReturnstatus404IfNotFoundHappens()
        {

            // Arrange
            var defaultContext = new DefaultHttpContext();
            defaultContext.Response.Body = new MemoryStream();
            defaultContext.Request.Path = "/";

            // Act
            var middlewareInstance = new CustomExceptionMiddleware(next: (innerHttpContext) =>
            {
                throw new CustomNotFoundException();
            }, _logger.Object);

            await middlewareInstance.InvokeAsync(defaultContext);

            Assert.Equal(404, defaultContext.Response.StatusCode);

        }

        [Fact]
        public async Task ShouldReturnstatus409IfNotFoundHappens()
        {

            // Arrange
            var defaultContext = new DefaultHttpContext();
            defaultContext.Response.Body = new MemoryStream();
            defaultContext.Request.Path = "/";

            // Act
            var middlewareInstance = new CustomExceptionMiddleware(next: (innerHttpContext) =>
            {
                throw new CustomNoRowsAffectedException();
            }, _logger.Object);

            await middlewareInstance.InvokeAsync(defaultContext);

            Assert.Equal(409, defaultContext.Response.StatusCode);

        }



        [Fact]
        public async Task ShouldReturnstatus422IfNotFoundHappens()
        {

            // Arrange
            var defaultContext = new DefaultHttpContext();
            defaultContext.Response.Body = new MemoryStream();
            defaultContext.Request.Path = "/";

            // Act
            var middlewareInstance = new CustomExceptionMiddleware(next: (innerHttpContext) =>
            {
                throw new CustomValidationException(new string[] { "error1" });
            }, _logger.Object);

            await middlewareInstance.InvokeAsync(defaultContext);

            Assert.Equal(422, defaultContext.Response.StatusCode);

            var exceptionOutput = "[\"error1\"]";
            defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();
            Assert.Equal(exceptionOutput, body);

        }
    }
}
