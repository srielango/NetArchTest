using NetArchTest.Rules;
using NetArchTest.Rules.Policies;

namespace NetArchTest
{
    public static class ArchitecturePolicies
    {
        public static void Run()
        {
            var architecturePolicy = Policy.Define("Passing Policy", "This policy demonstrates a valid policy with reasonable rules")
                    .For(Types.InCurrentDomain)
                    .Add(t => t.That()
                        .ResideInNamespace("NetArchTest")
                        .ShouldNot()
                        .HaveDependencyOn("Infrastructure"),
                        "Controlling external dependencies", "UI cannot directly refer DB Context")
                    .Add(t => t.That()
                        .AreInterfaces()
                        .Should()
                        .HaveNameStartingWith("I"),
                        "Generic implementation rules", "Interface names should start with an 'I'");
            Report(architecturePolicy.Evaluate(), Console.Out);

        }

        public static async Task ReportAsync(PolicyResults policyResults, TextWriter output)
        {
            if (!policyResults.HasViolations)
            {
                await output.WriteLineAsync($"No policy violations found for: {policyResults.Name}");
                return;
            }
            await output.WriteLineAsync($"Policy violations found for: {policyResults.Name}");

            foreach (var rule in policyResults.Results)
            {
                if (!rule.IsSuccessful)
                {
                    await output.WriteLineAsync("--------------------------------------------------");
                    await output.WriteLineAsync($"Rule failed: {rule.Name}");

                    foreach (var type in rule.FailingTypes)
                    {
                        await output.WriteLineAsync($"\t {type.FullName}");
                    }
                }
            }
            await output.WriteLineAsync("--------------------------------------------------");
        }

        public static void Report(PolicyResults results, TextWriter output)
        { 
            ReportAsync(results, output).GetAwaiter().GetResult();
        }
    }
}

