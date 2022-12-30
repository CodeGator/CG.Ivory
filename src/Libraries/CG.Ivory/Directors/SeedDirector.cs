
using CG.Ivory.Options;

namespace CG.Ivory.Directors;

/// <summary>
/// This class is a default implementation of the <see cref="ISeedDirector"/>
/// interface.
/// </summary>
public class SeedDirector : SeedDirectorBase<SeedDirector>
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the TODO manager for this director.
    /// </summary>
    internal protected readonly ITodoManager _todoManager = null!;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="SeedDirector"/>
    /// class.
    /// </summary>
    /// <param name="todoManager">The TODO manager to use with this director.</param>
    /// <param name="logger">The logger to use with this director.</param>
    public SeedDirector(
        ITodoManager todoManager,
        ILogger<SeedDirector> logger
        ) : base(logger)
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(todoManager, nameof(todoManager));

        // Save the reference(s).
        _todoManager = todoManager;
    }

    #endregion

    // *******************************************************************
    // Protected methods.
    // *******************************************************************

    #region Protected methods

    /// <inheritdoc/>
    protected override async Task SeedFromConfiguration(
        string objectName,
        IConfiguration dataSection,
        bool force,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Decide what to do with the incoming data.
        switch (objectName.ToLower().Trim())
        {
            case "todos":
                await SeedTodos(
                    dataSection,
                    force,
                    userName,
                    cancellationToken
                    ).ConfigureAwait(false);
                break;
            default:
                throw new ArgumentException(
                    $"Don't know how to seed '{objectName}' types!"
                    );
        }
    }

    #endregion

    // *******************************************************************
    // Private methods.
    // *******************************************************************

    #region Private methods

    /// <summary>
    /// This method performs a seeding operation to TODO tasks.
    /// </summary>
    /// <param name="dataSection">The data to use for the operation.</param>
    /// <param name="force"><c>true</c> to force the seeding operation when data
    /// already exists in the associated table(s), <c>false</c> to stop the 
    /// operation whenever data is detected in the associated table(s).</param>
    /// <param name="userName">The user name of the person performing the 
    /// operation.</param>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    private async Task SeedTodos(
        IConfiguration dataSection,
        bool force,
        string userName,
        CancellationToken cancellationToken = default
        )
    {
        // Log what we are about to do.
        _logger.LogDebug(
            "Checking the force flag"
            );

        // Should we check for existing data?
        if (!force)
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Checking for existing TODO tasks"
                );

            // Are the existing TODO tasks?
            var hasExistingData = await _todoManager.AnyAsync(
                cancellationToken
                ).ConfigureAwait(false);

            // Should we stop?
            if (hasExistingData)
            {
                // Log what we didn't do.
                _logger.LogWarning(
                    "Skipping seeding TODO tasks because the 'force' flag " +
                    "was not specified and there are existing text messages " +
                    "in the database."
                    );
                return; // Nothing else to do!
            }
        }

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Binding the incoming seed data to our options"
                );

            // Bind the incoming data to our options.
            var options = new TodoSeedingOptions();
            dataSection.Bind(options);

            // Log what we are about to do.
            _logger.LogDebug(
                "Validating the incoming seed data"
                );

            // Validate the options.
            Guard.Instance().ThrowIfInvalidObject(options, nameof(options));

            // Log what we are about to do.
            _logger.LogDebug(
                "Seeding '{count}' TODO tasks",
                options.Todos
                );

            // Loop through the objects.
            foreach (var todo in options.Todos)
            {
                // Log what we are about to do.
                _logger.LogDebug(
                    "Creating a TODO task"
                    );

                // Create the TODO task.
                _ = await _todoManager.CreateAsync(
                    todo,
                    cancellationToken
                    ).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to seed one or more TODO tasks!"
                );
        }
    }

    #endregion
}
