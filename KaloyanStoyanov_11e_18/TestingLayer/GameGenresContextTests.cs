using BusinessLayer;
using DataLayer;
using NUnit.Framework;

namespace TestingLayer;

[TestFixture]
public class GenresContextTests
{
    static GameGenresContext gameGenresContext;
    static GenresContextTests()
    {
        gameGenresContext = new GameGenresContext(TestManager.dbContext);
    }

    [Test]
    public void CreateGenre()
    {
        GameGenre gameGenre = new GameGenre("action");
        int gameGenresBefore = TestManager.dbContext.GameGenres.Count();

        gameGenresContext.Create(gameGenre);

        int gameGenresAfter = TestManager.dbContext.GameGenres.Count();
        GameGenre lastGameGenre = TestManager.dbContext.GameGenres.Last();
        Assert.That(gameGenresBefore + 1 == gameGenresAfter && lastGameGenre.Name == gameGenre.Name,
            "Names are not equal or game genre is not created!");
    }

    [Test]
    public void ReadGenre()
    {
        GameGenre newGameGenre = new GameGenre("action");
        gameGenresContext.Create(newGameGenre);

        GameGenre gameGenre = gameGenresContext.Read(1);

        Assert.That(gameGenre.Name == "action", "Read() does not get GameGenre by id!");
    }

    [Test]
    public void ReadAllGenres()
    {
        int gameGenresBefore = TestManager.dbContext.GameGenres.Count();

        int gameGenresAfter = gameGenresContext.ReadAll().Count;

        Assert.That(gameGenresBefore == gameGenresAfter, "ReadAll() does not return all of the GameGenres!");
    }

    [Test]
    public void UpdateGenre()
    {
        GameGenre newGameGenre = new GameGenre("action");
        gameGenresContext.Create(newGameGenre);

        GameGenre lastGameGenre = gameGenresContext.ReadAll().Last();
        lastGameGenre.Name = "Updated GameGenre";

        gameGenresContext.Update(lastGameGenre, false);

        Assert.That(gameGenresContext.Read(lastGameGenre.Id).Name == "Updated GameGenre",
        "Update() does not change the GameGenre's name!");
    }

    [Test]
    public void DeleteGenre()
    {
        GameGenre newGameGenre = new GameGenre("action");
        gameGenresContext.Create(newGameGenre);

        List<GameGenre> gameGenres = gameGenresContext.ReadAll();
        int gameGenresBefore = gameGenres.Count;
        GameGenre gameGenre = gameGenres.Last();

        gameGenresContext.Delete(gameGenre.Id);

        int gameGenresAfter = gameGenresContext.ReadAll().Count;
        Assert.That(gameGenresBefore == gameGenresAfter + 1, "Delete() does not delete a agme genre!");
    }
}