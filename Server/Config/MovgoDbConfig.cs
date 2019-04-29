using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromocodeService.Config
{
    public class MongoDBConfig
    {
        public string Database { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string ConnectionString
        {
            get
            {

                return "mongodb://localhost:27017";


                //if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                //    return $@"mongodb://{Host}:{Port}";

                //return $@"mongodb://{User}:{Password}@{Host}:{Port}";
            }
        }
    }
}
