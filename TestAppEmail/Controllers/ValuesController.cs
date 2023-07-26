using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppEmail.Data;
using TestAppEmail.Models;

namespace TestAppEmail.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly DateDbContext _context;

        public ValuesController(DateDbContext context)
        {
            _context = context;
        }

        // GET: api/DateModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DateModel>>> GetProducts()
        {
            //_context.ExecuteSSISPackage();
            return await _context.SystemParamaterTest.ToListAsync();
        }

        /* Update the System date in the SQL table */
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<DateModel>>> Put(int id, [FromBody] DateModel updatedModel)
        {
            var model = await _context.SystemParamaterTest.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            model.DateValue = updatedModel.DateValue;
            _context.SaveChanges();

            var updatedData = await _context.SystemParamaterTest.ToListAsync();
            return updatedData;
        }
    }
}
