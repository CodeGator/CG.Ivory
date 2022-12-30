
namespace CG.Ivory.Data;

/// <summary>
/// This class is a sample data-context.
/// </summary>
public class IvoryDbContext : DbContext
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains a collection of TODO items.
    /// </summary>
    public virtual DbSet<Entities.TodoEntity> Todos { get; set; } = null!;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="IvoryDbContext"/>
    /// class.
    /// </summary>
    /// <param name="options">The options to use for the operation.</param>
    public IvoryDbContext(
        DbContextOptions<IvoryDbContext> options
        ) : base(options)
    {

    }

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <summary>
    /// This method creates the data-model for the data-context.
    /// </summary>
    /// <param name="modelBuilder">The model builder to use for the operation.</param>
    protected override void OnModelCreating(
        ModelBuilder modelBuilder
        )
    {
        // Map the entities.
        modelBuilder.ApplyConfiguration(new TodoMap(modelBuilder));

        // Give the base class a chance.
        base.OnModelCreating(modelBuilder);
    }

    #endregion
}
