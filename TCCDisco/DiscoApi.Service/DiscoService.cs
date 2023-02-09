using DiscoApi.Respositorios.Repositorio;
using Models.DiscoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscoApi.Service
{
    public class DiscoService
    {

        private readonly DiscoRepositorio _repositorio;

        public DiscoService(DiscoRepositorio repositorio)
        {

            _repositorio = repositorio;

        }

        public List<Disco> Listar(string? nome)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarProdutos(nome);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public void Inserir()
        {
            try
            {
                _repositorio.AbrirConexao();
                _repositorio.Inserir();
            }
            finally
            {
                _repositorio.FecharConexao();
            }





        }
    }
}
