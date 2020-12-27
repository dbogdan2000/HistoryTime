using System;
using System.Collections.Generic;
using System.Linq;
using HistoryTime.Data;
using HistoryTime.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HistoryTime.Controllers
{
    [Route("api/roles")]
    public class RolesController : Controller
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _rolesRepository.Get();
            return Ok(roles.ToList());
        }
    }
}