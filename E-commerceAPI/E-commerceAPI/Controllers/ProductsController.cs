using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_commerceAPI.Model;
using E_commerceAPI.Repository.ProductRepository;
using E_commerceAPI.DTO;
using E_commerceAPI.Repository.CategoryRepository;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using E_commerceAPI.Repository.OrderRepository;

namespace E_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository ProductRepository;
        ICategoryRepository CategoryRepository;
        IOrderRepository orderRepository;
        public ProductsController(IProductRepository _ProductRepository, 
            ICategoryRepository _CategoryRepository,IOrderRepository _orderRepository)
        {
            ProductRepository = _ProductRepository;
            CategoryRepository = _CategoryRepository;
            orderRepository = _orderRepository;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                List<ProductDTO> productDTOs = new List<ProductDTO>();
                List<Product> products = new List<Product>();
                products = ProductRepository.GetAll();
                if(products != null)
                {
                    //foreach (Product product in products)
                    //{
                    //    productDTOs.Add(new ProductDTO()
                    //    {
                    //        CategoryID = product.CategoryID,
                    //        CreationDate = product.CreationDate,
                    //        Discription = product.Discription,
                    //        Image = product.Image,
                    //        Name = product.Name,
                    //        Id = product.Id,
                    //        Price = product.Price,
                    //        Quantity = product.Quantity,
                    //        category = product.category,
                    //    });
                    //}
                    return Ok(products);
                }
                return BadRequest("No Product Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET: api/Products/5
        [HttpGet("{id}",Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = ProductRepository.GetById(id);

                if (product == null)
                {
                    return NotFound();
                }
                //ProductDTO productDTO = new ProductDTO()
                //{
                //    category = product.category,
                //    CategoryID = product.CategoryID,
                //    CreationDate = product.CreationDate,
                //    Discription = product.Discription,
                //    Id = product.Id,
                //    Image = product.Image,
                //    Name = product.Name,
                //    Price = product.Price,
                //    Quantity = product.Quantity

                //};
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET: api/Products/Category?CateogryID=5
        [HttpGet]
        [Route("Category")]
        public IActionResult GetProductCategory(int CateogryID)
        {
            try
            {

                var category = CategoryRepository.GetById(CateogryID);
                var products = category.products;

                if (products == null)
                {
                    return NotFound();
                }
                //ProductDTO productDTO = new ProductDTO()
                //{
                //    category = product.category,
                //    CategoryID = product.CategoryID,
                //    CreationDate = product.CreationDate,
                //    Discription = product.Discription,
                //    Id = product.Id,
                //    Image = product.Image,
                //    Name = product.Name,
                //    Price = product.Price,
                //    Quantity = product.Quantity

                //};
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public IActionResult PatchProduct(int id)//, Product product)
        {
            //try
            //{
            //    if (id != product.id)
            //    {
            //        return BadRequest();
            //    }

            //    if(ProductRepository.Update(id, product) > 0)
            //    {
            //        string URL = Url.Link("GetProduct", new { id = product.id });
            //        return Ok(product);
            //    }
            //    return BadRequest(product);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            try
            {
                var product = JsonConvert.DeserializeObject<Product>(HttpContext.Request.Form["product"]);

                if (id != product.id)
                {
                    return BadRequest();
                }
                var formCollection = Request.Form;

                var file = formCollection.Files.FirstOrDefault();
                var folderName = Path.Combine("assets", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file != null)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    product.Image = dbPath;
                    if (ProductRepository.Update(id, product) > 0)
                    {
                        return Ok(product);
                    }
                    return BadRequest(product);
                }
                else
                {
                    if (ProductRepository.Update(id, product) > 0)
                    {
                        return Ok(product);
                    }
                    return StatusCode(200);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //[Route("Upload")]
        //public async Task<IActionResult> Upload(ProductDTO productDTO)
        //{
        //    try
        //    {
        //        //var formCollection = await Request.ReadFormAsync();
        //        //foreach (var key in formCollection.Keys)
        //        //{
        //        //    //var val = HttpContext.Request.Form[key];
        //        //}
        //        //var val = HttpContext.Request.Form["file"];
        //        //var yourObject = JsonConvert.DeserializeObject<ProductDTO>(HttpContext.Request.Form["product"]);
        //        //ProductDTO productDTO=new ProductDTO();
        //        Product product = new Product()
        //        {
        //            CategoryID = productDTO.CategoryID,
        //            CreationDate = productDTO.CreationDate,
        //            Discription = productDTO.Discription,
        //            Image = productDTO.Image,
        //            Quantity = productDTO.Quantity,
        //            Price = productDTO.Price,
        //            Name = productDTO.Name
        //        };
        //        //var file = productDTO.file;
        //        //var folderName = Path.Combine("assets", "Images");
        //        //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //        //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //        //var fullPath = Path.Combine(pathToSave, fileName);
        //        //var dbPath = Path.Combine(folderName, fileName);
        //        //using (var stream = new FileStream(fullPath, FileMode.Create))
        //        //{
        //        //    file.CopyTo(stream);
        //        //}

        //        //UploadImage(productDTO.file);
        //        if (ProductRepository.Insert(product) > 0)
        //        {
        //            //string URL = Url.Link("GetProduct", new { id = product.id });
        //            return Ok(product);
        //        }
        //        return BadRequest(product);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult PostProduct()
        {
            try
            {
                var formCollection = Request.Form;
                var productDTO = JsonConvert.DeserializeObject<ProductDTO>(HttpContext.Request.Form["product"]);
                Product product = new Product()
                {
                    CategoryID = productDTO.CategoryID,
                    CreationDate = productDTO.CreationDate,
                    Discription = productDTO.Discription,
                    Quantity = productDTO.Quantity,
                    Price = productDTO.Price,
                    Name = productDTO.Name
                };
                var file = formCollection.Files.First();
                var folderName = Path.Combine("assets", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    product.Image = dbPath;
                    if (ProductRepository.Insert(product) > 0)
                    {
                        return Ok(product);
                    }
                    return BadRequest(product);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        [Route("Order")]
        public IActionResult PostOrder(Order order)
        {
            try
            {
                if(order != null)
                {
                    if (orderRepository.Insert(order) > 0)
                    {
                        foreach (var item in order.Products)
                        {
                            Product product = ProductRepository.GetById(item.ProductID);
                            product.Quantity = product.Quantity - item.Quantity;
                            ProductRepository.Update(item.ProductID, product);
                        }
                        return Ok(order);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                if(ProductRepository.Delete(id) > 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Data Deleted");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
