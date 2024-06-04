using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karhering.Repository
{
    internal class ClientRepos
    {
        private readonly Baza _db;
        
        public ClientRepos() : this(new Baza())
        {

        }

        public ClientRepos(Baza db)
        {
            this._db = db;
        }

        public Client GetUser(int userId)
        {
            using var db = _db.getConnection();

            var client = db.QueryFirstOrDefault<Client>("SELECT * FROM client WHERE id_polz = @UserId", new { UserId = userId });
            return client;
        }
    }
}
