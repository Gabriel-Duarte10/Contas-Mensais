using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contas_Mensais.Data.Models;
using Contas_Mensais.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Contas_Mensais.Controllers
{
    [ApiController]
    [Route("api/cartoes")]
    public class CartoesController : ControllerBase
    {
        private readonly CartoesRepository _repository;

        public CartoesController(CartoesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cartoes = await _repository.GetAllAsync();
                return Ok(cartoes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cartao = await _repository.GetByIdAsync(id);
                return Ok(cartao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cartoes cartao)
        {
            try
            {
                await _repository.CreateAsync(cartao);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Cartoes cartao)
        {
            try
            {
                await _repository.UpdateAsync(cartao);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}