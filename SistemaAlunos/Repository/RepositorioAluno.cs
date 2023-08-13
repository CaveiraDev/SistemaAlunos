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


            string query = "INSERT INTO ALUNO( MATRICULA, NOME, CPF, Nascimento, Sexo) VALUES (@matricula, @nome, @cpf, @nascimento, @sexo);";

            try
            {
                using FbCommand cmd = new FbCommand(query, conn);
                cmd.Parameters.AddWithValue("@matricula", aluno.Matricula);
                cmd.Parameters.AddWithValue("@nome", aluno.Nome);
                cmd.Parameters.AddWithValue("@cpf", aluno.CPF);
                cmd.Parameters.AddWithValue("@nascimento", aluno.Nascimento);
                cmd.Parameters.AddWithValue("@sexo", aluno.Sexo);

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
            string query = "UPDATE Aluno SET NOME = @nome, EMAIl= @email, TELEFONE = @telefone, Serie = @serie WHERE MATRICULA =  @matricula;";

            try
            {
                using FbCommand cmd = new FbCommand(query, conn);
                cmd.Parameters.AddWithValue("@matricula", aluno.Matricula);
                cmd.Parameters.AddWithValue("@nome", aluno.Nome);
                cmd.Parameters.AddWithValue("@cpf", aluno.CPF);
                cmd.Parameters.AddWithValue("@nascimento", aluno.Nascimento);
                cmd.Parameters.AddWithValue("@sexo", aluno.Sexo);

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
            string query = "DELETE FROM ALUNO WHERE MATRICULA = @matricula;";

            try
            {
                using FbCommand cmd = new FbCommand(query, conn);
                cmd.Parameters.AddWithValue("@matricula", matricula);

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
            string query = "SELECT * FROM ALUNO WHERE MATRICULA = @matricula;";

            try
            {
                using (FbCommand cmd = new FbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@matricula", matricula);

                    using (FbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Aluno aluno = new Aluno
                            {

                                Matricula = Convert.ToInt32(reader["MATRICULA"]),
                                Nome = reader["NOME"].ToString(),
                                CPF = reader["EMAIL"].ToString(),
                                Nascimento = DateTime.Parse(reader["NASCIMENTO"].ToString()),
                                Sexo = (EnumeradorSexo)Enum.Parse(typeof(EnumeradorSexo), reader["SEXO"].ToString())
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
            string query = "SELECT * FROM ALUNO ORDER BY MATRICULA ASC;";

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

                                Matricula = Convert.ToInt32(reader["MATRICULA"]),
                                Nome = reader["NOME"].ToString(),
                                CPF = reader["EMAIL"].ToString(),
                                Nascimento = DateTime.Parse(reader["NASCIMENTO"].ToString()),
                                Sexo = (EnumeradorSexo)Enum.Parse(typeof(EnumeradorSexo), reader["SEXO"].ToString())                  // Preencha aqui as outras propriedades do objeto Aluno, se houver
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
