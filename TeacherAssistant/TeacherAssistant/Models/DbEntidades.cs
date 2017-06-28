using System.Data.Entity;
using TeacherAssistant.Models;

namespace TeacherAssistant
{
    public class DbEntidades : DbContext
    {
        public DbEntidades() { }

        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Material> Materiais { get; set; }
        public DbSet<Licao> Licoes { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Forum> Foruns { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<Questionario> Questionarios { get; set; }
        public DbSet<Questao> Questoes { get; set; }
        public DbSet<MensagemUsuario> MensagensUsuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasMany(p => p.MateriaisCurtidos)
                .WithMany(s => s.Curtidas)
                .Map(c =>
                {
                    c.MapLeftKey("UsuarioId");
                    c.MapRightKey("MaterialId");
                    c.ToTable("Curtida_Material");
                });

            modelBuilder.Entity<Usuario>()
               .HasMany(p => p.Turmas)
               .WithMany(s => s.Alunos)
               .Map(c =>
               {
                   c.MapLeftKey("UsuarioId");
                   c.MapRightKey("TurmaId");
                   c.ToTable("Turma_Aluno");
               });

            modelBuilder.Entity<Questao>()
               .HasMany(p => p.Questionarios)
               .WithMany(s => s.Questoes)
               .Map(c =>
               {
                   c.MapLeftKey("QuestaoId");
                   c.MapRightKey("QuestionarioId");
                   c.ToTable("Questao_Questionario");
               });
        }
    }
}