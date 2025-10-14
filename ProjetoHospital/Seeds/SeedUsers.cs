namespace ProjetoHospital.Seeds
{
    using Microsoft.AspNetCore.Identity;
    using ProjetoHospital.Entities;

    public static class SeedUsers
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();


            var email = "rodrigokortementz@gmail.com";
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                var username = Guid.NewGuid().ToString();

                user = new Usuario
                {
                    Nome = "Suporte Rodrigo Mentz",
                    UserName = username,
                    NormalizedUserName = username.ToUpper(),
                    Email = email,
                    EmailConfirmed = true,
                    PhoneNumber = "54997035424",
                    PhoneNumberConfirmed = false,
                    Ativo = true,
                };

                var result = await userManager.CreateAsync(user, "HpCanela25!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Recepcão/Enfermagem");
                }
                else
                {
                    throw new Exception($"Erro ao criar usuário inicial: {string.Join(", ", result.Errors)}");
                }
            }
        }
    }
}