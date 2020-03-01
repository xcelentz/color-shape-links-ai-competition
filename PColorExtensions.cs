/// @file
/// @brief This file contains the ::ColorShapeLinks.Common.PColorExtensions
/// class.
///
/// @author Nuno Fachada
/// @date 2020
/// @copyright [MPLv2](http://mozilla.org/MPL/2.0/)

namespace ColorShapeLinks.Common
{
    /// <summary>Extension methods for the <see cref="PColor"/> enum.</summary>
    public static class PColorExtensions
    {
        /// <summary>
        /// Provides a consistent way to get a formatted thinker name which
        /// includes the color with which he's playing.
        /// </summary>
        /// <param name="color">Thinker's color.</param>
        /// <param name="name">Thinker's name.</param>
        /// <returns>A formatted thinker name with color.</returns>
        public static string FormatName(this PColor color, string name) =>
            $"{name} ({color})";

        /// <summary>
        /// Returns the other color.
        /// </summary>
        /// <param name="color">This color.</param>
        /// <returns>
        /// * Returns ::PColor.White if this color is ::PColor.Red.
        /// * Returns ::PColor.Red if this color is ::PColor.White.
        /// </returns>
        public static PColor Other(this PColor color) =>
            color == PColor.White ? PColor.Red : PColor.White;

        /// <summary>
        /// Returns the shape associated with this player's color for
        /// winning purposes.
        /// </summary>
        /// <param name="color">This color.</param>
        /// <returns>
        /// * Returns ::PShape.Round if color is PColor.White.
        /// * Returns ::PShape.Square if color is PColor.Red.
        /// </returns>
        public static PShape Shape(this PColor color) =>
            color == PColor.White ? PShape.Round : PShape.Square;

        /// <summary>
        /// Is the given piece a friend of this color? In other words, is the
        /// given piece of the same color as this color or associated with the
        /// same shape for winning purposes as this color?
        /// </summary>
        /// <param name="color">This color.</param>
        /// <param name="piece">The piece to check for friendship.</param>
        /// <returns>
        /// `true` is given piece is a friend, `false` otherwise.
        /// </returns>
        public static bool FriendOf(this PColor color, Piece piece) =>
            color == piece.color || color.Shape() == piece.shape;

        /// <summary>
        /// Returns the Winner associated with this color.
        /// </summary>
        /// <param name="color">This color.</param>
        /// <returns>
        /// * Winner.White if <paramref name="color"/> is PColor.White.
        /// * Winner.Red if <paramref name="color"/> is PColor.Red.
        /// </returns>
        public static Winner ToWinner(this PColor color) =>
            color == PColor.White ? Winner.White : Winner.Red;
    }
}
