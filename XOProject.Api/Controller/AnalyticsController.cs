using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XOProject.Api.Model.Analytics;
using XOProject.Services.Domain;
using XOProject.Services.Exchange;
using Microsoft.AspNetCore.Mvc;

namespace XOProject.Api.Controller
{
    [Route("api")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("daily/{symbol}/{year}/{month}/{day}")]
        public async Task<IActionResult> Daily([FromRoute] string symbol, [FromRoute] int year, [FromRoute] int month, [FromRoute] int day)
        {
		
			var result = await _analyticsService.GetDailyAsync(symbol, new DateTime(year, month, day));
			if (result != null)
			{
				var dailyResult = new DailyModel
				{
					Price = Map(result),
					Symbol = symbol,
					Day = new DateTime(year, month, day)
				};

				return Ok(dailyResult);
			}
			else
				return NotFound("not found");

		}


        [HttpGet("weekly/{symbol}/{year}/{week}")]
        public async Task<IActionResult> Weekly([FromRoute] string symbol, [FromRoute] int year, [FromRoute] int week)
        {
			var result = await _analyticsService.GetWeeklyAsync(symbol,year, week);
			if (result != null)
			{
				var weeklyresult = new WeeklyModel()
				{
					Symbol = symbol,
					Year = year,
					Week = week,
					Price = Map(result)
				};

				return Ok(result);
			}
			else
				return NotFound();
        }

        [HttpGet("monthly/{symbol}/{year}/{month}")]
        public async Task<IActionResult> Monthly([FromRoute] string symbol, [FromRoute] int year, [FromRoute] int month)
        {
	
			var result = await _analyticsService.GetMonthlyAsync(symbol, year, month);
			if (result != null)
			{
				var monthlyResult = new MonthlyModel()
				{
					Symbol = symbol,
					Year = year,
					Month = month,
					Price = Map(result)
				};

				return Ok(result);
			}
			else
				return NotFound();
        }

        private PriceModel Map(AnalyticsPrice price)
        {

            return new PriceModel()
            {
                Open = price.Open,
                Close = price.Close,
                High = price.High,
                Low = price.Low
            };
        }
    }
}