using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_commerceAPI.DTO;
using E_commerceAPI.Model;
using E_commerceAPI.Repository.CategoryRepository;
using System.Net;

namespace E_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryController(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        // GET: api/Category
        [HttpGet]
        [ProducesResponseType(typeof(CategoryDTO), (int)HttpStatusCode.OK)]
        public IActionResult GetCategoryDTO()
        {

            try
            {
                var categoryDTO = _CategoryRepository.GetAll();
                if (categoryDTO != null)
                {
                    return Ok(categoryDTO);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET: api/Category/5
        [HttpGet("{id:int}",Name = "GetCategoryDTO")]
        public IActionResult GetCategoryDTO(int id)
        {
            try
            {
                var category = _CategoryRepository.GetById(id);
                CategoryDTO categoryDTO=new CategoryDTO()
                {
                    Name = category.Name,
                    id = id,
                    products = category.products
                };
                if (categoryDTO == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id:int}")]
        public IActionResult PatchCategoryDTO(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.id)
            {
                return BadRequest();
            }
            try
            {
                Category category = new Category()
                {
                    products = categoryDTO.products,
                    Name = categoryDTO.Name,
                    id = id
                };
                if (_CategoryRepository.Update(id, category) > 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Data Saved");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostCategoryDTO(CategoryDTO categoryDTO)
        {
            try
            {
                Category category = new Category()
                {
                    Name = categoryDTO.Name
                };
                if(_CategoryRepository.Insert(category) > 0)
                {
                    string URL = Url.Link("GetCategoryDTO", new { id = category.id });
                    return Created(URL, category);
                }
                return BadRequest(categoryDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Category/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCategoryDTO(int id)
        {
            try
            {
                if(_CategoryRepository.Delete(id) > 0)
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
//try
//{

//}
//catch (Exception ex)
//{
//    return BadRequest(ex.Message);
//}