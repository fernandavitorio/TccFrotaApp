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
    public class ApontamentosController : Controller
    {

        private readonly FrotaAppDbContext _dbContext;
        public ApontamentosController(FrotaAppDbContext dbContext)
        {

            _dbContext = dbContext;

        }

        // GET api/apontamento/GetAll
        [HttpGet("[action]")]
        public IEnumerable<ApontamentoViewModel> GetAll()
        {
            return _dbContext.Apontamentos
            .Include(a => a.Veiculo)
            .Include(a => a.Motorista)
            .Include(a => a.Coletor1)
            .Include(a => a.Coletor2)
            .Include(a => a.Coletor3)
            .Select(a => new ApontamentoViewModel()
            {
                Id = a.Id,
                DtAtualizacao = a.DtAtualizacao,
                AditionalInformation = a.AditionalInformation,
                Setor = a.Setor.ToString(),
                Tipo = a.Tipo.ToString(),
                VeiculoId = a.VeiculoId,
                VeiculoIdentificador = a.Veiculo.Identificador,
                VeiculoPlaca = a.Veiculo.Placa,
                MotoristaId = a.MotoristaId,
                MotoristaNome = a.Motorista.Nome,
                Coletor1Id = a.Coletor1Id,
                Coletor1Nome = a.Coletor1.Nome,
                Coletor2Id = a.Coletor2Id,
                Coletor2Nome = a.Coletor2.Nome,
                Coletor3Id = a.Coletor3Id,
                Coletor3Nome = a.Coletor3.Nome,
            });
        }

        // GET api/apontamento/GetAllParrents
        [HttpGet("[action]")]
        public IEnumerable<ApontamentoViewModel> GetAllParrents()
        {
            return _dbContext.Apontamentos
            .Where(a => a.ApontamentoInicialId == null)
            .Include(a => a.Veiculo)
            .Include(a => a.Motorista)
            .Include(a => a.Coletor1)
            .Include(a => a.Coletor2)
            .Include(a => a.Coletor3)
            .Select(a => new ApontamentoViewModel()
            {
                Id = a.Id,
                DtAtualizacao = a.DtAtualizacao,
                AditionalInformation = a.AditionalInformation,
                Setor = a.Setor.ToString(),
                Tipo = a.Tipo.ToString(),
                VeiculoId = a.VeiculoId,
                VeiculoIdentificador = a.Veiculo.Identificador,
                VeiculoPlaca = a.Veiculo.Placa,
                MotoristaId = a.MotoristaId,
                MotoristaNome = a.Motorista.Nome,
                Coletor1Id = a.Coletor1Id,
                Coletor1Nome = a.Coletor1.Nome,
                Coletor2Id = a.Coletor2Id,
                Coletor2Nome = a.Coletor2.Nome,
                Coletor3Id = a.Coletor3Id,
                Coletor3Nome = a.Coletor3.Nome,
            });
        }

        // GET api/apontamento/GetAllChilds
        [HttpGet("[action]")]
        public IEnumerable<ApontamentoViewModel> GetAllChilds(int id)
        {
            return _dbContext.Apontamentos
            .Where(a => a.ApontamentoInicialId == id)
            .Include(a => a.Veiculo)
            .Include(a => a.Motorista)
            .Include(a => a.Coletor1)
            .Include(a => a.Coletor2)
            .Include(a => a.Coletor3)
            .Select(a => new ApontamentoViewModel()
            {
                Id = a.Id,
                DtAtualizacao = a.DtAtualizacao,
                AditionalInformation = a.AditionalInformation,
                Setor = a.Setor.ToString(),
                Tipo = a.Tipo.ToString(),
                VeiculoId = a.VeiculoId,
                VeiculoIdentificador = a.Veiculo.Identificador,
                VeiculoPlaca = a.Veiculo.Placa,
                MotoristaId = a.MotoristaId,
                MotoristaNome = a.Motorista.Nome,
                Coletor1Id = a.Coletor1Id,
                Coletor1Nome = a.Coletor1.Nome,
                Coletor2Id = a.Coletor2Id,
                Coletor2Nome = a.Coletor2.Nome,
                Coletor3Id = a.Coletor3Id,
                Coletor3Nome = a.Coletor3.Nome,
            });
        }

        // POST api/apontamento
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ApontamentoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //primeiro buscamos pelo apontamento inicial
            Apontamento apontamentoInicial = _dbContext.Apontamentos.OrderBy(a => a.DtAtualizacao).Include(a => a.Veiculo).LastOrDefault(a => a.VeiculoId == model.VeiculoId);

            //de acordo com o tipo de atividade precisamos fazer uma validação especifica sobre o objeto a ser gravado no banco
            var tipoApontamento = Enum.Parse<TIPO_APONTAMENTO>(model.Tipo);

            //somente apontamentos iniciais podem ter referencia nula para apontamento pai, 
            if (apontamentoInicial == null && tipoApontamento != TIPO_APONTAMENTO.INICIAL)
            {
                return BadRequest(Errors.AddErrorToModelState("apontamento_failure", "Não existe uma atividade inicial em aberta para o veículo " + apontamentoInicial.Veiculo.Identificador, ModelState));
            }

            //verifica se já existe um filho do apontamento inicial com o mesmo tipo do apontamento a ser criado.
            if (apontamentoInicial != null && apontamentoInicial.Apontamentos.Any(a => a.Tipo == tipoApontamento))
            {
                return BadRequest(Errors.AddErrorToModelState("apontamento_failure", "Já existe uma atividade [" + tipoApontamento.ToString() + "] em aberta para o veículo " + apontamentoInicial.Veiculo.Identificador, ModelState));
            }

            //Convertemos nosso viewmodel para a entidade do colaborador
            var apontamento = new Apontamento()
            {
                DtAtualizacao = model.DtAtualizacao,
                AditionalInformation = model.AditionalInformation,
                Setor = Enum.Parse<SETOR>(model.Setor),
                Tipo = tipoApontamento,
                VeiculoId = model.VeiculoId,
                MotoristaId = model.MotoristaId,
                Coletor1Id = model.Coletor1Id,
                Coletor2Id = model.Coletor2Id,
                Coletor3Id = model.Coletor3Id,
                ApontamentoInicialId = tipoApontamento == TIPO_APONTAMENTO.INICIAL ? null : (int?)apontamentoInicial.Id
            };

            //adiciona o objeto do contexto do banco e faz o comite das modificações para o banco de dados
            _dbContext.Apontamentos.Add(apontamento);
            await _dbContext.SaveChangesAsync();

            return new OkObjectResult("Apontamento criado");
        }





    }
}
