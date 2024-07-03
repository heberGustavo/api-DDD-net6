using Entities.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebAPI.Models;

namespace TestProjectAPIDDD
{
	[TestClass]
	public class UnitTest1
	{
		private const string BaseURL = "https://localhost:7185";

		private static string Token { get; set; }

        [TestMethod]
		public void TestMessageGetAll()
		{
			var result = ChamarApiGet(string.Concat(BaseURL, "/api/GetAll"));

			var list = JsonConvert.DeserializeObject<List<Message>>(result).ToList();

			Assert.IsTrue(list.Any());
		}

		[TestMethod]
		public void TestMessageAdd()
		{
			var messageViewModel = new MessageViewModel
			{
				Titulo = "Teste pelo Teste",
				Ativo = true,
				DataCadastro = DateTime.Now,
				DataAlteracao = DateTime.Now.AddDays(1),
				UserId = "618d8a88-2bf4-4948-aa24-d635d6fa3801"
			};

			var result = ChamarApiPost(string.Concat(BaseURL, "/api/Add"), messageViewModel).Result;

			Assert.IsTrue(result.Equals("OK"));
		}

		#region Private Methods

		private async void GetToken()
		{
			using (var _cliente = new HttpClient())
			{
				string urlApiGerarToken = "https://localhost:7185/api/Autenticar";

				var userTeste = new Login
				{
					email = "heber@heber1.com",
					senha = "Gustavo@123",
					cpf = "123123"
				};

				string jsonObjeto = JsonConvert.SerializeObject(userTeste);
				var content = new StringContent(jsonObjeto, Encoding.UTF8, "application/json");

				var resultado = _cliente.PostAsync(urlApiGerarToken, content);
				resultado.Wait();
				if (resultado.Result.IsSuccessStatusCode)
				{
					var tokenJson = resultado.Result.Content.ReadAsStringAsync();
					Token = JsonConvert.DeserializeObject(tokenJson.Result).ToString();
				}
			}
		}

		private string ChamarApiGet(string url)
		{
			GetToken();
			if (!string.IsNullOrWhiteSpace(Token))
			{
				using (var _cliente = new HttpClient())
				{
					_cliente.DefaultRequestHeaders.Clear();
					_cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

					var response = _cliente.GetStringAsync(url);
					response.Wait();
					return response.Result;
				}
			}

			return string.Empty;
		}

		private async Task<string> ChamarApiPost(string url, object dados = null)
		{
			string jsonObjeto = dados != null ? JsonConvert.SerializeObject(dados) : string.Empty;
			var content = new StringContent(jsonObjeto, Encoding.UTF8, "application/json");

			GetToken();
			if (!string.IsNullOrWhiteSpace(Token))
			{
				using (var _cliente = new HttpClient())
				{
					_cliente.DefaultRequestHeaders.Clear();
					_cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

					var response = await _cliente.PostAsync(url, content);
					if (response.IsSuccessStatusCode)
						return "OK";
				}
			}

			return string.Empty;
		}

		#endregion

	}
}