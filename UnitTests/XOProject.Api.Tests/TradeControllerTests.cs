using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XOProject.Api.Controller;
using XOProject.Api.Model;
using XOProject.Services.Exchange;

namespace XOProject.Api.Tests
{
    public class TradeControllerTests
    {
		private Mock<ITradeService> _tradeService = new Mock<ITradeService>();
		private TradeController _tradeController;
		public TradeControllerTests()
		{
			_tradeController = new TradeController(_tradeService.Object);
		}

		[Test]
		public async Task TradeController_post_shouldInsertRecord()
		{
			TradeModel model = new TradeModel
			{
			  Action="BUY",
			  Symbol="CGI",
			  NoOfShares=15,
			  PortfolioId=2
			};

			var result =await _tradeController.Post(model);
			Assert.NotNull(result);
			var created = result as CreatedResult;
			Assert.NotNull(created);

		}
		[Test]
		public async Task TradeController_post_shouldReturnInvalidFormat()
		{
			TradeModel model = new TradeModel
			{
				Action = "XYZ",
				Symbol = "CGI",
				NoOfShares = 15,
				PortfolioId = 2
			};

			var result = await _tradeController.Post(model);
			Assert.NotNull(result);
			var created = result as BadRequestResult;
			Assert.NotNull(created);
		}
		[Test]
		public void TradeController_get_shouldFetchRecord()
		{

			int portfolioid = 1;
			var result =  _tradeController.GetAllTradings(portfolioid);
			Assert.NotNull(result);

		}

		[Test]
		public async Task TradeController_get_returns_NotFoundResult()
		{
			int portfolioid = 247878;
			var result = await _tradeController.GetAllTradings(portfolioid);
			Assert.NotNull(result);
			var notfound = result as NotFoundResult;
			Assert.NotNull(notfound);
		}

		[Test]
		public async Task TradeController_get_returns_BadRequestResult()
		{
			int portfolioid = -223;
			var result = await _tradeController.GetAllTradings(portfolioid);
			Assert.NotNull(result);
			var badrequest = result as BadRequestResult;
			Assert.NotNull(badrequest);
		}
	}
}
