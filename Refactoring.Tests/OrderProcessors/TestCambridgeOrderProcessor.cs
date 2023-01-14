using FluentAssertions;
using Moq;
using Rectoring.Services;
using Refactoring.Web.DomainModels;
using Refactoring.Web.Services.Interfaces;
using Refactoring.Web.Services.OrderProcessors;

namespace Refactoring.Tests.OrderProcessors;

public class TestCambridgeOrderProcessor
{
    public class PrintAdvertAndUpdateOrderAsyncMethod
    {
        [Fact]
        public async void Given_DateIsTuesday_ImageUrl_Set_OnOrderAdvert()
        {
            //Arrange
            var testOrder = new Order
            {
                Id = "Foo"
            };
            
            var fakeAdvertPrinter = new Mock<IAdvertPrinter>();
            var fakeChamberOfCommerceApi = new Mock<IChamberOfCommerceApi>();
            var fakeDataTimeResolver = new Mock<IDateTimeResolver>();

            fakeDataTimeResolver.Setup(m => m.IsItTuesday()).Returns(true);
            
            
            var fakeDataResult = new DataResult
            {
                ThumbnailUrl = "http://example.com/some_thumbnail.png",
                Title ="My title"
            };
            fakeChamberOfCommerceApi
                .Setup(m => m.GetImageAndThumbnailDataFor(It.IsAny<String>()))
                .Returns(Task.FromResult(fakeDataResult));
            
            //Act
            var sut = new CambridgeProcessor(
                fakeChamberOfCommerceApi.Object,
                fakeAdvertPrinter.Object,
                fakeDataTimeResolver.Object);
            
            var result = await sut.PrintAdvertAndUpdateOrderAsync(testOrder);
            //Assert
            result.Advert?.ImageUrl.Should().Be(fakeDataResult.ThumbnailUrl);
        }
        
        [Fact]
        public async void Given_DateIsNotTuesday_ImageUrl_NotSet_OnOrderAdvert()
        {
            //Arrange
            var testOrder = new Order
            {
                Id = "Foo"
            };
            
            var fakeAdvertPrinter = new Mock<IAdvertPrinter>();
            var fakeChamberOfCommerceApi = new Mock<IChamberOfCommerceApi>();
            var fakeDataTimeResolver = new Mock<IDateTimeResolver>();

            fakeDataTimeResolver.Setup(m => m.IsItTuesday()).Returns(false);
            
            
            var fakeDataResult = new DataResult
            {
                ThumbnailUrl = "http://example.com/some_thumbnail.png",
                Title ="My title"
            };
            fakeChamberOfCommerceApi
                .Setup(m => m.GetImageAndThumbnailDataFor(It.IsAny<String>()))
                .Returns(Task.FromResult(fakeDataResult));
            
            //Act
            var sut = new CambridgeProcessor(
                fakeChamberOfCommerceApi.Object,
                fakeAdvertPrinter.Object,
                fakeDataTimeResolver.Object);
            
            var result = await sut.PrintAdvertAndUpdateOrderAsync(testOrder);
            //Assert
            result.Advert?.ImageUrl.Should().BeNull();
        }
    }
}