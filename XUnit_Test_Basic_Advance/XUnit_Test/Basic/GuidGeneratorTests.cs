using Domain.FirstLearning;
using Xunit.Abstractions;

namespace ProjectTest_XUnit.FirstLearning
{
    // Creating An Instanse Of A Class For Each Function

    // In this case it create a new guid for each class that it call the guid generator.
    // Meaning that the GuidGenerator class is different for each class
    // But if I want to use the same guid in both class i need to inject the class like the second class (GuidGeneratorTestsone)
    public class Guid_Generator_Tests_DifferentGuids
    {
        private readonly GuidGenerator _guidGenerator;
        private readonly ITestOutputHelper _output;
        public Guid_Generator_Tests_DifferentGuids(ITestOutputHelper output)
        {
            _output = output;
            _guidGenerator = new GuidGenerator();
        }

        [Fact]
        public void GuidTestOne()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}"); // diferent guid from the second function

        }

        [Fact]
        public void GuidTestTwo()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}"); // Different Guid from the first function

        }
    }











    // =================================================================================================================================================================================

    // Creating An Instanse Of A Class For All Functions In The Class

    // To use the same class in all of your tests in this namespace, you need to act like here and use IClassFixture.
    // Meaning that for calling each test it is not going to create a new instanse of the class
    // In the other words, it inject the GuidGenerator class and re use the class in all of your tests
    // This is work when you want to use Db for instance or something you donot want to change it again for each call
    public class Guid_Generator_Tests_OneInstance_ForClass_UsingFixture : IClassFixture<GuidGenerator>
    {
        private readonly GuidGenerator _guidGenerator;
        private readonly ITestOutputHelper _output;
        public Guid_Generator_Tests_OneInstance_ForClass_UsingFixture(ITestOutputHelper output, GuidGenerator guidGenerator)
        {
            _output = output;
            _guidGenerator = guidGenerator;
        }

        [Fact]
        public void GuidTestOne()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}");

        }

        [Fact]
        public void GuidTestTwo()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}");

        }
    }











    // =================================================================================================================================================================================

    // Creating An Instanse Of A Class For All Functions In The Class And Use Disposal Function

    // To Dispose the class after using it you can use IDisposable interface
    // This is usefull when you want to delete something after runnning the test like deleting something from Database  
    // You can do it from Despose class
    public class Guid_Generator_Tests_OneInstance_ForClass_UsingFixture_And_Disposal : IClassFixture<GuidGenerator>, IDisposable
    {
        private readonly GuidGenerator _guidGenerator;
        private readonly ITestOutputHelper _output;
        public Guid_Generator_Tests_OneInstance_ForClass_UsingFixture_And_Disposal(ITestOutputHelper output, GuidGenerator guidGenerator)
        {
            _output = output;
            _guidGenerator = guidGenerator;
        }

        [Fact]
        public void GuidTestOne()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}");

        }

        [Fact]
        public void GuidTestTwo()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}");

        }

        public void Dispose()
        {
            // If you want to clesre some thing like deleteing something from DataBase or any other thing after the test you can do it here
            _output.WriteLine("This class is disposed");
        }
    }











    // =================================================================================================================================================================================

    // Creating An Instanse Of A Class For All Classess In The Collection And Use It In Any Class

    // If you want to use your injected class like GuidGenerator here in other places as well you can use collection
    // in above class we used IClassFixture<GuidGenerator> to inject our GuidGenerator to the GuidGeneratorTestsonewithDisposal class 
    // But what if we want to use GuidGenerator in other classes as well??
    // To do that, you can add your class to a collection and then call that collection anywhere you like and you can use that class again and again.
    // the class will be generated just once and you can use it a lot of time.
    // A database connection can be a good example. you have just one connection and you will use that many time in many classes


    // First of All you ned to define a collection like here:
    [CollectionDefinition("Guid Generator")]
    // Then you create an empty class inheriting from ICollectionFixture<classname>
    public class GuidGeneratorDefinition : ICollectionFixture<GuidGenerator> { };


    // Then whereever you like you can use it like here
    // You need to pass the name of collection and inject the class in constructor and then the class use that collection 
    [Collection("Guid Generator")]
    public class Guid_Generator_Tests_OneInstance_ForAllClasses_Using_Collection_One
    {
        private readonly GuidGenerator _guidGenerator;
        private readonly ITestOutputHelper _output;
        public Guid_Generator_Tests_OneInstance_ForAllClasses_Using_Collection_One(ITestOutputHelper output, GuidGenerator guidGenerator)
        {
            _output = output;
            _guidGenerator = guidGenerator;
        }
        [Fact]
        public void GuidTestOne()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}");
        }
        [Fact]
        public void GuidTestTwo()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}");
        }
    }







    // Now we used the "Guid Generator" collection here again
    // In these Two classes all Guids from GuidGenerator are the same and it not create a new guid for any of them.
    [Collection("Guid Generator")]
    public class Guid_Generator_Tests_OneInstance_ForAllClasses_Using_Collection_Two
    {
        private readonly GuidGenerator _guidGenerator;
        private readonly ITestOutputHelper _output;
        public Guid_Generator_Tests_OneInstance_ForAllClasses_Using_Collection_Two(ITestOutputHelper output, GuidGenerator guidGenerator)
        {
            _output = output;
            _guidGenerator = guidGenerator;
        }
        [Fact]
        public void GuidTestOne()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}");
        }
        [Fact]
        public void GuidTestTwo()
        {
            var guid = _guidGenerator.RandomGuid;
            _output.WriteLine($"The guid was: {guid}");
        }
    }



}
