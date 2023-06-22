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
    [Route("api/compras")]
    public class ComprasController : ControllerBase
    {
        private readonly ComprasRepository _repository;

        public ComprasController(ComprasRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var compras = await _repository.GetAllAsync();
                return Ok(compras);
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
                var compra = await _repository.GetByIdAsync(id);
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComprasRequest compra)
        {
            try
            {
                await _repository.CreateAsync(compra);
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
        [HttpPost("liberar/{cartaoId}/{valor}")]
        public async Task<ActionResult> LiberarLimiteCartao(int cartaoId, double valor)
        {
            try
            {
                await _repository.LiberarLimiteCartaoAsync(cartaoId, valor);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}