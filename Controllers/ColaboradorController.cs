using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TccFrotaApp.Data;
using TccFrotaApp.Helpers;
using TccFrotaApp.Models;
using TccFrotaApp.ViewModels;

namespace TccFrotaApp.Controllers
{
    [Route("api/[controller]")]
    public class ColaboradorController : Controller
    {
        private static string EMPTY_PASSWORD = "PASSWORD";
        private readonly FrotaAppDbContext _appDbContext;
        private readonly UserManager<Login> _userManager;
        private readonly IMapper _mapper;

        public ColaboradorController(UserManager<Login> userManager, IMapper mapper, FrotaAppDbContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        // GET api/colaborador
        [HttpGet]
        public IEnumerable<ColaboradorViewModel> GetAll()
        {
            return _appDbContext.Colaboradores.Include(c => c.Login).Select(c => new ColaboradorViewModel()
            {
                Id = c.Id,
                Nome = c.Nome,
                Matricula = c.Matricula,
                Funcao = c.Funcao.ToString(),
                Email = c.Login != null ? c.Login.UserName : "",
                Senha = EMPTY_PASSWORD
            });
        }


        // POST api/colaborador
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ColaboradorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //verificamos se o colaborador já existe

            if (_appDbContext.Colaboradores.Any(c => c.Matricula == model.Matricula))
            {
                return BadRequest(Errors.AddErrorToModelState("colaborador_failure", "Colaborador com a matricula [" + model.Matricula + "] já está cadastrado", ModelState));

            }


            //Convertemos nosso viewmodel para a entidade do colaborador
            var colaborador = new Colaborador()
            {
                Nome = model.Nome,
                Matricula = model.Matricula,
                Funcao = (TIPO_COLABORADOR)Enum.Parse(typeof(TIPO_COLABORADOR), model.Funcao),
            };

            //se o colaborador não for um coletor ele precisa de acesso ao sistema logo criamos uma credencia com a utilização do asp net core identity
            if (colaborador.Funcao != TIPO_COLABORADOR.COLETOR)
            {
                var login = new Login() { UserName = model.Email };

                var result = await _userManager.CreateAsync(login, model.Senha);
                if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

                colaborador.LoginId = login.Id;
            }

            //adiciona o objeto do contexto do banco e faz o comite das modificações para o banco de dados
            _appDbContext.Colaboradores.Add(colaborador);
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Colaborador criado");
        }

        // PUT api/colaborador/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ColaboradorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //verificamos se o colaborador que vamos atualizar já está cadastrado
            var colaborador = await _appDbContext.Colaboradores.Include(a => a.Login).FirstOrDefaultAsync(c => c.Id == id);

            if (colaborador == null)
            {
                return NotFound("Colaborador não encontrado");
            }

            //se o colaborador não for um coletor ele precisa de acesso ao sistema logo criamos uma credencia com a utilização do asp net core identity
            //se a função foi alterada então precisamos verificar se precisa de uma credencial
            if (colaborador.Funcao != TIPO_COLABORADOR.COLETOR && colaborador.Login == null)
            {
                if (colaborador.Login == null)
                {
                    var login = new Login() { UserName = model.Email };
                    var result = await _userManager.CreateAsync(login, model.Senha);
                    if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
                    colaborador.LoginId = login.Id;
                }
                else
                {
                    //se o cabloco setou uma senha nova vamos remover a antiga e então adicionar a nova
                    if (model.Senha != EMPTY_PASSWORD)
                    {
                        await _userManager.RemovePasswordAsync(colaborador.Login);
                        await _userManager.AddPasswordAsync(colaborador.Login, model.Senha);
                    }
                    await _userManager.SetUserNameAsync(colaborador.Login, model.Email);
                }
            }
            else
            {
                //se o colaborador é um coletor forçamos o obj a não possuir um login
                colaborador.LoginId = null;
                colaborador.Login = null;
            }

//autualiza outros campos
            colaborador.Nome = model.Nome;
            colaborador.Funcao = Enum.Parse<TIPO_COLABORADOR>(model.Funcao);

            //atualiza o objeto do contexto do banco e faz o comite das modificações para o banco de dados
            _appDbContext.Colaboradores.Update(colaborador);
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Colaborador Atualizado");
        }

        // DELETE api/colaborador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //verificamos se o colaborador que vamos deletar está cadastrado
            var colaborador = await _appDbContext.Colaboradores.FirstOrDefaultAsync(c => c.Id == id);

            if (colaborador == null)
            {
                return NotFound("Colaborador não encontrado");
            }

            //remove o objeto do contexto do banco e faz o comite das modificações para o banco de dados
            _appDbContext.Colaboradores.Remove(colaborador);
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Colaborador Deletado");
        }


    }
}
