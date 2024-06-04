using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Karhering.Repository
{
    internal class CarRepos
    {
        private readonly Baza _db;
        public CarRepos() : this (new Baza())
        {
            
        }
        public CarRepos(Baza db) 
        {
            this._db = db;
        }

        public List<Car> GetCars()
        {
            using var db = _db.getConnection();

            var cars = db.Query<Car>("select * from car").ToList();
            return cars;
        }



    }
}
