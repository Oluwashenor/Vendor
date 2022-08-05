using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Vendor.Data;
using Vendor.Models.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vendor.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsAPIController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public AccountsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/<AccountsController>
        [HttpPost]
        public async Task<IActionResult> Table(string Range)
        {
           
            if (Range == null) Range = "Daily";
            
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault(); // get total page size
                var start = Request.Form["start"].FirstOrDefault(); // get starte length size from request.
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault(); // check if there is any search characters passed
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var outlet = _context.Outlets.Find(15);
                if (outlet == null)
                {
                    return NotFound();
                }
                var transactions = await _context.Transactions.Where(t => t.OutletId == outlet.Id).ToListAsync();
                var data = new List<ViewAccountDTO>();
               
                if (Range.Contains("Daily"))
                {
                    data = transactions.GroupBy(t => t.TransactionTime.Day).Select(t => new ViewAccountDTO
                    {
                        Name = "Daily Transaction",
                        Month = t.Select(t => t.TransactionTime.Day).First(),
                        Amount = t.Select(t => t.Amount).Sum(),
                    }).ToList();
                }
                //else if (Range.Contains("Weekly"))
                //{
                //    data = transactions.GroupBy(t => t.TransactionTime.`).Select(t => new ViewAccountDTO
                //    {
                //        Name = "Daily Transaction",
                //        Month = t.Select(t => t.TransactionTime.Month).First(),
                //        Amount = t.Select(t => t.Amount).Sum(),
                //    }).ToList();
                //}
                else
                {
                    data = transactions.GroupBy(t => t.TransactionTime.Month).Select(t => new ViewAccountDTO
                    {
                        Name = "Monthly Transaction",
                        Month = t.Select(t => t.TransactionTime.Month).First(),
                        Amount = t.Select(t => t.Amount).Sum(),
                    }).ToList();
                }
                   
                recordsTotal = data.Count();
                var d = data.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, range=Range, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = d };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET api/<AccountsController>/5
        [HttpGet]
        public string Get(int? id)
        {
            return "value";
        }

    }
}
