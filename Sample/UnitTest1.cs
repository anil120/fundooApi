using FundooAPI.DataContext;
using FundooAPI.Repository;
using FundooAPI.Services;
using FundooData.Models;
using FundooNotes.Controllers;
using GenFu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sample
{
    public class UnitTest1
    {
        private AuthenticationContext authenticationContext;
        private DbContextOptions DbContextOptions;
        private IEnumerable<NotesModel> GetFakeData()
        {
            var persons = A.ListOf<NotesModel>(26);
            persons.ForEach(x => x.Id = Guid.NewGuid());
            return persons.Select(_ => _);
        }
        public UnitTest1()
        {
            InitContext();
        }
        // [Fact]
        // public void Get_WhenCalled_ReturnsOkResult()
        // {
        //     var control = new NotesController(this.authenticationContext.Object, this.repository.Object);
        //     // Act
        //     var okResult = controller.GetNotes();
        //     // Assert
        //     Assert.IsType<OkObjectResult>(okResult);
        // }
        // [Fact]
        // public void GetNotesTest()
        // {
        //     // Arrange
        //     var service = new Mock<INotesService>();
        //     var persons = GetFakeData();
        //     service.Setup(x => x.GetAllItems()).Returns(persons);
        //     Guid id = new Guid();
        //     // Act
        //     var results = controller.GetNotes(id);
        //     var count = results;
        //     // Assert
        //     Assert.Equal(26, 26);
        // }
        // [Fact]
        // public void Task_GetPostById_Return_OkResult()
        // {
        //     try
        //     {
        //         //Arrange  
        //         var control = new NotesController(this.authenticationContext.Object, this.repository.Object);
        //         //var postId = 2;
        //
        //         //Act  
        //         var data = controller.GetNotes();
        //
        //         //Assert  
        //         Assert.IsType<OkObjectResult>(data);
        //     }
        //     catch (Exception e)
        //     {
        //         throw new Exception(e.Message);
        //     }
        //
        // }

        [Fact]
        public void AddNoteWithModelToTestAddNoteFeature()
        {
            try
            {
                var repository = new Mock<INotesRepository>();

                var controller = new NotesController(authenticationContext, repository.Object);
                NotesModel notesModel = new NotesModel()
                {
                    Title = "Testing",
                    ColorCode = "dsd",
                    Email = "dadsa",
                    Id = new Guid(),
                    ImageUrl = "test",
                    IsArchieve = true,
                    IsPin = false,
                    IsTrash = false,
                    Reminder = "dadsa",
                    TakeANote = "asdfds"
                };

                var result = controller.PostNotes(notesModel);

                Assert.NotNull(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private AuthenticationContext _context;

        [Fact]
        public void InitContext()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var builder = new DbContextOptionsBuilder<AuthenticationContext>()
                .UseInMemoryDatabase();
            var context = new AuthenticationContext(builder.Options);

            var notes = Enumerable.Range(1, 10)
                .Select(i => new NotesModel { Id = new Guid(), Title = $"Sample{i}", TakeANote = "Wrox Press" });
            context.Notes.AddRange(notes);
            int changed = context.SaveChanges();
            _context = context;
        }




        INotesRepository notesRepository;
        [Fact]
        public void TestGetBookById()
        {
            string expectedTitle = "222";
            var contexts = new Mock<AuthenticationContext>();
            var controller = new NotesController(_context, notesRepository);
            Guid guid = new Guid("146c9eb1-dffc-46a5-d355-08d6bf36d109");
            var result = controller.GetNotes(guid);
            Assert.Equal(expectedTitle, result.ToString());
        }
    }
}
