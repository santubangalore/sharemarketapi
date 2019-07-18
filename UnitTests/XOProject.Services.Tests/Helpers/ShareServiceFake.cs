using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XOProject.Repository.Domain;
using XOProject.Services.Exchange;
using System.Linq;
using XOProject.Repository.Exchange;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace XOProject.Services.Tests.Helpers
{
	public class ShareServiceFake : GenericService<HourlyShareRate>,  IShareService
	{
		private List<HourlyShareRate> _shareRepository;

		public ShareServiceFake(IShareRepository shareRepository):base (shareRepository)
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
		public async Task<IList<HourlyShareRate>> GetAllAsync()
		{
			return  _shareRepository;
		}

		public async Task<HourlyShareRate> GetByIdAsync(int id)
		{
			return _shareRepository.Where(p => p.Id == id).Single();
		}

		public async Task<IList<HourlyShareRate>> GetBySymbolAsync(string symbol)
		{
			return _shareRepository.Where(p => p.Symbol == symbol).ToList();
		}

		public async Task<HourlyShareRate> GetHourlyAsync(string symbol, DateTime dateAndHour)
		{
			return _shareRepository.Where(p => p.Symbol == symbol && p.TimeStamp.Date == dateAndHour.Date && p.TimeStamp.Hour == dateAndHour.Hour).FirstOrDefault();
		}

		public async Task<HourlyShareRate> GetLastPriceAsync(string symbol)
		{

			 var result=   _shareRepository.Where(x => x.Symbol.Equals(symbol))
				.OrderByDescending(x => x.TimeStamp)
				.FirstOrDefault();
			return result;
		}

		public async Task InsertAsync(HourlyShareRate entity)
		{
			_shareRepository.Add(entity);
		}

		public async  Task UpdateAsync(HourlyShareRate entity)
		{
			var item = _shareRepository.Where(p => p.Symbol == entity.Symbol).OrderByDescending(p => p.TimeStamp).Single();
			_shareRepository.Remove(item);
			_shareRepository.Add(entity);
		}
		public async Task<HourlyShareRate> UpdateLastPriceAsync(string symbol, decimal rate)
		{
			var item = _shareRepository.Where(p => p.Symbol == symbol).OrderByDescending(p => p.TimeStamp).FirstOrDefault(); 
			item.Rate = rate;
			return item;
		}
	}
}
