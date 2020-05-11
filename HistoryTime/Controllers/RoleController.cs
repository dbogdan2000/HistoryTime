using System;
using System.Collections.Generic;
using System.Linq;
using HistoryTime.Data;
using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/roles")]
    public class RoleController : Controller
    {
        private readonly IRolesRepository _rolesRepository;

        public RoleController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var roles = _rolesRepository.Get();
            return Ok(roles.ToList());
        }
    }
}