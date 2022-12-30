
namespace CG.Ivory.Data.Entities
{
    /// <summary>
    /// This class is a sample entity that depicts a TODO item.
    /// </summary>
    public class TodoEntity
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property is sample key for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This property is a sample property for the entity.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// This property is a sample property for the entity.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// This property is a sample property for the entity.
        /// </summary>
        public bool IsDone { get; set; }

        #endregion
    }
}
