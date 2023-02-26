namespace Chart.Core.Parser
{
    /// <summary>
    /// Internal structure for printer messages
    /// </summary>
    internal class PrinterMessage
    {
        /// <summary>
        /// The content of the message.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Zero-indexed depth of the message.
        /// </summary>
        public int Depth { get; set; }

        public PrinterMessage(string content, int depth)
        {
            this.Content = content;
            this.Depth = depth;
        }

        /// <summary>
        /// Return the message as an indented string.
        /// </summary>
        public override string ToString()
        {
            return string.Join("", Enumerable.Repeat("   ", Depth)) + Content;
        }
    }

    /// <summary>
    /// Context for printing indented messages
    /// </summary>
    public class PrinterContext
    {
        /// <summary>
        /// The current depth of indentation.
        /// </summary>
        private int Depth { get; set; } = 0;

        /// <summary>
        /// List of all lines in the context.
        /// </summary>
        private List<PrinterMessage> Lines { get; set; } = new List<PrinterMessage>();

        /// <summary>
        /// Write a message to the context, with newline.
        /// </summary>
        /// <param name="content">The content of the message.</param>
        public void WriteLine(string content)
        {
            this.Lines.Add(new PrinterMessage(content, this.Depth));
        }

        /// <summary>
        /// Write a message to the context.
        /// </summary>
        /// <param name="content">The content of the message.</param>
        public void Write(string content)
        {
            if(!this.Lines.Any())
            {
                this.WriteLine(content);
                return;
            }

            this.Lines.Last().Content += content;
        }

        /// <summary>
        /// Descend one level of indentation.
        /// </summary>
        public void Descend()
            => this.Depth++;

        /// <summary>
        /// Ascend one level of indentation.
        /// </summary>
        public void Ascend()
            => this.Depth--;

        /// <summary>
        /// Clear all messages.
        /// </summary>
        public void Clear()
            => this.Lines.Clear();

        /// <summary>
        /// Return messages as indented strings.
        /// </summary>
        public override string ToString()
        {
            return string.Join("\n", this.Lines.Select(v => v.ToString()));
        }
    }
}