using EuEstudo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace EuEstudo.Filters
{
    public class PaginaUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if(string.IsNullOrEmpty(sessaoUsuario)) 
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary{{"controller","login"}, {"action","index"}});
            }
            else 
            {
                Usuario usuario = JsonSerializer.Deserialize<Usuario>(sessaoUsuario);
                if(usuario == null) 
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary{{"controller","login"},{"action","index"}});
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
