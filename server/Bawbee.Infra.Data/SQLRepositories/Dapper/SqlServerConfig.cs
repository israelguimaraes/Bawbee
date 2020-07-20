namespace Bawbee.Infra.Data.SQLRepositories.Dapper
{
    public class SqlServerConfig
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"Server={Server};Database={Database};User Id={User};Password={Password};";
        }
    }
}
