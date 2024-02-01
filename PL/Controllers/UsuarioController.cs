using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PL.Controllers;

public class UsuarioController : Controller
{
    public bool ValidateToken(string tokenString)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("InicioDeSesionEmail123456789123456789");

        try
        {
            tokenHandler.ValidateToken(tokenString, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        string token = HttpContext.Session.GetString("Token");
        if (!ValidateToken(token))
        {
            ViewBag.Message = "Debe Iniciar sesion";
            return PartialView("ModalLogin");
        }
        ML.Usuario usuario = new();
        ML.Result result = BL.Usuario.GetAll();
        if (result.Correct)
        {
            usuario.Usuarios = result.Objects;
            return View(usuario);
        }
        else
        {
            return View(usuario);
        }
    }

    [HttpGet]
    public IActionResult Form(int IdUsuario)
    {
        string token = HttpContext.Session.GetString("Token");
        if (!ValidateToken(token))
        {
            ViewBag.Message = "Debe Iniciar sesion";
            return PartialView("ModalLogin");
        }

        ML.Usuario usuario = new();

        if (IdUsuario == 0)
        {
            return View(usuario);
        }
        ML.Result result = BL.Usuario.GetById(IdUsuario);
        if (result.Correct)
        {
            usuario = (ML.Usuario)result.Object;
            return View(usuario);
        }
        else
        {
            ViewBag.Message = result.ErrorMessage;
            return PartialView("Modal");
        }
    }

    [HttpPost]
    public IActionResult Form(ML.Usuario usuario)
    {
        string token = HttpContext.Session.GetString("Token");
        if (!ValidateToken(token))
        {
            ViewBag.Message = "Debe Iniciar sesion";
            return PartialView("ModalLogin");
        }
        if (usuario.IdUsuario == 0)
        {
            ML.Result result = BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                ViewBag.Message = "Se inserto correctamente el registro";
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al insertar el registro";
            }
            return PartialView("Modal");
        }
        else
        {
            ML.Result result = BL.Usuario.Update(usuario);
            if (result.Correct)
            {
                ViewBag.Message = "Se actualizo correctamente el registro";
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al actualizar el registro";
            }
            return PartialView("Modal");
        }
    }

    [HttpGet]
    public IActionResult Delete(int IdUsuario)
    {
        string token = HttpContext.Session.GetString("Token");
        if (!ValidateToken(token))
        {
            ViewBag.Message = "Debe Iniciar sesion";
            return PartialView("ModalLogin");
        }
        ML.Result result = BL.Usuario.Delete(IdUsuario);
        if (result.Correct)
        {
            ViewBag.Message = "Se inactivo correctamente el registro";
        }
        else
        {
            ViewBag.Message = "Ocurrió un error al inactivar el registro";
        }
        return PartialView("Modal");
    }
}
