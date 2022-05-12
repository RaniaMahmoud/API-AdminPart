using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_commerceAPI.Model;
using E_commerceAPI.Repository.AddressRepository;

namespace E_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository _AddressRepository;

        public AddressesController(IAddressRepository AddressRepository)
        {
            _AddressRepository = AddressRepository;
        }

        // GET: api/Addresses/5
        [HttpGet("{id}",Name = "GetAddress")]
        public IActionResult GetAddress(int id)
        {
            try
            {
                var address = _AddressRepository.GetById(id);

                if (address == null)
                {
                    return NotFound();
                }

                return Ok(address);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostAddress(Address address)
        {
            try
            {
                _AddressRepository.Insert(address);
                string URL = Url.Link("GetAddress", new { id = address.id });
                return Created(URL, address);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
