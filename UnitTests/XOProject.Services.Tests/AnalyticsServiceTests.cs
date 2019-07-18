
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XOProject.Repository.Exchange;
using XOProject.Services.Exchange;

namespace XOProject.Services.Tests
{
    public class AnalyticsServiceTests
    {
		private AnalyticsService _service ;
		private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();

		public AnalyticsServiceTests()
		{
			_service = new AnalyticsService(_shareRepositoryMock.Object);
		}

		[Test]
		public async Task AnalyticsService_should_get_DailyData()
		{
			int day = 20;
			int month = 2;
			int year = 2019;
			string symbol = "IDC";
			var result =  _service.GetDailyAsync(symbol, new DateTime(year, month, day));
			Assert.NotNull(result);
			
		}

		[Test]
		public async Task AnalyticsService_should_get_WeeklyData()
		{
			int week = 2;
			int year = 2019;
			string symbol = "IDC";
			var result = _service.GetWeeklyAsync(symbol, year, week);
			Assert.NotNull(result);
		}

		[Test]
		public async Task AnalyticsService_should_get_MonthlyData()
		{
			int month = 2;
			int year = 2019;
			string symbol = "IDC";
			var result = _service.GetMonthlyAsync(symbol,year,month);
			Assert.NotNull(result);
		}
	}
}
