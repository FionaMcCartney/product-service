using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProductService.Models;

namespace ProductService.Controllers
{
    public class ProductsController : ApiController
    {
        private ProductServiceContext db = new ProductServiceContext();

        // GET: api/Products
        public IQueryable<ProductDTO> GetProducts()
        {
            var products = from b in db.Products
                           select new ProductDTO()
                           {
                               Id = b.Id,
                               Name = b.Name,
                               CategoryName = b.Category.Name
                           };

            return products;

        }

        // GET: api/Products/5
        [ResponseType(typeof(ProductDetailDTO))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            var product = await db.Products.Include(b => b.Category).Select(b =>
              new ProductDetailDTO()
              {
                  Id = b.Id,
                  Name = b.Name,
                  CategoryName = b.Category.Name,
                  Description = b.Description
              }).SingleOrDefaultAsync(b => b.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(ProductDTO))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();

            // Load Category name
            db.Entry(product).Reference(x => x.Category).Load();

            var dto = new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryName = product.Category.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, dto);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}