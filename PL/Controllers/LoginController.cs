using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PL.Controllers;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Form()
    {
        return View("Form");
    }

    [HttpPost]
    public IActionResult Form(ML.Usuario usuario)
    {
        ML.Result result = new();
        try
        {
            result = BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                ViewBag.Message = "Se registro correctamente";
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al registrar el usuario";
            }
            return PartialView("Modal");

        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.Ex = ex;
            result.ErrorMessage = ex.Message;
            ViewBag.Message = "Ocurrió un error al registrar el usuario";
            return PartialView("Modal");
        }
    }

    [HttpPost]
    public IActionResult Login(string Email, string Contraseña)
    {
        ML.Result result = new();
        try
        {
            result = BL.Usuario.GetByEmail(Email, Contraseña);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;

                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email),
                };

                // Clave secreta para firmar el token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("InicioDeSesionEmail123456789123456789"));

                // Crear credenciales de firma usando la clave secreta
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Crear el token JWT
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: creds
                );

                // Escribir el token como una cadena
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                HttpContext.Session.SetString("Token",tokenString);

                return RedirectToAction("GetAll", "Usuario");
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return PartialView("Modal");
            }
        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.Ex = ex;
            result.ErrorMessage = ex.Message;
            ViewBag.Message = "Ocurrio un error al iniciar session";
            return PartialView("Modal");
        }
    }
}
