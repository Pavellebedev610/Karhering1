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
        public int GetBonusByPromo(string promo)
        {
            using var db = _db.getConnection();

            var bonus = db.QueryFirstOrDefault<int>("SELECT bonus FROM client WHERE promo = @Promo", new { Promo = promo });
            return bonus;
        }
        public void UpdateUserBonus(int userId, int newBonus)
        {
            using var db = _db.getConnection();
            db.Execute("UPDATE client SET bonus = @NewBonus WHERE id_polz = @UserId", new { NewBonus = newBonus, UserId = userId });
        }
        public bool IsPromoCodeUsed(int userId, string promo)
        {
            using var db = _db.getConnection();

            var result = db.ExecuteScalar<int>("SELECT COUNT(*) FROM UsedPromoCodes WHERE user_id = @UserId AND promo_code = @Promo", new { UserId = userId, Promo = promo });

            return result > 0;
        }
        public void RecordUsedPromoCode(int userId, string promo)
        {
            using var db = _db.getConnection();

            db.Execute("INSERT INTO UsedPromoCodes (user_id, promo_code) VALUES (@UserId, @Promo)", new { UserId = userId, Promo = promo });
        }
    }
}
