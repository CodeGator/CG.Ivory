
namespace CG.Ivory.Models
{
    /// <summary>
    /// This class is a sample model that depicts a TODO item.
    /// </summary>
    public class TodoModel
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property is sample key for the model.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This property is a sample property for the model.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// This property is a sample property for the model.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// This property is a sample property for the model.
        /// </summary>
        public bool IsDone { get; set; }

        #endregion
    }
}
