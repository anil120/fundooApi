//-----------------------------------------------------------------------
// <copyright file="NotesModel.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace FundooData.Models
{
    using System;

    /// <summary>
    /// notes model is model class for notes
    /// </summary>
    public class NotesModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the take a note.
        /// </summary>
        /// <value>
        /// The take a note.
        /// </value>
        public string TakeANote { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remind me].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [remind me]; otherwise, <c>false</c>.
        /// </value>
        public bool RemindMe { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is archieve.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is archieve; otherwise, <c>false</c>.
        /// </value>
        public bool IsArchieve { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pin; otherwise, <c>false</c>.
        /// </value>
        public bool IsPin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is thrash.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is thrash; otherwise, <c>false</c>.
        /// </value>
        public bool IsTrash { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the color code.
        /// </summary>
        /// <value>
        /// The color code.
        /// </value>
        public string ColorCode { get; set; }

        /// <summary>
        /// Gets or sets the reminder.
        /// </summary>
        /// <value>
        /// The reminder.
        /// </value>
        public string Reminder { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
    }
}
