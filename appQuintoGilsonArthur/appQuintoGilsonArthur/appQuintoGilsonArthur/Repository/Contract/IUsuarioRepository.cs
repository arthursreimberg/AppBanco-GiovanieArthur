﻿using appQuintoGiovaniArthur.Models;

namespace appQuintoGiovaniArthur.Repository.Contract
{
    public interface IUsuarioRepository
    {
        //CRUD
        IEnumerable<Usuario> ObterTodosUsuarios();

        void Cadastrar(Usuario usuario);

        void Atualizar(Usuario usuario);

        Usuario ObterUsuario(int Id);

        void Excluir(int id);
    }
}
