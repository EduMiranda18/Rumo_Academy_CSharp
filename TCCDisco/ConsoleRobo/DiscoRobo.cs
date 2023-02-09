using Models.DiscoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Reflection;

namespace ConsoleRobo
{
    public class DiscoRobo
    {
        private readonly string _urlBase;
        public DiscoRobo(string urlBase) {
        
            _urlBase = urlBase;
        }
        
        public List<Disco> ObterDisco() {

            
            var client = new HttpClient();
            var result = client.GetAsync("https://www.discosdevinil.com.br/categorias/discos-de-vinil-nacionais?page=1").Result;

            Utf8EncodingProvider.Register();
            var html = result.Content.ReadAsStringAsync().Result;

            // peguei o html e obtive o total de produtos da pagina
            // fingi que fiz calculo, pegando o total de produtos da pagina e fazendo um calculo.
            var totalPagina = 10;

            var paginas = Enumerable.Range(1, totalPagina);

            var listaDisco = new List<Disco>();
            foreach (var pagina in paginas)
            {
                result = client.GetAsync(_urlBase + "categorias/discos-de-vinil-nacionais?page=" + pagina).Result;
                html = result.Content.ReadAsStringAsync().Result;

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                //selecionei os produtos pela classe css card-item
                var produtos = doc.DocumentNode.SelectNodes("//a[contains(@class, 'product')]");

                foreach (var produto in produtos)
                {
                    var elementoPreco = produto.SelectNodes(".//div[contains(@class, 'price-value bold')]");
                    if (elementoPreco is null)
                        continue;

                    var preco = decimal.Parse(elementoPreco[0].InnerText.Replace("R$ ", ""));

                    var linkCompleto = _urlBase;
                    var titulo = produto.SelectNodes(".//div[contains(@class, 'title')]").First().InnerText.Replace("\"", "");

                    listaDisco.Add(new Disco
                    {
                        Nome = titulo,
                        Link = linkCompleto,
                        Preco = preco
                    });
                }
            }
            return listaDisco;
        }
    }

        

    
    public class Utf8EncodingProvider : EncodingProvider
    {
        public override Encoding GetEncoding(string name)
        {
            return name == "utf8" ? Encoding.UTF8 : null;
        }

        public override Encoding GetEncoding(int codepage)
        {
            return null;
        }

        public static void Register()
        {
            Encoding.RegisterProvider(new Utf8EncodingProvider());
        }
    }
}
