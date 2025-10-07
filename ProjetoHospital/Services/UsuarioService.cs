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
        public async Task<ResponseModel<List<UsuarioViewModel>>> GetAsync()
        {
            var usuarios = await usuarioRepository
                .FindAllAsync(
                    u => true,
                    query => query
                        .Include(u => u.UsuarioRoles)
                        .ThenInclude(ur => ur.Role))
                .ConfigureAwait(false);

            var usuariosViewModel = usuarios
                .Select(u => new UsuarioViewModel(
                    u.Id,
                    u.Nome,
                    u.UsuarioRoles?.FirstOrDefault() is not null
                        ? new PerfilViewModel(
                            u.UsuarioRoles.First().RoleId,
                            u.UsuarioRoles.First().Role?.Name ?? string.Empty)
                        : new PerfilViewModel("0", string.Empty),
                    u.PhoneNumber ?? string.Empty,
                    u.Ativo))
                .ToList();

            var response = new ResponseModel<List<UsuarioViewModel>>
            {
                Data = usuariosViewModel
            };

            return response;
        }

        public async Task<ResponseModel> CadastrarAsync(
            UsuarioViewModel cadastro)
        {
            if (string.IsNullOrWhiteSpace(cadastro.NumeroTelefone)
                || string.IsNullOrEmpty(cadastro.Nome))
            {
                return new ResponseModel(
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
                return new ResponseModel(
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

                return new ResponseModel(
                    notifications);
            }

            var role = await roleManager
                .FindByIdAsync(cadastro.Perfil.Id)
                .ConfigureAwait(false);

            if (role == null)
            {
                return new ResponseModel(
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

            return new ResponseModel();
        }

        public async Task<ResponseModel> AtualizarAsync(
            UsuarioViewModel cadastro)
        {
            if (string.IsNullOrWhiteSpace(cadastro.NumeroTelefone)
                || string.IsNullOrEmpty(cadastro.Nome))
            {
                return new ResponseModel(
                    new List<Notification>
                        {
                            new Notification("Usuario.Atualizar", "Cadastro incorreto"),
                        });
            }

            var usuario = await usuarioManager
                .FindByIdAsync(cadastro.Id)
                .ConfigureAwait(false);

            if (usuario == null)
            {
                return new ResponseModel(
                    new List<Notification>
                        {
                            new Notification("Usuario.Atualizar", "Usuário não encontrado"),
                        });
            }

            var celularTratado = new string(cadastro.NumeroTelefone
                .Where(char.IsLetterOrDigit)
                .ToArray());

            var usuarioComCelularExistente = await usuarioRepository
                .FindAsync(u => u.PhoneNumber == celularTratado && u.Id != cadastro.Id)
                .ConfigureAwait(false);

            if (usuarioComCelularExistente != null)
            {
                return new ResponseModel(
                    new List<Notification>
                        {
                            new Notification("Usuario.Atualizar", "Já existe um usuário cadastrado com esse número de telefone."),
                        });
            }

            usuario.Modify(
                cadastro.Nome,
                celularTratado,
                cadastro.Ativo);

            var resultado = await usuarioManager
                .UpdateAsync(usuario)
                .ConfigureAwait(false);

            if (!resultado.Succeeded)
            {
                var notifications = new List<Notification>();

                foreach (var error in resultado.Errors)
                {
                    notifications.Add(
                        new Notification("Usuario.Atualizar", error.Description));
                }

                return new ResponseModel(
                    notifications);
            }

            var role = await roleManager
                .FindByIdAsync(cadastro.Perfil.Id)
                .ConfigureAwait(false);

            if (role == null)
            {
                return new ResponseModel(
                    new List<Notification>
                        {
                            new Notification("Usuario.Atualizar", "Perfil não encontrado."),
                        });
            }

            var rolesDoUsuario = await usuarioManager
                .GetRolesAsync(usuario)
                .ConfigureAwait(false);

            if (role.Name != rolesDoUsuario.FirstOrDefault())
            {
                await usuarioManager
                    .RemoveFromRolesAsync(
                        usuario,
                        rolesDoUsuario)
                    .ConfigureAwait(false);

                await usuarioManager
                   .AddToRoleAsync(
                       usuario,
                       role.Name)
                   .ConfigureAwait(false);
            }

            return new ResponseModel();
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

        public async Task<ResponseModel> ResetarSenhaAsync(
            ResetarSenhaViewModel resetarSenhaViewModel)
        {
            var usuario = await usuarioManager
                .FindByIdAsync(resetarSenhaViewModel.Id)
                .ConfigureAwait(false);

            if (usuario is null)
            {
                return new ResponseModel(
                    new List<Notification>
                        {
                            new Notification("Usuario.ResetarSenha", "Usuário não encontrado."),
                        });
            }

            var token = await usuarioManager
                .GeneratePasswordResetTokenAsync(usuario)
                .ConfigureAwait(false);

            var resultado = await usuarioManager
                .ResetPasswordAsync(
                    usuario,
                    token,
                    resetarSenhaViewModel.Senha)
                .ConfigureAwait(false);

            if (!resultado.Succeeded)
            {
                return new ResponseModel(resultado.Errors.Select(e => new Notification(
                    "Usuario.ResetarSenha",
                    e.Description)));
            }

            return new ResponseModel();
        }
    }
}