namespace Chart.Core
{
    /// <summary>
    /// Enumeration of available validation stages, of which a validation rule can operate in.
    /// </summary>
    public enum ValidationStage
    {
        /// <summary>
        /// A given validation rule is executed before any fields have been resolved.
        /// </summary>
        Enter,

        /// <summary>
        /// A given validation rule is executed after all fields have been resolved.
        /// </summary>
        Leave,
    }
}