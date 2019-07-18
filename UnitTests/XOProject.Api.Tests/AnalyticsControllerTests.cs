using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XOProject.Api.Controller;
using XOProject.Api.Tests.Helpers;
using XOProject.Repository;
using XOProject.Repository.Exchange;
using XOProject.Services.Exchange;

namespace XOProject.Api.Tests
{
    public class AnalyticsControllerTests
    {
		private IAnalyticsService _analyticsService  = new AnalyticsServiceFake();
		private AnalyticsController  _analyticsController;

		public  AnalyticsControllerTests()
		{
			_analyticsController = new AnalyticsController(_analyticsService);
		}
		
		[Test]
		public async Task AnalyticsControllerDaily_ShouldReturnData()
		{
			string symbol = "REL";
			int year = 2018;
			int month = 10;
			int day =9;
			var result = await _analyticsController.Daily(symbol, year, month, day);
			Assert.NotNull(result);
		
		}

		[Test]
		public async Task AnalyticsControllerWeeklyShouldReturnData()
		{
			string symbol = "REL";
			int year = 2018;
			int week = 3;
			var result = await _analyticsController.Weekly(symbol, year, week);
			Assert.NotNull(result);
		}

		[Test]
		public async Task AnalyticsControllerMonthlyShouldReturnData()
		{
			//daily/{symbol}/{year}/{month}/{day}
			string symbol = "REL";
			int year = 2018;
			int month = 10;
			
			var result = await _analyticsController.Monthly(symbol, year, month);
			Assert.NotNull(result);
		
		}

	}
}
