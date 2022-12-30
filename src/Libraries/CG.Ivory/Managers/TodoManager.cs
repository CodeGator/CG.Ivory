
namespace CG.Ivory.Managers;

/// <summary>
/// This class is a default implementation of the <see cref="ITodoManager"/>
/// interface.
/// </summary>
internal class TodoManager : ITodoManager
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the repository for this manager.
    /// </summary>
    internal protected readonly ITodoRepository _todoRepository = null!;

    /// <summary>
    /// This field contains the logger for this manager.
    /// </summary>
    internal protected readonly ILogger<ITodoManager> _logger = null!;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="TodoManager"/>
    /// class.
    /// </summary>
    /// <param name="todoRepository">The repository to use for this manager.</param>
    /// <param name="logger">The logger to use for this manager.</param>
    public TodoManager(
        ITodoRepository todoRepository,
        ILogger<ITodoManager> logger
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(todoRepository, nameof(todoRepository))
            .ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _todoRepository = todoRepository;
        _logger = logger;
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <inheritdoc/>
    public virtual async Task<bool> AnyAsync(
        CancellationToken cancellationToken = default
        )
    {
        // Defer to the repository for the search.
        var result = await _todoRepository.AnyAsync(
            cancellationToken
            ).ConfigureAwait(false);

        // Return the results.
        return result;
    }

    // *******************************************************************
                    
    /// <inheritdoc/>
    public virtual async Task<TodoModel> CreateAsync(
        TodoModel model,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(model, nameof(model));

        // Defer to the repository for the create.
        var newModel = await _todoRepository.CreateAsync(
            model,
            cancellationToken
            ).ConfigureAwait( false );

        // Return the results.
        return newModel;
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(
        TodoModel model,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(model, nameof(model));

        // Defer to the repository for the delete.
        await _todoRepository.DeleteAsync(
            model,
            cancellationToken
            ).ConfigureAwait(false);
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<List<TodoModel>> FindAllAsync(
        CancellationToken cancellationToken = default
        )
    {
        // Defer to the repository for the search.
        var models = await _todoRepository.FindAllAsync(
            cancellationToken
            ).ConfigureAwait(false);

        // Return the results.
        return models;
    }

    // *******************************************************************

    /// <inheritdoc/>
    public virtual async Task<TodoModel> UpdateAsync(
        TodoModel model,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(model, nameof(model));

        // Defer to the repository for the update.
        var changedModel = await _todoRepository.UpdateAsync(
            model,
            cancellationToken
            ).ConfigureAwait(false);

        // Return the results.
        return changedModel;
    }

    #endregion
}
