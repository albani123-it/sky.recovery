namespace sky.recovery.Helper.Config
{
    public class DbContextSettings
    {
       
            public sqlserver sqlserver { get; set; }
            public postgresql postgresql { get; set; }

        }
        public class sqlserver
        {
            public string ConnectionString_log { get; set; }
        }
        public class postgresql
        {
            public string ConnectionString_coll { get; set; }
            public string ConnectionString_core { get; set; }

        }
    }

