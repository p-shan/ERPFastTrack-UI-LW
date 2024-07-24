using ERPFastTrack.Abstraction.AbstractClass;
using ERPFastTrack.DBGround.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPFastTrack.API.Internals.Controllers.Exposed
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ERPFastTrackUIContext _context;
		private readonly OrgRoleManagerAbstract _roleManager;

		public AuthController(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager)
		{
			_context = context;
			_roleManager = roleManager;
		}

	}
}
