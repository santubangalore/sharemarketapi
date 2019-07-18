using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using XOProject.Api.Controller;
using XOProject.Api.Model;
using XOProject.Services.Exchange;

namespace XOProject.Api.Tests
{
    public class PortfolioControllerTests
    {
		private Mock<IPortfolioService> _portfolioService =  new Mock<IPortfolioService>();
		private readonly PortfolioController _portfolioController;

		public PortfolioControllerTests()
		{
			_portfolioController = new PortfolioController(_portfolioService.Object);
		}

		[Test]
		public void Get_fetchesPortfolio()
		{
			int portfolio = 1;

			var result=_portfolioController.GetPortfolio(portfolio);

		}

		[Test]

		public async Task Post_InsertsPortfolio()
		{
			PortfolioModel model = new PortfolioModel
			{
			 Id=1,
			 Name="New Customer",
			   
			};

			var result = await _portfolioController.Post(model);
			Assert.NotNull(result);

			var createdResult = result as CreatedResult;
			Assert.NotNull(createdResult);

		}

	}
}
