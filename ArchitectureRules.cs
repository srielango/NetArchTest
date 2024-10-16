using NetArchTest.Rules;

namespace NetArchTest
{
    public static class ArchitectureRules
    {
        public static void Run()
        {
            var result = Types.InCurrentDomain()
                .That()
                .ResideInNamespace("NetArchTest")
                .ShouldNot()
                .HaveDependencyOn("Infrastructure")
                .GetResult().IsSuccessful;

            //if (result == false)
            //{
            //    throw new Exception("UI cannot directly refer infrastructure");
            //}
        }
    }
}
