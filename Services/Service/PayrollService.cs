using BusinessObjects.Models;
using DataAssetObjects;
using Repositories.Interface;
using Services.InterfaceServie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
	public class PayrollService : IPayrollService
	{
		private readonly IPayrollRepository ipayrollRepository;
		public PayrollService(IPayrollRepository ipayrollRepository)
		{
			this.ipayrollRepository = ipayrollRepository;
		}

		public List<Payroll> GetAll() => ipayrollRepository.GetAll();

		public Payroll GetById(int id) => ipayrollRepository.GetById(id);
	}
}
