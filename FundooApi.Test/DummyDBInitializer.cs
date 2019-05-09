using FundooAPI.DataContext;
using FundooData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooApi.Test
{
    public class DummyDBInitializer
    {
        public DummyDBInitializer()
        {
        }

        public void Seed(AuthenticationContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Notes.AddRange(
        new NotesModel()
        {
            Id = new Guid("924928d3-af04-45d8-13ec-08d6bda38726"),
            Title = "qwerty",
            TakeANote = null,
            RemindMe = false,
            IsArchieve = false,
            IsPin = false,
            IsTrash = false,
            ImageUrl = null,
            ColorCode = null,
            Reminder = null,
            Email = "luckykaran95@gmail.com"
        }
            //new NotesModel() { Name = "VISUAL STUDIO", Slug = "visualstudio" },
            //new NotesModel() { Name = "ASP.NET CORE", Slug = "aspnetcore" },
            //new NotesModel() { Name = "SQL SERVER", Slug = "sqlserver" }
            );
            context.SaveChanges();
        }
    }
}
