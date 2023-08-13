using SistemaAlunos.Models.Enum;

namespace SistemaAlunos.Models.Domain
{
    public class Aluno : IAluno
    {
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }
        public EnumeradorSexo Sexo { get; set; }


        public Aluno()
        {

        }

        public Aluno(int matricula, string nome, string cpf, DateTime nascimento, EnumeradorSexo sexo)
        {
            Matricula = matricula;
            Nome = nome;
            CPF = cpf;
            Nascimento = nascimento;
            Sexo = sexo;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Aluno)
            {
                return false;
            }

            Aluno outroAluno = (Aluno)obj;
            return this.Matricula == outroAluno.Matricula;
        }

        public override int GetHashCode()
        {
            return Matricula.GetHashCode();
        }

        public override string ToString()
        {
            return "Matricula: " + Matricula
                   + "\nAluno:  " + Nome
                    + "\nCPF:  " + CPF
                    + "\nNascimento:  " + Nascimento
                    + "\nSexo:  " + Sexo;
        }
    }
}
