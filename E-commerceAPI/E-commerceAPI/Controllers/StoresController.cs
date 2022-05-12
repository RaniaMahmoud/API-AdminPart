using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_commerceAPI.DTO;
using E_commerceAPI.Model;
using E_commerceAPI.Repository.StoreRepository;
using E_commerceAPI.Repository.BrancheRepository;

namespace E_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository StoreRepository;
        private IBrancheRepository brancheRepository;
        public StoresController(IStoreRepository _StoreRepository, IBrancheRepository _BrancheRepository)
        {
            StoreRepository = _StoreRepository;
            brancheRepository = _BrancheRepository;
        }

        // GET: api/Stores
        [HttpGet]
        public IActionResult GetStore()
        {
            try
            {
                List<StoreDTO> storeDTOs = new List<StoreDTO>();
                foreach (Store item in StoreRepository.GetAll())
                {
                    StoreDTO store = new StoreDTO()
                    {
                        Id = item.id,
                        Name = item.Name,
                        Logo = item.Logo,
                        Branches =item.Branches
                    };
                    storeDTOs.Add(store);
                }
                if (storeDTOs != null)
                {
                    return Ok(storeDTOs);
                }
                else
                {
                    return BadRequest("Empety");
                }
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Stores/5
        [HttpGet("{id:int}",Name = "GetStoreById")]
        public IActionResult GetStoreById(int id)
        {
            try
            {
                Store store=new Store();
                
                store = StoreRepository.GetById(id);
                StoreDTO storeDTO = new StoreDTO()
                {
                    Name = store.Name,
                    Id = store.id,
                    Logo = store.Logo,
                    Branches = store.Branches
                };
                if(storeDTO != null)
                {
                    return Ok(storeDTO);
                }
                else
                {
                    return BadRequest("Empty");
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostStore(StoreDTO storeDTO)
        {
            if (storeDTO.Name != null && storeDTO.Logo != null && storeDTO.Branches != null)
            {
                try
                {                    
                    Store store = new Store()
                    {
                        Name = storeDTO.Name,
                        Logo = storeDTO.Logo
                    };
                    if (StoreRepository.Insert(store) > 0)
                    {
                        foreach (var item in storeDTO.Branches)
                        {
                            Branche branche = new Branche()
                            {
                                Name = item.Name,
                                StoreId = store.id
                            };
                            brancheRepository.Insert(branche);
                        }
                        string URL = Url.Link("GetStoreById", new { id = store.id });
                        return Created(URL, store);
                    }
                    else
                    {
                        return BadRequest("Not Saved");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
