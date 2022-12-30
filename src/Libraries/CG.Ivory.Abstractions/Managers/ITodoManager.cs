
namespace CG.Ivory.Managers;

/// <summary>
/// This interface represents an object that performs operations related
/// to <see cref="TodoModel"/> objects.
/// </summary>
public interface ITodoManager
{
    /// <summary>
    /// This method indicates whether there are any <see cref="TodoModel"/> objects
    /// in the system.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that is monitored
    /// for the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns <c>true</c> if there
    /// are any <see cref="TodoModel"/> objects; <c>false</c> otherwise.</returns>
    Task<bool> AnyAsync(
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method creates a new <see cref="TodoModel"/> instance.
    /// </summary>
    /// <param name="model">The model to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token the is monitored
    /// throughout the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns the new
    /// model instance.</returns>
    Task<TodoModel> CreateAsync(
        TodoModel model,
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method deletes a <see cref="TodoModel"/> instance.
    /// </summary>
    /// <param name="model">The model to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token the is monitored
    /// throughout the lifetime of the method.</param>
    /// <returns>A task to perform the operation.</returns>
    Task DeleteAsync(
        TodoModel model,
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method searches for all the <see cref="TodoModel"/> objects.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token the is monitored
    /// throughout the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns a collection 
    /// of <see cref="TodoModel"/> objects.</returns>
    Task<List<TodoModel>> FindAllAsync(
        CancellationToken cancellationToken = default
        );

    /// <summary>
    /// This method updates a <see cref="TodoModel"/> instance.
    /// </summary>
    /// <param name="model">The model to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token the is monitored
    /// throughout the lifetime of the method.</param>
    /// <returns>A task to perform the operation that returns the updated
    /// model instance.</returns>
    Task<TodoModel> UpdateAsync(
        TodoModel model,
        CancellationToken cancellationToken = default
        );
}
