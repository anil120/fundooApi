using FundooAPI.DataContext;
using FundooAPI.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooApi.Test
{
    public class PostControllerTest
    {
        private NotesRepository repository;
        public static DbContextOptions<AuthenticationContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=.;Initial Catalog=FundooData;Integrated Security=True";

        static PostControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<AuthenticationContext>()
                .UseSqlServer(connectionString)
                .Options;

        }
        public PostControllerTest()
        {
            var context = new AuthenticationContext(dbContextOptions);
            DummyDBInitializer db = new DummyDBInitializer();
            db.Seed(context);

            repository = new NotesRepository(context);

        }
    }
}
