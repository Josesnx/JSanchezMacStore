using DL;
using Microsoft.EntityFrameworkCore;
using ML;

namespace BL;

public class Usuario
{
    public static Result Add(ML.Usuario usuario)
    {
        Result result = new();
        try
        {
            using JsanchezMacStoreContext context = new();
            var query = context.Database.ExecuteSql($"UsuarioAdd {usuario.Nombre},{usuario.ApellidoPaterno},{usuario.ApellidoMaterno},{usuario.Edad},{usuario.Email},{usuario.Contraseña}");
            //var query1 = context.Usuarios.Add(Map(usuario));
            if (query >= 1)
            {
                result.Correct = true;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un error al insertar el registro";
            }
        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.ErrorMessage = ex.Message;
            result.Ex = ex;
        }
        return result;
    }

    public static Result Update(ML.Usuario usuario)
    {
        Result result = new();
        try
        {
            using JsanchezMacStoreContext context = new();
            var query = context.Database.ExecuteSql($"UsuarioUpdate {usuario.IdUsuario},{usuario.Nombre},{usuario.ApellidoPaterno},{usuario.ApellidoMaterno},{usuario.Edad},{usuario.Email},{usuario.Contraseña}");
            //var query1 = context.Usuarios.Add(Map(usuario));
            if (query >= 1)
            {
                result.Correct = true;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un error al insertar el registro";
            }
        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.ErrorMessage = ex.Message;
            result.Ex = ex;
        }
        return result;
    }

    public static Result Delete(int IdUsuario)
    {
        Result result = new();
        try
        {
            using JsanchezMacStoreContext context = new();
            var query = context.Database.ExecuteSql($"UsuarioDelete {IdUsuario}");
            //var query1 = context.Usuarios.Add(Map(usuario));
            if (query >= 1)
            {
                result.Correct = true;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un error al insertar el registro";
            }
        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.ErrorMessage = ex.Message;
            result.Ex = ex;
        }
        return result;
    }

    public static Result GetAll()
    {
        Result result = new();
        try
        {
            using JsanchezMacStoreContext context = new();
            var query = context.Usuarios.FromSqlRaw("UsuarioGetAll").ToList();
            //var query1 = context.Usuarios.Add(Map(usuario));
            if (query != null)
            {
                result.Objects = [];
                foreach (var item in query)
                {
                    ML.Usuario usuario = new()
                    {
                        IdUsuario = item.Idusuario,
                        Nombre = item.Nombre,
                        ApellidoPaterno = item.Apellidopaterno,
                        ApellidoMaterno = item.Apellidomaterno,
                        Edad = item.Edad,
                        Email = item.Email,
                        Contraseña = item.Contraseña
                    };
                    result.Objects.Add(usuario);
                }
                result.Correct = true;
            }
        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.ErrorMessage = ex.Message;
            result.Ex = ex;
        }
        return result;
    }

    public static Result GetById(int IdUsuario)
    {
        Result result = new();
        try
        {
            using JsanchezMacStoreContext context = new();
            var query = context.Usuarios.FromSql($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
            //var query1 = context.Usuarios.Add(Map(usuario));
            if (query != null)
            {
                result.Object = new();
                ML.Usuario usuario = new()
                {
                    IdUsuario = query.Idusuario,
                    Nombre = query.Nombre,
                    ApellidoPaterno = query.Apellidopaterno,
                    ApellidoMaterno = query.Apellidomaterno,
                    Edad = query.Edad,
                    Email = query.Email,
                    Contraseña = query.Contraseña
                };
                result.Object = usuario;
                result.Correct = true;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un error,no se logro encontrar el usuario";
            }
        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.ErrorMessage = ex.Message;
            result.Ex = ex;
        }
        return result;
    }

    public static Result GetByEmail(string Email, string contraseña)
    {
        Result result = new();
        try
        {
            using JsanchezMacStoreContext context = new();
            var listUsuario = context.Usuarios.ToList();
            var query = listUsuario.Where(u => u.Email == Email).Select(u =>
            new { u.Idusuario, u.Email, u.Contraseña }).FirstOrDefault();
            if (query != null)
            {
                result.Object = new();
                ML.Usuario usuario = new()
                {
                    IdUsuario = query.Idusuario,
                    Email = query.Email,
                    Contraseña = query.Contraseña
                };
                if (usuario.Contraseña != contraseña)
                {
                    result.Correct = false;
                    result.ErrorMessage = "La Contraseña es incorrecta";
                    return result;
                }
                result.Object = usuario;
                result.Correct = true;
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "El Email es incorrecto";
            }
        }
        catch (Exception ex)
        {
            result.Correct = false;
            result.ErrorMessage = ex.Message;
            result.Ex = ex;
        }
        return result;
    }

    //public static DL.Usuario Map(ML.Usuario usuarioMl)
    //{
    //    DL.Usuario usuarioDL = new()
    //    {
    //        Idusuario = usuarioMl.IdUsuario,
    //        Nombre = usuarioMl.Nombre!,
    //        Apellidopaterno = usuarioMl.ApellidoPaterno!,
    //        Apellidomaterno = usuarioMl.ApellidoMaterno!,
    //        Email = usuarioMl.Email!,
    //        Contraseña = usuarioMl.Contraseña!
    //    };

    //    return usuarioDL;
    //}
}
