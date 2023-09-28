using lph_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace lph_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private static IEnumerable<Patient> Patients = new List<Patient>
    {
        new Patient(1, "Smithy", "Incorrect", "Smith@gmail.com", "+3610123456", "John", "Smith"),
        new Patient(2, "Fizzy", "Incorrect", "FizzBuzz@gmail.com", "+3620123456", "Fizz", "Buzz"),
        new Patient(3, "Dough", "Incorrect", "JohnDoe@gmail.com", "+3630123456", "John", "Doe"),
    };

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        System.Threading.Thread.Sleep(1000);
        return Ok(Patients);
    }

    [HttpDelete("Delete:{id}")]
    public IActionResult Delete(int id)
    {
        var patientsCopy = Patients.ToList();
        var patient = Patients.FirstOrDefault(patient => patient.Id == id);

        if (patient == null)
        {
            return NotFound("Id not found.");
        }
        
        patientsCopy.Remove(patient);
        Patients = patientsCopy;
        return Ok(id);
    }
}