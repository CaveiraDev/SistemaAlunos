using FirebirdSql.Data.FirebirdClient;

namespace SistemaAlunos.Factory
{
    public class ConexaoComBD
    {

        private static string _caminho =@"Database=D:\DBESTAGIO.FDB; DataSource=localhost;Port=3050;User=SYSDBA; password=masterkey; ";
        private static FbConnection conn = null;

        //CONECTAR
        public static FbConnection GetConexao()
        {
            if (conn == null || conn.State != System.Data.ConnectionState.Open)
            {
                FbConnection.ClearAllPools();
                conn = new FbConnection(_caminho);
                conn.Open();
            }

            return conn;
        }
    }
}
