namespace ProjetoHospital.Services
{
    using Flunt.Notifications;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using ProjetoHospital.Entities;
    using ProjetoHospital.Models;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using System.Data;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class UsuarioService(
        IGenericRepository<Usuario> usuarioRepository,
        IGenericRepository<AlteracoesDeUsuario> alteracoesDeUsuarioRepository,
        UserManager<Usuario> usuarioManager,
        SignInManager<Usuario> signInManager,
        RoleManager<Role> roleManager,
        IConfiguration configuration)
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

            var alteracaoDeUsuario = new AlteracoesDeUsuario(
                cadastro.idUsuarioExecutante,
                usuario.Id,
                "Criação de usuário",
                DateTime.Now);

            await alteracoesDeUsuarioRepository
                .InsertAsync(alteracaoDeUsuario);

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

            var alteracaoDeUsuario = new AlteracoesDeUsuario(
                cadastro.idUsuarioExecutante,
                usuario.Id,
                "Alteração de usuário",
                DateTime.Now);

            await alteracoesDeUsuarioRepository
                .InsertAsync(alteracaoDeUsuario);

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

            if (resetarSenhaViewModel.Senha == "HpCanela25!")
            {
                usuario.PhoneNumberConfirmed = false;

                await usuarioManager
                    .UpdateAsync(usuario)
                    .ConfigureAwait(false);
            }
            else
            {
                usuario.PhoneNumberConfirmed = true;

                await usuarioManager
                    .UpdateAsync(usuario)
                    .ConfigureAwait(false);
            }

            return new ResponseModel();
        }

        public async Task<ResponseModel<AcessoModel>> LoginAsync(
            LoginViewModel login)
        {
            var usuarioDb = await usuarioRepository
                .FindAsync(u => u.PhoneNumber == login.NumTelefone
                    && u.Ativo)
                .ConfigureAwait(false);

            if (usuarioDb is null)
            {
                return new ResponseModel<AcessoModel>(
                    null,
                    new List<Notification>
                        {
                            new Notification("Usuario.Login", "Usuário não encontrado."),
                        });
            }

            var usuario = await usuarioManager
                .FindByIdAsync(usuarioDb.Id)
                .ConfigureAwait(false);

            if (usuario is null)
            {
                return new ResponseModel<AcessoModel>(
                    null,
                    new List<Notification>
                        {
                            new Notification("Usuario.Login", "Usuário não encontrado."),
                        });
            }

            var result = await signInManager
                .PasswordSignInAsync(
                    usuario,
                    login.Senha,
                    true,
                    true)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return new ResponseModel<AcessoModel>(
                    null,
                    new List<Notification>
                        {
                            new Notification("Usuario.Login", "Usuário ou senha incorretos."),
                        });
            }

            await usuarioRepository
                .UpdateAsync(usuario)
                .ConfigureAwait(false);

            var rolesUser = await usuarioManager
                .GetRolesAsync(usuario)
                .ConfigureAwait(false);

            var roleName = rolesUser.FirstOrDefault();

            if (roleName is null)
            {
                return new ResponseModel<AcessoModel>(
                    null,
                    new List<Notification>
                        {
                            new Notification("Usuario.Login", "Usuário sem permissão de acesso."),
                        });
            }

            var role = await roleManager
                .FindByNameAsync(roleName)
                .ConfigureAwait(false);

            var token = await this.GerarJWTAsync(
                   usuario,
                   role.Name)
               .ConfigureAwait(false);

            var acesso = new AcessoModel(
                token,
                login.NumTelefone,
                usuario,
                role,
                usuario.PhoneNumberConfirmed);

            return new ResponseModel<AcessoModel>(acesso);
        }

        private async Task<string> GerarJWTAsync(
            Usuario usuario,
            string role,
            bool tokenTemValidade = true,
            bool addRole = true,
            bool addAudience = true)
        {
            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(await usuarioManager
                .GetClaimsAsync(usuario)
                .ConfigureAwait(false));

            identityClaims.AddClaim(new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.Id.ToString()));

            if (addRole)
            {
                identityClaims.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("xvQ7qG9PZrW0n6pQXeE7x8A3Oa6fK9YtG5vW2zM4rJ1hL8nP0eC7sT9dU2qR6jV5mB1xF8wK3tS4cY0zN7uH9pD==");
            var jwtSettings = configuration.GetSection("Jwt");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = "PROJETOHOSPITAL",
                Audience = addAudience
                    ? jwtSettings["Audience"]
                    : default,
                Expires = tokenTemValidade
                    ? DateTime.UtcNow.AddHours(120)
                    : DateTime.UtcNow.AddYears(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            return tokenHandler
                .WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}