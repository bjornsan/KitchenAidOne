using KitchenAid.DataAccess;
using KitchenAid.Model.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenAid.InventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageProductsController : ControllerBase
    {
        private readonly InventoryContext _context;

        public StorageProductsController(InventoryContext context)
        {
            _context = context;
        }

        // GET: api/StorageProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StorageProduct>>> GetStorageProducts()
        {
            return await _context.StorageProducts.ToListAsync();
        }

        // GET: api/StorageProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageProduct>> GetStorageProduct(int id)
        {
            var storageProduct = await _context.StorageProducts.FindAsync(id);

            if (storageProduct == null)
            {
                return NotFound();
            }

            return storageProduct;
        }

        // PUT: api/StorageProducts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStorageProduct(int id, StorageProduct storageProduct)
        {
            if (id != storageProduct.StorageId)
            {
                return BadRequest();
            }

            _context.Entry(storageProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageProductExists(id))
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

        // POST: api/StorageProducts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StorageProduct>> PostStorageProduct(StorageProduct storageProduct)
        {
            _context.StorageProducts.Add(storageProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StorageProductExists(storageProduct.StorageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStorageProduct", new { id = storageProduct.StorageId }, storageProduct);
        }

        // DELETE: api/StorageProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StorageProduct>> DeleteStorageProduct(int id)
        {
            var storageProduct = await _context.StorageProducts.FindAsync(id);
            if (storageProduct == null)
            {
                return NotFound();
            }

            _context.StorageProducts.Remove(storageProduct);
            await _context.SaveChangesAsync();

            return storageProduct;
        }

        private bool StorageProductExists(int id)
        {
            return _context.StorageProducts.Any(e => e.StorageId == id);
        }
    }
}
