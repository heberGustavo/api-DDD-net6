using Entities.Entities;
using Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebAPI.Models;
using WebAPI.Token;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[AllowAnonymous]
		[Produces("application/json")]
		[HttpPost("/api/Autenticar")]
		public async Task<IActionResult> Autenticar([FromBody] Login login)
		{
			try
			{
				if (login == null || string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
					return Unauthorized();

				var result = await _signInManager.PasswordSignInAsync(login.email, login.senha, false, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					var userCurrent = await _userManager.FindByNameAsync(login.email);

					var token = new TokenJWTBuilder()
						.AddSecurityKey(JwtSecurityKey.Create("this is my custom Secret key for authentication"))
						.AddSubject("Empresa - Heber Gustavo .NET")
						.AddIssuer("Empresa.Teste.Security.Bearer")
						.AddAudience("Empresa.Teste.Security.Bearer")
						.AddClaim("IdUsuario", userCurrent.Id)
						.AddExpiry(5)
						.Builder();

					return Ok(token.value);
				}

				return Unauthorized();
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		[AllowAnonymous]
		[Produces("application/json")]
		[HttpPost("/api/CadastrarUsuario")]
		public async Task<IActionResult> CadastrarUsuario([FromBody] Login login)
		{
			if (login == null || string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
				return BadRequest("Necessário informar Email e Senha");

			var user = new ApplicationUser
			{
				UserName = login.email,
				Email = login.email,
				CPF = login.cpf,
				Tipo = TipoUsuario.Comum
			};

			var resultCreate = await _userManager.CreateAsync(user, login.senha);
			if (resultCreate.Errors.Any())
				return BadRequest(resultCreate.Errors);

			// Geração de Confirmação, caso precise
			var userId = await _userManager.GetUserIdAsync(user);
			var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

			// retorno email
			code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
			var resultado2 = await _userManager.ConfirmEmailAsync(user, code);
			
			if (resultado2.Succeeded)
				return Ok("Usuário Adicionado com Sucesso");
			else
				return Ok("Erro ao confirmar usuários");
		}
	}
}
