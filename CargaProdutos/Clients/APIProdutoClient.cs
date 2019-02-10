using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using CargaProdutos.Models;

namespace CargaProdutos.Clients
{
    public class  APIProdutoClient
    {
        private HttpClient _client;

        public APIProdutoClient(HttpClient client)
        {
            _client = client;
        }

        public void IncluirProduto(Produto produto)
        {
            HttpResponseMessage response = _client.PostAsync(
                "produtos", new StringContent(
                    JsonConvert.SerializeObject(produto),
                    Encoding.UTF8, "application/json")).Result;

            Console.WriteLine(
                response.Content.ReadAsStringAsync().Result);
        }

        public List<Produto> ListarProdutos()
        {
            HttpResponseMessage response = _client.GetAsync(
                "produtos").Result;

            List<Produto> resultado = null;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                resultado = JsonConvert.DeserializeObject<List<Produto>>(conteudo);
            }
            else
                Console.WriteLine("Token provavelmente expirado!");

            return resultado;
        }        
    }
}