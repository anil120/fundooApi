//-----------------------------------------------------------------------
// <copyright file="AuthenticationContext.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace FundooAPI.DataContext
{
    using FundooAPI.Models;
    using FundooData.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using GenFu;
    using System;

    /// <summary>
    /// Authentication context is class which is inheriting identity context class and in this class we are registering our model class with dbset
    /// </summary>
    public class AuthenticationContext : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the of context class <see cref="AuthenticationContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a data base context option<see cref="T:Microsoft.EntityFrameworkCore.DbContext" />.</param>
        public AuthenticationContext(DbContextOptions options) : base(options)
        {
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // seeding
        //    // var i=Guid.NewGuid();
        //    var personsToSeed = A.ListOf<NotesModel>(26);
        //    personsToSeed.ForEach(x => x.Id = Guid.NewGuid());
        //    modelBuilder.Entity<NotesModel>().HasData(personsToSeed);
        //}

        /// <summary>
        /// database set is a property of context class in which we are registering our model
        /// </summary>
        /// <value>
        /// The application users.
        /// </value>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        /// <summary>
        /// database set is a property of context class in which we are registering our model
        /// </summary>
        /// <value>
        /// The notes models.
        /// </value>
        public DbSet<NotesModel> Notes { get; set; }
        public DbSet<CollaboratorTbl> tblCollaborator { get; set; }
        public DbSet<NotesLabelTable> tblNotesLabel { get; set; }
        public DbSet<LableTbl> tblLabel { get; set; }
        // public DbSet<CloudinarySettings> CloudinarySettings { get; set; }
    }
}
