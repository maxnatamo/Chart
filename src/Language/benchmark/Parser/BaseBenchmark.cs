namespace Chart.Language.Parsers.Benchmarks
{
    public class BaseBenchmark
    {
        public List<string> GetGraphTestFiles()
        {
            return new List<string>
            {
                "hero.graphql",
                "github.graphql"
            };
        }

        /// <summary>
        /// Read file contents from a file based in the Resources/-directory.
        /// </summary>
        /// <param name="fileName">The name of the file to read.</param>
        /// <returns>The full path of the specified file.</returns>
        protected string ReadTestFile(string fileName)
            => File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName));
    }
}