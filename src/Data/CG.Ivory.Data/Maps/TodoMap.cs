
namespace CG.Ivory.Data.Maps;

/// <summary>
/// This class is an EFCore configuration map for the <see cref="Entities.TodoEntity"/>
/// entity type.
/// </summary>
internal class TodoMap : EntityMapBase<Entities.TodoEntity>
{
    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="TodoMap"/>
    /// class.
    /// </summary>
    /// <param name="modelBuilder">The model builder to use with this map.</param>
    public TodoMap(
        ModelBuilder modelBuilder
        ) : base(modelBuilder)
    {

    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method configures the <see cref="Entities.TodoEntity"/> entity.
    /// </summary>
    /// <param name="builder">The builder to use for the operation.</param>
    public override void Configure(
        EntityTypeBuilder<Entities.TodoEntity> builder
        )
    {
        // Setup the table.
        builder.ToTable(
            "Todos",
            "Ivory"
            );

        // Setup the property.
        builder.Property(e => e.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        // Setup the primary key.
        builder.HasKey(e => new { e.Id });

        // Setup the column.
        builder.Property(e => e.Name)
            .HasMaxLength(32)
            .IsRequired();

        // Setup the column.
        builder.Property(e => e.DueDate);

        // Setup the column.
        builder.Property(e => e.IsDone)
            .IsRequired();

        // Setup the index.
        builder.HasIndex(e => new
        {
            e.Name,
            e.DueDate,
            e.IsDone
        },
        "IX_Todos"
        );
    }

    #endregion
}
