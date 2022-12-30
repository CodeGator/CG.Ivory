
namespace CG.Ivory.Options;

/// <summary>
/// This class contains configuration settings related to seeding 
/// <see cref="TodoModel"/> objects.
/// </summary>
internal class TodoSeedingOptions
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains a list of <see cref="TodoModel"/> objects 
    /// to seed.
    /// </summary>
    [Required]
    public List<TodoModel> Todos { get; set; } = null!;

    #endregion
}
