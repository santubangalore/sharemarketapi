using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using XOProject.Api.Controller;
using XOProject.Api.Model;
using XOProject.Repository.Domain;
using XOProject.Repository.Exchange;
using XOProject.Services.Exchange;
using XOProject.Services.Tests.Helpers;

namespace XOProject.Api.Tests
{
    public class ShareControllerTests
    {
		private  IShareService _shareServiceMock;
		private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();
		private readonly ShareController _shareController;

        public ShareControllerTests()
        {
			_shareServiceMock = new ShareServiceFake(_shareRepositoryMock.Object);
			_shareController = new ShareController(_shareServiceMock);
        }

        [TearDown]
        public void Cleanup()
        {
			_shareServiceMock = null;
        }

		[Test]
		public async Task put_shouldUpdateSharePrice()
		{
			string symbol = "CBI";
			decimal listprice = 899.9M;

			var result = await _shareController.UpdateLastPrice(symbol,listprice);

		}

		[Test]
        public async Task Post_ShouldInsertHourlySharePrice()
        {
            // Arrange
            var hourRate = new HourlyShareRateModel
            {
                Symbol = "CBI",
                Rate = 330.0M,
                TimeStamp = new DateTime(2018, 01, 16, 14, 5, 0)
            };

            // Act
            var result = await _shareController.Post(hourRate);

            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
        }

		[Test]
		public void Get_GetLatestPrice()
		{
			string symbol = "CBI";
			var result = _shareController.GetLatestPrice(symbol);
			Assert.NotNull(result);
	
		}

		[Test]
		public async Task Post_ShouldInsertDailySharePrice()
		{
			// Arrange
			var hourRate = new HourlyShareRateModel
			{
				Symbol = "CBI",
				Rate = 330.0M,
				TimeStamp = new DateTime(2018, 01, 16, 14, 5, 0)
			};

			// Act
			var result = await _shareController.Post(hourRate);

			// Assert
			Assert.NotNull(result);

			// TODO: This unit test is broken, the result received from the Post method is correct.
			var createdResult = result as CreatedResult;
			Assert.NotNull(createdResult);
		
		}

	}
}
