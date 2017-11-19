using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly FrotaAppDbContext _appDbContext;
        private readonly UserManager<Login> _userManager;
        private readonly IMapper _mapper;

        public ColaboradorController(UserManager<Login> userManager, IMapper mapper, FrotaAppDbContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<Login>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _appDbContext.Colaboradores.AddAsync(new Colaborador { LoginId  = userIdentity.Id});
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Employee created");
        }

    }
}
