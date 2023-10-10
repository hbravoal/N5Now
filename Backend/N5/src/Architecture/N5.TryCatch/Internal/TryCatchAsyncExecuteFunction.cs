namespace N5.TryCatch.Internal;

/// <summary>
/// Try Async Catch with return object
/// </summary>
/// <typeparam name="T">class to return</typeparam>
public class TryCatchAsyncExecute<T>
    where T : class
{
    private Func<Task<T>> _execute;
    private Func<Exception, Task<T>> _executeCatch;
    protected Func<T, Task> _executeFinally;

    /// <summary>
    /// Cosntructor Try Catch with return object
    /// </summary>
    /// <param name="execute"></param>
    public TryCatchAsyncExecute(Func<Task<T>> execute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _execute = execute;
    }

    /// <summary>
    /// Catch with return, apply when need return object if use try or catch
    /// </summary>
    /// <param name="executeCatch">function catch</param>
    /// <returns>Object manage try catch</returns>
    public TryCatchAsyncExecute<T> Catch(Func<Exception, Task<T>> executeCatch)
    {
        ArgumentNullException.ThrowIfNull(executeCatch);
        _executeCatch = executeCatch;
        return this;
    }

    /// <summary>
    /// independently of use try or catch execute this function to end method
    /// </summary>
    /// <param name="executFinally">Function finaly</param>
    /// <returns>Object manage try catch</returns>
    public TryCatchAsyncExecute<T> Finally(Func<T, Task> executFinally)
    {
        ArgumentNullException.ThrowIfNull(executFinally);
        _executeFinally = executFinally;
        return this;
    }

    /// <summary>
    /// Execute to try catch with object return
    /// </summary>
    /// <returns>Task to execute with object</returns>
    public async Task<T> Apply()
    {
        T result = default;
        try
        {
            if (_execute is not null) result = await _execute();
        }
        catch (Exception ex)
        {
            if (_executeCatch is not null) result = await _executeCatch(ex);
        }
        finally
        {
            if (_executeFinally is not null) await _executeFinally(result);
        }

        return result;
    }
}