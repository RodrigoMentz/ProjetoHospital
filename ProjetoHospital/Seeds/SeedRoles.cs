namespace ProjetoHospital.Seeds
{
    using Microsoft.AspNetCore.Identity;
    using ProjetoHospital.Entities;

    public static class SeedRoles
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            string[] roles = new[] { "Limpeza", "Recepcão/Enfermagem", "Inspeção da limpeza", "Manutenção" };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new Role
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    };

                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}