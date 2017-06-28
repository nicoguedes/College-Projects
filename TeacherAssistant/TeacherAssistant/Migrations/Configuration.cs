namespace TeacherAssistant.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TeacherAssistant.Util;

    internal sealed class Configuration : DbMigrationsConfiguration<TeacherAssistant.DbEntidades>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TeacherAssistant.DbEntidades context)
        {
            if (!context.Usuarios.Where(m => m.Tipo == (byte)TipoUsuario.Professor).Any())
            {
                var administrador = new Models.Usuario()
                {
                    Login = "admin",
                    Senha = Criptografia.CriptografarMd5("123"),
                    Nome = "Administrador do Sistema",
                    Tipo = (byte)TipoUsuario.Professor
                };

                context.Usuarios.Add(administrador);
                context.SaveChanges();

            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
