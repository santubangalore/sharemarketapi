using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using XOProject.Repository.Domain;
using XOProject.Repository.Exchange;
using XOProject.Services.Exchange;
using System.Linq;
using XOProject.Services.Tests.Helpers;

namespace XOProject.Services.Tests
{
    public class TradeServiceTests
    {
		private readonly Mock<ITradeRepository> _tradeRepositoryMock = new Mock<ITradeRepository>();
		Mock<IShareRepository> shareRepository = new Mock<IShareRepository>();
		
		private  TradeService _tradeService;

		public TradeServiceTests()
		{
			IShareService _ishareServiceMock = new ShareServiceFake(shareRepository.Object);
			_tradeRepositoryMock.Setup(p => p.Query()).Returns(populateTradeData().AsQueryable());
			_tradeService = new TradeService(_tradeRepositoryMock.Object, _ishareServiceMock);
		}

		private List<Trade> populateTradeData()
		{
			List<Trade> _tradelist = new List<Trade>
			{
				new Trade{ Id=1, Action="BUY", ContractPrice=134.0M, NoOfShares=25, PortfolioId=1, Symbol="REL" },
				new Trade{ Id=1, Action="BUY", ContractPrice=134.0M, NoOfShares=25, PortfolioId=1, Symbol="REL" },
				new Trade{ Id=1, Action="BUY", ContractPrice=134.0M, NoOfShares=25, PortfolioId=1, Symbol="REL" },
				new Trade{ Id=1, Action="BUY", ContractPrice=134.0M, NoOfShares=25, PortfolioId=1, Symbol="REL" },
				new Trade{ Id=1, Action="BUY", ContractPrice=134.0M, NoOfShares=25, PortfolioId=1, Symbol="REL" }
			};
			return _tradelist;
		}
		[TearDown]
		public void Cleanup()
		{
			_tradeService = null;
		}

		[Test]
		public void TradeService_should_fetchData()
		{
			var result = _tradeService.GetByPortfolioId(1);
			Assert.NotNull(result);
		}
	}
}
