using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOProject.Repository.Domain;
using XOProject.Services.Domain;
using XOProject.Services.Exchange;

namespace XOProject.Api.Tests.Helpers
{
	public class AnalyticsServiceFake : IAnalyticsService
	{
		private readonly List<HourlyShareRate> _shareRepository;

		public AnalyticsServiceFake()
		{
			_shareRepository = new List<HourlyShareRate>()
			{
				new HourlyShareRate() { Id=1, Symbol="REL", Rate=12.90M, TimeStamp=new DateTime(2018,10,9,11,25,32) },
				new HourlyShareRate() { Id=2, Symbol="REL", Rate=13.90M, TimeStamp=new DateTime(2018,10,9,13,48,19) },
				new HourlyShareRate() { Id=3, Symbol="REL", Rate=14.90M, TimeStamp=new DateTime(2018,10,9,15,25,12) },
				new HourlyShareRate() { Id=4, Symbol="CBI", Rate=15.90M, TimeStamp=new DateTime(2018,10,9,14,5,11) },
				new HourlyShareRate() { Id=5, Symbol="CBI", Rate=16.90M, TimeStamp=new DateTime(2018,10,9,12,15,26) },
				new HourlyShareRate() { Id=6, Symbol="LST", Rate=17.90M, TimeStamp=new DateTime(2018,10,9,12,01,01) },
				new HourlyShareRate() { Id=7, Symbol="CBI", Rate=15.90M, TimeStamp=new DateTime(2018,10,9,14,5,11) },
				new HourlyShareRate() { Id=8, Symbol="CBI", Rate=16.90M, TimeStamp=new DateTime(2018,10,9,12,15,26) },
				new HourlyShareRate() { Id=9, Symbol="LST", Rate=17.90M, TimeStamp=new DateTime(2018,10,9,12,01,01) },

				new HourlyShareRate() { Id=10, Symbol="CBI", Rate=15.90M, TimeStamp=new DateTime(2018,10,9,14,5,11) },
				new HourlyShareRate() { Id=11, Symbol="CBI", Rate=16.90M, TimeStamp=new DateTime(2018,10,9,12,15,26) },
				new HourlyShareRate() { Id=12, Symbol="LST", Rate=17.90M, TimeStamp=new DateTime(2018,10,9,12,01,01) },

				new HourlyShareRate() { Id=14, Symbol="CBI", Rate=15.90M, TimeStamp=new DateTime(2018,10,9,14,5,11) },
				new HourlyShareRate() { Id=15, Symbol="CBI", Rate=16.90M, TimeStamp=new DateTime(2018,10,9,12,15,26) },
				new HourlyShareRate() { Id=16, Symbol="LST", Rate=17.90M, TimeStamp=new DateTime(2018,10,9,12,01,01) },

				new HourlyShareRate() { Id=17, Symbol="CBI", Rate=15.90M, TimeStamp=new DateTime(2018,1,9,14,5,11) },
				new HourlyShareRate() { Id=18, Symbol="CBI", Rate=16.90M, TimeStamp=new DateTime(2018,1,14,12,15,26) },
				new HourlyShareRate() { Id=19, Symbol="LST", Rate=17.90M, TimeStamp=new DateTime(2018,1,6,12,01,01) },

				new HourlyShareRate() { Id=20, Symbol="CBI", Rate=15.90M, TimeStamp=new DateTime(2018,04,11,14,5,11) },
				new HourlyShareRate() { Id=21, Symbol="CBI", Rate=16.90M, TimeStamp=new DateTime(2018,03,10,12,15,26) },
				new HourlyShareRate() { Id=22, Symbol="LST", Rate=17.90M, TimeStamp=new DateTime(2018,02,01,12,01,01) },

				new HourlyShareRate() { Id=23, Symbol="CBI", Rate=15.90M, TimeStamp=new DateTime(2018,04,11,14,5,11) },
				new HourlyShareRate() { Id=24, Symbol="CBI", Rate=16.90M, TimeStamp=new DateTime(2018,03,10,12,15,26) },
				new HourlyShareRate() { Id=25, Symbol="LST", Rate=17.90M, TimeStamp=new DateTime(2018,02,01,12,01,01) },

				new HourlyShareRate() { Id=26, Symbol="CBI", Rate=15.90M, TimeStamp=new DateTime(2018,1,18,14,5,11) },
				new HourlyShareRate() { Id=27, Symbol="CBI", Rate=16.90M, TimeStamp=new DateTime(2018,1,20,12,15,26) },
				new HourlyShareRate() { Id=28, Symbol="LST", Rate=17.90M, TimeStamp=new DateTime(2018,1,24,12,01,01) },

				new HourlyShareRate() { Id=29, Symbol="CBI", Rate=15.90M, TimeStamp=new DateTime(2018,1,16,14,5,11) },
				new HourlyShareRate() { Id=30, Symbol="CBI", Rate=16.90M, TimeStamp=new DateTime(2018,1,24,12,15,26) },
				new HourlyShareRate() { Id=31, Symbol="LST", Rate=17.90M, TimeStamp=new DateTime(2018,1,28,12,01,01) }
			};
		}


		public async Task<AnalyticsPrice> GetDailyAsync(string symbol, DateTime day)
		{
			var Dailyaggregate = _shareRepository.Where(p => p.Symbol == symbol && p.TimeStamp.Date == day.Date);
			if (Dailyaggregate.Any())
			{
				var dailyMax = Dailyaggregate.Max(p => p.Rate);
				var dailyMin = Dailyaggregate.Min(p => p.Rate);
				var dailyOpen = Dailyaggregate.OrderBy(t => t.TimeStamp)
								.FirstOrDefault().Rate;
				var dailyClose = Dailyaggregate.OrderByDescending(p => p.TimeStamp).FirstOrDefault().Rate;

				return new AnalyticsPrice
				{
					Open = dailyOpen,
					Close = dailyClose,
					High = dailyMax,
					Low = dailyMin
				};
			}
			else
			{
				return new AnalyticsPrice();
			}
		}

		public async Task<AnalyticsPrice> GetWeeklyAsync(string symbol, int year, int week)
		{
			var weeklyaggregate = _shareRepository.Where(p => p.TimeStamp.Year == year);
			var query = from o in weeklyaggregate
						where GetISOWeek(o.TimeStamp) == week
						select o;

			var weeklyMax = query.Max(p => p.Rate);
			var weeklyMin = query.Min(p => p.Rate);
			var weeklyOpen = query.OrderBy(t => t.TimeStamp).FirstOrDefault().Rate;

			var weeklyClose = query.OrderByDescending(p => p.TimeStamp).FirstOrDefault().Rate;
			return new AnalyticsPrice
			{
				Open = weeklyOpen,
				Close = weeklyClose,
				High = weeklyMax,
				Low = weeklyMin
			};
		}

		public async Task<AnalyticsPrice> GetMonthlyAsync(string symbol, int year, int month)
		{
			var monthlyaggegate = _shareRepository.Where(p => p.TimeStamp.Year == year && p.TimeStamp.Month == month);

			var monthlyMax = monthlyaggegate.Max(p => p.Rate);
			var monthlyMin = monthlyaggegate.Min(p => p.Rate);
			var monthlyOpen = monthlyaggegate.OrderBy(t => t.TimeStamp)
								.FirstOrDefault().Rate;
			var monthlyClose = monthlyaggegate.OrderByDescending(p => p.TimeStamp).FirstOrDefault().Rate;
			return new AnalyticsPrice
			{
				Open = monthlyOpen,
				Close = monthlyClose,
				High = monthlyMax,
				Low = monthlyMin
			};
		}

		private int GetISOWeek(DateTime day)
		{
			return System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(day, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
		}
	}
}
