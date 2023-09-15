using FirebirdSql.Data.FirebirdClient;
using SistemaAlunos.Factory;
using SistemaAlunos.Models.Domain;
using SistemaAlunos.Models.Enum;

namespace SistemaAlunos.Repository
{
    public class RepositorioAluno : RepositorioAbstrato
    {


        

        private readonly FbConnection conn;
        public RepositorioAluno()
        {
            conn = ConexaoComBD.GetConexao();
        }

        public override void  Add(Aluno aluno)
        {


            string query = "INSERT INTO ALUNOS( MATRICULA, NOME, CPF, NASCIMENTO, SEXO) VALUES (@ALU_MATRICULA, @ALU_NOME, @ALU_CPF, @ALU_NASCIMENTO, @ALU_SEXO);";

            try
            {
                using FbCommand cmd = new FbCommand(query, conn);
                cmd.Parameters.AddWithValue("@ALU_MATRICULA", aluno.Matricula);
                cmd.Parameters.AddWithValue("@ALU_NOME", aluno.Nome);
                cmd.Parameters.AddWithValue("@ALU_CPF", aluno.CPF);
                cmd.Parameters.AddWithValue("@ALU_NASCIMENTO", aluno.Nascimento);
                cmd.Parameters.AddWithValue("@ALU_SEXO", aluno.Sexo);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"Aluno(a) {aluno.Nome} Adicionado com sucesso!");



            }
            catch (Exception erro)
            {

                Console.WriteLine($"Erro ao Adicionar um Aluno(a), detalhe do erro {erro}");
            }

        }
        public override void Update(Aluno aluno)
        {
            string query = "UPDATE ALUNOS SET ALU_NOME = @ALU_NOME, ALU_CPF= @ALU_CPF, ALU_NASCIMENTO = @ALU_NASCIMENTO, ALU_SEXO = @ALU_SEXO WHERE ALU_MATRICULA =  @ALU_MATRICULA;";

            try
            {
                using FbCommand cmd = new FbCommand(query, conn);
                cmd.Parameters.AddWithValue("@ALU_MATRICULA", aluno.Matricula);
                cmd.Parameters.AddWithValue("@ALU_NOME", aluno.Nome);
                cmd.Parameters.AddWithValue("@ALU_CPF", aluno.CPF);
                cmd.Parameters.AddWithValue("@ALU_NASCIMENTO", aluno.Nascimento);
                cmd.Parameters.AddWithValue("ALU_SEXO", aluno.Sexo);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"Dados do Aluno(a) {aluno.Nome} Atualizado com sucesso!");

            }
            catch (Exception erro)
            {

                Console.WriteLine($"Erro na atualiacao do Aluno(a), detalhe do erro {erro}");
            }
        }

        public override void Remove(int matricula)
        {
            string query = "DELETE FROM ALUNOS WHERE ALU_MATRICULA = @ALU_MATRICULA ;";

            try
            {
                using FbCommand cmd = new FbCommand(query, conn);
                cmd.Parameters.AddWithValue("ALU_MATRICULA ", matricula);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Aluno Removido com Sucesso!");
            }
            catch (Exception erro)
            {

                Console.WriteLine($"Erro ao deletar um Aluno, detalhe do erro {erro}");
            }
        }


        public override IEnumerable<Aluno> Get(int matricula)
        {
            List<Aluno> listaDeAlunos = new List<Aluno>();
            string query = "SELECT * FROM ALUNOS WHERE ALU_MATRICULA = @ALU_MATRICULA ;";

            try
            {
                using (FbCommand cmd = new FbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ALU_MATRICULA ", matricula);

                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Aluno aluno = new Aluno
                            {

                                Matricula = Convert.ToInt32(reader["MATRICULA"]),
                                Nome = reader["ALU_NOME"].ToString(),
                                CPF = reader["ALU_CPF"].ToString(),
                                Nascimento = DateTime.Parse(reader["ALU_NASCIMENTO"].ToString()),
                                Sexo = (EnumeradorSexo)Enum.Parse(typeof(EnumeradorSexo), reader["ALU.SEXO"].ToString())
                                // Preencha aqui as outras propriedades do objeto Aluno, se houver
                            };
                            listaDeAlunos.Add(aluno);
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine($"Erro ao Listar todos Aluno(a), detalhe do erro: {erro}");
            }

            return listaDeAlunos;
        }

        public override IEnumerable<Aluno> GetAll()
        {
            List<Aluno> listaDeAlunos = new List<Aluno>();
            string query = "SELECT * FROM ALUNOs ORDER BY ALU_MATRICULA ASC;";

            try
            {
                using (FbCommand cmd = new FbCommand(query, conn))
                {


                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Aluno aluno = new Aluno
                            {

                                Matricula = Convert.ToInt32(reader["ALU_MATRICULA"]),
                                Nome = reader["ALU_NOME"].ToString(),
                                CPF = reader["ALU_CPF"].ToString(),
                                Nascimento = DateTime.Parse(reader["ALU_NASCIMENTO"].ToString()),
                                Sexo = (EnumeradorSexo)Enum.Parse(typeof(EnumeradorSexo), reader["ALU_SEXO"].ToString())       
                            };
                            listaDeAlunos.Add(aluno);
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine($"Erro ao Listar todos Aluno(a), detalhe do erro: {erro}");
            }

            return listaDeAlunos;
        }
    }
}
