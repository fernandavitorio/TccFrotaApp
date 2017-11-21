using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TccFrotaApp.Data;
using TccFrotaApp.Helpers;
using TccFrotaApp.Models;
using TccFrotaApp.ViewModels;

namespace TccFrotaApp.Controllers
{
    [Route("api/[controller]")]
    public class VeiculoController : Controller
    {

        private readonly FrotaAppDbContext _dbContext;
        public VeiculoController(FrotaAppDbContext dbContext)
        {

            _dbContext = dbContext;

        }

        // GET api/veiculo
        [HttpGet]
        public IEnumerable<VeiculoViewModel> GetAll()
        {
            return _dbContext.Veiculos
            .Select(a => new VeiculoViewModel()
            {
                Id = a.Id,
                Marca = a.Marca,
                Modelo = a.Modelo,
                Identificador = a.Identificador,
                Placa = a.Placa
            });
        }

        // POST api/veiculo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]VeiculoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Convertemos nosso viewmodel para a entidade do colaborador
            var veiculo = new Veiculo()
            {
                Id = model.Id,
                Marca = model.Marca,
                Modelo = model.Modelo,
                Identificador = model.Identificador,
                Placa = model.Placa
            };


            //adiciona o objeto do contexto do banco e faz o comite das modificações para o banco de dados
            _dbContext.Veiculos.Add(veiculo);
            await _dbContext.SaveChangesAsync();

            return new OkObjectResult("Veiculo criado");
        }

        /// PUT api/veiculo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]VeiculoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //verificamos se o veiculo que vamos atualizar já está cadastrado
            var veiculo = await _dbContext.Veiculos.FirstOrDefaultAsync(c => c.Id == id);

            if (veiculo == null)
            {
                return NotFound("Veiculo não encontrado");
            }

            veiculo.Marca = model.Marca;
            veiculo.Modelo = model.Modelo;
            veiculo.Placa = model.Placa;

            //atualiza o objeto do contexto do banco e faz o comite das modificações para o banco de dados
            _dbContext.Veiculos.Update(veiculo);
            await _dbContext.SaveChangesAsync();

            return new OkObjectResult("Veiculo Atualizado");
        }

        // DELETE api/veiculo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //verificamos se o colaborador que vamos deletar está cadastrado
            var veiculo = await _dbContext.Veiculos.FirstOrDefaultAsync(c => c.Id == id);

            if (veiculo == null)
            {
                return NotFound("Veiculo não encontrado");
            }

            //remove o objeto do contexto do banco e faz o comite das modificações para o banco de dados
            _dbContext.Veiculos.Remove(veiculo);
            await _dbContext.SaveChangesAsync();

            return new OkObjectResult("Veiculo Deletado");
        }



    }
}
