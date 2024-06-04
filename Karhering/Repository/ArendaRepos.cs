using Dapper;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karhering.Repository
{
    internal class ArendaRepos
    {
        private readonly Baza _db;

        public ArendaRepos() : this(new Baza())
        {

        }

        public ArendaRepos(Baza db)
        {
            this._db = db;
        }

        public Arenda GetArenda(int UserId)
        {
            using var db = _db.getConnection();

            var arenda = db.QueryFirstOrDefault<Arenda>("SELECT * FROM client WHERE id_polz = @UserId", new { UserId = UserId });
            return arenda;
        }
    }
}
