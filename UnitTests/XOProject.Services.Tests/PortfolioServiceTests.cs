using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using XOProject.Repository.Exchange;
using XOProject.Services.Exchange;

namespace XOProject.Services.Tests
{
   public class PortfolioServiceTests
   {
		private readonly Mock<IPortfolioRepository> _shareRepositoryMock = new Mock<IPortfolioRepository>();
		private readonly PortfolioService _portfolioService;

		public PortfolioServiceTests()
		{
			_portfolioService = new PortfolioService(_shareRepositoryMock.Object);
		}

		[TearDown]
		public void Cleanup()
		{
			_shareRepositoryMock.Reset();
		}

		[Test]
		public void PortfolioService_should_fetchData()
		{
			int month = 2;
			int year = 2019;
			string symbol = "IDC";
			var result = _portfolioService.GetAllAsync();
			Assert.NotNull(result);
		}
	}
}
