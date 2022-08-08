namespace Identity.Core.Settings
{
    public class PersistenceSettings
    {
        public bool UseMsSql { get; set; }

        public PersistenceConnectionStrings ConnectionStrings { get; set; }

        public class PersistenceConnectionStrings
        {
            public string MSSQLIdentity { get; set; }
        }
    }
}