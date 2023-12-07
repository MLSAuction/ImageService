using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ImageService.Controllers;
using ImageService.Models;
using ImageService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ImageServiceTests
{
    [TestFixture]
    public class ImageControllerTests
    {
        private Mock<IImageRepository> _imageRepositoryStub;
        private Mock<ILogger<ImageController>> _loggerMock;
        private Mock<IConfiguration> _configurationMock;
        private ImageController _imageController;

        [SetUp]
        public void Setup()
        {
            _imageRepositoryStub = new Mock<IImageRepository>();
            _loggerMock = new Mock<ILogger<ImageController>>();
            _configurationMock = new Mock<IConfiguration>();

            _imageController = new ImageController(_loggerMock.Object, _configurationMock.Object, _imageRepositoryStub.Object);
        }

        [Test]
        public void GetImage_ValidImageId_ReturnsOkResult()
        {
            // Arrange
            int validImageId = 1;
            ImageDTO expectedImage = new ImageDTO { ImageId = validImageId };
            _imageRepositoryStub.Setup(repo => repo.GetImage(validImageId)).Returns(expectedImage);

            // Act
            var result = _imageController.GetImage(validImageId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode);

            // Ensure that GetImage method was called with the correct imageId
            _imageRepositoryStub.Verify(repo => repo.GetImage(validImageId), Times.Once);

            // Ensure that the returned image is the expected image
            Assert.AreEqual(expectedImage, okResult.Value);
        }

        [Test]
        public void GetImage_NonexistentImageId_ReturnsNotFoundResult()
        {
            // Arrange
            int nonexistentImageId = 999;
            _imageRepositoryStub.Setup(repo => repo.GetImage(nonexistentImageId)).Returns((ImageDTO)null);

            // Act
            var result = _imageController.GetImage(nonexistentImageId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var notFoundResult = (NotFoundResult)result;
            Assert.AreEqual(404, notFoundResult.StatusCode);

            // Ensure that GetImage method was called with the correct imageId
            _imageRepositoryStub.Verify(repo => repo.GetImage(nonexistentImageId), Times.Once);
        }

        [Test]
        public void Delete_ValidImageId_ReturnsOkResult()
        {
            // Arrange
            int validImageId = 1;

            // Act
            var result = _imageController.Delete(validImageId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            var okResult = (OkResult)result;
            Assert.AreEqual(200, okResult.StatusCode);

            // Ensure that DeleteImage method was called with the correct imageId
            _imageRepositoryStub.Verify(repo => repo.DeleteImage(validImageId), Times.Once);
        }

    }
}
