using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TccFrotaApp.Data;

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


        


    }
}
