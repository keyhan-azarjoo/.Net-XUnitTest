using Domain.Advance.Models;

namespace ProjectTest_XUnit.Advance.Fixtures
{

    // We create a list of users as the test user 
    public static class UserFixture
    {
        public static List<User> GetTestUsers() => new() {
                new User {
                    Id = 1,
                    Name = "Keyhan",
                    Email = "Keyhan@Gmail.com",
                    Address = new Address {
                        Street = "Street 1",
                        City = "Northampton",
                        PostCode = "NN1 000"
                    }
                },
                new User {
                    Id = 2,
                    Name = "Keyhan2",
                    Email = "Keyhan2@Gmail.com",
                    Address = new Address {
                        Street = "Street 2",
                        City = "Northampton2",
                        PostCode = "NN1 222"
                    }
                },
                new User {
                    Id = 3,
                    Name = "Keyhan3",
                    Email = "Keyhan3@Gmail.com",
                    Address = new Address {
                        Street = "Street 3",
                        City = "Northampton3",
                        PostCode = "NN1 333"
                    }
                }
            };
    }
}
