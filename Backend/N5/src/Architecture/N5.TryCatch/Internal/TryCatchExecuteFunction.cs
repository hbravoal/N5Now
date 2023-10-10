namespace N5.TryCatch.Internal;

/// <summary>
/// Try Catch with return object
/// </summary>
/// <typeparam name="T">class to return</typeparam>
public class TryCatchExecute<T>
    where T : class
{
    private Func<T> _execute;
    private Func<Exception, T> _executeCatch;
    protected Action<T> _executeFinally;

    /// <summary>
    /// Cosntructor Try Catch with return object
    /// </summary>
    /// <param name="execute"></param>
    public TryCatchExecute(Func<T> execute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _execute = execute;
    }

    /// <summary>
    /// Catch with return, apply when need return object if use try or catch
    /// </summary>
    /// <param name="executeCatch">function catch</param>
    /// <returns>Object manage try catch</returns>
    public TryCatchExecute<T> Catch(Func<Exception, T> executeCatch)
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
    public TryCatchExecute<T> Finally(Action<T> executFinally)
    {
        ArgumentNullException.ThrowIfNull(executFinally);
        _executeFinally = executFinally;
        return this;
    }

    /// <summary>
    /// Execute to try catch with object return
    /// </summary>
    /// <returns>Task to execute with object</returns>
    public Task<T> Apply()
        => Task.Factory.StartNew<T>(() =>
        {
            T result = null;
            try
            {
                if (_execute is not null) result = _execute();
            }
            catch (Exception ex)
            {
                if (_executeCatch is not null) result = _executeCatch(ex);
            }
            finally
            {
                if (_executeFinally is not null) _executeFinally(result);
            }

            return result;
        });
}