using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace APIEstudiantes.Controllers
{
    [Route("api/estudiantes")]
    [ApiController]
    [Authorize]
    public class EstudianteController : Controller
    {
        
        private readonly ILogger<EstudianteController> _logger;        

        public EstudianteController(ILogger<EstudianteController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
   

     private static  List<Estudiante> estudiantes = new List<Estudiante>
    {
        new Estudiante { Id = 1, Nombre = "Juan", Apellido = "Perez", FechaNacimiento = new DateTime(1990, 5, 15), Mail = "juan@ejemplo.com" },
        new Estudiante { Id = 2, Nombre = "María", Apellido = "Gómez", FechaNacimiento = new DateTime(1995, 8, 20), Mail = "maria@ejemplo.com" },
        new Estudiante { Id = 3, Nombre = "Pedro", Apellido = "López", FechaNacimiento = new DateTime(1985, 3, 10), Mail = "pedro@ejemplo.com" },
       
    };

  
     
    [HttpGet]
    [Authorize("read:estudiantes")]
    public IActionResult GetAllStudents()
    {       
        return Ok(estudiantes);
    }

    [HttpGet("{id}")]
    [Authorize("read:estudiantes")]
    public IActionResult GetStudentById(int id)
    {
        var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);
        if (estudiante == null)
        {
            return NotFound();
        }
        return Ok(estudiante);
    }

    [HttpPost]
    [Authorize("write:estudiantes")]
    public IActionResult CreateStudent(Estudiante estudiante)
    {
        estudiante.Id = estudiantes.Max(s => s.Id + 1 );
        estudiantes.Add(estudiante);
        return CreatedAtAction(nameof(GetStudentById), new { id = estudiante.Id }, estudiantes);     
     
    }

    [HttpPut("{id}")]
    [Authorize("write:estudiantes")]
    public IActionResult UpdateStudent(int id, Estudiante estudiante)
    {
        var existeEstudiante = estudiantes.FirstOrDefault(e => e.Id == id);
        if (existeEstudiante == null)
        {
            return NotFound();
        }
        existeEstudiante.Nombre = estudiante.Nombre;
        existeEstudiante.Apellido = estudiante.Apellido;
        existeEstudiante.FechaNacimiento = estudiante.FechaNacimiento;
        existeEstudiante.Mail = estudiante.Mail;
        return Ok(estudiantes);
    }

    [HttpDelete("{id:int}")]
    [Authorize("write:estudiantes")]
    public IActionResult DeleteStudent(int id)
    {
        var existeEstudiante = estudiantes.FirstOrDefault(e => e.Id == id);
        if (existeEstudiante == null)
        {
            return NotFound();
        }
        estudiantes.Remove(existeEstudiante);
        return Ok(estudiantes);
    }
}

}
