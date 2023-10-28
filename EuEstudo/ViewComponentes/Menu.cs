using EuEstudo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EuEstudo.ViewComponentes
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
            Usuario usuario = JsonSerializer.Deserialize<Usuario>(sessaoUsuario);
            return View(usuario);
        }
    }
}
