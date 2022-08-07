using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendor.Constants;
using Vendor.Data;
using Vendor.Models;
using Vendor.Models.DTOs;
using static Vendor.Utilities.Enums;

namespace Vendor.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SalesAPIController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
        }

        // GET: api/SalesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            return await _context.Sales.ToListAsync();
        }

        // GET: api/SalesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // PUT: api/SalesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, Sale sale)
        {
            if (id != sale.Id)
            {
                return BadRequest();
            }

            _context.Entry(sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SalesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostSale(CreateSalesDTO sales)
        { 
            var paymentType = sales.PaymentType;
          
           
            var customer =await _context.Customers.FindAsync(sales.CustomerId);

            var transaction = new Transaction
            {
                OutletId = sales.OutletId,
                CashierId = sales.CashierId,
                CustomerId = sales.CustomerId,
                TransactionTime = DateTime.Now,
                Amount = sales.Amount,
                PaymentType = (PaymentTypes)sales.PaymentType,
            };

            _context.Transactions.Add(transaction);
            var saveTransaction = await _context.SaveChangesAsync();
            if (saveTransaction.Equals(0))
            {
                return BadRequest();
            }
            foreach (var s in sales.Menus)
            {
                var sale = new Sale()
                {
                    TransactionId = transaction.Id,
                    MenuId = s.Id,
                    Quantity = s.Quantity

                };
                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/SalesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
