using System.Diagnostics.Metrics;

namespace LoanManagementSystem.Tests;
//here can write test case.. 
/// <summary>
    //Instead of testing DbContext directly:
    //Better architecture:
    //Controller → Service → Repository → DbContext
//like code 
//LoanManagementSystem.Test
//│
//├── Services
//│   └── UserServiceTests.cs
//│
//├── Controllers
//│   └── UsersControllerTests.cs
//│
//└── Helpers
//    └── TestDbContextFactory.cs
/// </summary>

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }
}