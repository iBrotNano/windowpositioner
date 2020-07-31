namespace MarcelMelzig.WindowPositioner.Core.WindowManagement
{
    /// <summary>
    /// A struct to represent a window's dimensions.
    /// </summary>
    /// <remarks>The order of the properties must not be changed. The are used to store data from a Win32 method.</remarks>
    internal struct Rect
    {
        /// <summary>
        /// The upper left x coordinate.
        /// </summary>
        public int UpperLeftX { get; set; }

        /// <summary>
        /// The upper left y coordinate.
        /// </summary>
        public int UpperLeftY { get; set; }

        /// <summary>
        /// The lower right x coordinate.
        /// </summary>
        public int LowerRightX { get; set; }

        /// <summary>
        /// The lower right y coordinate.
        /// </summary>
        public int LowerRightY { get; set; }
    }
}