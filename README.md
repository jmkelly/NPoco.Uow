#A super simple unit of work implementation on top of the NPoco micro ORM.#

For information about NPoco, see https://github.com/schotime/NPoco

```C#
using Shouldly;
using Xunit;

namespace NPoco.UoW
{
    public class Example
    {
        [Fact]
        public void CreateAContextAndUseASession()
        {
            IDatabase database = new Database("Your connection name");
            //Ideally your context object should be not long lived.  Request
            //scoping is ideal (if used in an MVC / web type scenario).  Contexts are 
            //cheap.

            Context context = new Context(database);

            using (var session = context.OpenSession())
            {
                var db = session.Database;
                var james = new User{Email = "james@test.com"};
                var john = new User {Email = "john@test.com"};
                
                db.Insert(james);
                db.Insert(john);

                using (var innerSession = context.OpenSession())
                {
                    var nonExistentUser = new User {Email = "doesntmatter@test.com"};
                    innerSession.Database.Insert(nonExistentUser);
                    //the inner session is not committed, because we didn't call innerSession.SaveChanges()
                    //easy way to roll back in tests, just dont call SaveChanges().  If an exception is hit before
                    //SaveChanges(), then its rolled back as well.
                }
                session.SaveChanges();

                var users = db.Fetch<User>();
                users.Count.ShouldBe(2);
                //the outer session is committed (so james and john are committed to the db)
            }
        }

        [TableName("dbo.User")]
        [PrimaryKey("Id")]
        public class User
        {
            public int Id { get; set; }
            public string Email { get; set; }
        }
    }
}
```
