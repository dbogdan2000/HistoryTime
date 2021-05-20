using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryTime.Data;
using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/roles")]
    public class RolesController : Controller
    {
        private readonly IRepository<Role> _rolesRepository;

        public RolesController(IRepository<Role> rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _rolesRepository.GetAll();
            return Ok(roles);
        }
    }
}