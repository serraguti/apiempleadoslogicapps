using ApiEmpleadosOAuth.Helpers;
using ApiEmpleadosOAuth.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEmpleadosOAuth.Controllers
{
    //http://www.apiempleados/auth
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        RepositoryEmpleados repo;
        HelperToken helpertoken;

        public AuthController(RepositoryEmpleados repo
            , HelperToken helpertoken)
        {
            this.repo = repo;
            this.helpertoken = helpertoken;
        }
    }
}
