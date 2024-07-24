using Microsoft.AspNetCore.Mvc;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.Abstraction.AbstractClass;
using Microsoft.EntityFrameworkCore;
using ERPFastTrack.DBGround.DBModels.Custom;

namespace ERPFastTrack.API.Internals.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private readonly ERPFastTrackUIContext _context;
		private readonly OrgRoleManagerAbstract _roleManager;

		public ProjectsController(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager)
		{
			_context = context;
			_roleManager = roleManager;
		}

		// GET: api/Projects
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
		{
			if (_context.Projects == null)
			{
				return NotFound();
			}
			return await _context.Projects.Where(x => x.OrgId == _roleManager.Role.OrgId).ToListAsync();
		}


		// GET: api/Projects
		[HttpGet("Jobs")]
		public async Task<ActionResult<IEnumerable<Project>>> GetJobs()
		{
			if (_context.Projects == null)
			{
				return NotFound();
			}
			return await _context.Projects.Where(x => x.OrgId == _roleManager.Role.OrgId).ToListAsync();
		}

		// GET: api/Projects/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Project>> GetProject(int id)
		{
			if (_context.Projects == null)
			{
				return NotFound();
			}
			var project = await _context.Projects.FindAsync(id);

			if (project == null)
			{
				return NotFound();
			}

			return project;
		}

		// PUT: api/Projects/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProject(int id, Project project)
		{
			if (id != project.Id)
			{
				return BadRequest();
			}

			_context.Entry(project).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProjectExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Projects
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Project>> PostProject(Project project)
		{
			if (_context.Projects == null)
			{
				return Problem("Entity set 'ERPFastTrackUIContext.Projects'  is null.");
			}
			_context.Projects.Add(project);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProject", new { id = project.Id }, project);
		}

		// DELETE: api/Projects/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProject(int id)
		{
			if (_context.Projects == null)
			{
				return NotFound();
			}
			var project = await _context.Projects.FindAsync(id);
			if (project == null)
			{
				return NotFound();
			}

			_context.Projects.Remove(project);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ProjectExists(int id)
		{
			return (_context.Projects?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
