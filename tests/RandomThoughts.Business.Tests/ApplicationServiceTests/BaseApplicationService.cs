using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RandomThoughts.Business.ApplicationServices.Thoughts;
using RandomThoughts.Business.Base;
using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.DataAccess.Repositories.Thoughts;
using RandomThoughts.Domain;
using RandomThoughts.Test.Common.Abstract;
using RandomThoughts.Test.Common.Services;
using Xunit;

namespace RandomThoughts.Business.Tests.ApplicationServiceTests
{
    /// <summary>
    ///     This class contains the uniTests for the <see cref="BaseApplicationService{TEntity,TKey}"/>
    /// </summary>
    public class BaseApplicationServiceTests : IBaseApplicationServiceTest<ThoughtsApplicationService, Thought, int>
    {
        /// <summary>
        ///     Create an instance of the actual implementation of the interface
        ///      <see cref="IBaseApplicationService{TEntity,TKey}"/> for testing its operations
        /// </summary>
        /// <param name="context">The DBcontext for be used in the unit tests</param>
        /// <returns>An instance of the class <see cref="ThoughtsApplicationService"/></returns>
        public ThoughtsApplicationService GetInstance(RandomThoughtsDbContext context)
        {
            return new ThoughtsApplicationService(new ThoughtsRepository(context));
        }

        public ThoughtsApplicationService GetInstance(DbContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Add(), SaveChanges() and SingleOrDefault()
        /// </summary>
        [Fact]
        public void Test_Add()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as RandomThoughtsDbContext;

            var user = new ApplicationUser();
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            appService.Add(new Thought { Title = "Thought1",Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            appService.SaveChanges();

            var Thought = appService.SingleOrDefault(t=>t.Title == "Thought1");

            Assert.NotNull(Thought);
            Assert.Equal(Thought.Title, "Thought1");
            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - AddAsync(), SaveChangesAsync() and SingleOrDefaultAsync()
        /// </summary>
        [Fact]
        public async Task Test_AddAsync()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;
            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};
            var appService = GetInstance(context);

            await appService.AddAsync(new Thought { Id = 1, Title = "Thought1",Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            await appService.SaveChangesAsync();

            var Thought = await appService.SingleOrDefaultAsync(1);

            Assert.NotNull(Thought);
            Assert.Equal(Thought.Title, "Thought1");
            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - AddRange()
        /// </summary>
        [Fact]
        public void Test_AddRange()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext(DbContextResolver.DbContextProvider.SqlServer) as RandomThoughtsDbContext;
            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};
            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 1; i < 11; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            appService.AddRange(Thoughts);
            appService.SaveChanges();

            for (int i = 1; i < 11; i++)
            {
                var Thought = appService.SingleOrDefault(i);

                Assert.NotNull(Thought);
                Assert.Equal(Thought.Title, $"Thought{i}");
            }

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - AddRangeAsync(), - Exist(Tkey id)
        /// </summary>
        [Fact]
        public async Task Test_AddRangeAsync_ExistId()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;
            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};
            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body",  ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                var ThoughtExist = appService.Exists( i);

                Assert.True(ThoughtExist, "There is an object with Id == Thought" + i);
            }

            var notRealThoughtExist = appService.Exists( 20);

            Assert.False(notRealThoughtExist, "There is no object with id == 20");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Exist(TEntity obj)
        /// </summary>
        [Fact]
        public async Task Test_ExistObj()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                var ThoughtExist = appService.Exists(Thoughts[i]);

                Assert.True(ThoughtExist, $"There is an object with Id == Thought{i} and Name == Thought{i}" + i);
            }

            var notRealThoughtExist = appService.Exists(new Thought { Id = 1, Title = "NotInDB",Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});

            Assert.False(notRealThoughtExist, "There is no object with id == NotInDB and Name = NotInDB");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Exist(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_ExistFunc()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                var ThoughtExist = appService.Exists(x => x.Id ==  i);

                Assert.True(ThoughtExist, $"There is an object with Id == Thought{i}");
            }

            var notRealThoughtExist = appService.Exists(x => x.Id == 20);

            Assert.False(notRealThoughtExist, "There is no object with id == 20");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - ExistAsync(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_ExistAsyncFunc()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var results = new List<bool>(10);

            for (int i = 0; i < 10; i++)
            {
                results.Add(await appService.ExistsAsync(x => x.Id ==  i));
            }

            Assert.True(results.TrueForAll(result => result));

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - ExistAsync(TEntity obj)
        /// </summary>
        [Fact]
        public async Task Test_ExistAsyncObj()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                var ThoughtExist = await appService.ExistsAsync(Thoughts[i]);

                Assert.True(ThoughtExist, $"There is an object with Id == Thought{i} and Name == Thought{i}" + i);
            }

            var notRealThoughtExist = appService.Exists(new Thought { Id = 1, Title = "NotInDB",Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});

            Assert.False(notRealThoughtExist, "There is no object with id == 1 and Title = NotInDB");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - ExistAsync(Tkey id)
        /// </summary>
        [Fact]
        public async Task Test_ExistAsyncId()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                var ThoughtExist = await appService.ExistsAsync(i);

                Assert.True(ThoughtExist, "There is an object with Id == Thought" + i);
            }

            var notRealThoughtExist = appService.Exists( 20);

            Assert.False(notRealThoughtExist, "There is no object with id == 20");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - ReadAll(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_ReadAllFilter()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var returnedElements = appService.ReadAll(Thought => Thought.Title.Contains("Thought"));

            Assert.NotEmpty(returnedElements);

            var returnedElementsList = new List<Thought>(returnedElements);
            Assert.Equal(Thoughts, returnedElements);

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - ReadAllAsync(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_ReadAllAsyncFilter()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var returnedElements = await appService.ReadAllAsync(Thought => Thought.Title.Contains("Thought"));

            Assert.NotEmpty(returnedElements);

            var returnedElementsList = new List<Thought>(returnedElements);
            Assert.Equal(Thoughts, returnedElements);

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Remove(TKey Id)
        /// </summary>
        [Fact]
        public async Task Test_RemoveID()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var idToRemove = 1;

            appService.Remove(idToRemove);
            appService.SaveChanges();

            var removedElementExist = appService.Exists(idToRemove);

            Assert.False(removedElementExist, $"The element with Id '{idToRemove}' was removed from DB");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Remove(TEntity obj)
        /// </summary>
        [Fact]
        public async Task Test_RemoveObj()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var objToRemove = Thoughts[0];

            appService.Remove(objToRemove);
            appService.SaveChanges();

            var removedElementExist = appService.Exists(objToRemove);

            Assert.False(removedElementExist, $"The element with Id == '{objToRemove.Id}' was removed from DB");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Remove(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_RemoveFilter()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var filterToRemove = new Func<Thought, bool>(Thought => Thought.Title.Contains("Thought")
            && Thought.Id < 5);

            appService.Remove(filterToRemove);
            appService.SaveChanges();

            var removedElementsExist = appService.Exists(filterToRemove);

            Assert.False(removedElementsExist, $"The elements that satisfied the filter were removed from DB");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - RemoveAsync(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_RemoveAsyncFilter()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var filterToRemove = new Func<Thought, bool>(Thought => Thought.Title.Contains("Thought")
            && Thought.Id < 5);

            await appService.RemoveAsync(filterToRemove);
            appService.SaveChanges();

            var removedElementsExist = appService.Exists(filterToRemove);

            Assert.False(removedElementsExist, $"The elements that satisfied the filter were removed from DB");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - RemoveAsync(TEntity obj)
        /// </summary>
        [Fact]
        public async Task Test_RemoveAsyncObj()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var objToRemove = Thoughts[0];

            await appService.RemoveAsync(objToRemove);
            appService.SaveChanges();

            var removedElementExist = appService.Exists(objToRemove);

            Assert.False(removedElementExist, $"The element with Id == '{objToRemove.Id}' was removed from DB");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - RemoveAsync(TKey Id)
        /// </summary>
        [Fact]
        public async Task Test_RemoveAsyncID()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var idToRemove = 1;

            await appService.RemoveAsync(idToRemove);
            appService.SaveChanges();

            var removedElementExist = appService.Exists(idToRemove);

            Assert.False(removedElementExist, $"The element with Id '{idToRemove}' was removed from DB");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - RemoveRange(IEnumerable)
        /// </summary>
        [Fact]
        public async Task Test_RemoveRange()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var filterToRemove = new Func<Thought, bool>(Thought => Thought.Title.Contains("Thought")
            && Thought.Id < 5);

            var elementsToRemove = Thoughts.GetRange(0, 5);

            appService.RemoveRange(elementsToRemove);
            appService.SaveChanges();

            var removedElementsExist = appService.Exists(filterToRemove);

            Assert.False(removedElementsExist, $"The elements that satisfied the filter were removed from DB");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - RemoveRangeAsync(IEnumerable)
        /// </summary>
        [Fact]
        public async Task Test_RemoveRangeAsync()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var filterToRemove = new Func<Thought, bool>(Thought => Thought.Title.Contains("Thought")
            && Thought.Id < 5);

            var elementsToRemove = Thoughts.GetRange(0, 5);

            await appService.RemoveRangeAsync(elementsToRemove);
            appService.SaveChanges();

            var removedElementsExist = appService.Exists(filterToRemove);

            Assert.False(removedElementsExist, $"The elements that satisfied the filter were removed from DB");

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - SingleOrDefault(Func filter)
        /// </summary>
        [Fact]
        public async Task Test_SingleOrDefaultFilter()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var filterToThrowException = new Func<Thought, bool>(Thought => Thought.Title.Contains("Thought")
            && Thought.Id < 5);

            var filterToGetSingleELement = new Func<Thought, bool>(Thought => Thought.Title.Contains("Thought")
            && Thought.Id == 5);

            var filterToGetDefualtELement = new Func<Thought, bool>(Thought => Thought.Title.Contains("Thought")
            && Thought.Id == 20);

            Assert.Throws(typeof(InvalidOperationException), () => { appService.SingleOrDefault(filterToThrowException); });

            var returnedThought = appService.SingleOrDefault(filterToGetSingleELement);
            Assert.NotNull(returnedThought);

            var nullReturnedThought = appService.SingleOrDefault(filterToGetDefualtELement);
            Assert.Null(nullReturnedThought);

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - Update(TEntity obj)
        /// </summary>
        [Fact]
        public async Task Test_Update()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var ThoughtToUpdate = Thoughts[0];

            ThoughtToUpdate.Title = "UpdatedName";

            appService.Update(ThoughtToUpdate);
            appService.SaveChanges();

            var returnedElement = appService.SingleOrDefault(ThoughtToUpdate.Id);

            Assert.Equal(ThoughtToUpdate, returnedElement);

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - UpdateAsync(TEntity obj)
        /// </summary>
        [Fact]
        public async Task Test_UpdateAsync()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            var ThoughtToUpdate = Thoughts[0];

            ThoughtToUpdate.Title = "UpdatedName";

            await appService.UpdateAsync(ThoughtToUpdate);
            appService.SaveChanges();

            var returnedElement = appService.SingleOrDefault(ThoughtToUpdate.Id);

            Assert.Equal(ThoughtToUpdate, returnedElement);

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - UpdateRange(IEnumerable{TEntity} objs)
        /// </summary>
        [Fact]
        public async Task Test_UpdateRange()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                Thoughts[i].Title = $"UpdatedName{i}";
            }

            appService.UpdateRange(Thoughts);
            appService.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                var existUpdatedElement = appService.Exists(Thoughts[i]);
                Assert.True(existUpdatedElement);
            }

            resolver.Dispose();
        }

        /// <summary>
        ///     Tests the operations:
        ///     - UpdateRangeAsync(IEnumerable{TEntity} objs)
        /// </summary>
        [Fact]
        public async Task Test_UpdateRangeAsync()
        {
            var resolver = new DbContextResolver();
            var context = resolver.SetContext() as RandomThoughtsDbContext;

            var user = new ApplicationUser() { };
            var thoughtHole = new ThoughtHole(){Name = "Thought", Description = "Thought description"};

            var appService = GetInstance(context);

            var Thoughts = new List<Thought>(10);

            for (int i = 0; i < 10; i++)
            {
                Thoughts.Add(new Thought { Title = "Thought" + i, Id = i,Body = "Thought Body", ApplicationUser = user, ThoughtHole = thoughtHole});
            }

            await appService.AddRangeAsync(Thoughts);
            appService.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                Thoughts[i].Title = $"UpdatedName{i}";
            }

            await appService.UpdateRangeAsync(Thoughts);
            appService.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                var existUpdatedElement = appService.Exists(Thoughts[i]);
                Assert.True(existUpdatedElement);
            }

            resolver.Dispose();
        }
    }
}
