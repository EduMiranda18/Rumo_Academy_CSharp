using ConsoleRobo;
using Microsoft.Extensions.Configuration;
using Models.DiscoApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscoApi.Respositorios.Repositorio
{
    public class DiscoRepositorio: Contexto
    {
        private readonly DiscoRobo _DiscoRobo;

        public DiscoRepositorio (IConfiguration configuration) : base(configuration)
        {

            _DiscoRobo = new DiscoRobo("https://www.discosdevinil.com.br/");

        }

        public List<Disco> ListarProdutos(string? nome)
        {
            string comandoSql = @"SELECT Nome, Preco, PrecoAntigo, Link, DataBusca FROM Discos";

            if (!string.IsNullOrWhiteSpace(nome))
                comandoSql += " WHERE Nome LIKE @Nome";

            using (var cmd = new MySqlCommand(comandoSql, _conn))
            {
                if (!string.IsNullOrWhiteSpace(nome))
                    cmd.Parameters.AddWithValue("@Nome", "%" + nome + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var discos = new List<Disco>();
                    while (rdr.Read())
                    {
                       var disco = new Disco();
                       disco.Nome = Convert.ToString(rdr["Nome"]);
                       disco.Preco = Convert.ToDecimal(rdr["Preco"]);
                       disco.PrecoAntigo = rdr["PrecoAntigo"] is DBNull ? 0 : Convert.ToDecimal(rdr["PrecoAntigo"]);
                       disco.Link = Convert.ToString(rdr["Link"]);
                       disco.DataBusca = Convert.ToDateTime(rdr["DataBusca"]);
                        discos.Add(disco);
                    }
                    return discos;
                }
            }
        }

        public void Inserir()
        {
            var discos = _DiscoRobo.ObterDisco().ToList();

            foreach (var disco in discos)
            {
                if (ProdutoExiste(disco.Nome, disco.Preco))
                {
                    AtualizarProduto(disco);
                }
                else
                {
                    InserirProduto(disco);
                }
            }
        }

        private bool ProdutoExiste(string nome, decimal preco)
        {
            string comandoSql = "SELECT COUNT(*) FROM Discos WHERE Nome = @Nome AND Preco = @Preco";

            using (MySqlCommand command = new MySqlCommand(comandoSql, _conn))
            {
                command.Parameters.AddWithValue("@Nome", nome);
                command.Parameters.AddWithValue("@Preco", preco.ToString("N2", CultureInfo.InvariantCulture));

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        private void AtualizarProduto(Disco disco)
        {
            string comandoSql = @"UPDATE Discos SET Preco = @Preco, PrecoAntigo = @PrecoAntigo, DataBusca = @DataBusca WHERE Nome = @Nome";
            decimal precoAntigo = ObterPrecoAtualDoProduto(disco.Nome);
            if (precoAntigo != disco.Preco)
            {
                using (MySqlCommand command = new MySqlCommand(comandoSql, _conn))
                {
                    command.Parameters.AddWithValue("@Nome", disco.Nome);
                    command.Parameters.AddWithValue("@Preco", disco.Preco.ToString("N2", CultureInfo.InvariantCulture));
                    command.Parameters.AddWithValue("@PrecoAntigo", precoAntigo.ToString("N2", CultureInfo.InvariantCulture));
                    command.Parameters.AddWithValue("@DataBusca", disco.DataBusca);

                    command.ExecuteNonQuery();
                }
            }
        }

        private decimal ObterPrecoAtualDoProduto(string nome)
        {
            string comandoSql = @"SELECT Preco FROM Discos WHERE Nome = @Nome";
            decimal preco = 0;
            using (MySqlCommand command = new MySqlCommand(comandoSql, _conn))
            {
                command.Parameters.AddWithValue("@Nome", nome);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        preco = Convert.ToDecimal(reader["Preco"], CultureInfo.InvariantCulture);
                    }
                }
            }

            return preco;
        }

        private void InserirProduto(Disco disco)
        {
            string comandoSql = @"INSERT INTO Discos (Nome, Preco, Link, DataBusca) VALUES (@Nome, @Preco, @Link, @DataBusca)";

            using (MySqlCommand command = new MySqlCommand(comandoSql, _conn))
            {
                command.Parameters.AddWithValue("@Nome", disco.Nome);
                command.Parameters.AddWithValue("@Preco", disco.Preco.ToString("N2", CultureInfo.InvariantCulture));
                command.Parameters.AddWithValue("@Link", disco.Link);
                command.Parameters.AddWithValue("@DataBusca", disco.DataBusca);

                command.ExecuteNonQuery();
            }
        }

    }

}

