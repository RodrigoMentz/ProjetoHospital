namespace ProjetoHospital.Services
{
    using Flunt.Notifications;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ProjetoHospital.Entities;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using System.Data;

    public class UsuarioService(
        IGenericRepository<Usuario> usuarioRepository,
        UserManager<Usuario> usuarioManager,
        RoleManager<Role> roleManager)
        : IUsuarioService
    {
        public async Task<ResponseModel<Usuario>> Cadastrar(UsuarioViewModel cadastro)
        {
            if (string.IsNullOrWhiteSpace(cadastro.NumeroTelefone)
                || string.IsNullOrEmpty(cadastro.Nome))
            {
                return new ResponseModel<Usuario>(
                    null,
                    new List<Notification>
                        {
                            new Notification("Usuario.Cadastrar", "Cadastro incorreto"),
                        });
            }

            var celularTratado = new string(cadastro.NumeroTelefone
                .Where(char.IsLetterOrDigit)
                .ToArray());

            var usuarioComCelularExistente = await usuarioRepository
                .FindAsync(u => u.PhoneNumber == celularTratado)
                .ConfigureAwait(false);

            if (usuarioComCelularExistente != null)
            {
                return new ResponseModel<Usuario>(
                    null,
                    new List<Notification>
                        {
                            new Notification("Usuario.Cadastrar", "Já existe um usuário cadastrado com esse número de telefone."),
                        });
            }

            var usuario = new Usuario(
                cadastro.Nome,
                celularTratado,
                cadastro.Ativo);

            var username = Guid.NewGuid().ToString();
            usuario.UserName = username;
            usuario.NormalizedUserName = username.ToUpper();

            var password = "HpCanela25!";

            var resultado = await usuarioManager
                .CreateAsync(
                    usuario,
                    password)
                .ConfigureAwait(false);

            if (!resultado.Succeeded)
            {
                var notifications = new List<Notification>();

                foreach (var error in resultado.Errors)
                {
                    notifications.Add(
                        new Notification("Usuario.Cadastrar", error.Description));
                }

                return new ResponseModel<Usuario>(
                    null,
                    notifications);
            }

            var role = await roleManager
                .FindByIdAsync(cadastro.Perfil.Id)
                .ConfigureAwait(false);

            if (role == null)
            {
                return new ResponseModel<Usuario>(
                    null,
                    new List<Notification>
                        {
                            new Notification("Usuario.Cadastrar", "Perfil não encontrado."),
                        });
            }

            await usuarioManager
                .AddToRoleAsync(
                    usuario,
                    role.Name)
                .ConfigureAwait(false);

            return new ResponseModel<Usuario>(usuario);
        }

        public async Task<ResponseModel<List<PerfilViewModel>>> GetPerfis()
        {
            var roles = await roleManager
        .Roles
        .ToListAsync()
        .ConfigureAwait(false);

            var perfis = roles
                .Select(r => new PerfilViewModel(r.Id, r.Name ?? string.Empty))
                .ToList();

            return new ResponseModel<List<PerfilViewModel>>
            {
                Data = perfis
            };
        }
    }
}