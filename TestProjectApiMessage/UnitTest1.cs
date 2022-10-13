using Entities.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace TestProjectApiMessage
{
    [TestClass]
    public class UnitTest1
    {
        public static string Token { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            var result = CallApiGet("https://localhost:7124/api/List");

            var listMessage = JsonConvert.DeserializeObject<Message[]>(result).ToList();

            Assert.IsTrue(listMessage.Any());
        }

        public string CallApiGet(string url)
        {
            GetToken(); // Gerar token
            if (!string.IsNullOrWhiteSpace(Token))
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Clear();
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var response = cliente.GetStringAsync(url);
                    response.Wait();
                    return response.Result;
                }
            }

            return null;
        }

        public async Task<string> CallApiPost(string url, object dados = null)
        {

            string JsonObjeto = dados != null ? JsonConvert.SerializeObject(dados) : "";
            var content = new StringContent(JsonObjeto, Encoding.UTF8, "application/json");

            GetToken(); // Gerar token
            if (!string.IsNullOrWhiteSpace(Token))
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Clear();
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var response = cliente.PostAsync(url, content);
                    response.Wait();
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var data = await response.Result.Content.ReadAsStringAsync();

                        return data;
                    }
                }
            }

            return null;
        }

        public void GetToken()
        {
            string urlApiGeraToken = "https://localhost:7124/api/CreateTokenIdentity";

            using (var cliente = new HttpClient())
            {
                string login = "teste2@teste.com.br";
                string password = "Teste1324654.";
                var dados = new
                {
                    email = login,
                    password = password,
                    cpf = "string"
                };
                string JsonObjeto = JsonConvert.SerializeObject(dados);
                var content = new StringContent(JsonObjeto, Encoding.UTF8, "application/json");

                var resultado = cliente.PostAsync(urlApiGeraToken, content);
                resultado.Wait();
                if (resultado.Result.IsSuccessStatusCode)
                {
                    var tokenJson = resultado.Result.Content.ReadAsStringAsync();
                    Token = JsonConvert.DeserializeObject(tokenJson.Result).ToString();
                }

            }
        }
    }
}