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
    public class ApontamentoController : Controller
    {

        private readonly FrotaAppDbContext _dbContext;
        public ApontamentoController(FrotaAppDbContext dbContext)
        {

            _dbContext = dbContext;

        }

        // GET api/apontamento/GetAll
        [HttpGet]
        public IEnumerable<ApontamentoViewModel> GetAll()
        {
            return _dbContext.Apontamentos
            .Include(a => a.Veiculo)
            .Include(a => a.Motorista)
            .Include(a => a.Coletor1)
            .Include(a => a.Coletor2)
            .Include(a => a.Coletor3)
            .OrderBy(a => a.DtAtualizacao)
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

        [HttpGet("[action]")]
        [Route("apontamentos.csv")]
        [Produces("text/csv")]
        public IActionResult GetAllAsCsv()
        {
            var apontamentosToExport = _dbContext.Apontamentos
          .Include(a => a.Veiculo)
          .Include(a => a.Motorista)
          .Include(a => a.Coletor1)
          .Include(a => a.Coletor2)
          .Include(a => a.Coletor3)
          .OrderBy(a => a.DtAtualizacao)
          .Select(a => new
          {
              Id = a.Id,
              DtAtualizacao = a.DtAtualizacao,
              Setor = a.Setor.ToString(),
              Tipo = a.Tipo.ToString(),
              AditionalInformation = a.AditionalInformation,
              VeiculoId = a.VeiculoId,
              VeiculoIdentificador = a.Veiculo != null ? a.Veiculo.Identificador : "",
              VeiculoPlaca = a.Veiculo != null ? a.Veiculo.Placa : "",
              MotoristaId = a.MotoristaId,
              MotoristaNome = a.Motorista != null ? a.Motorista.Nome : "",
              MotoristaMatricula = a.Motorista != null ? a.Motorista.Matricula : 0,
              Coletor1Id = a.Coletor1Id,
              Coletor1Nome = a.Coletor1 != null ? a.Coletor1.Nome : "",
              Coletor1Matricula = a.Coletor1 != null ? a.Coletor1.Matricula : 0,
              Coletor2Id = a.Coletor2Id,
              Coletor2Nome = a.Coletor2 != null ? a.Coletor2.Nome : "",
              Coletor2Matricula = a.Coletor2 != null ? a.Coletor2.Matricula : 0,
              Coletor3Id = a.Coletor3Id,
              Coletor3Nome = a.Coletor3 != null ? a.Coletor3.Nome : "",
              Coletor3Matricula = a.Coletor3 != null ? a.Coletor3.Matricula : 0,
          }).ToList();

            return Ok(apontamentosToExport);
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
            .Include(a => a.Apontamentos)
            .OrderByDescending(a => a.DtAtualizacao)
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
                EmAberto = !a.Apontamentos.Any(b => b.Tipo == TIPO_APONTAMENTO.KM_FINAL)
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
            .OrderBy(a => a.DtAtualizacao)
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
            Apontamento apontamentoInicial = _dbContext.Apontamentos
            .OrderBy(a => a.DtAtualizacao)
            .Include(a => a.Veiculo)
            .Include(a => a.Apontamentos)
            .LastOrDefault(a => a.Tipo == TIPO_APONTAMENTO.INICIAL && a.VeiculoId == model.VeiculoId);

            //de acordo com o tipo de atividade precisamos fazer uma validação especifica sobre o objeto a ser gravado no banco
            var tipoApontamento = Enum.Parse<TIPO_APONTAMENTO>(model.Tipo);

            //somente apontamentos iniciais podem ter referencia nula para apontamento pai, 
            if ((apontamentoInicial == null || apontamentoInicial.Apontamentos.Any(a => a.Tipo == TIPO_APONTAMENTO.KM_FINAL)) && tipoApontamento != TIPO_APONTAMENTO.INICIAL)
            {
                return BadRequest(Errors.AddErrorToModelState("apontamento_failure", "Não existe uma atividade inicial em aberta para o veículo ", ModelState));
            }

            //verifica se já existe um apontamento inicial em aberto, no caso sem o apontamento de km final 
            //ou então um filho do apontamento inicial com o mesmo tipo do apontamento a ser criado.
            if (apontamentoInicial != null &&
                ((tipoApontamento == TIPO_APONTAMENTO.INICIAL && !apontamentoInicial.Apontamentos.Any(a => a.Tipo == TIPO_APONTAMENTO.KM_FINAL))
                 ||
                  apontamentoInicial.Apontamentos.Any(a => a.Tipo == tipoApontamento)))
            {
                return BadRequest(Errors.AddErrorToModelState("apontamento_failure", "Já existe uma atividade [" + tipoApontamento.ToString() + "] em aberta para o veículo " + apontamentoInicial.Veiculo.Identificador, ModelState));
            }

            //se não for um apontamento inicial devemos copiar os dados do pai para o filho
            if (apontamentoInicial != null && tipoApontamento != TIPO_APONTAMENTO.INICIAL)
            {
                model.Setor = apontamentoInicial.Setor.ToString();
                model.VeiculoId = apontamentoInicial.VeiculoId;
                model.MotoristaId = apontamentoInicial.MotoristaId;
                model.Coletor1Id = apontamentoInicial.Coletor1Id;
                model.Coletor2Id = apontamentoInicial.Coletor2Id;
                model.Coletor3Id = apontamentoInicial.Coletor3Id;
            }

            //Convertemos nosso viewmodel para a entidade do colaborador
            var apontamento = new Apontamento()
            {
                DtAtualizacao = DateTime.Now,
                AditionalInformation = model.AditionalInformation,
                Setor = Enum.Parse<SETOR>(model.Setor),
                Tipo = tipoApontamento,
                VeiculoId = model.VeiculoId,
                MotoristaId = model.MotoristaId,
                Coletor1Id = model.Coletor1Id,
                Coletor2Id = model.Coletor2Id <= 0 ? null : (int?)model.Coletor2Id,
                Coletor3Id = model.Coletor3Id <= 0 ? null : (int?)model.Coletor3Id,
                ApontamentoInicialId = tipoApontamento == TIPO_APONTAMENTO.INICIAL ? null : (int?)apontamentoInicial.Id
            };

            //adiciona o objeto do contexto do banco e faz o comite das modificações para o banco de dados
            _dbContext.Apontamentos.Add(apontamento);
            await _dbContext.SaveChangesAsync();

            return new OkObjectResult("Apontamento criado");
        }





    }
}
