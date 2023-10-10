using N5.TryCatch.Internal;

namespace N5.TryCatch.Extend;

/// <summary>
/// Extend TryCatch Funcion
/// </summary>
public static class TryCatchExtend
{
    #region Sync

    /// <summary>
    /// Try catch function
    /// </summary>
    /// <param name="value">Object to apply Try</param>
    /// <param name="actionExecute">Action Execute Try Catch</param>
    /// <returns>Object complement </returns>
    /// <example>
    /// this.Try(() => { Console.WriteLine("OK"); })
    ///     .Catch((ex) => { Console.WriteLine("OK"); })
    ///     .Finally(() => { Console.WriteLine("OK"); })
    ///     .Apply();
    /// </example>
    public static TryCatchExecute Try(this object value, Action actionExecute)
        => new TryCatchExecute(actionExecute);

    /// <summary>
    /// Try Catch function wth return class
    /// </summary>
    /// <typeparam name="T">Type xlass to return</typeparam>
    /// <param name="value">Object to apply Try</param>
    /// <param name="actionExecute">fucntion execute and return class</param>
    /// <returns>Object complement</returns>
    /// <example>
    /// this.Try<TestClass>(() => { return new(); })
    ///     .Catch((ex) => { return new(); })
    ///     .Finally(() => { Console.WriteLine("OK"); })
    ///     .Apply();
    /// </example>
    public static TryCatchExecute<T> Try<T>(this object value, Func<T> actionExecute) where T : class
        => new TryCatchExecute<T>(actionExecute);

    #endregion Sync

    #region Async

    /// <summary>
    /// Try catch function
    /// </summary>
    /// <param name="value">Object to apply Try</param>
    /// <param name="actionExecute">Action Execute Try Catch</param>
    /// <returns>Object complement </returns>
    /// <example>
    /// await this.TryAsync(async () => await Task.Run(() => Console.WriteLine("OK")))
    ///           .Catch(async (ex) => await Task.Run(() => Console.WriteLine("OK")))
    ///           .Finally(async () => await Task.Run(() => Console.WriteLine("OK")))
    ///           .Apply();
    /// </example>
    public static TryCatchAsyncExecute TryAsync(this object value, Func<Task> actionExecute)
        => new TryCatchAsyncExecute(actionExecute);

    /// <summary>
    /// Try Catch function wth return class
    /// </summary>
    /// <typeparam name="T">Type xlass to return</typeparam>
    /// <param name="value">Object to apply Try</param>
    /// <param name="actionExecute">fucntion execute and return class</param>
    /// <returns>Object complement</returns>
    /// <example>
    /// await this.TryAsync(async () => await Task.FromResult<RequestTest>(new()))
    ///           .Catch(async (ex) => await Task.FromResult<RequestTest>(new()))
    ///           .Finally((a) => await Task.Run(() => Console.WriteLine("OK")))
    ///           .Apply();
    /// </example>
    public static TryCatchAsyncExecute<T> TryAsync<T>(this object value, Func<Task<T>> actionExecute) where T : class
        => new TryCatchAsyncExecute<T>(actionExecute);

    #endregion Async
}